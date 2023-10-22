using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Aspose.Cells;

public class Base
{
    private Dictionary<string, Product> _shop = new Dictionary<string, Product>();

    private string NameFix(string name)
    {
        Regex[] regex = new Regex[5];
        regex[0] = new Regex(@"\[(\S*)\]");
        regex[1] = new Regex(@"\((\S*)\)");
        regex[2] = new Regex(@"Сигареты с фильтром\s*");
        regex[3] = new Regex(@"Сиг\.\s*");
        regex[4] = new Regex(@"");
        foreach (var reg in regex)
        {
            name = reg.Replace(name, "");
        }
        return name;
    }

    public void Open_Base(string Path)
    {
        Workbook _wb = new Workbook(Path);
        Worksheet ws = _wb.Worksheets[0];
        int rows = ws.Cells.MaxDataRow + 1;
        for (int i = 0; i < rows; i++)
        {
            if (!_shop.ContainsKey(ws.Cells[i, 0].Value.ToString()))
            {
                Product _new = new Product(ws.Cells[i, 0].Value.ToString(), Int32.Parse(ws.Cells[i, 1].Value.ToString()), Int32.Parse(ws.Cells[i, 3].Value.ToString()));
                _shop.Add(ws.Cells[i, 0].Value.ToString(), _new);
            }
            else
            {
                // Product _new = new Product(ws.Cells[i, 6].Value.ToString() + "${i}", Int32.Parse(ws.Cells[i, 29].Value.ToString()));
                //  _shop.Add(ws.Cells[i, 6].Value.ToString(), _new);
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
            if (!_shop.ContainsKey(ws.Cells[i, 6].Value.ToString()))
            {
                Product _new = new Product(ws.Cells[i, 6].Value.ToString(), Int32.Parse(ws.Cells[i, 29].Value.ToString()));
                _shop.Add(ws.Cells[i, 6].Value.ToString(), _new);
            }
            else
            {
                // Product _new = new Product(ws.Cells[i, 6].Value.ToString() + "${i}", Int32.Parse(ws.Cells[i, 29].Value.ToString()));
                //  _shop.Add(ws.Cells[i, 6].Value.ToString(), _new);
            }
        }
    }

    public void read()
    { 
       
    }
    private Product GetProduct(string key)
    {
        return _shop[key];
    }
    public void print() 
    {
        foreach (var item in _shop)
        {
            item.Value.print();
        }
    }

    public async void compare(Base base_shop)
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
}