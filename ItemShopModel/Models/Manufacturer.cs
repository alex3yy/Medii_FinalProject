using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemShopModel.Models
{
    public class Manufacturer
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Manufacturer Name")]
        [StringLength(50)]
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
