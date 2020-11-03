using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface ICurrencyService
    {
        void UpdateCurrency();
        Decimal GetRate(string code);
        Decimal GetRateByDate(string code, DateTime date);
    }
}