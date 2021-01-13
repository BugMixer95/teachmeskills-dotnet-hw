using System;
using System.Collections.Generic;
using System.Text;

namespace HW4_ProductInventory.products
{
    public class Vodka : Alcohol
    {
        public Vodka (int qty)
        {
            _id = 3;
            _price = 8.30;
            _qty = qty;
        }
    }
}
