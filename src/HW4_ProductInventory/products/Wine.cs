using System;
using System.Collections.Generic;
using System.Text;

namespace HW4_ProductInventory.products
{
    public class Wine : Alcohol
    {
        public string _sort;
        public int _sortId;

        public Wine()
        {
        }

        public Wine (int qty)
        {
            _id = 2;
            _price = 11.50;
            _qty = qty;
        }

        public Wine (int qty, string sort)
        {
            _id = 2;

            if (sort == "white")
            {
                _sortId = 1;
            }
            else if (sort == "red")
            {
                _sortId = 2;
            }
            else if (sort == "rose")
            {
                _sortId = 3;
            }

            _price = 11.50;
            _qty = qty;
            _sort = sort;
        }

        public enum MenuExtraWine
        {
            WhiteWine = 1,
            RedWine,
            RoseWine,
            Skip
        }
    }
}
