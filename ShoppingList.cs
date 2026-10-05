// Holds the items and takes care of loading and saving them.

//Skapar klassen "ShoppingList" och meddelar att den håller en lista. 
// path är en låda som kommer ihåg sökvägen
// private innebär att bara klassen kommer åt den och de andra klasserna kan inte nå den.
// String är ett värde som betyder text, path är en text
//Item är den listan som innehåller alla Item objekten 
class ShoppingList
{
    private List<Item> items = new List<Item>();
    private string path;

    public ShoppingList(string path)
    {
        this.path = path;
    }
// void innebär att metoden inte returnerar något 
// Item och item fungerar som en brevlåda och tar emot ett Item
// Listans add lägger item sist i listan
    public void Add(Item item)
    {
        items.Add(item);
    }

    // Användaren ser varorna i ordning 1, 2, 3....
    // Om du tar bort varor så behålls ändå en ordning men den rättar sig efter förändringen.
    // Listan räknar från 0 men användaren från 1, därför tas number - 1 bort.
    // Bool istället för void då void inte returnerar något. Bool ger ´ja eller nej´,
    // true om varan togs bort och false om numret inte finns.
    public bool RemoveAt(int number)
    {
        // Numret måste finnas i listan, annars kraschar items.RemoveAt
        if (number < 1 || number > items.Count)
        {
            return false;
        }
        
        items.RemoveAt(number - 1);
        return true; 
    }

    // Adderar summorna av alla varor från listan
    // Det börjar på 0 och går sedan igenom varje index (i)
    // varans pris läggs in 
    // ger tillbaka summan till koden som anropade den
    public int Total()
    {
        int sum = 0;

        for (int i = 0; i < items.Count; i++) //Loopen börjar på 0 så att första varan kommer med.
        {
            sum += items[i].Price;
        }

        return sum;
    }

    // Letar fram en vara i listan, finns den inte blir de null
    public Item Find(string name)
    {
        // Går igenom en i taget
        foreach (Item item in items)
        {
            // jämför alla namn
            if (item.Name == name)
            {
                return item;
            }
        }

        return null;
    }

    public void Print()
    {
        //Samma här, går igenom listan med index i, börjar på 0
        for (int i = 0; i < items.Count; i++)
        {
            // denna kod gör så att användaren kan se listan 1, 2, 3 osv istället för 0, 1, 2...
            // efter index skrivs item ut med hjälp av ToString som finns i Item.cs
            Console.WriteLine($"{i + 1}. {items[i]}");
        }
// skriver ut summan av alla priser
        Console.WriteLine($"Totalt: {Total()} kr");
    }

    // Skriver ut ett item per rad i listan
    // add lägger alltid in i slutet av listan

    public void Save()
    {
        List<string> lines = new List<string>();
        //priset kommer först och namnet sist
        foreach (Item item in items)
        {
            
            lines.Add($"{item.Price};{item.Name}");
        }

        try
        {
            // Join sätter ihop raderna med radbrytning och ytterligare en radbrytning
            // läggs sist. Filen skrivs över varje gång
            File.WriteAllText(path, string.Join("\r\n", lines) + "\r\n");
            Console.WriteLine("Listan är sparad.");
        }
        // Fångar fel om filen är låst av ett annat program eller disken är full.
        catch (IOException ex)
        {
            Console.WriteLine($"Listan kunde inte sparas: {ex.Message}");
        }
        // Fångar fel om programmet saknar rättighet att skriva till filen.
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"Listan kunde inte sparas, saknar behörighet: {ex.Message}");
        }
    }

    // läser in varorna från filen och lägger dem i listan när programmet startar
    public void Load()
    {
        // Om filen inte finns (t.ex. första gången programmet körs) avslutas Load
        // och listan börjar tom i stället för att krascha.
        if (!File.Exists(path))
        {
            return;
        }
        // läser filen och delar upp den i rader direkt, klarar både \r\n och \n
        string[] lines = File.ReadAllLines(path);
        // går igenom raderna en i taget
        foreach (string line in lines)
        {
            //Koden antar att parts [0] är priset och parts[1] är namnet
            string[] parts = line.Split(';');
            // gör om [0] till ett heltal sedan skapar ett item och namnet samt priset 
            // och lägger in det i listan.
            items.Add(new Item(parts[1], int.Parse(parts[0])));
        }
    }
}
