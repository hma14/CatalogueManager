using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogueManager.Controllers
{
    public class Maths
    {
        private static int CalcPower(int Base, int Exponent)
        {
            int Product = 1;
            for (int i = 1; i <= Exponent; i++)
            {
                Product = Product * Base;
            }
            return Product;
        }
    }
}