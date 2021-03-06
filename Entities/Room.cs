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
    
    public partial class Room
    {
        public Room()
        {
            this.Bookings = new HashSet<Booking>();
            this.RoomEquipments = new HashSet<RoomEquipment>();
            this.RoomServices = new HashSet<RoomService>();
        }
    
        public int RoomId { get; set; }
        public int SiteId { get; set; }
        public string RoomName { get; set; }
        public int RoomStatusId { get; set; }
        public Nullable<int> RoomTypeId { get; set; }
        public bool IsLegacy { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Width { get; set; }
        public Nullable<decimal> Height { get; set; }
        public Nullable<decimal> MeterSquare { get; set; }
        public Nullable<int> Floor { get; set; }
        public Nullable<decimal> BasePrice { get; set; }
        public byte[] Concurrency { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateUpdated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual RoomStatus RoomStatus { get; set; }
        public virtual RoomType RoomType { get; set; }
        public virtual Site Site { get; set; }
        public virtual ICollection<RoomEquipment> RoomEquipments { get; set; }
        public virtual ICollection<RoomService> RoomServices { get; set; }
    }
}
