using Aspose.Cells;
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Base @base = new Base();
        @base.Open_Base("Base.xlsx");
        @base.Open_Shop("Shop1.xls");
        @base.MakeOrder();
    }
}