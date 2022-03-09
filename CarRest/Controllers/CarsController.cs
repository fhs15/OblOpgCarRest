using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRest.Managers;
using Cars;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarsManager manager = new CarsManager();

        // GET: api/<ValuesController>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [HttpGet]
        public ActionResult<IEnumerable<Car>> Get([FromQuery] string model, [FromQuery] int? maxprice, [FromQuery] string licenseplate)
        {
            var Cars = manager.GetAllCars(model, maxprice, licenseplate);

            if (Cars.Count() < 1) return NoContent();
            return Ok(Cars);
        }

        // GET api/<ValuesController>/5
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [HttpGet("{id}")]
        public ActionResult<Car> Get(int id)
        {
            Car car = manager.GetCar(id);

            if (car == null) return NotFound();

            return Ok(car);
        }

        // POST api/<ValuesController>
        [ProducesResponseType(201)]
        [HttpPost]
        public ActionResult<Car> Post([FromBody] Car newCar)
        {
            Car car = manager.PostCar(newCar);
            return Created("/api/cars/" + car.Id, car);
        }

        // DELETE api/<ValuesController>/5
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        [HttpDelete("{id}")]
        public ActionResult<Car> Delete(int id)
        {
            Car car = manager.DeleteCar(id);
            if (car == null) return BadRequest();
            return Ok(car); 
        }
    }
}
