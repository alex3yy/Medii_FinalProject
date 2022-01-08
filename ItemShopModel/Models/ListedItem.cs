using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemShopModel.Models
{
    public class ListedItem
    {
        public int SellerID { get; set; }
        public int ItemID { get; set; }
        public Seller Seller { get; set; }
        public Item Item { get; set; }
    }
}
