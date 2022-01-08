using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemShopModel.Models
{
    public class Item
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ManufacturerID { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }

        public Manufacturer Manufacturer { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<ListedItem> ListedItems { get; set; }
    }
}
