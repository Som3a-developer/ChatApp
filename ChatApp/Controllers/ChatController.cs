using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ChatApp.Data;
using ChatApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ChatApp.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChatController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> PrivateChat(string receiverId)
        {
            if (string.IsNullOrEmpty(receiverId))
            {
                return RedirectToAction("Index", "Home");
            }

            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(currentUserId))
            {
                return Unauthorized();
            }

            var receiver = await _context.Users.FirstOrDefaultAsync(u => u.Id == receiverId);
            if (receiver == null)
            {
                return NotFound();
            }

            var messages = await _context.Messages
                .Where(m => (m.SenderId == currentUserId && m.ReceiverId == receiverId) ||
                            (m.SenderId == receiverId && m.ReceiverId == currentUserId))
                .OrderBy(m => m.SentAt)
                .ToListAsync();

            var viewModel = new PrivateChatViewModel
            {
                ReceiverId = receiverId,
                ReceiverDisplayName = receiver.DisplayName,
                Messages = messages
            };

            return View(viewModel);
        }
    }
}