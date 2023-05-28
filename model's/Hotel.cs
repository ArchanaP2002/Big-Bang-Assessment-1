using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HotelApi.model_s
{
    public class Hotel
    {
        [Key]
        public int HotelId { get; set; }
      //  [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Location { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Required]
        public string? Amenities { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

      
        public virtual List<Room> Room { get; set; }
 
    }
}
