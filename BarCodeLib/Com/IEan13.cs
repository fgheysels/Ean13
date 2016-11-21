using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLib.Com
{
    #region COM Interfaces

    [ComVisible(true)]
    [Guid("BC20A5FC-1873-4FB8-BF1E-81E725F943E8")]
    public interface IEan13
    {
        int CalculateCheckDigit(string code);

        string GetEan13EncodedString(string ean13);

        string GetCodePartFromEan13(string ean13Code);

        string GetCheckDigitFromEan13(string ean13Code);
    }

    #endregion
}
