using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemShopModel.Models;

namespace Zaharia_Alexandru_Lab2.Models.ItemShopViewModels
{
    public class SellerIndexData {
        public IEnumerable<Seller> Sellers { get; set; }
        public IEnumerable<Item> Items { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}