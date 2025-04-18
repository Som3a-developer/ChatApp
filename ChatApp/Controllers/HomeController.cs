using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChatApp.Data;
using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ChatApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            string currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(currentUserId))
            {
                return Unauthorized();
            }

            var users = await _context.Users
                .Where(u => u.Id != currentUserId)
                .Select(u => new UserListViewModel
                {
                    Id = u.Id,
                    DisplayName = u.DisplayName,
                    Email = u.Email
                })
                .ToListAsync();

            return View(users);
        }
    }
}