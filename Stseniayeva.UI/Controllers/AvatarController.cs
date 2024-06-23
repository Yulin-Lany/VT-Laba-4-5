using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Stseniayeva.UI.Data;

namespace Stseniayeva.UI.Controllers
{
    public class AvatarController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        IWebHostEnvironment _env;
        public AvatarController(UserManager<ApplicationUser>
       userManager, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _env = env;
        }
        public async Task<FileResult> GetAvatar()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user.Avatar.Length > 0)
                return File(user.Avatar, "image/...");
            else
            {
                var avatarPath = "/Images/anonymous.png";

                return File(_env.WebRootFileProvider
                .GetFileInfo(avatarPath)
               .CreateReadStream(), "image/...");
            }
        }
    }
}
