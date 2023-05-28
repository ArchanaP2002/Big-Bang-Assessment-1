using System.ComponentModel.DataAnnotations;

namespace HotelApi.model_s
{
    public class ServersDetails
    {
        [Key]
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public Hotel? Hotel { get; set; }
    }
}
