using BarCodeLib.Com;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace BarCodeLib
{
    [ComVisible(true)]
    [Guid("E82F62D2-0EFE-4BAD-A395-F2D6A9B3AF19")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Ean13 : IEan13
    {
        public static int CalculateCheckDigit(string code)
        {
            if (code.Length != 12)
            {
                throw new ArgumentException("Unable to create checkdigit. Code should be 12 characters long.");
            }

            int sum1 = 0;
            int sum2 = 0;

            for (int i = code.Length - 1; i >= 1; i -= 2)
            {
                int result1 = (int)char.GetNumericValue(code[i]);
                int result2 = (int)char.GetNumericValue(code[i - 1]);

                sum1 += result1;
                sum2 += result2;
            }

            sum1 *= 3;

            int result = sum1 + sum2;

            int checkDigit = 10 - (result % 10);

            if (checkDigit == 10) { return 0; }

            return checkDigit;
        }

        public static string GetEan13EncodedString(string ean13)
        {
            ean13 = ean13.ToNumericValue();

            if (ean13.Length != 13)
            {
                throw new ArgumentException("ean13", "No valid ean13 code passed.");
            }

            StringBuilder sb = new StringBuilder();

            int firstChar = (int)Char.GetNumericValue(ean13[0]);

            sb.Append(ean13[0]);
            sb.Append(GetTableAValue((int)Char.GetNumericValue(ean13[1])));

            for (int i = 2; i < 7; i++)
            {
                bool tableA = DetermineTableA(firstChar, i);

                if (tableA)
                {
                    sb.Append(GetTableAValue((int)Char.GetNumericValue(ean13[i])));
                }
                else
                {
                    sb.Append(GetTableBValue((int)Char.GetNumericValue(ean13[i])));
                }
            }

            sb.Append("*");

            for (int i = 7; i < ean13.Length; i++)
            {
                sb.Append((GetTableCValue((int)Char.GetNumericValue(ean13[i]))));
            }

            sb.Append("+");

            return sb.ToString();
        }

        private static bool DetermineTableA(int firstChar, int i)
        {
            bool tableA = false;

            switch (i)
            {
                case 2:
                    if (firstChar <= 3) { tableA = true; }
                    break;

                case 3:
                    switch (firstChar)
                    {
                        case 0:
                        case 4:
                        case 7:
                        case 8:
                            tableA = true;
                            break;
                    }
                    break;

                case 4:
                    switch (firstChar)
                    {
                        case 0:
                        case 1:
                        case 4:
                        case 5:
                        case 9:
                            tableA = true;
                            break;
                    }
                    break;

                case 5:
                    switch (firstChar)
                    {
                        case 0:
                        case 2:
                        case 5:
                        case 6:
                        case 7:
                            tableA = true;
                            break;
                    }
                    break;

                case 6:
                    switch (firstChar)
                    {
                        case 0:
                        case 3:
                        case 6:
                        case 8:
                        case 9:
                            tableA = true;
                            break;
                    }
                    break;
            }

            return tableA;
        }

        private static char GetTableAValue(int digit)
        {
            return (char)(digit + 65);
        }

        private static char GetTableBValue(int digit) { return (char)(digit + 75); }

        private static char GetTableCValue(int digit) { return (char)(digit + 97); }

        public static string GetCheckDigitFromEan13(string ean13Code)
        {
            if (String.IsNullOrEmpty(ean13Code))
            {
                return string.Empty;
            }

            if (ean13Code.Length == 13)
            {
                return ean13Code.Substring(12, 1);
            }

            return string.Empty;
        }

        public static string GetCodePartFromEan13(string ean13Code)
        {
            if (String.IsNullOrWhiteSpace(ean13Code))
            {
                return string.Empty;
            }

            var numericValue = ean13Code.ToNumericValue();

            if (numericValue.Length >= 13)
            {
                return numericValue.Substring(0, 12);
            }

            return ean13Code;
        }

        int IEan13.CalculateCheckDigit(string code)
        {
            return Ean13.CalculateCheckDigit(code);
        }

        string IEan13.GetEan13EncodedString(string ean13)
        {
            return Ean13.GetEan13EncodedString(ean13);
        }

        string IEan13.GetCodePartFromEan13(string ean13Code)
        {
            return Ean13.GetCodePartFromEan13(ean13Code);
        }

        string IEan13.GetCheckDigitFromEan13(string ean13Code)
        {
            return Ean13.GetCheckDigitFromEan13(ean13Code);
        }
    }



}

