using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Text;
class Product
{
    private string _name;
    private int _amount;

    public Product(string name, int amount)
    {
        _name = name;
        _amount = amount;
    }
    public void setName(string name) { this._name = name; }
    public string getName() { return this._name; }
    public int getAmount() { return this._amount; }
    public void setAmount(int amount) { this._amount = amount; }
    public void print()
    {
        Console.WriteLine(_name + ": " + this._amount);
    }
    public string printIn()
    {
        return _name + ": " + this._amount;
    }
}