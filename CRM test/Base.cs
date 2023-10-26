using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Aspose.Cells;

public class Base
{
    private Dictionary<string, Vendor> _vendors = new Dictionary<string, Vendor>();
    private Dictionary<string, Product> _productsInShop = new Dictionary<string, Product>();

    public void Open_Base(string Path)
    {
        Workbook _wb = new Workbook(Path);
        Worksheet ws = _wb.Worksheets[0];
        int rows = ws.Cells.MaxDataRow + 1;
        for (int i = 0; i < rows; i++)
        {
            if (!_vendors.ContainsKey(ws.Cells[i, 4].Value.ToString()))
            {
                Vendor _new = new Vendor(ws.Cells[i, 4].Value.ToString());
                _vendors.Add(ws.Cells[i, 4].Value.ToString(), _new);               
            }
            if (!_vendors[ws.Cells[i, 4].Value.ToString()].IsContains(ws.Cells[i, 4].Value.ToString()))
            {
                Product _new = new Product(ws.Cells[i, 0].Value.ToString(), Int32.Parse(ws.Cells[i, 1].Value.ToString()), Int32.Parse(ws.Cells[i, 3].Value.ToString()));
                _vendors[ws.Cells[i, 4].Value.ToString()].Add(ws.Cells[i, 0].Value.ToString(), _new);
            }
        }
    }

    public void Open_Shop(string Path) 
    {
        Workbook _wb = new Workbook(Path);
        Worksheet ws = _wb.Worksheets[0];
        int rows = ws.Cells.MaxDataRow + 1;
        for (int i = 1; i < rows; i++)
        {
            if (!_productsInShop.ContainsKey(ws.Cells[i, 6].Value.ToString()))
            {
                Product _new = new Product(ws.Cells[i, 6].Value.ToString(), Int32.Parse(ws.Cells[i, 29].Value.ToString()));
                _productsInShop.Add(ws.Cells[i, 6].Value.ToString(), _new);
            }
        }
    }

    public void read()
    { 
       
    }

    public String[] GetVendors()
    {
        string[] names = new string[_vendors.Count()];
        int i = 0;
        foreach (var vendor in _vendors)
        {
            names[i] = vendor.Value.GetName();
            ++i;
        }
        return names;
    }

    public Vendor GetVendor(string vendorName)
    {
        return _vendors[vendorName];
    }
    private Product GetProduct(string vendor, string key)
    {
        return _vendors[vendor].GetProduct(key);
    }

    public void print(string vendor)
    {
        _vendors[vendor].print();
    }

   /* public async void compare(Base base_shop)
    {
        Dictionary<string, Product> res = new Dictionary<string, Product>();
        foreach(var item in _shop) 
        {
            if (item.Value.getAmount()/2 > base_shop.GetProduct(item.Value.getName()).getAmount())
            {
                Product _new = new Product(NameFix(item.Value.getName()), item.Value.getAmount() - base_shop.GetProduct(item.Value.getName()).getAmount(), item.Value.getPack());
                res.Add(NameFix(item.Value.getName()), _new);
            }
        }
        foreach (var item in res)
        {
            item.Value.Ceiling();
            using (StreamWriter writer = new StreamWriter("Result.txt", true))
            {
                await writer.WriteAsync(item.Value.printIn() + "\n");
            }
        }
    }
    */
    private void compare(Vendor vendor)
    {
        Vendor res = _vendors[vendor.GetName()].Compare(vendor);
        res.PrintOut();
    }

    public void MakeOrder()
    {
        foreach (var vendor in _vendors)
        {
            Vendor res = new Vendor(vendor.Key);
            foreach (var product in _productsInShop)
            {
                if (vendor.Value.IsContains(product.Key))
                {
                    res.Add(product.Key, product.Value);
                }
            }
            compare(res);
        }
    }
}