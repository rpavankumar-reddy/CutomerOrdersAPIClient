using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAPIClient
{
    public class OrderItems
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public List<Items> ItemBasketList { get; set; }
    }
    public class Items
    {
        public string ItemName { get; set; }
        //public float ItemPrice { get; set; }
        public int ItemQuantity { get; set; }
    }
}
