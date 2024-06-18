using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain.Repositories.Commands.CreateRestaurant;
using FluentValidation;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;



namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantsController(IMediator mediator) : ControllerBase
    {
        //get all restaurants
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
            return Ok(restaurants);
        }

        //get specific restaurant
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));
       
            // chk if restaurant exist
            if (restaurant == null)
                return NotFound();

            return Ok(restaurant);
        }

        //update restaurant
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateRestaurant(int id, [FromBody] UpdateRestaurantCommand command)
        {
            command.Id = id;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = await mediator.Send(command);

            if (isUpdated)
            {
                //return NoContent(); // this return 203 no contentwhich mean it successful deleted
                return Ok(new { message = "Restaurant successfully Updated" });
            }

            return NotFound();
        }

        //Delete restaurant
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {
            var isDeleted = await mediator.Send(new DeleteRestaurantCommand(id));

            // chk if restaurant deleted
            if (isDeleted)
                //return NoContent(); // this return 203 no contentwhich mean it successful deleted
                return Ok(new { message = "Restaurant successfully deleted" });


            return NotFound();
        }

        //create restaurant
        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody]CreateRestaurantCommand command)
        {

            try
            {
                int id = await mediator.Send(command);
                return CreatedAtAction(nameof(GetById), new { id }, null);
            }
            catch (RestaurantAlreadyExistsException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { errors = ex.Errors });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

    }
}
