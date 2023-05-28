using HotelApi.data;
using HotelApi.model_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelApi.Controllers
{

    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly HotelContext _context;

        //takes a HotelContext parameter and assigns it to the _context field.

        public RoomController(HotelContext context)
        {
            _context = context;
        }

        [HttpGet] // Read from Data base 
        public IActionResult Get()
        {
            try
            {
                var rooms = _context.Rooms.ToList();//property to get all as a list 
                if (rooms.Count == 0)
                {
                    return NotFound("Not available.");
                }
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]// ready by id 
        public IActionResult Get(int id)
        {
            try
            {
                var room = _context.Rooms.Find(id);
                if (room == null)
                {
                    return NotFound($"not found with ID: {id}");
                }
                return Ok(room);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/room/available
        [HttpGet("available")]
        public IActionResult GetAvailable()
        {
            try
            {
                var rooms = _context.Rooms.Where(r => r.Availability == true).ToList();
                if (rooms.Count == 0)
                {
                    return NotFound("No available rooms.");
                }
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/room/available/{hotelId}
        [HttpGet("available/{hotelId}")]
        public IActionResult GetAvailableByHotel(int hotelId)
        {
            try
            {
                var rooms = _context.Rooms.Where(r => r.HotelId == hotelId && r.Availability == true).ToList();
                if (rooms.Count == 0)
                {
                    return NotFound($"No available rooms in hotel with ID: {hotelId}");
                }
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost] // Creating
        public IActionResult Post(Room model)
        {
            try
            {
                _context.Add(model);
                _context.SaveChanges();
                return Ok(" created successfully.");
            }
            catch (Exception ex)
            {
                string errorMessage = "An error occurred ";
                if (ex.InnerException != null)
                {
                    errorMessage += " Inner Exception: " + ex.InnerException.Message;
                }
                return BadRequest(errorMessage);
            }
        }

        [HttpPut] //Update
        public IActionResult Put(Room model)
        {
            try
            {
                if (model == null || model.RoomId == 0)
                {
                    return BadRequest("Invalid data or ID.");
                }

                var category = _context.Rooms.Find(model.RoomId);
                if (category == null)
                {
                    return NotFound($"Category not found with ID: {model.RoomId}");
                }

                category.Type = model.Type;
                category.Availability = model.Availability;

                _context.SaveChanges();

                return Ok("updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")] //delete by id
        public IActionResult Delete(int id)
        {
            try
            {
                var Room = _context.Rooms.Find(id);
                if (Room == null)
                {
                    return NotFound($" not found with ID: {id}");
                }

                _context.Rooms.Remove(Room);
                _context.SaveChanges();

                return Ok("Category deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





    }
}












