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
        public IActionResult Details(ShoppingCart shoppingCart)
        {
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId = claims.Value;
			
			ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.GetFirstorDefault(
				x=>x.ApplicationUserId == claims.Value && x.ProductId == shoppingCart.ProductId);
			if(cartFromDb == null)
			{
				_unitOfWork.ShoppingCart.Add(shoppingCart);
				_unitOfWork.Save();

			}
			else
			{
				// it will increment the count because user already addded some counts at past
				_unitOfWork.ShoppingCart.IncrementCount(cartFromDb, shoppingCart.Count);
				_unitOfWork.Save();

			}
			return RedirectToAction("Index");
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