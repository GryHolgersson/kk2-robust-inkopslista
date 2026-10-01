// One item on the shopping list.

//Skapar klassen "Item" och medelar vad som ingår, Namn samt Price. 
// get och set hämtar samt sparar värdet. 
// public innebär att andra klasser får använda det. 
// String samt int är värden 
class Item
{
    public string Name { get; set; }
    public int Price { get; set; }

// Konstruktorn körs en gång, när ett nytt Item skapas med new Item.
// Den tar emot ett namn och ett pris i parametrarna name och price.
// Sedan kopierar den värdena till propertyerna Name och Price,
// så att objektet kommer ihåg dem.

    public Item(string name, int price)
    {
        Name = name;
        Price = price;
    }
// public = får användas av andra. 
// override = ersätter en metod som klassen ärvt
// det som skapas och ges tillbaka till användaren är text (string)
// ToString visar vad koden anropar  
// return skickar tillbaka resultatet till koden som anropade metoden
// { } - tom låda som fylls i av datorn utefter dina tidigare inmatningar 
// $ gör att det inte bara skrivs ut som text utan att hålen fylls i
// " " meddelar string 
// resterande utanför { } skrivs ut i texten 
    public override string ToString()
    {
        return $"{Name} - {Price} kr";
    }
}
