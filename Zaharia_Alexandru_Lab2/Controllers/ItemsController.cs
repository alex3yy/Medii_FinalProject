using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ItemShopModel.Data;
using ItemShopModel.Models;
using Microsoft.AspNetCore.Authorization;
using Zaharia_Alexandru_Lab2.Models.ItemShopViewModels;

namespace Zaharia_Alexandru_Lab2.Controllers {

    [Authorize(Roles = "Employee")]
    public class ItemsController : Controller {
        private readonly ItemShopContext _context;

        public ItemsController(ItemShopContext context)
        {
            _context = context;
        }

        // GET: Items
        [AllowAnonymous]
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var allSellers = _context.Sellers
                .Include(i => i.ListedItems).ThenInclude(i => i.Item);
            var allItems = new List<ItemData>();

            foreach (var seller in allSellers)
            {
                var sellerItemsID = new HashSet<int>(seller.ListedItems.Select(c => c.ItemID));

                foreach (var sellerItemID in sellerItemsID)
                {
                    var item = await _context.Items
                    .Include(i => i.Manufacturer)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.ID == sellerItemID);


                    allItems.Add(new ItemData
                    {
                        ID = item.ID,
                        Title = item.Title,
                        Description = item.Description,
                        SellerName = seller.Name,
                        ManufacturerName = item.Manufacturer.Name,
                        Price = item.Price
                    });
                }
            }
            ViewData["Items"] = allItems;

            var queryableItems = allItems.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                queryableItems = queryableItems.Where(s => s.Title.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    queryableItems = queryableItems.OrderByDescending(b => b.Title);
                    break;

                case "Price":
                    queryableItems = queryableItems.OrderBy(b => b.Price);
                    break;

                case "price_desc":
                    queryableItems = queryableItems.OrderByDescending(b => b.Price);
                    break;

                default:
                    queryableItems = queryableItems.OrderBy(b => b.Title);
                    break;
            }
            
            int pageSize = 3;
            return View(PaginatedList<ItemData>.Create(queryableItems, pageNumber ?? 1, pageSize));
        }

        // GET: Items/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(c => c.Manufacturer)
                .Include(s => s.Orders)
                .ThenInclude(e => e.Customer)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Description,ManufacturerID,Price")] Item item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newItem = new Item
                    {
                        ID = item.ID,
                        Title = item.Title,
                        Description = item.Description,
                        ManufacturerID = item.ManufacturerID,
                        Price = item.Price,
                        Manufacturer = _context.Manufacturers.Single(s => s.ID == item.ManufacturerID)
                    };
                    _context.Add(newItem);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex*/)
            {
                ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists.");
            }
            return View(item);
        }

        // GET: Item/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var itemToUpdate = await _context.Items.FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Item>(itemToUpdate, "", s => s.ManufacturerID, s => s.Description, s => s.Title, s => s.Price))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists.");
                }
            }
            return View(itemToUpdate);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(c => c.Manufacturer)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (item == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Delete failed. Try again";
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ID == id);
        }
    }
}