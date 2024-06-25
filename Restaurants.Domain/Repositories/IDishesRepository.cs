using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Repositories
{
    public interface IDishesRepository
    {
        Task<Dish> GetByNameAndRestaurantIdAsync(string name, int restaurantId);

        Task<int> Create(Dish entity);
    }
}
