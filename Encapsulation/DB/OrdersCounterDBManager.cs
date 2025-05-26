using Encapsulation.DB.DataSchemas;
using System.Text.Json;

namespace Encapsulation.DB
{
    internal class OrdersCounterDBManager
    {
        private string relativeDBPath = @"../../../../Encapsulation/DB/Data/Counter.json";

        private JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
        // Check if the file exists

        public async Task CreateDB()
        {
            if (!File.Exists(relativeDBPath))
            {
                try
                {
                    // Create the file and write an empty list to it
                    using (FileStream fs = new(relativeDBPath, FileMode.Create))
                    {
                        await JsonSerializer.SerializeAsync(fs, new CounterOfOrders(), options);
                        //await fs.DisposeAsync();
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error creating DB: {ex.Message}");
                    Console.ResetColor();
                }
            }
        }

        public async Task<CounterOfOrders> GetCounter()
        {
            try
            {
                using (FileStream fs = new(relativeDBPath, FileMode.Open))
                {
                    CounterOfOrders counter = await JsonSerializer.DeserializeAsync<CounterOfOrders>(fs, options);
                    return counter;
                    //await fs.DisposeAsync();
                }
            }
            catch (JsonException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                Console.ResetColor();
                return null;
            }
        }

        public async Task IncreaseOrderId()
        {
            var counter = await GetCounter();
            counter.CurrentOrderId++;
            try
            {
                using (FileStream fw = new(relativeDBPath, FileMode.Create))
                {
                    await JsonSerializer.SerializeAsync(fw, counter, options);
                    //await fw.DisposeAsync();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error updating counter: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}

