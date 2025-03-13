using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Restaurats.Domain.Entities;
using Restaurats.Domain.Repositories;
using Restaurats.Infrastructure.Presistence;

namespace Restaurats.Infrastructure.Repositories;
internal class RestaurantRepository(RestaurantDbContext context) :
    GenericRepository<Restaurant>(context),IRestaurantRepository
{
    private readonly RestaurantDbContext _context = context;
    public async Task<IEnumerable<Restaurant>> GetAllPagedAsync
        (string? searchTerm, string? sortBy, string? orderBy, CancellationToken cancellationToken = default)
    {
        var query = _context.Restaurants.AsQueryable();
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(r => r.Name.ToLower().Contains(searchTerm.ToLower()));
        }
        if (!string.IsNullOrWhiteSpace(sortBy) && !string.IsNullOrWhiteSpace(orderBy))
        {
            var sortedDic=new Dictionary<string, Expression<Func<Restaurant, object>>>
            {
                { "name", r => r.Name },
                { "description", r => r.Description },
                { "id", r => r.Id }

            };
            var expression = sortedDic[sortBy.ToLower()];
            
            query = orderBy switch
            {
                "asc" => query.OrderBy(expression),
                "desc" => query.OrderByDescending(expression),
                _ => query
            };
        }
        return await query.ToListAsync(cancellationToken);
    }
}
