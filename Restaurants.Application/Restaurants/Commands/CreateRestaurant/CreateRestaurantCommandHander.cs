using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;


namespace Restaurants.Domain.Repositories.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHander(ILogger<CreateRestaurantCommandHander> logger, 
        IMapper mapper,
        IRestaurantsRepository restaurantsRepository) : IRequestHandler<CreateRestaurantCommand, int>
    {
        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creatingg a new restaurant");

            var restaurant = mapper.Map<Restaurant>(request);

            int id = await restaurantsRepository.Create(restaurant);

            return id;
        }
    }
}
