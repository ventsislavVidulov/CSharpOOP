using Newtonsoft.Json;

namespace Encapsulation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string relativePath = @"../../../../Encapsulation/DB/Data/Data.JSON";
            var fs = new FileStream(relativePath, FileMode.Create, FileAccess.ReadWrite);

            string jsonString = @"{
             'user_FirstName':'Peter',
            'user_LastName':'Paul'
             }";

            var json = JsonConvert.SerializeObject(jsonString);
        }
    }
}
