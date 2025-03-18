using Restaurats.Domain.Constants;
using Restaurats.Domain.Entities;

namespace Restaurats.Domain.Interfaces;
public interface IRestaurantAuthorizationService
{
    bool Authorize(Restaurant restaurant,ResourceOperation resourceOperation);
}
