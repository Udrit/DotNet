using Microsoft.AspNetCore.Mvc;
using UdritDhakal.ViewModels;
using UdritDhakal.Services;

namespace UdritDhakal_SMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StudentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddStudent()
        { 
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> AddStudent(CreateStudentViewModel vm)
        //{
        //    await _studentService.AddStudent(vm);
        //    return RedirectToAction ("Index");
        //}

    }
}
