using System;
using System.Collections.Generic;
using System.Text;

namespace HW4_ProductInventory.products
{
    public class Cognac : Alcohol
    {
        public Cognac (int qty)
        {
            _id = 5;
            _price = 43.50;
            _qty = qty;
        }
    }
}
