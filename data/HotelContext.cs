using HotelApi.model_s;
using Microsoft.EntityFrameworkCore;

namespace HotelApi.data
{
    public class HotelContext :DbContext

    {
        public HotelContext(DbContextOptions<HotelContext> options) : base(options)
        {

        }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<ServersDetails> Servers { get; set; }

        public int GetRoomCount(int roomId, int hotelId)
        { 
            var roomCount = Rooms.Where(r => r.RoomId == roomId && r.HotelId == hotelId).Count();
            return roomCount;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>()
               .HasOne(r => r.Hotel)
               .WithMany(h => h.Room)
               .HasForeignKey(r => r.HotelId)
               .OnDelete(DeleteBehavior.Cascade);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=LAPTOP-PKS4NBKU\\SQLEXPRESS; database=HotelApi; integrated security=true; TrustServerCertificate=true");
        }

       
    }
}
