using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using Dal;
using Entities;

namespace Business
{
    public class RoomService
    {
        public static List<RoomType> GetRoomTypeList()
        {
            using (var context = new MimosaEntities())
            {
                return context.RoomTypes.AsNoTracking().ToList();
            }
        }

        public static List<Room> ListRoom(int? area, int? money, int? roomType, string district, int pageIndex, int pageSize,
            out int totalRecords)
        {
            using (var context = new MimosaEntities())
            {
                IQueryable<Room> query = context.Rooms;
                if (area.HasValue)
                {
                    if (area == (int)Area.Less10MeterSquare)
                    {
                        query = query.Where(x => x.MeterSquare < 10);
                    }
                    else if (area == (int)Area.MeterSquar10And20)
                    {
                        query = query.Where(x => x.MeterSquare >= 10 && x.MeterSquare <= 20);
                    }
                    else if (area == (int)Area.MeterSquar20And30)
                    {
                        query = query.Where(x => x.MeterSquare >= 20 && x.MeterSquare <= 30);
                    }
                    else if (area == (int)Area.Than30MeterSquare)
                    {
                        query = query.Where(x => x.MeterSquare >= 30);
                    }
                }

                if (money.HasValue)
                {
                    if (money == (int)Money.Less3Million)
                    {
                        query = query.Where(x => x.BasePrice < 3000000);
                    }
                    else if (money == (int)Money.Million3And5)
                    {
                        query = query.Where(x => x.BasePrice >= 3000000 && x.BasePrice <= 5000000);
                    }
                    else if (money == (int)Money.Than5Million)
                    {
                        query = query.Where(x => x.BasePrice > 5000000);
                    }
                }

                if(!string.IsNullOrWhiteSpace(district))
                {
                    query = query.Where(x => x.Site.ContactInformation.District.Equals(district));
                }

                if (roomType.HasValue)
                {
                    query = query.Where(x => x.RoomTypeId == roomType.Value);
                }

                totalRecords = query.Count();
                return query.OrderByDescending(x => x.RoomId).Skip(pageSize * (pageIndex - 1)).Take(pageSize).Include(x => x.RoomStatus).AsNoTracking().ToList();
            }
        }

        public static Room RoomDetails(int roomId)
        {
            using (var context = new MimosaEntities())
            {
                return
                    context.Rooms.Include(x => x.RoomServices.Select(c => c.Service))
                        .Include(x => x.RoomEquipments.Select(c => c.Equipment))
                        .Include(x => x.RoomStatus)
                        .Include(x => x.Site.ContactInformation)                        
                        .AsNoTracking()
                        .FirstOrDefault(x => x.RoomId == roomId);
            }
        }

        public static List<Image> GetListImageRoomDetails(int roomId)
        {
            using (var context = new MimosaEntities())
            {
                var serviceRoomId =
                    context.RoomServices.Where(x => x.RoomId == roomId)
                        .AsNoTracking()
                        .Select(x => x.ServiceId)
                        .ToArray();

                var equipmentRoomId =
                    context.RoomEquipments.Where(x => x.RoomId == roomId)
                        .AsNoTracking()
                        .Select(x => x.EquipmentId)
                        .ToArray();

                var query = context.Images.Where(x => x.ItemId == roomId && x.ImageTypeId == 3);
                query = query.Union(context.Images.Where(x => serviceRoomId.Contains(x.ItemId) && x.ImageTypeId == 5));
                query = query.Union(context.Images.Where(x => equipmentRoomId.Contains(x.ItemId) && x.ImageTypeId == 4));

                return query.AsNoTracking().ToList();
            }
        }

        public static List<Image> GetListImageRoom(int[] roomIdList)
        {
            using (var context = new MimosaEntities())
            {
                return context.Images.Where(x => roomIdList.Contains(x.ItemId)).AsNoTracking().ToList();
            }
        }

        public static void InsertCustomer()
        {
            var date = DateTime.Now;
            using (var context = new MimosaEntities())
            {
                var item =
                    context.VisitorOfDays.FirstOrDefault(
                        x => x.Day == date.Day && x.Month == date.Month && x.Year == date.Year);
                if (item == null)
                {
                    context.VisitorOfDays.Add(new VisitorOfDay { Count = 1, DateTime = date, Day = date.Day, Month = date.Month, Year = date.Year });
                }
                else
                {
                    item.Count++;
                }
                context.SaveChanges();
            }
        }

        public static Tuple<int?, int?, int?> GetStatistic()
        {
            int? count1 = 0;
            int? count2 = 0;
            int? count3 = 0;
            using (var context = new MimosaEntities())
            {
                var item = context.VisitorOfDays.FirstOrDefault(
                    x => x.Day == DateTime.Now.Day && x.Month == DateTime.Now.Month && x.Year == DateTime.Now.Year);
                if (item != null)
                {
                    count1 = item.Count;
                }

                var firstDayOfWeek = FirstDayOfWeekUtility.GetFirstDayOfWeek(DateTime.Now);
                var lastDayOfWeek = firstDayOfWeek.AddDays(7);

                count2 =
                    context.VisitorOfDays.Where(
                        x => x.DateTime >= firstDayOfWeek && x.DateTime <= lastDayOfWeek).Sum(x => x.Count);

                var day = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                var lastDateMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, day);
                var firstDateMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

                count3 =
                    context.VisitorOfDays.Where(
                        x => x.DateTime >= firstDateMonth && x.DateTime <= lastDateMonth).Sum(x => x.Count);


            }
            return new Tuple<int?, int?, int?>(count1, count2, count3);
        }

    }

    public static class FirstDayOfWeekUtility
    {
        /// <summary>
        /// Returns the first day of the week that the specified
        /// date is in using the current culture. 
        /// </summary>
        public static DateTime GetFirstDayOfWeek(DateTime dayInWeek)
        {
            CultureInfo defaultCultureInfo = CultureInfo.CurrentCulture;
            return GetFirstDayOfWeek(dayInWeek, defaultCultureInfo);
        }

        /// <summary>
        /// Returns the first day of the week that the specified date 
        /// is in. 
        /// </summary>
        public static DateTime GetFirstDayOfWeek(DateTime dayInWeek, CultureInfo cultureInfo)
        {
            DayOfWeek firstDay = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);

            return firstDayInWeek;
        }
    }
}
