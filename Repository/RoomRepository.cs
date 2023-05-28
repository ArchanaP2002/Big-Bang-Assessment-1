using HotelApi.data;
using HotelApi.model_s;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelApi.Repository
{
    public class RoomRepository : IRoom
    {
        private readonly HotelContext _context;

        public RoomRepository(HotelContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/room")]
        [ProducesResponseType(typeof(IEnumerable<Room>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IEnumerable<Room> GetRooms()
        {
            try
            {
                return _context.Rooms.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve rooms.", ex);
            }
        }

        [HttpGet]
        [Route("api/room/{id}")]
        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Room GetRoomById(int id)
        {
            try
            {
                return _context.Rooms.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Room not found with ID: {id}", ex);
            }
        }

        // GET: api/room/available
        [HttpGet("available")]
        [ProducesResponseType(typeof(IEnumerable<Room>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IEnumerable<Room> GetAvailableRooms()
        {
            try
            {
                return _context.Rooms.Where(r => r.Availability == true).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve available rooms.", ex);
            }
        }

        // GET: api/room/available/{hotelId}
        [HttpGet("available/{hotelId}")]
        [ProducesResponseType(typeof(IEnumerable<Room>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IEnumerable<Room> GetAvailableRoomsByHotel(int hotelId)
        {
            try
            {
                return _context.Rooms.Where(r => r.HotelId == hotelId && r.Availability == true).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve available rooms in hotel with ID: {hotelId}", ex);
            }
        }

        [HttpPost]
        [Route("api/room")]
        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Room PostRoom([FromBody] Room room)
        {
            try
            {
                _context.Add(room);
                _context.SaveChanges();
                return room;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create room.", ex);
            }
        }

        [HttpPut]
        [Route("api/room")]
        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Room PutRoom([FromBody] Room room)
        {
            try
            {
                _context.Update(room);
                _context.SaveChanges();
                return room;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update room.", ex);
            }
        }

        [HttpDelete]
        [Route("api/room/{id}")]
        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public void DeleteRoom(int id)
        {
            try
            {
                var room = _context.Rooms.Find(id);
                if (room != null)
                {
                    _context.Remove(room);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception($"Room not found with ID: {id}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete room.", ex);
            }
        }
    }
}  

