﻿using Microsoft.AspNetCore.Mvc;
using ProjectManagementTool.Models;
using System.Diagnostics;

namespace ProjectManagementTool.Controllers
{
    public class HomeController : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult ErrorPage()
        {
            return View();
        }
    }
}