﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TestingAkbas.Controllers
{
    public class PriceListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}