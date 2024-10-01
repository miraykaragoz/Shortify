using Microsoft.AspNetCore.Mvc;
using UrlShortener.Data;
using UrlShortener.Models;

namespace UrlShortener.Controllers;

public class UrlController : Controller
{
    private readonly AppDbContext _context;

    public UrlController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Urlshortener()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Urlshortener(string originalUrl)
    {
        string shortUrl = GenerateRandomString(6);
        
        string fullShortUrl = $"https://shortify.miraykaragoz.com.tr/{shortUrl}";
    
        var url = new Url()
        {
            OriginalUrl = originalUrl,
            ShortUrl = shortUrl
        };
    
        _context.Urls.Add(url);
        _context.SaveChanges();
    
        ViewBag.FullShortUrl = fullShortUrl; 
        ViewBag.ShortUrl = shortUrl;
    
        return View("NewUrl");
    }

    public IActionResult RedirectToOriginal(string shortUrl)
    {
        var url = _context.Urls.FirstOrDefault(u => u.ShortUrl == shortUrl);

        if(url == null)
        {
            return BadRequest();
        }

        return Redirect(url.OriginalUrl);
    } 
    
    private string GenerateRandomString(int length)
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ_abcdefghijklmnopqrstuvwxyz-0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }
}