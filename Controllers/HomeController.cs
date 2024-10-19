using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Data;
using UrlShortener.Models;

namespace UrlShortener.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}