using Microsoft.Extensions.Logging;
using Restaurants.Application.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants
{
    internal class RestaurantsService(IRestaurantsRepository restaurantsRepository, 
        ILogger<RestaurantsService> logger) : IRestaurantsService
    {
        public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
        {
            logger.LogInformation("Get all restaurants");
            var restaurants = await restaurantsRepository.GetAllAsync();

            var restaurantDtos = restaurants.Select(RestaurantDto.FromEntity);

            return restaurantDtos!;
        }

        public async Task<RestaurantDto?> GetById(int id)
        {
            logger.LogInformation($"Getting restaurant {id}");
            var restaurant = await restaurantsRepository.GetByIdAsync(id);
            var restaurantDtos = RestaurantDto.FromEntity(restaurant);
            return restaurantDtos;
        }
    }
}
