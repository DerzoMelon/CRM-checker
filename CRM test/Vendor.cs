using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

public class Vendor
{
    private Dictionary<string, Product> _products = new Dictionary<string, Product>();
    private string _name;

    public Vendor(string name) 
    {
        _name = name;
    }

    private string NameFix(string name)
    {
        return Regex.Replace(name, @"\[(\S*)\]\s*|\([^)]+\)\s*|Сигареты с фильтром\s*|Cигареты с фильтром\s*|Сиг\.\s*|\d+,\d+\s*руб\.+\s*|Кретек\s*|\d+,\d+\s*руб\.+\s*|(\.*\,*\s*MT)\s*|""|Сиг\S*\s*", "");
    }

    public int GetLenght() { return _products.Count; }

    public void print()
    {
        foreach (var item in _products)
        {
            item.Value.print();
        }
    }

    public void Add(string name, Product product)
    {
        _products.Add(name, product);
    }

    public Product GetProduct(string key)
    {
        return _products[key];
    }

    public bool IsContains(string name)
    {
        return _products.ContainsKey(name);
    }

    public string GetName() { return _name; }
    public Vendor Compare(Vendor vendor)
    {
        Vendor res = new Vendor(vendor.GetName());
        foreach (var item in _products)
        {
            if (item.Value.getAmount() / 2 > vendor.GetProduct(item.Value.getName()).getAmount())
            {
                Product _new = new Product(NameFix(item.Value.getName()), item.Value.getAmount() - vendor.GetProduct(item.Value.getName()).getAmount(), item.Value.getPack());
                _new.Ceiling();
                res.Add(NameFix(item.Value.getName()), _new);
            }
        }
        return res;
    }
    public async void PrintOut()
    {
        foreach (var item in _products)
        {
            using (StreamWriter writer = new StreamWriter(_name + ".txt", true))
            {
                await writer.WriteAsync(item.Value.printIn() + "\n");
            }
        }
    }
}