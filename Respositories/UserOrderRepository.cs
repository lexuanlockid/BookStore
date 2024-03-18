using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Respositories
{
    public class UserOrderRepository : IUserOrderRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        public UserOrderRepository(ApplicationDbContext db, IHttpContextAccessor contextAccessor, UserManager<IdentityUser> userManager) 
        { 
            _db = db;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }
        public async Task<IEnumerable<Order>> UserOrders()
        {
            var userId = GetUserId();
            if(string.IsNullOrEmpty(userId))
            {
                throw new Exception("User doesn't login");
            }
            var order = await _db.Orders
                        .Include(x => x.OrderStatus)
                        .Include(x=>x.OrderDetail)                        
                        .ThenInclude(x=>x.Product)
                        .ThenInclude(x=>x.Genre)
                        .Where(a=>a.UserId==userId)
                        .ToListAsync();
            return order;
        }
        private string GetUserId()
        {
            var principal = _contextAccessor.HttpContext.User;
            var userId = _userManager.GetUserId(principal);
            return userId;
        }
    }
}
