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
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productView = new()
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

                return View(productView);
            }
            else
            {
                productView.product = _unitOfWork.Product.GetFirstofDefault(u=>u.Id==id);
                return View(productView);
            }
            
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
                    var uploads = Path.Combine(wwwPath, @"images\products\");
                    var extension = Path.GetExtension(file.FileName);

                    if(obj.product.ImageUrl != null)
                    {
                        var oldImage = Path.Combine(wwwPath,obj.product.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImage))
                        {
                            System.IO.File.Delete(oldImage);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(
                        uploads, 
                        fileName+extension),
                        FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.product.ImageUrl = @"\images\products\"+fileName+extension;
                }
                if(obj.product.Id == 0)
                {
                    _unitOfWork.Product.Add(obj.product);
                }
                else
                {
                    _unitOfWork.Product.Update(obj.product);
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }



        #region API CALLS
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties:"Category,CoverType");
            return Json(new
            {
                data = productList
            });
        }
        [HttpDelete] 
        public IActionResult Delete(int? id) 
        {
            var obj = _unitOfWork.Product.GetFirstofDefault(u => u.Id == id);
            if(obj == null)
            {
                return Json(new
                {
                    success = false,
                    message="Error while Deleting"
                });
            }
            var oldImage = Path.Combine(_webHostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImage))
            {
                System.IO.File.Delete(oldImage);
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            return Json(new
            {
                success = true,
                message = "Product Deleted Successfully"
            });
        }
        

        #endregion
    }
}
