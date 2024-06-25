
using Microsoft.AspNetCore.Http.HttpResults;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Domain.Exeception;

namespace Restaurants.API.Middlewares
{
    public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware

    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (DuplicateDishException ex)
            {
                context.Response.StatusCode = 409;
                await context.Response.WriteAsync(ex.Message);
                logger.LogError(ex.Message);
            }
            catch (RestaurantAlreadyExistsException ex)
            {
                context.Response.StatusCode = 409;
                await context.Response.WriteAsync(ex.Message);
                logger.LogError(ex.Message);
            }
            catch (NotFoundExeception notFound)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFound.Message);

                logger.LogError(notFound.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");

            }
        }
    }
}
