﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Filters;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Repository.Repository.AboutRepository;
using Repository.Repository.CaseRepository;
using Repository.Repository.CategoryRepository;

namespace AspFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [TypeFilter(typeof(Auth))]
    public class AboutPolicyController : Controller
    {
        private Repository.Models.Admin _admin => RouteData.Values["Admin"] as Repository.Models.Admin;
        private readonly IAboutRepository _aboutRepository;
        public AboutPolicyController(IAboutRepository aboutRepository)
        {
            _aboutRepository = aboutRepository;
        }
        public IActionResult Index()
        {
            var model = _aboutRepository.GetAboutPolicy();
            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.AboutUs = _aboutRepository.GetAboutFull();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AboutPolicy model)
        {
            ViewBag.AboutUs = _aboutRepository.GetAboutFull();
            if (model == null) return NotFound();
            if (ModelState.IsValid)
            {
                model.AddedBy = _admin.Name;
                model.AddedDate = DateTime.Now;
                _aboutRepository.CreateAboutPolicy(model);
                return RedirectToAction("index");
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            ViewBag.AboutUs = _aboutRepository.GetAboutFull();
            var model = _aboutRepository.AboutPolicylById(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AboutPolicy model)
        {
            ViewBag.AboutUs = _aboutRepository.GetAboutFull();
            var updateAboutPolicy = _aboutRepository.AboutPolicylById(model.Id);
            if (model == null) return NotFound();
            if (ModelState.IsValid)
            {
                updateAboutPolicy.ModifiedBy = _admin.Name;
                updateAboutPolicy.ModifiedDate = DateTime.Now;
                _aboutRepository.UpdatePolicy(updateAboutPolicy, model);
                return RedirectToAction("index");
            }
            return View(model);
        }
        public IActionResult Remove(int id)
        {
            var policy = _aboutRepository.AboutPolicylById(id);
            _aboutRepository.RemovePolicy(policy);
            var model = _aboutRepository.GetAboutPolicy();
            return PartialView("index", model);
        }
    }
}