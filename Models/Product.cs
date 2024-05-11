using System;
using System.Collections.Generic;

namespace Shop.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int PcategoryId { get; set; }
        public int ProductQuantity { get; set; }

        public virtual Category Pcategory { get; set; } = null!;
    }
}
