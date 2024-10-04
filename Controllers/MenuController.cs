using FastFoodOrderingApp_BackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastFoodOrderingApp_BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MenuController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetMenuItems()
        {
            var menuItems = await _context.MenuItems.ToListAsync();
            return Ok(menuItems);
        }

        // Admin-only: Create a new menu item
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateMenuItem([FromBody] MenuItem menuItem)
        {
            // Create a new MenuItem instance from the provided model
            var newMenuItem = new MenuItem
            {
                MenuItemId = menuItem.MenuItemId,
                Name = menuItem.Name,
                Description = menuItem.Description,
                Price = menuItem.Price,
                ImageUrl = menuItem.ImageUrl
            };

            // Add the new menu item to the database and save changes
            _context.MenuItems.Add(newMenuItem);
            await _context.SaveChangesAsync();

            // Return the newly created menu item
            return Ok(newMenuItem);
        }
        // Admin-only: Update an existing menu item
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateMenuItem(int id, [FromBody] MenuItem updatedMenuItem)
        {
            // Find the existing menu item by ID
            var existingMenuItem = await _context.MenuItems.FindAsync(id);
            if (existingMenuItem == null)
            {
                return NotFound("Menu item not found.");
            }

            // Update the menu item with new values
            existingMenuItem.Name = updatedMenuItem.Name;
            existingMenuItem.Description = updatedMenuItem.Description;
            existingMenuItem.Price = updatedMenuItem.Price;
            existingMenuItem.ImageUrl = updatedMenuItem.ImageUrl;

            // Save changes to the database
            _context.MenuItems.Update(existingMenuItem);
            await _context.SaveChangesAsync();

            return Ok(existingMenuItem);
        }
        // Admin-only: Delete an existing menu item
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            // Find the menu item by ID
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null)
            {
                return NotFound("Menu item not found.");
            }

            // Remove the menu item from the database
            _context.MenuItems.Remove(menuItem);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Menu item deleted successfully." });
        }

    }
}
