using System;
using System.Collections.Generic;
using System.Text;

namespace HW4_ProductInventory.products
{
    public class Gin : Alcohol
    {
        public Gin (int qty)
        {
            _id = 4;
            _price = 28.00;
            _qty = qty;
        }
    }
}
