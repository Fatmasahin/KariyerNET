﻿using KariyerNET.BLL.Abstract.CompanySide;
using KariyerNET.Model.CompanySide;
using KariyerNET.UI.MVC.CustomFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KariyerNET.UI.MVC.Controllers
{
    public class CompanyController : Controller
    {
        ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomFilter()]
        public ActionResult Login(Company company)
        {
            var gelenKullanici = _companyService.Get(company.ID);
            if (gelenKullanici != null)
            {
                Session["Kullanici"] = gelenKullanici;
                return RedirectToAction("Index", "Home");
            }
            //Else yapılacak 
            ViewBag.Error = "Kullanıcı Bulunamadı, Lütfen Üye Olunuz!";
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Company company)
        {
            //Burası tamamlanacak.


            //try
            //{
            //    _companyService.Insert(company);
            //    //Eğer 
            //    return View();
            //}
            //catch (Exception ex)
            //{
            //    ViewBag.Error = "Kayıt olma hatası!";
            //    return View();

            //}

            return RedirectToAction("Login", "Company");
        }



    }
}