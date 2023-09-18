using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Aspose.Cells;

public class Base
{
    private Workbook _wb;
    private Dictionary<string, Product> _shop = new Dictionary<string, Product>();
    public Base(string file)
    {
        _wb = new Workbook(file);
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

    public async void compare(Base @base)
    {
        Dictionary<string, Product> res = new Dictionary<string, Product>();
        foreach(var item in _shop) 
        {
            if(@base.GetProduct(item.Value.getName()).getAmount()/2 > item.Value.getAmount());
            {
                Product _new = new Product(item.Value.getName(), @base.GetProduct(item.Value.getName()).getAmount() - item.Value.getAmount());
            }
        }

        foreach (var item in res)
        {
            using (StreamWriter writer = new StreamWriter("Result.txt", true))
            {
                await writer.WriteAsync(item.Value.printIn());
            }
        }
    }
}