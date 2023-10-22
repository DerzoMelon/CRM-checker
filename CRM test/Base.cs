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

    public void Open_Base(string Path)
    {
        _wb = new Workbook(Path);
        Worksheet ws = _wb.Worksheets[0];
        int rows = ws.Cells.MaxDataRow + 1;
        for (int i = 0; i < rows; i++)
        {
            if (!_shop.ContainsKey(ws.Cells[i, 0].Value.ToString()))
            {
                Product _new = new Product(ws.Cells[i, 0].Value.ToString(), Int32.Parse(ws.Cells[i, 1].Value.ToString()));
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
        _wb = new Workbook(Path);
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
                Product _new = new Product(item.Value.getName(), item.Value.getAmount() - base_shop.GetProduct(item.Value.getName()).getAmount());
                res.Add(item.Value.getName(), _new);
            }
        }

        foreach (var item in res)
        {
            using (StreamWriter writer = new StreamWriter("Result.txt", true))
            {
                await writer.WriteAsync(item.Value.printIn() + "\n");
            }
        }
    }
}