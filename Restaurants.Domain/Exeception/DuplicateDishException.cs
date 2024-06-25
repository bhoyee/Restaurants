using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Exeception
{
    public class DuplicateDishException : Exception
    {
        public DuplicateDishException(string dishName, int restaurantId)
            : base($"A dish with name '{dishName}' already exists for restaurant with ID {restaurantId}.")
        {
        }
    }

}
