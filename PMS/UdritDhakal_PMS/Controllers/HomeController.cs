using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using UdritDhakal.Infrastructure.IRepository.ICrudService;
using UdritDhakal.Models.Entity;
using UdritDhakal.Models.ViewModels;
using UdritDhakal_PMS.Models;

namespace UdritDhakal_PMS.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }


    }
}
