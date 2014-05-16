using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Dal;
using Entities;
namespace Business
{
    public class BookRoomService
    {
        public static void BookRoom(Customer customer, ContactInformation contact, int roomId, DateTime startDate, DateTime endDate, int[] service, int[] equipment)
        {
            using (var context = new MimosaEntities())
            {
                decimal totalPrice = 0;
                var room = context.Rooms.FirstOrDefault(x => x.RoomId == roomId);
                
                if (contact != null)
                {
                    context.ContactInformations.Add(contact);
                    context.SaveChanges();
                }

                if (room != null)
                {
                    var bookRoom = new Booking
                    {
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now,
                        CreatedBy = string.Format("{0} {1}", customer.FirstName, customer.LastName),
                        UpdatedBy = string.Format("{0} {1}", customer.FirstName, customer.LastName),
                        RoomId = roomId,
                        BookDate = DateTime.Now,
                        CustomerId = customer.CustomerId,
                        BookingStatusId = 1,
                        RoomPrice = room.BasePrice,
                        Description = "",
                        ContractDateStart = startDate,
                        ContractDateEnd = endDate
                    };

                    if (service != null)
                    {
                        foreach (var id in service)
                        {
                            var itemService = context.RoomServices.FirstOrDefault(x => x.ServiceId == id);
                            if (itemService != null)
                            {
                                totalPrice += itemService.Price ?? 0;
                                bookRoom.BookingRoomServices.Add(
                                    new BookingRoomService
                                {
                                    RoomServiceId = itemService.RoomServiceId,
                                    DateCreated = DateTime.Now,
                                    DateUpdated = DateTime.Now,
                                    Price = itemService.Price,
                                    CreatedBy = string.Format("{0} {1}", customer.FirstName, customer.LastName),
                                    UpdatedBy = string.Format("{0} {1}", customer.FirstName, customer.LastName)
                                });
                            }
                        }
                    }

                    if (equipment != null)
                    {
                        foreach (var id in equipment)
                        {
                            var itemEquipment = context.RoomEquipments.FirstOrDefault(x => x.EquipmentId == id);
                            if (itemEquipment != null)
                            {
                                totalPrice += itemEquipment.Price ?? 0;
                                bookRoom.BookingRoomEquipments.Add(
                                    new BookingRoomEquipment
                                    {
                                        RoomEquipmentId = itemEquipment.RoomEquipmentId,
                                        DateCreated = DateTime.Now,
                                        DateUpdated = DateTime.Now,
                                        Price = itemEquipment.Price,
                                        CreatedBy = string.Format("{0} {1}", customer.FirstName, customer.LastName),
                                        UpdatedBy = string.Format("{0} {1}", customer.FirstName, customer.LastName)
                                    });
                            }
                        }
                    }

                    if (contact != null)
                    {
                        customer.ContactInformationId = contact.ContactInformationId;
                    }
                    customer.OrganisationID = room.Site.OrganisationID;
                    bookRoom.TotalPrice = totalPrice;
                    customer.Bookings.Add(bookRoom);

                    context.Customers.Add(customer);
                    context.SaveChanges();
                }
            }
        }
    }
}
