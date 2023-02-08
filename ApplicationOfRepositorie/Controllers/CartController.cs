using ApplicationOfRepositorie.Models;
using ApplicationOfRepositorie.Models.ViewModels;
using ApplicationOfRepositorie.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApplicationOfRepositorie.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public int OrderTotal { get; set; }

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartVM = new ShoppingCartVM()
            {
                listCarts = _unitOfWork.ShoppingCart.GetAll(u=>u.ApplicationUserId == claim.Value, 
                includeProperties:"Product"),
                OrderHeader = new()
            };
            foreach(var cart in ShoppingCartVM.listCarts)
            {
                cart.Price = GetPriceBaseOnQuantity(cart.Count, cart.Product.Price, cart.Product.Price50, cart.Product.Price100);
                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price*cart.Count);
			}
            return View(ShoppingCartVM);
        }

        public IActionResult plus(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.GetFirstorDefault(u=>u.TableId == cartId);
            _unitOfWork.ShoppingCart.IncrementCount(cartFromDb, 1);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        } 
        
        public IActionResult minus(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.GetFirstorDefault(u => u.TableId == cartId);
            if(cartFromDb.Count <= 0) 
            {
				_unitOfWork.ShoppingCart.Remove(cartFromDb);
			}
            else
            {
				_unitOfWork.ShoppingCart.DecrementCount(cartFromDb, 1);
			}
			_unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult remove(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.GetFirstorDefault(u=>u.TableId == cartId);
            _unitOfWork.ShoppingCart.Remove(cartFromDb);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Summary()
        {
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			ShoppingCartVM = new ShoppingCartVM()
			{
				listCarts = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value,
				includeProperties: "Product"),
				OrderHeader = new()
			};

            ShoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.GetFirstorDefault(
                u => u.Id == claim.Value);
            ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.Name;
            ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
			ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.ApplicationUser.StreetAddress;
			ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City;
			ShoppingCartVM.OrderHeader.State = ShoppingCartVM.OrderHeader.ApplicationUser.State;
			ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.ApplicationUser.PostalCode;

			foreach (var cart in ShoppingCartVM.listCarts)
			{
				cart.Price = GetPriceBaseOnQuantity(cart.Count, cart.Product.Price, cart.Product.Price50, cart.Product.Price100);
				ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
			}
			return View(ShoppingCartVM);
        }
        [HttpPost, ActionName("Summary")]
        [ValidateAntiForgeryToken]
		public IActionResult SummaryPOST()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartVM.listCarts = _unitOfWork.ShoppingCart.GetAll(
                u => u.ApplicationUserId == claim.Value, 
                includeProperties: "Product");

			ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
			ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusPending;
			ShoppingCartVM.OrderHeader.OrderDate = DateTime.Now;
			ShoppingCartVM.OrderHeader.ApplicationUserId = claim.Value;

			foreach (var cart in ShoppingCartVM.listCarts)
			{
				cart.Price = GetPriceBaseOnQuantity(cart.Count, cart.Product.Price, cart.Product.Price50, cart.Product.Price100);
				ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
			}

            _unitOfWork.OrderHeader.Add(ShoppingCartVM.OrderHeader);
            _unitOfWork.Save();
			foreach (var cart in ShoppingCartVM.listCarts)
			{
                OrderDetail orderDetail = new OrderDetail()
                {
                    ProductId = cart.ProductId,
                    OrderId = ShoppingCartVM.OrderHeader.Id,
                    Count = cart.Count,
                    Price = cart.Price
                };
                _unitOfWork.OrderDetail.Add(orderDetail);
                _unitOfWork.Save();
			}

            _unitOfWork.ShoppingCart.RemoveRange(ShoppingCartVM.listCarts);
            _unitOfWork.Save();
			return RedirectToAction("Index","Home");
		}

		// creating a function because it will return the price total according to the quantity of particular book.
		private double GetPriceBaseOnQuantity(double quantity, double price, double price50, double price100)
        {
            if(quantity <= 20)
            {
                return price;
            }
            else
            {
                if(quantity <= 100)
                {
                    return price50;
                }
                return price100;
            }
        }
    }
}
