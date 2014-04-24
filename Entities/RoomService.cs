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
    
    public partial class RoomService
    {
        public RoomService()
        {
            this.BookingRoomServices = new HashSet<BookingRoomService>();
        }
    
        public int RoomServiceId { get; set; }
        public int RoomId { get; set; }
        public int ServiceId { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Description { get; set; }
        public byte[] Concurrency { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateUpdated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    
        public virtual ICollection<BookingRoomService> BookingRoomServices { get; set; }
        public virtual Room Room { get; set; }
        public virtual Service Service { get; set; }
    }
}
