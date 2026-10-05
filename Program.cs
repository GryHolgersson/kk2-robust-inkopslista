// Skapar en ny inköpslista som sparar och läser varorna från filen items.txt
ShoppingList list = new ShoppingList("items.txt");
// Läser in varorna från filen när programmet startar
list.Load();

// while (true) är en loop som körs om och om igen tills break körs (menyval 5)
while (true)
{
    // Skriver ut en tom rad, listan med alla varor och totalsumman, och sedan menyn
    Console.WriteLine();
    list.Print();
    Console.WriteLine();
    Console.WriteLine("1. Lägg till vara");
    Console.WriteLine("2. Ta bort vara");
    Console.WriteLine("3. Spara");
    Console.WriteLine("4. Sök vara");
    Console.WriteLine("5. Avsluta");
    // Write (inte WriteLine) gör att användaren skriver på samma rad som "Välj: "
    Console.Write("Välj: ");

//TryParse försöker göra om texten till ett tal och krashar inte om användaren
//skriver något annat. Istället returneras false
//Där visas då ett meddelande och continue tar tillbaka början av loopen så menyn visas på nytt.
    if (!int.TryParse(Console.ReadLine(), out int choice))
    {
        Console.WriteLine("Du måste skriva en siffra. ");
        continue;
    }

    // Lägg till vara
    if (choice == 1)
    {
        Console.Write("Namn: ");
        string name = Console.ReadLine();
        Console.Write("Pris: ");
        //Kontrollerar att priset är ett tal och om inte visas ett meddelande.
        //continue gör samma som tidigare, loopen går tillbaka till början så användaren ser menyn utan att någon vara läggs till
        if (!int.TryParse(Console.ReadLine(), out int price))
        {
            Console.WriteLine("Priset måste vara ett heltal");
            continue;
        }
        // Skapar ett nytt Item och lägger in det sist i listan.
        // Item kastar undantag om namnet är tomt eller priset negativt.
        // catch fångar felet så att programmet inte kraschar och användaren får veta vad som var fel.
        try
        {
            list.Add(new Item(name, price));
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Varan kunde inte läggas till: {ex.Message}");
        }
    }
    // Ta bort vara
    else if (choice == 2)
    {
        Console.Write("Nummer: ");
        // Kontrollerar att numret är ett tal, samma som för priset.
        // Om inte visas ett meddelande och continue går tillbaka till menyn.
        if (!int.TryParse(Console.ReadLine(), out int number))
        {
            Console.WriteLine("Numret måste vara ett heltal.");
            continue;
        }
        // Tar bort varan med det numret som användaren ser i listan (1, 2, 3...).
        // RemoveAt svarar false om numret inte finns i listan, då får användaren veta det.
        if (!list.RemoveAt(number))
        {
            Console.WriteLine("Det finns ingen vara med det numret.");
        }
    }
    // Spara listan till filen
    else if (choice == 3)
    {
        list.Save();
    }
    // Sök efter en vara
    else if (choice == 4)
    {
        Console.Write("Namn att söka efter: ");
        string wanted = Console.ReadLine();
        // Find ger tillbaka varan om den finns, annars null (ingenting)
        Item found = list.Find(wanted);

        if (found == null)
        {
            Console.WriteLine("Varan finns inte i listan.");
        }
        else
        {
            // {found} skrivs ut med ToString i Item.cs, t.ex. "Mjölk - 15 kr"
            Console.WriteLine($"Hittade: {found}");
        }
    }
    // Avsluta
    else if (choice == 5)
    {
        // break avbryter while-loopen och programmet tar slut
        break;
    }
}
