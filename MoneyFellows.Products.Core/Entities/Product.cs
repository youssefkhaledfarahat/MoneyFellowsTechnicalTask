﻿namespace MoneyFellows.Products.Core.Entities
{
    public class Product : BaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }
    }
}
