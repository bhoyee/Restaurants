
namespace Restaurants.Domain.Exeception
{
    public class NotFoundExeception(string resourceType, string resourceIdentifier) 
        : Exception($"{resourceType} with id: {resourceIdentifier} doesn't exist")
    {

    }
}
