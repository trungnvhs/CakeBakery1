using System;
using System.Collections.Generic;

namespace Cake.Models
{
    public partial class Product
    {
        public Product()
        {
            Feedbacks = new HashSet<Feedback>();
            OrderItems = new HashSet<OrderItem>();
        }

        public int ProductId { get; set; }
        public string? Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int? UnitInStock { get; set; }
        public string? Image { get; set; }
        public int? CategoryId { get; set; }
        public bool? Discontinued { get; set; }
        public string? Describe { get; set; }
        public double? Discount { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
