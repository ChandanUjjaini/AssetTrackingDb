using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTrackingDb
{
    internal class Validation
    {
        public static bool ChkData(string read)//Validation input all string
        {
            bool Flag;

            Flag = int.TryParse(read, out int num);

            if (Flag == false)
            {
                Flag = (String.IsNullOrWhiteSpace(read));
            }
            return Flag;
        }
        public static string ChkAbb(string read1)//Checking for alternate country name
        {
            string read = read1.ToLower();
            if ((read == "america") || (read == "us") || (read == "united states of america") || (read == "usa"))
            {
                read = "United States";
            }

            else if ((read == "uae"))
            {
                read = "United Arab Emirates";
            }
            else if ((read == "uk") || (read == "england") || (read == "great britain"))
            {
                read = "united Kingdom";
            }
            else
                read = read1;

            return read;
        }
        internal static bool DateVal(String read)
        {
            DateTime Data;
            bool Flag;

            Flag = (String.IsNullOrWhiteSpace(read));//Checking for null string
            if (Flag == false)
            {
                try
                {
                    Flag = DateTime.TryParse(read, out Data);
                    if (Flag == false)
                    {
                        Flag = true;
                    }
                    else
                    {
                        Flag = false;
                    }
                }
                catch (FormatException DateExp)
                {
                    Flag = true;
                    Console.WriteLine(DateExp.Message);
                }
            }

            if (Flag == false)
            {
                Flag = DateTime.TryParse(read, out Data);
                Flag = Data >= DateTime.Now;
            }
            return Flag;
        }
        public static bool codeVal(string read) //Checking for valid cuntry name
        {
            bool Flag;
            string Data;
            Data = Char.ToUpper(read[0]) + read.Substring(1);//Converting first character to uppercase
            string code = AssetIO.Ccode2(Data);
            Flag = Validation.ChkData(code);
            return Flag;
        }
        public static bool IdVal(string read, List<Asset> AssetD) //Checking for valid cuntry name
        {
            bool Flag;
            Flag = int.TryParse(read, out int num);
            if (Flag)
            {
                foreach (Asset aId in AssetD)
                {
                    if (Convert.ToInt32(read) == aId.Id) { Flag = true; break; }

                    else { Flag = false; }

                }

                if (Flag == false) { CommonOp.ErrorMessage("ID"); }
            }
            return Flag;
        }



    }
}
