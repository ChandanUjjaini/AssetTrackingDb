using AssetTrackingDb;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AssetTrackingDb
{
    internal class AssetOp
    {



        internal static void Input()
        {
            MyDbContext Context = new MyDbContext();
            bool Run = true;

            String Type; // Declaring variables for passing arguments
            String Brand;
            String Model;
            DateTime PDate;
            int Price;
            String? Location;
            String currency;
            int LPrice;

            {
                // Input asset data form user
                Type = AssetIO.GetType();
                Brand = AssetIO.GetBrand(Type);
                Model = AssetIO.GetModel(Type, Brand);
                DateTime Date1 = AssetIO.ReadDate("Purchase Date");
                
                Price = AssetIO.ReadNum("Asset Price in USD");//Getting price in USD
                Location = AssetIO.ReadData("Asset Location");
                Location = Char.ToUpper(Location[0]) + Location.Substring(1);//Converting first character to uppercase
                Location = Validation.ChkAbb(Location); //Check for country alias name and code
                currency = AssetIO.Ccode2(Location);  //Getting international Currency code 
                LPrice = Convert.ToInt32(AssetIO.LocalPrice(currency, Price)); //Converting USD price to local cuntry price
                Asset AssetD = new Asset();
                AssetD.Type = Type;
                AssetD.Brand = Brand;
                AssetD.Model = Model;
                AssetD.PDate = Date1;
                AssetD.Price = Price;
                AssetD.Location = Location;
                AssetD.Currency = currency;
                AssetD.LoPrice = LPrice;

                Context.Assets.Add(AssetD);
                
                Context.SaveChanges();
            }
        }

        internal static void Display(List<Asset> AssetD)
        {
            MyDbContext Context = new MyDbContext();
            bool Run = true;
            Console.Clear();
            //Menu outline
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(String.Format("{0,-10}", "ID".PadRight(3) + "Asset Type".PadRight(20) + "Brand".PadRight(12) + "Model".PadRight(19) + "Purchase Date".PadRight(17)
                + "Price (USD)".PadRight(12) + "Location".PadRight(12) + "Currency".PadRight(10) + "Local Price"));
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");


            foreach (Asset Asset in AssetD)
            {
                //bool flag = DateTime.TryParse(Asset.PDate.ToString(), out DateTime pDate);
                var y = DateTime.Now.Year - Asset.PDate.Year;
                var m = DateTime.Now.Month - Asset.PDate.Month;
                var TM = m + (y * 12);// Checking for asset expirt date

                if (TM > 36)//Expired asset

                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    CommonOp.PrintData(Asset.Id, Asset.Type, Asset.Brand, Asset.Model, Asset.PDate, Asset.Price, Asset.Location, Asset.Currency, Asset.LoPrice);
                    Console.ResetColor();
                }

                else if (TM <= 36 && TM > 33)//6 months to expire

                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    CommonOp.PrintData(Asset.Id, Asset.Type, Asset.Brand, Asset.Model, Asset.PDate, Asset.Price, Asset.Location, Asset.Currency, Asset.LoPrice);
                    Console.ResetColor();
                }

                else if (TM >= 30 && TM < 34)//3 months to expire

                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    CommonOp.PrintData(Asset.Id, Asset.Type, Asset.Brand, Asset.Model, Asset.PDate, Asset.Price, Asset.Location, Asset.Currency, Asset.LoPrice);
                    Console.ResetColor();
                }
                else
                {
                    CommonOp.PrintData(Asset.Id, Asset.Type, Asset.Brand, Asset.Model, Asset.PDate, Asset.Price, Asset.Location, Asset.Currency, Asset.LoPrice);
                }
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Asset Expired >");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("3 monts for Asset to Expired >");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("6 monts for Asset to Expired >");
            Console.ResetColor();
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
        }

        internal static void SortList(List<Asset> AssetD)
        {
            MyDbContext Context = new MyDbContext();
            Console.WriteLine("*****************************************");
            Console.WriteLine("* Please Enter your sort type *");
            Console.WriteLine("*****************************************");
            Console.WriteLine("* Sort by Type\t\t- 1\t\t*\n* Sort by Brand\t\t- 2\t\t*\n* Sort by Date\t\t- 3\t\t*\n* Sort by Price\t\t- 4\t\t*\n* Sort by Location\t- 5\t\t*");
            Console.WriteLine("*****************************************");
            int SortIn = AssetIO.Selchk("Sort Type", 5);



            int SorSel = Convert.ToInt32(SortIn);

            switch (SorSel)
            {
                case 1:
                    {
                        //Sort by Type
                        List<Asset> SortType = AssetD.OrderBy(AssetList => AssetList.Type).ToList();
                        Display(SortType);
                        break;
                    }
                case 2:
                    {
                        //Sort by Brand
                        List<Asset> SortType = AssetD.OrderBy(AssetList => AssetList.Brand).ToList();
                        Display(SortType);
                        break;
                    }
                case 3:
                    {
                        //Sort by Purchase Date
                        List<Asset> SortType = AssetD.OrderBy(AssetList => AssetList.PDate).ToList();
                        Display(SortType);
                        break;
                    }
                case 4:
                    {
                        //Sort by price
                        List<Asset> SortType = AssetD.OrderBy(AssetList => AssetList.Price).ToList();
                        Display(SortType);
                        break;
                    }
                case 5:
                    {
                        //Sort by asset Location
                        List<Asset> SortType = AssetD.OrderBy(AssetList => AssetList.Location).ToList();
                        Display(SortType);
                        break;
                    }
            }

        }
        internal static void Delete(List<Asset> AssetD)
        {
            MyDbContext Context = new MyDbContext();
            bool Run = true;

            {
                bool flag;
                String iD;
                AssetOp.Display(AssetD);               
                do
                {
                    Console.Write("Enter Asset ID to remove from list: ");
                    iD = Console.ReadLine();
                    flag = Validation.IdVal(iD, AssetD);
                } while (!flag);

                int ID = Convert.ToInt32(iD);
                Asset Assets = Context.Assets.FirstOrDefault(a => a.Id == ID);

                Context.Assets.Remove(Assets);//deleting asset
                Context.SaveChanges();
            }
        }
        internal static void UpdateAsset(List<Asset> AssetD)
        {
            MyDbContext Context = new MyDbContext();
            bool Run = true;

            {
                bool flag;
                String iD;
                AssetOp.Display(AssetD);
                do
                {
                    Console.Write("Enter Asset ID to Update a list: ");
                    iD = Console.ReadLine();
                    flag = Validation.IdVal(iD, AssetD);
                    
                } while (!flag);

                int ID = Convert.ToInt32(iD);
                Asset Assets = Context.Assets.FirstOrDefault(a => a.Id == ID);
                {
                    Assets.Type = AssetIO.GetType();
                    Assets.Brand = AssetIO.GetBrand(Assets.Type);
                    Assets.Model = AssetIO.GetModel(Assets.Type, Assets.Brand);
                    Assets.PDate = AssetIO.ReadDate("Purchase Date");
                    Assets.Price = AssetIO.ReadNum("Asset Price in USD");//Getting price in USD
                    String Location = AssetIO.ReadData("Asset Location");
                    Assets.Location = Char.ToUpper(Location[0]) + Location.Substring(1);//Converting first character to uppercase
                    Assets.Location = Validation.ChkAbb(Location); //Check for country alias name and code
                    Assets.Currency = AssetIO.Ccode2(Location);  //Getting international Currency code 
                    Assets.LoPrice = Convert.ToInt32(AssetIO.LocalPrice(Assets.Currency, Assets.Price));
                }
                
                Context.Assets.Update(Assets);
                
                Context.SaveChanges();
                
            }
        }
        internal static void LoadAsset() // sample asset data to start with the operation
        {
            MyDbContext Context = new MyDbContext();
            bool Run = true;

            String Type; // Declaring variables for passing arguments
            String Brand;
            String Model;
            DateTime PDate;
            int Price;
            String? Location;
            String currency;
            int LPrice;

            {
                Asset AssetD = new Asset();
                // Adding sample assets
                Type = "Laptop/Computers"; Brand = "Apple";Model = "MacBook Air"; 
                PDate = Convert.ToDateTime("11/25/2019"); Price = 209; Location = "Italy"; currency = AssetIO.Ccode2(Location);
                LPrice = Convert.ToInt32(AssetIO.LocalPrice(currency, Price)); //Converting USD price to local cuntry price
                AssetD.Type = Type;AssetD.Brand = Brand;AssetD.Model = Model;AssetD.PDate = PDate;
                AssetD.Price = Price;AssetD.Location = Location;AssetD.Currency = currency;AssetD.LoPrice = LPrice;
                Context.Assets.Add(AssetD);
                Context.SaveChanges();
            }
            {
                Asset AssetD = new Asset();
                Type = "Laptop/Computers"; Brand = "Asus"; Model = "ZenBook";
                PDate = Convert.ToDateTime("09/15/2019"); Price = 209; Location = "Norway"; currency = AssetIO.Ccode2(Location);
                LPrice = Convert.ToInt32(AssetIO.LocalPrice(currency, Price)); //Converting USD price to local cuntry price
                AssetD.Type = Type; AssetD.Brand = Brand; AssetD.Model = Model; AssetD.PDate = PDate;
                AssetD.Price = Price; AssetD.Location = Location; AssetD.Currency = currency; AssetD.LoPrice = LPrice;
                Context.Assets.Add(AssetD);
                Context.SaveChanges();
            }
            {
                Asset AssetD = new Asset();
                Type = "Mobile Phones"; Brand = "Apple"; Model = "iphone 13";
                PDate = Convert.ToDateTime("12/29/2021"); Price = 298; Location = "Denmark"; currency = AssetIO.Ccode2(Location);
                LPrice = Convert.ToInt32(AssetIO.LocalPrice(currency, Price)); //Converting USD price to local cuntry price
                AssetD.Type = Type; AssetD.Brand = Brand; AssetD.Model = Model; AssetD.PDate = PDate;
                AssetD.Price = Price; AssetD.Location = Location; AssetD.Currency = currency; AssetD.LoPrice = LPrice;
                Context.Assets.Add(AssetD);
                Context.SaveChanges();
            }
            {
                Asset AssetD = new Asset();
                Type = "Mobile Phones"; Brand = "Samsung"; Model = "Galaxy A21s";
                PDate = Convert.ToDateTime("04/29/2020"); Price = 200; Location = "Poland"; currency = AssetIO.Ccode2(Location);
                LPrice = Convert.ToInt32(AssetIO.LocalPrice(currency, Price)); //Converting USD price to local cuntry price
                AssetD.Type = Type; AssetD.Brand = Brand; AssetD.Model = Model; AssetD.PDate = PDate;
                AssetD.Price = Price; AssetD.Location = Location; AssetD.Currency = currency; AssetD.LoPrice = LPrice;
                Context.Assets.Add(AssetD);
                Context.SaveChanges();
            }
            {
                Asset AssetD = new Asset();
                Type = "Mobile Phones"; Brand = "Nokia"; Model = "Nokia 2660 DS";
                PDate = Convert.ToDateTime("11/29/2019"); Price = 200; Location = "Finland"; currency = AssetIO.Ccode2(Location);
                LPrice = Convert.ToInt32(AssetIO.LocalPrice(currency, Price)); //Converting USD price to local cuntry price
                AssetD.Type = Type; AssetD.Brand = Brand; AssetD.Model = Model; AssetD.PDate = PDate;
                AssetD.Price = Price; AssetD.Location = Location; AssetD.Currency = currency; AssetD.LoPrice = LPrice;
                Context.Assets.Add(AssetD);
                Context.SaveChanges();
            }
            {
                Asset AssetD = new Asset();
                Type = "Laptop/Computers"; Brand = "Lenovo"; Model = "ThinkPad X1";
                PDate = Convert.ToDateTime("05/19/2020"); Price = 388; Location = "Sweden"; currency = AssetIO.Ccode2(Location);
                LPrice = Convert.ToInt32(AssetIO.LocalPrice(currency, Price)); //Converting USD price to local cuntry price
                AssetD.Type = Type; AssetD.Brand = Brand; AssetD.Model = Model; AssetD.PDate = PDate;
                AssetD.Price = Price; AssetD.Location = Location; AssetD.Currency = currency; AssetD.LoPrice = LPrice;
                Context.Assets.Add(AssetD);
                Context.SaveChanges();
            }
            {
                Asset AssetD = new Asset();
                Type = "Laptop/Computers"; Brand = "Apple"; Model = "MacBook Pro";
                PDate = Convert.ToDateTime("10/22/2021"); Price = 1200; Location = "Sweden"; currency = AssetIO.Ccode2(Location);
                LPrice = Convert.ToInt32(AssetIO.LocalPrice(currency, Price)); //Converting USD price to local cuntry price
                AssetD.Type = Type; AssetD.Brand = Brand; AssetD.Model = Model; AssetD.PDate = PDate;
                AssetD.Price = Price; AssetD.Location = Location; AssetD.Currency = currency; AssetD.LoPrice = LPrice;
                Context.Assets.Add(AssetD);

                Context.SaveChanges();
            }
            
        }
    }
}
