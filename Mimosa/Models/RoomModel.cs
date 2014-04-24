using System;
using System.ComponentModel.DataAnnotations;
using Mimosa.Models.Search;

namespace Mimosa.Models
{
    public class RoomFilter : SearchFilter
    {
        public int? Area { get; set; }
        public int? Money { get; set; }
        public int? RoomType { get; set; }
        public string District { get; set; }
    }

   
    public class CustomerBookingRoomModel
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public int? Gender { get; set; }
        public int? Age { get; set; }
        public int ContractDuration { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string FirstNameContact { get; set; }
        public string LastNameContact { get; set; }
        public string AddressContact { get; set; }
        public string CityContact { get; set; }
        public string PhoneContact { get; set; }
        public string EmailContact { get; set; }
        public string FaxNumberContact { get; set; }


        public int[] Service { get; set; }
        public int[] Equipment { get; set; }
    }
}