using FastFoodOrderingApp_BackEnd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FastFoodOrderingApp_BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        // Inject both ApplicationDbContext and UserManager<ApplicationUser>
        public OrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            // Validate the incoming order object
            if (order == null || order.User == null || string.IsNullOrEmpty(order.User.Id))
            {
                return BadRequest("Invalid order or user information.");
            }

            // Retrieve the existing user by Id
            var existingUser = await _userManager.FindByIdAsync(order.User.Id);
            if (existingUser == null)
            {
                return BadRequest("User does not exist.");
            }

            // Create the new order and associate it with the existing user
            var newOrder = new Order
            {
                OrderId = Guid.NewGuid().ToString(), // Generate a new unique OrderId
                OrderDate = DateTime.Now,             // Set the current date/time
                TotalAmount = order.TotalAmount,      // Set the order's total amount
                User = existingUser                  // Associate the existing user
            };

            // Add the new order to the database and save changes
            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            return Ok(newOrder); // Return the newly created order as a response
        }
    }
}

