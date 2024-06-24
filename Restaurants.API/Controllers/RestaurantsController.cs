using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain.Repositories.Commands.CreateRestaurant;
using FluentValidation;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;



namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantsController(IMediator mediator) : ControllerBase
    {
        //get all restaurants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll()
        {
            var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
            return Ok(restaurants);
        }

        //get specific restaurant
        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantDto?>> GetById([FromRoute]int id)
        {
                var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));

                // chk if restaurant exist
                if (restaurant == null)
                    return NotFound();

                return Ok(restaurant);

        }

        //update restaurant
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRestaurant(int id, [FromBody] UpdateRestaurantCommand command)
        {
            command.Id = id;

            await mediator.Send(command);

           //return NoContent(); // this return 203 no contentwhich mean it successful deleted
            return Ok(new { message = "Restaurant successfully Updated" });
            
        }

        //Delete restaurant
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {
            await mediator.Send(new DeleteRestaurantCommand(id));

            //return NoContent(); // this return 203 no contentwhich mean it successful deleted
            return Ok(new { message = "Restaurant successfully deleted" });

        }

        //create restaurant
        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody]CreateRestaurantCommand command)
        {

  
                int id = await mediator.Send(command);
                return CreatedAtAction(nameof(GetById), new { id }, null);
 
        }

    }
}
