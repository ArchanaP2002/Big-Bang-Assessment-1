using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HotelApi.model_s
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        // [Required]
        [MaxLength(10)]
        public string? Number { get; set; }

        // [Required]
        [MaxLength(20)]
        public string? Type { get; set; }


        public bool Availability { get; set; }

        public int RoomCount { get; set; }

        [ForeignKey("Hotel")]
        public int HotelId { get; set; }

        //Navigation Property
        [JsonIgnore]
        public virtual Hotel ?Hotel { get; set; }

       
    }
}

