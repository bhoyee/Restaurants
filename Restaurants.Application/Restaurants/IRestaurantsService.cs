using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants
{
    internal interface IRestaurantsService
    {
        Task<IEnumerable<Restaurant>> GetAllRestaurants();
    }
}