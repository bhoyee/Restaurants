
namespace Restaurants.Domain.Exeception
{
    public class NotFoundExeception : Exception
    {
        public NotFoundExeception(string message ): base(message)
        { 

        }
    }
}
