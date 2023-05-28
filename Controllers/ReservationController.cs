
using HotelApi.model_s;
using HotelApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelApi.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservation _reservationRepository;

        public ReservationController(IReservation reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        [HttpGet] // Read from Data base 
        public IActionResult Get()
        {
            try
            {
                var reservation = _reservationRepository.GetReservation();//property to get all list 
                if (reservation == null)
                {
                    return NotFound("Not available.");
                }
                return Ok(reservation);
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
                var reservation = _reservationRepository.GetReservationById(id);
                if (reservation == null)
                {
                    return NotFound($"Not found with ID: {id}");
                }
                return Ok(reservation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost] // Creating
        public IActionResult Post(Reservation model)
        {
            try
            {
                _reservationRepository.PostReservation(model);
                return Ok("Reservation created successfully.");
            }
            catch (Exception ex)
            {
                string errorMessage = "An error occurred.";
                if (ex.InnerException != null)
                {
                    errorMessage += " Inner Exception: " + ex.InnerException.Message;
                }
                return BadRequest(errorMessage);
            }
        }

        [HttpPut] //Update
        public IActionResult Put(Reservation model)
        {
            try
            {
                if (model == null || model.ReservationId == 0)
                {
                    return BadRequest("Invalid data or ID.");
                }

                var reservation = _reservationRepository.PutReservation(model.ReservationId, model);
                return Ok("Reservation updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")] //delete by id
        public IActionResult Delete(int id)   //iactionresult is a data return type
        {
            try
            {
                var reservation = _reservationRepository.GetReservationById(id);
                if (reservation == null)
                {
                    return NotFound($"Reservation not found with ID: {id}");
                }

                _reservationRepository.DeleteReservation(id);
                return Ok("Reservation deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}

