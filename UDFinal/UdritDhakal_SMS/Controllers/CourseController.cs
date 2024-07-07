using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UdritDhakal.Infrastructure.IRepository;
using UdritDhakal.Models.Entity;
using UdritDhakal_SMS.Models;

namespace UdritDhakal_SMS.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly ICRUDServices<Course> _course;
        private readonly ICRUDServices<Student> _student;
        private readonly ICRUDServices<Degree> _degree;
        private readonly UserManager<ApplicationUser> _user;

        public CourseController(ICRUDServices<Course> course, ICRUDServices<Student> student, UserManager<ApplicationUser> user, ICRUDServices<Degree> degree)
        {
            _course = course;
            _student = student;
            _user = user;
            _degree = degree;
        }

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("ADMIN"))
            {
                var course = await _course.GetAllAsync();
                return View(course);
            }
            else
            {
                var course = await _course.GetAllAsync(p => p.IsActive);
                return View(course);
            }

        }
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> AddEdit(int id)
        {
            Course course = new Course();
            if (id != 0)
            {
                course = await _course.GetAsync(id);
            }
            return View(course);

        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(Course course)
        {
            if (ModelState.IsValid)
            {
                var UserId = _user.GetUserId(HttpContext.User);
                if (course.Id == 0)
                {
                    course.CreatedBy = UserId;
                    course.CreatedDate = DateTime.Now;
                    await _course.InsertAsync(course);
                }
                else if (course.Id != 0)
                {
                    Course updated_course = await _course.GetAsync(course.Id);
                    updated_course.IsActive = course.IsActive;
                    updated_course.CourseName = course.CourseName;
                    updated_course.CourseDescription = course.CourseDescription;
                    updated_course.CreditHours = course.CreditHours;
                    updated_course.FullMarks = course.FullMarks;
                    updated_course.ModifiedDate = DateTime.Now;
                    updated_course.ModifiedBy = UserId;
                    await _course.UpdateAsync(updated_course);
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(AddEdit));

        }


    }
}
