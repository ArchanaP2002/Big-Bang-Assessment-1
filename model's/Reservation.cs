using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HotelApi.model_s
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? CustomerName { get; set; }

        [Required]
        [EmailAddress]
        public string? CustomerEmail { get; set; }

        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }

        [JsonIgnore]
        public virtual Room Room { get; set; }

        [JsonIgnore]
        public virtual Hotel Hotel { get; set; }
    }
}

