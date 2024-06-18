using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class RestaurantAlreadyExistsException : Exception
    {
        public RestaurantAlreadyExistsException(string message) : base(message)
        {
        }
    }
}
