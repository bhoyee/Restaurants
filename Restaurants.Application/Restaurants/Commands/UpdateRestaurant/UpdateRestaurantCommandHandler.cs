﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exeception;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger,
        IMapper mapper,
        IRestaurantsRepository restaurantsRepository) : IRequestHandler<UpdateRestaurantCommand>
    {
        public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating restaurant with id: {Restaurant} with {@UpdatedRestaurant}", request.Id, request);

            var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);
            if (restaurant == null)
            {
                logger.LogWarning($"Restaurant with id {request.Id} not found.");
                throw new NotFoundExeception($"Restaurant with {request.Id} doesn't exist");

            }

            mapper.Map(request, restaurant);

            await restaurantsRepository.SaveChanges();

            logger.LogInformation($"Restaurant with id {request.Id} successfully updated.");

           
        }
    }
}
