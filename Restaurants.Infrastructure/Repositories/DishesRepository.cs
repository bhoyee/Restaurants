using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Repositories
{
    internal class DishesRepository(RestaurantsDbContext dbContext) : IDishesRepository
    {
        public async Task<int> Create(Dish entity)
        {
            dbContext.Dishes.Add(entity);
           await dbContext.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<Dish> GetByNameAndRestaurantIdAsync(string name, int restaurantId)
        {
            
            return await dbContext.Dishes
                .FirstOrDefaultAsync(d => d.Name == name && d.RestaurantId == restaurantId);
        }
    }
}
