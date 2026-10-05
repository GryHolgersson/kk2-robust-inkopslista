# Robust inköpslista

## Felrapport

Startkoden innehöll sex fel: fyra som får programmet att krascha, ett som ger fel resultat och ett som döljer att något gick fel.

### Fel 1: Programmet kraschar om man skriver bokstäver där det ska vara ett tal
**Vad hände:** Om man skrev bokstäver i menyvalet, i priset eller i numret för att ta bort en vara kraschade programmet med `FormatException`.

**Varför:** `Program.cs` använder `int.Parse` på det användaren skriver (rad 16, 23 och 29). `int.Parse` kastar ett undantag om texten inte är ett tal.

**Hur löst:** *Ej klart.*

### Fel 2: Programmet kraschar när man tar bort en vara som inte finns
**Vad hände:** Om man valde "Ta bort vara" och skrev ett nummer som inte fanns i listan, till exempel 4 när listan bara har 3 varor, eller 0, kraschade programmet med `ArgumentOutOfRangeException`.

**Varför:** `RemoveAt` i `ShoppingList` gör om numret till ett index med `number - 1` och skickar det vidare till listans `RemoveAt`. Listan räknar från 0, och med 3 varor är bara index 0, 1 och 2 giltiga. Ingen kontroll gjordes av numret innan det användes.

**Hur löst:** `RemoveAt` kontrollerar nu att numret ligger mellan 1 och antalet varor innan något tas bort. Metoden returnerar `bool` i stället för `void`: `true` om varan togs bort och `false` om numret inte fanns. Då kan `Program.cs` få veta om det gick bra och ge användaren ett meddelande.

*Ej klart:* `Program.cs` använder ännu inte svaret från `RemoveAt`.

### Fel 3: Programmet kraschar om items.txt saknas
**Vad hände:** Om filen `items.txt` inte fanns kraschade programmet direkt vid start med `FileNotFoundException`. Anropsstacken pekade på `ShoppingList.cs` rad 121, som anropades från `list.Load()` på rad 2 i `Program.cs`.

**Varför:** `Load` anropar `File.ReadAllText(path)` utan att först kontrollera att filen finns.

**Hur löst:** Först i `Load` finns nu en kontroll med `File.Exists(path)`. Om filen inte finns avslutas `Load` med `return`, och programmet startar med en tom lista i stället för att krascha. Jag valde `if` i stället för `try`/`catch`, eftersom det går att kontrollera i förväg om filen finns.

### Fel 4: Programmet kraschar vid start och sökningen hittar inte varor
**Vad hände:** När `items.txt` fanns kraschade programmet vid start med `IndexOutOfRangeException`. Dessutom hittade sökningen inte varor som syntes i listan.

**Varför:** `Save` avslutar varje rad med `\r\n`, men `Load` delar bara texten på `'\n'`. Det ger två problem:
- Den sista "raden" blir tom. `Split(';')` ger då bara en del, och `parts[1]` finns inte.
- Ett osynligt `\r` blir kvar i slutet av varje namn, så `"Mjölk"` blir `"Mjölk\r"` och `Find("Mjölk")` hittar ingenting.

**Hur löst:** *Påbörjat.* `File.ReadAllText` och `Split('\n')` är ersatta med `File.ReadAllLines`. Den delar upp filen i rader och klarar både `\r\n` och `\n`, så inget `\r` blir kvar i namnen och ingen tom rad skapas i slutet av filen.

*Ej klart:* Loopen kontrollerar ännu inte varje rad. En tom rad mitt i filen, en rad utan `;` eller ett pris som inte är ett tal får fortfarande programmet att krascha.

### Fel 5: Totalsumman blir fel
**Vad hände:** Totalsumman stämde inte när man räknade efter för hand. Den första varans pris saknades.

**Varför:** Loopen i `Total` börjar på `i = 1`, men listan räknar från 0. Den första varan, på index 0, räknas därför aldrig med.

**Hur löst:** Loopen börjar nu på `i = 0`, så att alla varor kommer med i summan. Villkoret `i < items.Count` var redan rätt och är oförändrat.

### Fel 6: Programmet säger att listan är sparad fast det misslyckades
**Vad hände:** Om sparandet misslyckades, till exempel för att filen var låst, fick användaren inte veta det. Programmet skrev "Listan är sparad." ändå.

**Varför:** `catch`-blocket i `Save` är tomt, och meddelandet skrivs ut efter `try`/`catch`, så det skrivs alltid ut.

**Hur löst:** "Listan är sparad." är flyttat in i `try`, direkt efter `WriteAllText`. Det skrivs därför bara ut när sparandet lyckades. Raden som stod efter `catch` och alltid skrevs ut är borttagen.

Den tomma `catch` är ersatt med två `catch` som fångar specifika undantag:
- `IOException`, till exempel att filen är låst av ett annat program eller att disken är full.
- `UnauthorizedAccessException`, att programmet saknar rättighet att skriva till filen.

Båda skriver ut att listan inte kunde sparas, tillsammans med felmeddelandet (`ex.Message`), så att användaren får veta vad som gick fel.

## Del 2

### Item skyddar sig själv
Konstruktorn i `Item` vägrar nu ogiltiga värden i stället för att skapa ett trasigt objekt:
- Tomt namn, `null` eller bara mellanslag ger `ArgumentException` (kontrolleras med `string.IsNullOrWhiteSpace`).
- Negativt pris ger `ArgumentOutOfRangeException`. Priset 0 är tillåtet.

*Ej klart:* `Program.cs` fångar ännu inte dessa undantag.

## Designval

## Klassdiagram
