using System.ComponentModel.DataAnnotations;

namespace HotelApi.model_s
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string? UserName { get; set; }

        public string? UserEmail { get; set; }
        public string? Password { get; set; }
    }
}
