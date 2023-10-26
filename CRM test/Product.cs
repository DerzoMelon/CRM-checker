using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Text;
public class Product
{
    private string _name;
    private int _amount;
    private int _inPack;

    public Product(string name, int amount)
    {
        _name = name;
        _amount = amount;
    }

    public Product(string name, int amount, int inPack) 
    {
        _name = name;
        _amount = amount;
        _inPack = inPack;
    }

    public void Ceiling()
    {
        _amount = (_amount + _inPack) / _inPack;
    }
    public void setName(string name) { this._name = name; }

    public string getName() { return this._name; }

    public int getAmount() { return this._amount; }

    public void setAmount(int amount) { this._amount = amount; }

    public int getPack() {  return this._inPack; }

    public void print()
    {
        Console.WriteLine(_name + ": " + this._amount);
    }

    public string printIn()
    {
            return _name + ": " + this._amount + ' ' + (this._amount > 1 ? "блока" : "блок");
    }
}