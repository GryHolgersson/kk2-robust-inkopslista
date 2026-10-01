// One item on the shopping list.

//Skapar klassen "Item" och medelar vad som ingår, Namn samt Price. 
// get och set hämtar samt sparar det som användaren matar in.
// public innebär att andra klasser får använda det. 
// String samt int är värden 
class Item
{
    public string Name { get; set; }
    public int Price { get; set; }

// andra klasser får använda klassen "Item" och meddela värdet string och int, Name samt Price. 
// det skapas namn och prices när användaren matar in. Detta är alltså inte ett satt värde. 

    public Item(string name, int price)
    {
        Name = name;
        Price = price;
    }
// public = får användas av andra. override = metdoden kan ersättas
// det som skapas och ges tillbaka till användaren är text (string)
// ToString visar när någon vill se vad som angivits i text. 
// return returnerar vad användaren ber om
// { } - tom låda som fylls i av datorn utefter dina tidigare inmatningar 
// $ gör att det inte bara skrivs ut som text utan att hålen fylls i
// " " meddelar string 
// resterande utanför { } skrivs ut i texten 
    public override string ToString()
    {
        return $"{Name} - {Price} kr";
    }
}
