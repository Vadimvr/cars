using cars_api.Db;
using cars_api.Models;
using cars_api.Models.ModelsDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cars_api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class CarController : ControllerBase
    {
        private readonly ICarRepository carRepository;

        public CarController(ICarRepository carRepository)
        {
            this.carRepository = carRepository;
        }

        /// <summary>
        /// Get car by Id 
        /// </summary>
        /// <param name="id">Id car</param>
        /// <returns><see cref="Car"/></returns>
        /// <response code="200">Success</response>
        /// <response code="404">Car was not found.</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CarDTO>> Get(int id)
        {
            var car = await carRepository.GetAsync(id);

            return car != null ? (CarDTO)car : NotFound();
        }


        /// <summary>
        /// Get all cars 
        /// </summary>
        /// <param name="page">Page</param>
        /// <param name="count_rows">Counts of entries per page</param>
        /// <returns>List cars</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Number or pages less than 1</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("{page}/{count_rows}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CarDTO>>> GetAll(int page = 1, int count_rows = 100)
        {
            if (page < 1 || count_rows < 1)
                return BadRequest();
            return Ok((await carRepository.GetCarsAsync(page, count_rows)).Select(i=> (CarDTO)i));
        }



        /// <summary>
        /// Getting a list of cars in the Id range
        /// </summary>
        /// <param name="start">Stat id</param>
        /// <param name="end">End id</param>
        /// <returns>List of cars</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If IDs are less than 1.</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost("{start}/{end}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CarDTO>>> GetRange(int start, int end)
        {
            if (start < 1 || end < 1)
                return BadRequest();
            return Ok((await carRepository.GetCarsRangeAsync(start, end)).Select(i => (CarDTO)i));

        }

        /// <summary>
        /// Create new car.
        /// </summary>
        /// <param name="car">New car</param>
        /// <returns>Ok or error</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If new car is null.</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CarDTO car)
        {
            if (car == null) return BadRequest();
            await carRepository.CreateAsync((Car)car);
            return Ok();
        }

        /// <summary>
        /// Update car
        /// </summary>
        /// <param name="id">Car ID</param>
        /// <param name="car">Updated car</param>
        /// <response code="200">Success</response>
        /// <response code="404">Car was not found.</response>
        /// <response code="400">Car is null</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, [FromBody] Car car)
        {
            if (car == null) return BadRequest();
            var carOld = carRepository.GetAsync(id);
            if (carOld == null)
                return NotFound();

            await carRepository.UpdateAsync(id, car);
            return Ok();
        }

        /// <summary>
        /// Delete  car by id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Success</response>
        /// <response code="404">Car was not found.</response>
        /// <response code="400">Car is null</response>
        /// <response code="401">If the user is unauthorized</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var carIsDeleted = await carRepository.DeleteAsync(id);

            return carIsDeleted ? Ok() : NotFound();
        }
    }
}
