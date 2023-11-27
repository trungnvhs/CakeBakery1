using System;
using System.Collections.Generic;

namespace Cake.Models
{
    public partial class Blog
    {
        public Blog()
        {
            Comments = new HashSet<Comment>();
        }

        public int BlogId { get; set; }
        public string? BlogTitle { get; set; }
        public string? Description { get; set; }
        public int? CreateBy { get; set; }
		public string? image { get; set; }
		public DateTime CreateDate { get; set; }

        public virtual User? CreateByNavigation { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
