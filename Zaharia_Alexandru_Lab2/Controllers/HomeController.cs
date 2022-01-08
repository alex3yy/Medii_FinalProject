using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ItemShopModel.Data;
using ItemShopModel.Models;
using Zaharia_Alexandru_Lab2.Models;

namespace Zaharia_Alexandru_Lab2.Controllers {

    public class HomeController : Controller {
        private readonly ItemShopContext _context;

        public HomeController(ItemShopContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<ActionResult> Statistics()
        {
            IQueryable<Order> data =
            from order in _context.Orders
            select new Order()
            {
                OrderDate = order.OrderDate,
                Quantity = order.Quantity
            };
            return View(await data.AsNoTracking().ToListAsync());
        }

        public IActionResult Chat()
        {
            return View();
        }
    }
}