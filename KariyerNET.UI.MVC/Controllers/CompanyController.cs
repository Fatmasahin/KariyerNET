﻿using KariyerNET.BLL.Abstract;
using KariyerNET.BLL.Abstract.CompanySide;
using KariyerNET.BLL.Abstract.EmployeeSide;
using KariyerNET.Model.CompanySide;
using KariyerNET.Model.EmployeeSide;
using KariyerNET.UI.MVC.CustomFilter;
using KariyerNET.UI.MVC.Tools;
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
        ICityService _cityService;
        ITownService _townService;
        IPerfectionService _perfectionService;
        IJobAdvertService _jobAdvertService;
        IEducationService _educationService;

        public CompanyController(ICompanyService companyService, ITownService townService, ICityService cityService, IPerfectionService perfectionService, IJobAdvertService jobAdvertService, IEducationService educationService)
        {
            _companyService = companyService;
            _townService = townService;
            _cityService = cityService;
            _perfectionService = perfectionService;
            _jobAdvertService = jobAdvertService;
            _educationService = educationService;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [IsLoginFilter()]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Company company)
        {
            var gelenKullanici = _companyService.CompanyLogin(company.EMail, company.Password);
            if (gelenKullanici != null)
            {
                Session["user"] = gelenKullanici;
                return RedirectToAction("Index", "Home"); //babanın evine git:D
            }
            ViewBag.Error = "Kullanıcı Bulunamadı, Lütfen Üye Olunuz!";
            return View();
        }


        [IsLoginFilter]
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Company company)
        {
            try
            {
                if (Request.Files.Count > 0)
                {
                    //Guid nesnesini benzersiz dosya adı oluşturmak için tanımladık ve Replace ile aradaki “-” işaretini atıp yan yana yazma işlemi yaptık.
                    string DosyaAdi = Guid.NewGuid().ToString().Replace("-", "");
                    //Kaydetceğimiz resmin uzantısını aldık.
                    string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    string TamYolYeri = "~/Content/image/KullaniciResimleri/" + DosyaAdi + uzanti;
                    //Eklediğimiz Resni Server.MapPath methodu ile Dosya Adıyla birlikte kaydettik.
                    Request.Files[0].SaveAs(Server.MapPath(TamYolYeri));
                    //Ve veritabanına kayıt için dosya adımızı değişkene aktarıyoruz.
                    company.LogoURL = DosyaAdi + uzanti;
                }
                _companyService.Insert(company);
                TempData["Message"] = "Kayıt oluşturuldu";
                MailHelper.SendConfirmationMail(company.EMail, company.CompanyName);
                return RedirectToAction("Login", "Company");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }


        }



        [HttpGet]
        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgetPassword(Company company)
        {
            var kullanici = _companyService.GetByMail(company.EMail);
            bool sonuc = MailHelper.SendForgottenPasswordMail(kullanici.EMail, kullanici.CompanyName, kullanici.Password);
            if (!sonuc)
            {
                ViewBag.Error = "Mail gönderilemedi, daha sonra tekrar deneyiniz.";
                return View();
            }
            TempData["Message"] = "Hatırlatma maili kayıtlı e-posta adresinize gönderildi"; 
            return RedirectToAction("Login", "Company");
        }


        [HttpGet]
        public ActionResult PublishedJobAdvert()
        {
            ViewBag.Iller = _cityService.GetAll();
            ViewBag.Ilce = _townService.GetAll();
            ViewBag.Yetenekler = _perfectionService.GetAll();
            ViewBag.Egitim = Enum.GetValues(typeof(EducationLevel));
            ViewBag.Askerlik = Enum.GetValues(typeof(MilitaryState));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PublishedJobAdvert(JobAdvert jobAdvert)
        {
            try
            {
                _jobAdvertService.Insert(jobAdvert);
                ViewBag.Message = "İlan yayınlandı.";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Iller = _cityService.GetAll();
                ViewBag.Ilce = _townService.GetAll();
                ViewBag.Yetenekler = _perfectionService.GetAll();
                ViewBag.Egitim = Enum.GetValues(typeof(EducationLevel));
                ViewBag.Askerlik = Enum.GetValues(typeof(MilitaryState));
                ViewBag.Error = ex.Message;
                return View();
            }
        }

    }
}