using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AssetTrackingDb
{
    internal class CommonOp
    {
        internal static void ErrorMessage(String data) // Common code to display error message
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Please Enter Valid {data}");// Error Message
            Console.ResetColor();
        }

        public static string ReadData(String read) // Common code to read data from console
        {
            Console.Write($"Please Enter {read}: ");
            String Data = Console.ReadLine();
            Data = Data.Trim();
            return Data;
        }

        // Common code to print data to console
        public static void PrintData(int Id, string Type, string Brand, string Model, DateTime PDate, int Price, string Location, string Currency, decimal Lprice)
           
        {
            
            String date = PDate.ToString().Substring(0, 10);
            string[] date1 = date.Split(' ');
            date = date1[0];            
            Console.WriteLine(String.Format("{0,-10}", Id.ToString().PadRight(3) + Type.PadRight(20) + Brand.PadRight(12) + Model.PadRight(19)
                    + date.ToString().PadRight(17) + Price.ToString().PadRight(12) + Location.PadRight(12)
                    + Currency.PadRight(10) + Lprice));
        }

        public static string ExRate(string code) //Method to get exchange rate from Google Finance
        {
            string data = null;
            try
            {
                HtmlAgilityPack.HtmlWeb website = new HtmlAgilityPack.HtmlWeb(); //Need to add HtmlAgilityPack Nuget by ZZZ projects
                String urlSrc = $"https://www.google.com/finance/quote/USD-" + code + "?hl=en";
                HtmlAgilityPack.HtmlDocument document = website.Load(urlSrc);
                data = document.DocumentNode.SelectSingleNode("//div[@class='YMlKec fxKbKc']").InnerText;
            }
            catch (NullReferenceException nullexp)
            {
                if (code == "USD")
                {
                    data = "1";
                }
            }
            catch (Exception ex)
            {
                data = "Repeat";
            }
            return data;
        }
    }
}
