using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;
using UdritDhakal.Infrastructure.IRepository;
using UdritDhakal.Models.Entity;
using UdritDhakal.Models.ViewModels;
using UdritDhakal_SMS.Models;

namespace UdritDhakal_SMS.Controllers
{
    public class DegreeController : Controller
    {

        private readonly ICRUDServices<Course> _course;
        private readonly ICRUDServices<Student> _student;
        private readonly ICRUDServices<Degree> _degree;
        private readonly UserManager<ApplicationUser> _user;
        private readonly ICRUDServices<DegreeCourse> _DegreeCourse;
        private readonly IRawSqlRepository rawSqlRepository;

        public DegreeController(ICRUDServices<Course> course, ICRUDServices<Student> student, UserManager<ApplicationUser> user, ICRUDServices<Degree> degree, ICRUDServices<DegreeCourse> degreeCourse, IRawSqlRepository rawSqlRepository)
        {
            _course = course;
            _student = student;
            _user = user;
            _degree = degree;
            _DegreeCourse = degreeCourse;
            this.rawSqlRepository = rawSqlRepository;
        }

        public async Task<IActionResult> Index(DegreeViewModel degreeViewModel)
        {
            ViewBag.Course = await _course.GetAllAsync();
            if (degreeViewModel.Degree.IsNullOrEmpty())
            {
                if (User.IsInRole("ADMIN"))
                {
                    degreeViewModel.Degree = await _degree.GetAllAsync();
                }
                else
                {
                    degreeViewModel.Degree = await _degree.GetAllAsync(p => p.IsActive);
                }
            }
            return View(degreeViewModel);
        }

        public async Task<IActionResult> Search(DegreeViewModel degreeViewModel)
        {
            ViewBag.Course = await _course.GetAllAsync();

            var result = rawSqlRepository.FromSql<Degree>("usp_getDegree @courseId, @level, @startDate, @query",
              new SqlParameter("@courseId", degreeViewModel.degreeSearchViewModel.CourseId == 0 ? (object)DBNull.Value : degreeViewModel.degreeSearchViewModel.CourseId),
              new SqlParameter("@level", degreeViewModel.degreeSearchViewModel.Level == null ? (object)DBNull.Value : degreeViewModel.degreeSearchViewModel.Level),
              new SqlParameter("@startDate", degreeViewModel.degreeSearchViewModel.StartDate == null ? (object)DBNull.Value : degreeViewModel.degreeSearchViewModel.StartDate),
              new SqlParameter("@query", degreeViewModel.degreeSearchViewModel.Query == null ? (object)DBNull.Value : degreeViewModel.degreeSearchViewModel.Query)
              ).ToList();

            degreeViewModel.Degree = result;

            return View(nameof(Index), degreeViewModel);

        }

        [Authorize]
        public async Task<IActionResult> AddEdit(int id)
        {
            Degree degree = new Degree();
            if (id != 0)
            {
                degree = await _degree.GetAsync(id);
            }
            return View(degree);

        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(Degree degree)
        {
            if (ModelState.IsValid)
            {
                var UserId = _user.GetUserId(HttpContext.User);
                if (degree.Id == 0)
                {
                    degree.CreatedBy = UserId;
                    degree.CreatedDate = DateTime.Now;
                    await _degree.InsertAsync(degree);
                }
                else if (degree.Id != 0)
                {
                    Degree updated_degree = await _degree.GetAsync(degree.Id);
                    updated_degree.IsActive = degree.IsActive;
                    updated_degree.StartDate = degree.StartDate;
                    updated_degree.DegreeName = degree.DegreeName;
                    updated_degree.DegreeDescription = degree.DegreeDescription;
                    updated_degree.Academic_Level = degree.Academic_Level;
                    updated_degree.No_Of_Semesters = degree.No_Of_Semesters;
                    updated_degree.No_Of_Years = degree.No_Of_Years;
                    updated_degree.TotalSeats = degree.TotalSeats;
                    updated_degree.ModifiedDate = DateTime.Now;
                    updated_degree.ModifiedBy = UserId;
                    await _degree.UpdateAsync(updated_degree);
                }
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(AddEdit));

        }

        [HttpGet]
        [Route("api/Degree/getAllCourses")]
        public async Task<IActionResult> getAllCourses(string query)
        {
            query = Regex.Escape(query);
            CourseSearchViewModel courseSearchViewModel = new CourseSearchViewModel();
            try
            {
                var courses = await _course.GetAllAsync();

                // In-memory filtering using Regex
                var filteredCourses = courses
                    .Where(p => Regex.IsMatch(p.CourseName, "^" + Regex.Escape(query) + ".*$", RegexOptions.IgnoreCase))
                    .ToList();
                courseSearchViewModel.Courses = filteredCourses;
            }
            catch (Exception ex)
            {

            }
            return Json(new { courseSearchViewModel.Courses });
        }

        [HttpPost]
        [Route("api/Degree/getSemesterCourses")]
        public async Task<IActionResult> getSemesterCourses(CourseSearchViewModel courseSearchViewModel)
        {
            try
            {
                var courseList = new List<CourseResultViewModel>();
                if (courseSearchViewModel.DegreeId != 0)
                {
                    var courseIds = await _DegreeCourse.GetAllAsync(p => p.DegreeId == courseSearchViewModel.DegreeId && p.Semester == courseSearchViewModel.Semester);

                    foreach (var courseId in courseIds)
                    {
                        CourseResultViewModel courseResultViewModel = new CourseResultViewModel();
                        var course = await _course.GetAsync(p => p.Id == courseId.CourseId);
                        courseResultViewModel.Id = course.Id;
                        courseResultViewModel.CourseName = course.CourseName;
                        courseList.Add(courseResultViewModel);
                    }
                }
                return Json(new { courseList });

            }
            catch (Exception ex) { return Json(new { courseSearchViewModel.Courses }); }
        }

        [HttpPost]
        [Route("api/Degree/setCourse")]
        public async Task<IActionResult> setCourse(CourseSearchViewModel courseSearchViewModel)
        {
            DegreeCourse degreeCourse = new DegreeCourse();
            degreeCourse.CourseId = courseSearchViewModel.CourseId;
            degreeCourse.DegreeId = courseSearchViewModel.DegreeId;
            degreeCourse.Semester = courseSearchViewModel.Semester;
            var result = await _DegreeCourse.InsertAsync(degreeCourse);
            return Json(new { result });
        }

        [HttpPost]
        [Route("api/Degree/DeleteCourse")]
        public async Task<IActionResult> DeleteCourse(CourseSearchViewModel courseSearchViewModel)
        {
            var degreeCourse = await _DegreeCourse.GetAsync(p => p.DegreeId == courseSearchViewModel.DegreeId && p.Semester == courseSearchViewModel.Semester && p.CourseId == courseSearchViewModel.CourseId);

            var result = _DegreeCourse.Delete(degreeCourse);
            return Json(new { result });
        }

        [HttpGet]
        [Route("api/Degree/getRemainingSeats")]
        public async Task<IActionResult> getRemainingSeats(int totalCount, int degreeId)
        {
            var occupiedSeats = await _student.GetAllAsync(p => p.DegreeId == degreeId);
            var Count = occupiedSeats.Count();
            return Json(new { Count });
        }

        [HttpPost]
        [Route("api/Degree/PostStudent")]
        public async Task<IActionResult> PostStudent(Student studentData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var UserId = _user.GetUserId(HttpContext.User);
                    var user = await _user.FindByIdAsync(UserId);

                    if (studentData.studentPhoto != null)
                    {
                        string fileDirectory = $"wwwroot/StudentImage";

                        if (!Directory.Exists(fileDirectory))
                        {
                            Directory.CreateDirectory(fileDirectory);
                        }
                        string uniqueFileName = Guid.NewGuid() + "_" + studentData.studentPhoto.FileName;
                        string filePath = Path.Combine(Path.GetFullPath($"wwwroot/StudentImage"), uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await studentData.studentPhoto.CopyToAsync(fileStream);
                            studentData.studenturl = $"StudentImage/" + uniqueFileName;
                        }

                    }

                    if (studentData.transcriptPhoto != null)
                    {
                        string fileDirectory = $"wwwroot/TranscriptImage";

                        if (!Directory.Exists(fileDirectory))
                        {
                            Directory.CreateDirectory(fileDirectory);
                        }
                        string uniqueFileName = Guid.NewGuid() + "_" + studentData.transcriptPhoto.FileName;
                        string filePath = Path.Combine(Path.GetFullPath($"wwwroot/TranscriptImage"), uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await studentData.transcriptPhoto.CopyToAsync(fileStream);
                            studentData.transcriptPhotoUrl = $"TranscriptImage/" + uniqueFileName;
                        }
                    }

                    if (studentData.citizenshipPhoto != null)
                    {
                        string fileDirectory = $"wwwroot/citizenshipPhotoImage";

                        if (!Directory.Exists(fileDirectory))
                        {
                            Directory.CreateDirectory(fileDirectory);
                        }
                        string uniqueFileName = Guid.NewGuid() + "_" + studentData.citizenshipPhoto.FileName;
                        string filePath = Path.Combine(Path.GetFullPath($"wwwroot/citizenshipPhotoImage"), uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await studentData.citizenshipPhoto.CopyToAsync(fileStream);
                            studentData.citizenshipPhotoUrl = $"citizenshipPhotoImage/" + uniqueFileName;
                        }

                    }
                    studentData.CreatedBy = UserId;
                    studentData.CreatedDate = DateTime.Now;
                    user.HasEnrolled = true;

                    var degree = await _degree.GetAsync(studentData.DegreeId);
                    await _degree.UpdateAsync(degree);
                    await _user.UpdateAsync(user);
                    var result = await _student.InsertAsync(studentData);
                }
                catch (Exception ex) { }
            }

            return Json(1);
        }

        [HttpGet]
        [Route("api/Degree/getSemesterNo")]
        public async Task<IActionResult> getSemesterNo(int DegreeId)
        {
            if (DegreeId != 0)
            {
                var Degree = await _degree.GetAsync(DegreeId);
                var number = Degree.No_Of_Semesters;
                return Json(new { number });
            }
            return Json(1);

        }

        [HttpGet]
        [Route("api/Degree/getDegrees")]
        public async Task<IActionResult> getDegrees(string query)
        {
            query = Regex.Escape(query);

            var degrees = await _degree.GetAllAsync();

            // In-memory filtering using Regex
            var degreeList = degrees
                .Where(p => Regex.IsMatch(p.DegreeName, "^" + Regex.Escape(query) + ".*$", RegexOptions.IgnoreCase))
                .ToList();

            return Json(new { degreeList });
        }
    }
}