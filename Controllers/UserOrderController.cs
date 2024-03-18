using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    [Authorize]
    public class UserOrderController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IUserOrderRepository _userOrderRepository;
        public UserOrderController(IUserOrderRepository userOrderRepository, ApplicationDbContext db) 
        {
            _userOrderRepository = userOrderRepository;
            _db = db;
        }

        public async Task<IActionResult> UserOrders()
        {
            var orders = await _userOrderRepository.UserOrders();
            return View(orders);
        }

        public ActionResult GetOrderDetail()
        {
            Order order = GetOrderData();
            return View(order);
        }

        private Order GetOrderData()
        {
            var orderDetails = _db.OrderDetails
                                  .Include(od => od.Product)
                                  .Include(od => od.Product.Genre)
                                  .ToList();

            // Map database entities to model classes
            var orderDetail = orderDetails.Select(od => new OrderDetail
            {
                Product = new Product
                {
                    Title = od.Product.Title,
                    ImgUrl = od.Product.ImgUrl,
                    Genre = new Genre
                    {
                        GenreName = od.Product.Genre.GenreName
                    },
                    Price = od.Product.Price
                },
                Quantity = od.Quantity
            }).ToList();

            // Create an instance of OrderModel and assign the list of order details
            var order = new Order
            {
                OrderDetail = orderDetail
            };
            return order;
        }
    }
}
