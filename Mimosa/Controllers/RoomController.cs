using System;
using System.Linq;
using System.Web.Mvc;
using Business;
using Entities;
using Mimosa.Models;
using System.Globalization;
using Mimosa.Models.Search;

namespace Mimosa.Controllers
{
    public class RoomController : Controller
    {
        public ActionResult Index(RoomFilter filter)
        {
            var roomTypeList = Business.RoomService.GetRoomTypeList().Select(x => new SelectListItem { Text = x.Name, Value = x.RoomTypeId.ToString() }).ToList();
            roomTypeList.Insert(0, new SelectListItem { Text = "-- All --", Value = "" });
            ViewBag.RoomTypeList = roomTypeList;

            var districtList = Business.DistrictService.GetDistricts().Select(x => new SelectListItem { Text = x.DistrictName_EN, Value = x.DistrictCode.ToString() }).ToList();
            districtList.Insert(0, new SelectListItem { Text = "-- All --", Value = "" });
            ViewBag.Districts = districtList;

            return View();
        }

        public ActionResult SearchRoom(RoomFilter filter)
        {
            int totalRecords;
            var list = Business.RoomService.ListRoom(filter.Area, filter.Money, filter.RoomType, filter.District, filter.PageIndex, filter.PageSize, out totalRecords);

            ViewBag.ListImage = Business.RoomService.GetListImageRoom(list.Select(x => x.RoomId).ToArray());
            var data = new SearchResult<Room>(filter)
            {
                TotalRecords = totalRecords,
                List = list
            };
            return PartialView("_RoomList", data);
        }

        public ActionResult RoomDetails(int roomId)
        {
            var roomTypeList = Business.RoomService.GetRoomTypeList().Select(x => new SelectListItem { Text = x.Name, Value = x.RoomTypeId.ToString() }).ToList();
            roomTypeList.Insert(0, new SelectListItem { Text = "-- All --", Value = "" });
            ViewBag.RoomTypeList = roomTypeList;

            var districtList = Business.DistrictService.GetDistricts().Select(x => new SelectListItem { Text = x.DistrictName_EN, Value = x.DistrictCode.ToString() }).ToList();
            districtList.Insert(0, new SelectListItem { Text = "-- All --", Value = "" });
            ViewBag.Districts = districtList;

            var data = Business.RoomService.RoomDetails(roomId);
            ViewBag.ListImage = Business.RoomService.GetListImageRoomDetails(roomId);

            return View(data);
        }

        public ActionResult BookRoom(CustomerBookingRoomModel model, string StartDate, string EndDate)
        {
            var customer = new Customer
            {
                Age = model.Age,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                CreatedBy = string.Format("{0} {1}", model.FirstName, model.LastName),
                UpdatedBy = string.Format("{0} {1}", model.FirstName, model.LastName),
                Gender = model.Gender
            };
            ContactInformation contact = null;
            if (!string.IsNullOrWhiteSpace(model.AddressContact) || !string.IsNullOrWhiteSpace(model.CityContact)
                || !string.IsNullOrWhiteSpace(model.EmailContact) || !string.IsNullOrWhiteSpace(model.FaxNumberContact)
                || !string.IsNullOrWhiteSpace(model.FirstNameContact) || !string.IsNullOrWhiteSpace(model.LastNameContact)
                || !string.IsNullOrWhiteSpace(model.PhoneContact))
            {
                contact = new ContactInformation
                {
                    Address = model.AddressContact,
                    City = model.CityContact,
                    Email = model.EmailContact,
                    FaxNumber = model.FaxNumberContact,
                    FirstName = model.FirstNameContact,
                    LastName = model.LastNameContact,
                    PhoneNumber = model.PhoneContact,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = string.Format("{0} {1}", model.FirstName, model.LastName),
                    UpdatedBy = string.Format("{0} {1}", model.FirstName, model.LastName),
                    ContactTypeId = 5,
                };
            }

            model.StartDate = DateTime.ParseExact(StartDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            model.EndDate = DateTime.ParseExact(EndDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            BookRoomService.BookRoom(customer, contact
                , model.RoomId, model.StartDate, model.EndDate, model.Service, model.Equipment);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}
