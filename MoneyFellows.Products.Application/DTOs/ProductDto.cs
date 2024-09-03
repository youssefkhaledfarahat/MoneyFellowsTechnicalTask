﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyFellows.Products.Application.DTOs
{
    public record ProductDto(int Id, string Name, string Description, decimal Price, int StockQuantity);
}
