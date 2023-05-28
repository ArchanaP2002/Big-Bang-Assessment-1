using HotelApi.data;
using HotelApi.model_s;
using Microsoft.EntityFrameworkCore;

namespace HotelApi.Repository
{
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelContext _hotelContext;

        public HotelRepository(HotelContext con)
        {
            _hotelContext = con;
        }


        public IEnumerable<Hotel> GetHotel()
        {
            try
            {
                return _hotelContext.Hotels.Include(x => x.Room).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve hotels.", ex);
            }
        }

        public Hotel GetHotelById(int HotelId)
        {
            try
            {
                return _hotelContext.Hotels.FirstOrDefault(x => x.HotelId == HotelId);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve hotel by ID.", ex);
            }
        }

        public Hotel PostHotel(Hotel hotel)
        {
            try
            {
                _hotelContext.Hotels.Add(hotel);
                _hotelContext.SaveChanges();
                return hotel;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create hotel.", ex);
            }
        }

        public Hotel PutHotel(int HotelId, Hotel hotel)
        {
            try
            {
                _hotelContext.Entry(hotel).State = EntityState.Modified;
                _hotelContext.SaveChanges();
                return hotel;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update hotel.", ex);
            }
        }

        public Hotel DeleteHotel(int HotelId)
        {
            try
            {
                var hotel = _hotelContext.Hotels.Find(HotelId);
                _hotelContext.Hotels.Remove(hotel);
                _hotelContext.SaveChanges();
                return hotel;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete hotel.", ex);
            }
        }

        public IEnumerable<Hotel> GetHotels(Hotel filter)
        {
            var query = _hotelContext.Hotels.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Location))
            {
                query = query.Where(h => h.Location == filter.Location);
            }

            if (!string.IsNullOrEmpty(filter.Amenities))
            {
                var amenitiesList = filter.Amenities.Split(',');
                query = query.Where(h => amenitiesList.Contains(h.Amenities));
            }

            return query.ToList();
        }

 
    }
    }
 
