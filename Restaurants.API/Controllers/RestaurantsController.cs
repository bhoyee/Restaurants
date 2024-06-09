using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var restaurants = ..;
            return Ok(restaurants);
        }
    }
}
