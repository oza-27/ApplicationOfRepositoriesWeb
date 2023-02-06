using ApplicationOfRepositorie.Models;
using ApplicationOfRepositorie.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ApplicationOfRepositorie.Controllers
{
	public class CategoryController : Controller
	{
		private IUnitOfWork _unitOfWork;
		public CategoryController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public IActionResult Index()
		{
			IEnumerable<Category> categoryList = _unitOfWork.Category.GetAll();
            return View(categoryList);
		}

		// Get Action method
		public IActionResult Create()
		{
			return View();
		}

		// POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Category obj)
		{
			_unitOfWork.Category.Add(obj);
			_unitOfWork.Save();
			return RedirectToAction("Index");
		}

		// Get For Edit
		public IActionResult Edit(int? id)
		{
			var obj = _unitOfWork.Category.GetFirstorDefault(u=>u.Id==id);
			if (obj == null)
			{
				return NotFound();
			}
			return View(obj);
		}

		// Post for edit
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Category obj)
		{
			_unitOfWork.Category.Update(obj);
			_unitOfWork.Save();	
			return RedirectToAction("Index");
		}

		// Get for Delete
		public IActionResult Delete(int? id)
		{
			var obj = _unitOfWork.Category.GetFirstorDefault(u=>u.Id==id);
			if (obj == null)
			{
				return NotFound();
			}
			return View(obj);
		}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult ActionResult(Category obj)
		{
			_unitOfWork.Category.Remove(obj);
			_unitOfWork.Save();	
			return RedirectToAction("Index");
		}
	}
}
