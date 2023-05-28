using HotelApi.data;
using HotelApi.model_s;
using HotelApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServersController : ControllerBase
    {
        private readonly HotelContext _context;

        //takes a ProductContext parameter and assigns it to the _context field.

        public ServersController(HotelContext context)
        {
            _context = context;
        }
 
        [HttpGet] // Read from Data base 
        public IActionResult Get()
        {
            try
            {
                var emp = _context.Servers.ToList();//property to get all as a list 
                if (emp.Count == 0)
                {
                    return NotFound("Not available.");
                }
                return Ok(emp);
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
                var emp = _context.Servers.Find(id);
                if (emp == null)
                {
                    return NotFound($" not found with ID: {id}");
                }
                return Ok(emp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost] // Creating
        public IActionResult Post(ServersDetails model)
        {
            try
            {
                _context.Add(model);
                _context.SaveChanges();
                return Ok("Details created successfully.");
            }
            catch (Exception ex)
            {
                string errorMessage = "An error occurred .";
                if (ex.InnerException != null)
                {
                    errorMessage += " Inner Exception: " + ex.InnerException.Message;
                }
                return BadRequest(errorMessage);
            }
        }

        [HttpPut] //Update
        public IActionResult Put(ServersDetails model)
        {
            try
            {
                if (model == null || model.EmployeeId == 0)
                {
                    return BadRequest("Invalid data or ID.");
                }

                var emp = _context.Servers.Find(model.EmployeeId);
                if (emp == null)
                {
                    return NotFound($"not found with ID: {model.EmployeeId}");
                }

                emp.EmployeeName = model.EmployeeName;

                _context.SaveChanges();

                return Ok(" updated successfully.");
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
                var emp = _context.Servers.Find(id);
                if  (emp == null)
                {
                    return NotFound($" not found with ID: {id}");
                }

                _context.Servers.Remove(emp);
                _context.SaveChanges();

                return Ok(" deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
