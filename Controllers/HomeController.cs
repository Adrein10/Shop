using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Models;
using System.Diagnostics;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShopsContext context;

        public HomeController(ILogger<HomeController> logger,ShopsContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("session") != null)
            {
                ViewBag.session = HttpContext.Session.GetString("session").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }
            CustomShop data = new CustomShop()
            {
                Cproduct = new Product(),
                categorylist = context.Categories.ToList()
            };
            return View(data);
        }
        [HttpPost]
        public IActionResult Index(CustomShop shop)
        {
            Product product = new Product()
            {
                ProductName = shop.Cproduct.ProductName,
                PcategoryId = shop.Cproduct.PcategoryId,
                ProductQuantity = shop.Cproduct.ProductQuantity  
            };
            context.Products.Add(product);
            context.SaveChanges();
            return RedirectToAction("Productlist");
        }
        public IActionResult Productlist()
        {
            if (HttpContext.Session.GetString("session") != null)
            {
                ViewBag.session = HttpContext.Session.GetString("session").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }
            var show = context.Products.Include(options => options.Pcategory).ToList();
            return View(show);
        }
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("session") != null)
            {
                ViewBag.session = HttpContext.Session.GetString("session").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }
            CustomShop shop = new CustomShop()
            {
                Cproduct = context.Products.Find(id),
                categorylist = context.Categories.ToList()
            };
            return View(shop);
        }
        [HttpPost]
        public IActionResult Delete(CustomShop shop ,int id)
        {

           var pro = context.Products.Find(id);
            context.Products.Remove(pro);
            context.SaveChanges();
            return RedirectToAction("productlist");
        }
        public IActionResult Privacy()
        {
            if (HttpContext.Session.GetString("session") != null)
            {
                ViewBag.session = HttpContext.Session.GetString("session").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public IActionResult Category()
        {
            if (HttpContext.Session.GetString("session") != null)
            {
                ViewBag.session = HttpContext.Session.GetString("session").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Category(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
            return RedirectToAction("Categorylist");
        }
        public IActionResult Categorylist()
        {
            if (HttpContext.Session.GetString("session") != null)
            {
                ViewBag.session = HttpContext.Session.GetString("session").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }
            var show = context.Categories.ToList();
            return View(show);
        }
        public IActionResult Delete2(int id)
        {
            if (HttpContext.Session.GetString("session") != null)
            {
                ViewBag.session = HttpContext.Session.GetString("session").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }
            var show = context.Categories.Find(id);
            return View(show);
        }
        [HttpPost]
        public IActionResult Delete2(Category category,int id)
        {
            context.Categories.Remove(category);
            context.SaveChanges();
            return RedirectToAction("Categorylist");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            var log = context.Users.Where(option => option.UserEmail == user.UserEmail && option.UserPassword == user.UserPassword).FirstOrDefault();
            if(log != null)
            {
                HttpContext.Session.SetString("session", log.UserName);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.failedlogin = "Login Failed";
            }
            return View();
        }
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("session") != null)
            {
                HttpContext.Session.Clear();
                HttpContext.Session.Remove("session");
                return RedirectToAction("Login");
            }
            
            return View();
        }
        public IActionResult signup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult signup(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            return RedirectToAction("Login");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}