using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ItemShopModel.Data;
using ItemShopModel.Models;
using Zaharia_Alexandru_Lab2.Models.ItemShopViewModels;

namespace Zaharia_Alexandru_Lab2.Controllers {

    [Authorize(Policy = "OnlySales")]
    public class SellersController : Controller {
        private readonly ItemShopContext _context;

        public SellersController(ItemShopContext context)
        {
            _context = context;
        }

        // GET: Sellers
        public async Task<IActionResult> Index(int? id, int? itemID)
        {
            var viewModel = new SellerIndexData();

            viewModel.Sellers = await _context.Sellers
                .Include(i => i.ListedItems)
                .ThenInclude(i => i.Item)
                .ThenInclude(i => i.Orders)
                .ThenInclude(i => i.Customer)
                .AsNoTracking()
                .OrderBy(i => i.Name)
                .ToListAsync();

            if (id != null)
            {
                ViewData["SellerID"] = id.Value;
                Seller seller = viewModel.Sellers.Where(
                i => i.ID == id.Value).Single();
                viewModel.Items = seller.ListedItems.Select(s => s.Item);
            }

            if (itemID != null)
            {
                ViewData["ItemID"] = itemID.Value;
                viewModel.Orders = viewModel.Items.Where(x => x.ID == itemID).Single().Orders;
            }

            return View(viewModel);
        }

        // GET: Sellers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // GET: Sellers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sellers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Address,PhoneNumber,EmailAddress")] Seller seller)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(seller);
        }

        // GET: Seller/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers
                .Include(i => i.ListedItems).ThenInclude(i => i.Item)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (seller == null)
            {
                return NotFound();
            }

            PopulateListedItemData(seller);

            return View(seller);
        }

        private void PopulateListedItemData(Seller seller)
        {
            var allItems = _context.Items;
            var sellerItems = new HashSet<int>(seller.ListedItems.Select(c => c.ItemID));
            var viewModel = new List<ListedItemData>();

            foreach (var item in allItems)
            {
                viewModel.Add(new ListedItemData
                {
                    ItemID = item.ID,
                    Title = item.Title,
                    IsListed = sellerItems.Contains(item.ID)
                });
            }
            ViewData["Items"] = viewModel;
        }

        // POST: Sellers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedItems)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sellerToUpdate = await _context.Sellers
                .Include(i => i.ListedItems)
                .ThenInclude(i => i.Item)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (await TryUpdateModelAsync<Seller>(sellerToUpdate,"",i => i.Name, i => i.Address, i => i.PhoneNumber, i => i.EmailAddress))
            {
                UpdateListedItems(selectedItems, sellerToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists.");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateListedItems(selectedItems, sellerToUpdate);
            PopulateListedItemData(sellerToUpdate);

            return View(sellerToUpdate);
        }

        private void UpdateListedItems(string[] selectedItems, Seller sellerToUpdate)
        {
            if (selectedItems == null)
            {
                sellerToUpdate.ListedItems = new List<ListedItem>();
                return;
            }

            var selectedItemsHS = new HashSet<string>(selectedItems);
            var listedItems = new HashSet<int>
            (sellerToUpdate.ListedItems.Select(c => c.Item.ID));

            foreach (var item in _context.Items)
            {
                if (selectedItemsHS.Contains(item.ID.ToString()))
                {
                    if (!listedItems.Contains(item.ID))
                    {
                        sellerToUpdate.ListedItems.Add(new ListedItem
                        {
                            SellerID = sellerToUpdate.ID,
                            ItemID = item.ID
                        });
                    }
                }
                else
                {
                    if (listedItems.Contains(item.ID))
                    {
                        ListedItem itemToRemove = sellerToUpdate.ListedItems.FirstOrDefault(i
                       => i.ItemID == item.ID);
                        _context.Remove(itemToRemove);
                    }
                }
            }
        }

        // GET: Sellers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // POST: Sellers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publisher = await _context.Sellers.FindAsync(id);
            _context.Sellers.Remove(publisher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SellerExists(int id)
        {
            return _context.Sellers.Any(e => e.ID == id);
        }
    }
}