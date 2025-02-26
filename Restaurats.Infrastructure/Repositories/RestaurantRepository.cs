using Restaurats.Domain.Entities;
using Restaurats.Domain.Repositories;
using Restaurats.Infrastructure.Presistence;

namespace Restaurats.Infrastructure.Repositories;
internal class RestaurantRepository(RestaurantDbContext context) :
    GenericRepository<Restaurant>(context),IRestaurantRepository
{
}
