//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Booking
    {
        public Booking()
        {
            this.BookingRoomEquipments = new HashSet<BookingRoomEquipment>();
            this.BookingRoomServices = new HashSet<BookingRoomService>();
        }
    
        public int BookingId { get; set; }
        public int RoomId { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public System.DateTime BookDate { get; set; }
        public int BookingStatusId { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> RoomPrice { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
        public Nullable<System.DateTime> ContractDateSign { get; set; }
        public Nullable<System.DateTime> ContractDateStart { get; set; }
        public Nullable<System.DateTime> ContractDateEnd { get; set; }
        public Nullable<decimal> ContractTotalPrice { get; set; }
        public byte[] Concurrency { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateUpdated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    
        public virtual BookingStatus BookingStatus { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Room Room { get; set; }
        public virtual ICollection<BookingRoomEquipment> BookingRoomEquipments { get; set; }
        public virtual ICollection<BookingRoomService> BookingRoomServices { get; set; }
    }
}
