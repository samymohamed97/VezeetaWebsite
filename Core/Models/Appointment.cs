using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public Days Days { get; set; }
        public  double FinalPrice { get; set; }
        public Status Status { get; set; }          
        public ICollection<Time> Times { get; set; }
        public int CouponCodeId { get; set; }
        public CouponCode CouponCode { get; set; }
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Days
    {
        Saturday,
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Friday,
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status
    {
        Pending ,
        Confirmed ,
        Canceled
    }
}

