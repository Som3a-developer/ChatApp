using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ChatApp.Models;

namespace ChatApp.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            var model = new ChatRoom
            {
                Name = User.Identity.Name
            };
            return View(model);
        }
    }
}