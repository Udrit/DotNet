using UdritDhakal.Infrastructure.IRepository.ICrudService;
using UdritDhakal.Models.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using UdritDhakal_PMS.Models;

namespace UdritDhakal_PMS.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICrudService<Category> _categoryCrudService;
        private readonly UserManager<ApplicationUser> _userManager;


        public CategoryController(ICrudService<Category> categoryCrudService, UserManager<ApplicationUser> userManager)
        {
            _categoryCrudService = categoryCrudService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {

            var categoryList = await _categoryCrudService.GetAllAsync();
            return View(categoryList);
        }

        public async Task<IActionResult> AddEdit(int Id)
        {
            Category category = new Category();
            category.IsAvaliable = true;
            if (Id > 0)
            {
                category = await _categoryCrudService.GetAsync(Id);
            }
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(Category category)
        {
            try
            {
                var user = _userManager.GetUserId(HttpContext.User);

                if (category.Id == 0)
                {
                    category.CreatedDate = DateTime.Now;
                    category.CreatedBy = user;
                    await _categoryCrudService.InsertAsync(category);
                    TempData["success"] = "Data Added Successfully";
                }
                else
                {
                    var categoryInfo = await _categoryCrudService.GetAsync(category.Id);
                    categoryInfo.categoryName = category.categoryName;
                    categoryInfo.categoryDescription = category.categoryDescription;
                    categoryInfo.IsAvaliable = category.IsAvaliable;

                    await _categoryCrudService.UpdateAsync(categoryInfo);
                    TempData["success"] = "Data Updated Successfully";

                }

                return RedirectToAction(nameof(Index));


            }
            catch (Exception ex)
            {
                TempData["error"] = "Insert Data Properly";
                return RedirectToAction(nameof(AddEdit));
            }
        }
           

          
        
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryCrudService.GetAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            await _categoryCrudService.DeleteAsync(category);
            TempData["error"] = "Data Deleted Successfully";
            return RedirectToAction(nameof(Index));


        }
    }
}
