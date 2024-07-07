using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using UdritDhakal.Infrastructure.IRepository;
using UdritDhakal.Infrastructure.IRepository.ICrudService;
using UdritDhakal.Models.Entity;
using UdritDhakal.Models.ViewModels;
using UdritDhakal_PMS.Models;

namespace UdritDhakal_PMS.Controllers
{
    public class DetailController : Controller
    {
        private readonly ILogger<DetailController> _logger;
        private readonly ICrudService<Category> _categoryCrudService;
        private readonly ICrudService<Product> _productCrudService;

        private readonly UserManager<ApplicationUser> _userManager;

        public DetailController(
            ICrudService<Product> productCrudService,
            UserManager<ApplicationUser> usermanager,

ICrudService<Category> categoryCrudService)
        {

            _productCrudService = productCrudService;
            _userManager = usermanager;
            _categoryCrudService = categoryCrudService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CategoryInfos = await _categoryCrudService.GetAllAsync();
            ProductViewModel productViewModel = new ProductViewModel();
            productViewModel.products = await _productCrudService.GetAllAsync(e => e.IsAvailable);


            return View(productViewModel);
        }

        public async Task<IActionResult> search(ProductViewModel productViewModel)
        {
            ViewBag.CategoryInfos = await _categoryCrudService.GetAllAsync();
            productViewModel.products = await _productCrudService.GetAllAsync(e => e.CategoryId == productViewModel.searchViewModel.categoryId);
            productViewModel.products = productViewModel.products.Where(p => Regex.IsMatch(p.ProductName, "^" + productViewModel.searchViewModel.ProductName + ".*$"));

            return View("Index", productViewModel);
        }

        public async Task<IActionResult> Home(int id)
        {

            return View();
        }


    }
}
