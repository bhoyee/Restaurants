﻿using AutoMapper;
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
            if (restaurant == null) throw new NotFoundExeception(nameof(Restaurant), request.RestaurantId.ToString());

            var dish = mapper.Map<Dish>(request);
            await dishesRepository.Create(dish);
            
        }
    }
}
