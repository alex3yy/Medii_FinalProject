using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemShopModel.Models
{
    public class Order
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public int ItemID { get; set; }

        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }
    }
}
