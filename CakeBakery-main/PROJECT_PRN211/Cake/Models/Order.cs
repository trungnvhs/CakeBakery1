using Cake.Models;
using System;
using System.Collections.Generic;

namespace Cake.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string? Note { get; set; }

        public virtual User? Customer { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}

