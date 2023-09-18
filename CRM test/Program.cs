using Aspose.Cells;
using System;
using System.Collections.Generic;

class Program
{
 /*   public static void generate(Dictionary<string, Product> shop)
    {
        int qt = Int32.Parse(Console.ReadLine());
        for (int i = 0; i < qt; i++)
        {
            Product _new = new Product(Console.ReadLine(), Int32.Parse(Console.ReadLine()));
            shop.Add(_new.getName(), _new);
        }
    }*/
    static void Main(string[] args)
    {
        Base base_shop = new Base("Shop1.xls");

        Base @base = new Base("Base.xls");

        @base.compare(base_shop);
    }
}