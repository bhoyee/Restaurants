﻿
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exeception;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger,
        IMapper mapper,
        IRestaurantsRepository restaurantsRepository) : IRequestHandler<DeleteRestaurantCommand>
    {
        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting restaurant with id: {RestaurantID}", request.Id);
            var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);
            if (restaurant is null)
                throw new NotFoundExeception($"Restaurant with {request.Id} doesn't exist");

            await restaurantsRepository.Delete(restaurant);
            
        }
    }
}
