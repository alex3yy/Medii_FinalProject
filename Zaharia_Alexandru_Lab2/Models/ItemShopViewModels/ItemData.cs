using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zaharia_Alexandru_Lab2.Models.ItemShopViewModels
{
    public class ItemData
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ManufacturerName { get; set; }
        public string SellerName { get; set; }
        public decimal Price { get; set; }
    }
}
