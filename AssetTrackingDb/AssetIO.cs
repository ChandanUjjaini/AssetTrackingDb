using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace AssetTrackingDb
{
    internal class AssetIO
    {
        public static int Selchk(string read, int count)
        {
            bool Flag;
            int Data = 0;
            String Input;
            do
            {
                Input = CommonOp.ReadData(read);
                Flag = Validation.ChkData(Input);
                if (Flag == false)
                {
                    CommonOp.ErrorMessage(read);
                }
                else if (Flag == true)
                {
                    Data = int.Parse(Input);
                    Flag = (Data > count);
                }
            } while (Flag == true);
            GC.Collect();
            return Data;
        }

        public static string ReadData(string read)// method to input data
        {
            bool Flag;
            string Data;
            do
            {
                Data = CommonOp.ReadData(read);
                Flag = Validation.ChkData(Data);
                if (read == "Asset Location")
                {
                    Data = Validation.ChkAbb(Data);
                    Flag = Validation.codeVal(Data);

                }
                if (Flag == true)

                    CommonOp.ErrorMessage(read);

            } while (Flag == true);
            GC.Collect();
            return Data;
        }
        public static DateTime ReadDate(string read) // Method to input date
        {
            bool Flag;
            string ReadDate;

            //bool valDate;
            do
            {
                ReadDate = CommonOp.ReadData(read + " in \"mm/dd/yyyy\" format");
                Flag = Validation.DateVal(ReadDate);
                if (Flag == true)
                {
                    CommonOp.ErrorMessage(read);
                }
            } while (Flag == true);
            Flag = DateTime.TryParse(ReadDate, out DateTime Data);


            return Data;

        }
        public static int ReadNum(string read)// Method to input number 
        {
            bool Flag;
            String PriceIn;
            int Data;
            do
            {
                PriceIn = CommonOp.ReadData(read);
                Flag = Validation.ChkData(PriceIn);
                if (Flag == false)
                {
                    CommonOp.ErrorMessage(read);
                }
            } while (Flag == false);
            Data = int.Parse(PriceIn);
            GC.Collect();
            return Data;
        }
        internal static string Ccode2(String name)
        {
            String Code = null;

            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            List<RegionInfo> countries = new List<RegionInfo>();
            foreach (CultureInfo ci in cultures)
            {
                RegionInfo regionInfo = new RegionInfo(ci.Name);
                if (countries.Count(x => x.EnglishName == regionInfo.EnglishName) <= 0)
                    countries.Add(regionInfo);
            }
            foreach (RegionInfo regionInfo in countries.OrderBy(x => x.EnglishName))
            {
                if (name == regionInfo.EnglishName)
                    Code = regionInfo.ISOCurrencySymbol;
            }
            return Code;
        }
        internal static decimal LocalPrice(string code, int price)
        {
            float data;
            getExr:
            String exrate = CommonOp.ExRate(code);
            if (exrate == "Repeat") { goto getExr; }
            float exRate = float.Parse(exrate);
            data = price * exRate; // Converting currency to local

            decimal Rdata = Convert.ToDecimal(data);
            Rdata = Math.Round(Rdata, 2);
            GC.Collect();
            return Rdata;
        }

        internal static string GetType()
        {
            string Gtype;

            Console.WriteLine("¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
            Console.WriteLine("¤ Please Select Asset Type ¤");
            Console.WriteLine("¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
            Console.WriteLine("¤ Laptop/Computers-\t1  ¤\n¤ Mobile Phones\t  -\t2  ¤");
            Console.WriteLine("¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");

            int Sel = Selchk("selection", 2);

            if (Sel == 1) { Gtype = "Laptop/Computers"; }
            else { Gtype = "Mobile Phones"; }
            return Gtype;
        }

        internal static string GetBrand(string type)
        {
            string Gbrand = null;


            if (type == "Laptop/Computers")
            {
                Console.WriteLine("¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
                Console.WriteLine("¤ Please Select Asset Type ¤");
                Console.WriteLine("¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
                Console.WriteLine("¤ Apple\t\t  -\t1  ¤\n¤ Asus\t\t  -\t2  ¤\n¤ Lenovo\t  -\t2  ¤");
                Console.WriteLine("¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");

                int Sel = Selchk("selection", 3);

                if (Sel == 1)
                {
                    Gbrand = "Apple";
                }

                else if (Sel == 2)
                {
                    Gbrand = "Asus";
                }
                else if (Sel == 3)
                {
                    Gbrand = "Lenovo";
                }

            }
            else
            {
                Console.WriteLine("¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
                Console.WriteLine("¤ Please Select Asset Type ¤");
                Console.WriteLine("¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
                Console.WriteLine("¤ Apple\t\t  -\t1  ¤\n¤ Samsung\t  -\t2  ¤\n¤ Nokia\t\t  -\t3  ¤");
                Console.WriteLine("¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
                int Sel = Selchk("selection", 3);

                if (Sel == 1)
                {
                    Gbrand = "Apple";
                }

                else if (Sel == 2)
                {
                    Gbrand = "Samsung";
                }
                else if (Sel == 3)
                {
                    Gbrand = "Nokia";
                }
            }

            return Gbrand;
        }

        internal static string GetModel(string brand, string model)
        {
            string Gmodel = null;
            if (brand == "Laptop/Computers" && model == "Apple")
            {
                Console.WriteLine("¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
                Console.WriteLine("¤ Please Select Asset Type ¤");
                Console.WriteLine("¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
                Console.WriteLine("¤ MacBook Air\t  -\t1  ¤\n¤ MacBook Pro\t  -\t2  ¤\n¤ iMac\t\t  -\t3  ¤");
                Console.WriteLine("¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
                int Sel = Selchk("selection", 3);

                if (Sel == 1)
                {
                    Gmodel = "MacBook Air";
                }

                else if (Sel == 2)
                {
                    Gmodel = "MacBook Pro";
                }
                else if (Sel == 3)
                {
                    Gmodel = "iMac";
                }
            }

            else if (brand == "Laptop/Computers" && model == "Asus")
            {
                Console.WriteLine("¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
                Console.WriteLine("¤ Please Select Asset Type ¤");
                Console.WriteLine("¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
                Console.WriteLine("¤ ZenBook\t    -\t1  ¤\n¤ ProArt StudioBook -\t2  ¤\n¤ VivoBook Flip\t    -\t3  ¤");
                Console.WriteLine("¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
                int Sel = Selchk("selection", 3);

                if (Sel == 1)
                {
                    Gmodel = "ZenBook";
                }

                else if (Sel == 2)
                {
                    Gmodel = "ProArt StudioBook";
                }
                else if (Sel == 3)
                {
                    Gmodel = "VivoBook Flip";
                }
            }

            else if (brand == "Laptop/Computers" && model == "Lenovo")
            {
                Console.WriteLine("¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
                Console.WriteLine("¤ Please Select Asset Type ¤");
                Console.WriteLine("¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
                Console.WriteLine("¤ ThinkPad X1\t  -\t1  ¤\n¤ Yoga 7i\t  -\t2  ¤\n¤ Legion 5\t  -\t2  ¤");
                Console.WriteLine("¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
                int Sel = Selchk("selection", 3);

                if (Sel == 1)
                {
                    Gmodel = "ThinkPad X1";
                }

                else if (Sel == 2)
                {
                    Gmodel = "Yoga 7i";
                }
                else if (Sel == 3)
                {
                    Gmodel = "Legion 5";
                }
            }
            else if (brand == "Mobile Phones" && model == "Apple")
            {
                Console.WriteLine("¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
                Console.WriteLine("¤ Please Select Asset Type ¤");
                Console.WriteLine("¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
                Console.WriteLine("¤ iphone 12\t  -\t1  ¤\n¤ iphone 13\t  -\t2  ¤\n¤ iphone 14\t  -\t2  ¤");
                Console.WriteLine("¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
                int Sel = Selchk("selection", 3);

                if (Sel == 1)
                {
                    Gmodel = "iphone 12";
                }

                else if (Sel == 2)
                {
                    Gmodel = "iphone 13";
                }
                else if (Sel == 3)
                {
                    Gmodel = "iphone 14";
                }
            }
            else if (brand == "Mobile Phones" && model == "Samsung")
            {
                Console.WriteLine("¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
                Console.WriteLine("¤ Please Select Asset Type ¤");
                Console.WriteLine("¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
                Console.WriteLine("¤ Galaxy S22 Ultra-\t1  ¤\n¤ Galaxy A21s\t  -\t2  ¤\n¤ Galaxy Z Fold\t  -\t2  ¤");
                Console.WriteLine("¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
                int Sel = Selchk("selection", 3);

                if (Sel == 1)
                {
                    Gmodel = "Galaxy S22 Ultra";
                }

                else if (Sel == 2)
                {
                    Gmodel = "Galaxy A21s";
                }
                else if (Sel == 3)
                {
                    Gmodel = "Galaxy Z Fold";
                }
            }
            else if (brand == "Mobile Phones" && model == "Nokia")
            {
                Console.WriteLine("¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
                Console.WriteLine("¤ Please Select Asset Type ¤");
                Console.WriteLine("¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
                Console.WriteLine("¤ G21\t\t  -\t1  ¤\n¤ Nokia 2660 DS\t  -\t2  ¤\n¤ 5710 Xpress\t  -\t2  ¤");
                Console.WriteLine("¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
                int Sel = Selchk("selection", 3);

                if (Sel == 1)
                {
                    Gmodel = "G21";
                }

                else if (Sel == 2)
                {
                    Gmodel = "Nokia 2660 DS";
                }
                else if (Sel == 3)
                {
                    Gmodel = "5710 Xpress";
                }
            }

            return Gmodel;
        }



    }

}
