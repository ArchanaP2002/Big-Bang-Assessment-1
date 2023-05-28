using HotelApi.model_s;
using Microsoft.AspNetCore.Mvc;

namespace HotelApi.Repository
{
    public interface IRoom
    {
        [HttpGet]
        [Route("api/room")]
        IEnumerable<Room> GetRooms();

        [HttpGet]
        [Route("api/room/{id}")]
        Room GetRoomById(int id);

        // GET: api/room/available
        [HttpGet("available")]
        IEnumerable<Room> GetAvailableRooms();

        // GET: api/room/available/{hotelId}
        [HttpGet("available/{hotelId}")]
        IEnumerable<Room> GetAvailableRoomsByHotel(int hotelId);

        [HttpPost]
        [Route("api/room")]
        Room PostRoom(Room room);

        [HttpPut]
        [Route("api/room/{id}")]
        Room PutRoom(Room room);

        [HttpDelete]
        [Route("api/room/{id}")]
        void DeleteRoom(int id);
    }
}

