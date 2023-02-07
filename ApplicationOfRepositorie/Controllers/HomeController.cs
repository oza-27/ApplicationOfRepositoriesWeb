using ApplicationOfRepositorie.Models;
using ApplicationOfRepositorie.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace ApplicationOfRepositorie.Controllers
{
    public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUnitOfWork _unitOfWork;

		public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
			return View(productList);
		}

		public IActionResult Details(int productId)
		{
			ShoppingCart shopCart = new()
			{
				Count = 1,
				ProductId = productId,
				Product = _unitOfWork.Product.GetFirstorDefault(i => i.Id == productId, includeProperties: "Category,CoverType")
			};
			return View(shopCart);
		}
		
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize] // Only authorize user can access this page
        public IActionResult Details(ShoppingCart spCart)
        {
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			spCart.ApplicationUserId = claims.Value;
			
			_unitOfWork.ShoppingCart.Add(spCart);
			_unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}