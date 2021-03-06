﻿using AspFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository.CaseRepository;
using Repository.Repository.HomeRepository;

namespace AspFinal.Controllers
{
    public class CaseController : Controller
    {
        private readonly IHomeRepository _homeRepository;
        private readonly ICaseRepository _caseRepository;

        public CaseController(IHomeRepository homeRepository, ICaseRepository caseRepository)
        {
            _homeRepository = homeRepository;
            _caseRepository = caseRepository;
        }
        //CaseStudies Page View Model//
        public IActionResult CaseStudies()
        {
            var model = new HomeViewModel
            {
                Categories = _homeRepository.GetCategories(),
                Settings = _homeRepository.GetSettings(),
                Agents = _homeRepository.GetAgents(),
            };
            return View(model);
        }

        //CaseSingle Page View Model//
        public IActionResult CaseSingle()
        {
            var model = new HomeViewModel
            {
                Categories = _homeRepository.GetCategories(),
                Settings = _homeRepository.GetSettings(),
                CaseStudies = _caseRepository.GetStudiesTrue(),
                Solutions = _caseRepository.GetSolutions(3)



            };
            ViewBag.Agents = _homeRepository.GetCaseAgent();
            return View(model);
        }
    }
}