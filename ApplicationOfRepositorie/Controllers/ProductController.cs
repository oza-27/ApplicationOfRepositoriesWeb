using ApplicationOfRepositorie.Models;
using ApplicationOfRepositorie.Models.ViewModels;
using ApplicationOfRepositorie.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ApplicationOfRepositorie.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll();
            return View(productList);
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM product = new()
            {
                product = new(),
                categoryList = _unitOfWork.Category.GetAll().Select(i=> new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                }),
                covertypeList = _unitOfWork.CoverType.GetAll().Select(u=> new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                })
            };
            if (id == null || id == 0)
            {
                //ViewBag.categoryList = categoryList;
                //ViewBag.covertypeList = covertypeList;

                return View(product);
            }
            return View(product);
        }
        //post 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            if(ModelState.IsValid)
            {
                string wwwPath = _webHostEnvironment.WebRootPath;
                if(file!=null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwPath, @"imgaes\products\");
                    var extension = Path.GetExtension(file.FileName);

                    using (var fileStreams = new FileStream(Path.Combine(
                        uploads,
                        fileName+extension),
                        FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.product.ImageUrl = @"\images\products\"+fileName+extension;
                }
                _unitOfWork.Product.Add(obj.product);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        #region API CALLS
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll();
            return Json(new
            {
                data = productList
            });
        }

        #endregion
    }
}
