using System;
using System.Collections.Generic;

namespace Cake.Models
{
    public partial class Feedback
    {
        public int FeadbackId { get; set; }
        public string? Description { get; set; }
        public int? Owner { get; set; }
        public int? ProductId { get; set; }
        public bool? Like { get; set; }
        public byte? Rate { get; set; }

        public virtual User? OwnerNavigation { get; set; }
        public virtual Product? Product { get; set; }
    }
}
