using System;
using System.Collections.Generic;
using System.Text;

namespace HW4_ProductInventory.products
{
    public class Whisky : Alcohol
    {
        public Whisky (int qty)
        {
            _id = 6;
            _price = 33.33;
            _qty = qty;
        }
    }
}
