using ApplicationOfRepositorie.Models;
using ApplicationOfRepositorie.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ApplicationOfRepositorie.Controllers
{
	public class CoverTypeController : Controller
	{
		private IUnitOfWork _unitOfWork;
		public CoverTypeController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public IActionResult Index()
		{
			IEnumerable<CoverType> categoryList = _unitOfWork.CoverType.GetAll();
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
		public IActionResult Create(CoverType obj)
		{
			_unitOfWork.CoverType.Add(obj);
			_unitOfWork.Save();
			return RedirectToAction("Index");
		}

		// Get For Edit
		public IActionResult Edit(int? id)
		{
			var obj = _unitOfWork.CoverType.GetFirstorDefault(u=>u.Id==id);
			if (obj == null)
			{
				return NotFound();
			}
			return View(obj);
		}

		// Post for edit
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(CoverType obj)
		{
			_unitOfWork.CoverType.Update(obj);
			_unitOfWork.Save();	
			return RedirectToAction("Index");
		}

		// Get for Delete
		public IActionResult Delete(int? id)
		{
			var obj = _unitOfWork.CoverType.GetFirstorDefault(u=>u.Id==id);
			if (obj == null)
			{
				return NotFound();
			}
			return View(obj);
		}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult ActionResult(CoverType obj)
		{
			_unitOfWork.CoverType.Remove(obj);
			_unitOfWork.Save();	
			return RedirectToAction("Index");
		}
	}
}
