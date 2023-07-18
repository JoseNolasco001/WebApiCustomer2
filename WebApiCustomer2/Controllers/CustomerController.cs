using DataBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiCustomer2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private PruebaContext _context;

        public CustomerController(PruebaContext context) 
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetList")]
        public IActionResult GetList()
        {
            List<Customer> lista = new List<Customer>();
            try
            {
                lista = _context.Customers.ToList();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message, response = lista });
            }
        }

        [HttpGet]
        [Route("{IdCustomer:int}")]
        public IActionResult GetById(int IdCustomer)
        {
            Customer customer = _context.Customers.Find(IdCustomer);
            if (customer == null)
            {
                return NotFound();
            }
            try
            {
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = customer });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message, response = customer });
            }
        }

        [HttpPost]
        [Route("Save")]
        public IActionResult SaveCustomer([FromBody] Customer customer)
        {
            try
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = customer });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message, response = customer });
            }
        }


        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateCustomer([FromBody] Customer customer)
        {
            Customer custom = _context.Customers.Find(customer.Id);
            if (custom == null)
            {
                return NotFound();
            }
            try
            {
                _context.Customers.Update(customer);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = customer });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message, response = customer });
            }
        }

        [HttpDelete("Delete/{IdCustomer:int}")]
        public IActionResult DeleteCustomer(int IdCustomer)
        {
            Customer customer = _context.Customers.Find(IdCustomer);
            if (customer == null)
            {
                return NotFound();
            }
            try
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = customer });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message, response = customer });
            }
        }
    }
}
