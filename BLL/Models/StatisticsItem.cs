using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    /// <summary>
    /// Statistic Item class
    /// Contains field of dateTime and value
    /// </summary>
    public class StatisticsItem
    {
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
    }
}
         