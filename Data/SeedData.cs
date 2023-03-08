using System.Text.Json;
using MalmöTradera.web.Models;

namespace westcoast_cars.web.Data
{
    public static class SeedData
    {
        public static async Task LoadItems(ItemsContext context)
        {
            // Steg 1.
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            // Steg 2. Vill endast ladda data om vår vehicles tabell är tom...
            if (context.Items.Any()) return;

            // Steg 3. Läsa in json informationen ifrån vår vehicles.json fil...
            var json = System.IO.File.ReadAllText("Data/json/items.json");

            // Steg 4. Omvandla json objekten till en lista av VehicleModel objekt...
            var manufacturers = JsonSerializer.Deserialize<List<ItemsModel>>(json, options);

            // Steg 5. Skicka listan med VehicleModel objekt till databasen...
            if (manufacturers is not null && manufacturers.Count > 0)
            {
                await context.Items.AddRangeAsync(manufacturers);
                await context.SaveChangesAsync();
            }
        }
    }
}