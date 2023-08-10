using Microsoft.AspNetCore.Mvc;

using taskApp.Domain.Entity;
using taskApp.Domain.Interfaces;
using taskApp.Domain.Services.Repositories;

namespace TaskManagementSystem.WebApp.Controllers
{
    public class AccountController : Controller
    {
      private readonly  IUserRepository _userRepository;
        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public User ActiveUser = null;

        public JsonResult LoginUsername(string username)
        {
            ActiveUser = _userRepository.ValidateUsername(username);
            if (ActiveUser != null)
            {
                return Json(ActiveUser.ID);
            }
            else
            {
                return Json(-1);
            }
        }
        public JsonResult LoginPassword(string password)
        {
            ActiveUser = _userRepository.ValidatePassword(password);
            if (ActiveUser != null)
            {
                if(ActiveUser.Type == 0)
                {
                    return Json(0);
                }
                if(ActiveUser.Type == 1)
                {
                    return Json(1);
                }
                else
                {
                    return Json(-1);
                }
                
            }
            else
            {
                return Json(-1);
            }
        }
    }
}