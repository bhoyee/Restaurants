using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exeception;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,
        IRestaurantsRepository restaurantsRepository,
        IDishesRepository dishesRepository,
        IMapper mapper) : IRequestHandler<CreateDishCommand>
    {
        public async Task Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating new dish: {@DishRequest}", request);
            var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);
            // chk if restaurant existing
            if (restaurant == null) throw new NotFoundExeception(nameof(Restaurant), request.RestaurantId.ToString());

            // chk if dish exist already with same restaurent
            var existingDish = await dishesRepository.GetByNameAndRestaurantIdAsync(request.Name, request.RestaurantId);
            if (existingDish != null)
            {
                logger.LogInformation("Dish with name {@DishName} already exists for restaurant {RestaurantId}", request.Name, request.RestaurantId);
                throw new DuplicateDishException(request.Name, request.RestaurantId);
            }

            var dish = mapper.Map<Dish>(request);
            await dishesRepository.Create(dish);
            
        }
    }
}
