using System;
using System.Collections.Generic;
using System.Text;

namespace HW4_ProductInventory.products
{
    public class Beer : Alcohol
    {
        public int _sort;
        public string _sortType;
        public bool _isHasExtraSorts;

        public Beer()
        {
        }

        public Beer (int qty)
        {
            _id = 1;
            _price = 3.00;
            _qty = qty;
        }

        public Beer (int qty, int sort)
        {
            _id = 1;

            if (sort == (int)MenuExtraBeer.LightBeer)
            {
                _sortType = "light";
            }
            else if (sort == (int)MenuExtraBeer.DarkBeer)
            {
                _sortType = "dark";
            }
            else if (sort == (int)MenuExtraBeer.WheatBeer)
            {
                _sortType = "wheat";
            }

            _price = 3.00;
            _qty = qty;
            _sort = sort;
        }

        public enum MenuExtraBeer
        {
            LightBeer = 1,
            DarkBeer,
            WheatBeer,
            Skip
        }
    }
}
