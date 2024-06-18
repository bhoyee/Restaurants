using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Domain.Entities;


namespace Restaurants.Domain.Repositories.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger, 
        IMapper mapper,
        IRestaurantsRepository restaurantsRepository) : IRequestHandler<CreateRestaurantCommand, int>
    {
        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new restaurant");

            // Check if a restaurant with the same details already exists
            var existingRestaurant = await restaurantsRepository.GetRestaurantByDetailsAsync(
                request.Name, request.ContactEmail, request.ContactNumber);

            if (existingRestaurant != null)
            {
                var existingDetails = new List<string>();

                if (existingRestaurant.Name == request.Name)
                    existingDetails.Add("Name");
                if (existingRestaurant.ContactEmail == request.ContactEmail)
                    existingDetails.Add("ContactEmail");
                if (existingRestaurant.ContactNumber == request.ContactNumber)
                    existingDetails.Add("ContactNumber");

                throw new RestaurantAlreadyExistsException($"A restaurant with the following details already exists: {string.Join(", ", existingDetails)}.");
            }

            var restaurant = mapper.Map<Restaurant>(request);

            int id = await restaurantsRepository.Create(restaurant);

            return id;
        }
    }
}
