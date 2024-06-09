﻿using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using System.Reflection.Metadata.Ecma335;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantsController(IRestaurantsService restaurantsService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var restaurants = await restaurantsService.GetAllRestaurants();
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var restaurant = await restaurantsService.GetById(id);
            // chk if restaurant exist
            if (restaurant == null)
                return NotFound();

            return Ok(restaurant);
        }
    }
}
