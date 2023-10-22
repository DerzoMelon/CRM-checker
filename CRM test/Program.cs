using Aspose.Cells;
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Base base_shop = new Base();
        base_shop.Open_Shop("Shop1.xls");

        Base @base = new Base();
        @base.Open_Base("Base.xlsx");

        @base.compare(base_shop);
    }
}