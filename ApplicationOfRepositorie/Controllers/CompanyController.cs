﻿using ApplicationOfRepositorie.Models;
using ApplicationOfRepositorie.Models.ViewModels;
using ApplicationOfRepositorie.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ApplicationOfRepositorie.Controllers
{
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Company companyObj = new();
            
            if (id == null || id == 0)
            {
                return View(companyObj);
            }
            else
            {
                companyObj = _unitOfWork.Company.GetFirstorDefault(u=>u.CompanyId==id);
                return View(companyObj);
            }
            
        }

        //post 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company obj)
        {
            if(ModelState.IsValid)
            {
                
                if(obj.CompanyId == 0)
                {
                    _unitOfWork.Company.Add(obj);
                    TempData["success"] = "Company Created Successfully";
                }
                else
                {
                    _unitOfWork.Company.Update(obj);
                    TempData["success"] = "Company Updated Successfully";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }



        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var companyList = _unitOfWork.Company.GetAll();
            return Json(new
            {
                data = companyList
            });
        }
        [HttpDelete] 
        public IActionResult Delete(int? id) 
        {
            var obj = _unitOfWork.Company.GetFirstorDefault(u => u.CompanyId == id);
            if(obj == null)
            {
                return Json(new
                {
                    success = false,
                    message="Error while Deleting"
                });
            }
            _unitOfWork.Company.Remove(obj);
            _unitOfWork.Save();
            return Json(new
            {
                success = true,
                message = "Company Deleted Successfully"
            });
        }
        

        #endregion
    }
}
