﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AspFinal.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult BlogGrid()
        {
            return View();
        }
        public IActionResult BlogSingle()
        {
            return View();
        }
    }
}