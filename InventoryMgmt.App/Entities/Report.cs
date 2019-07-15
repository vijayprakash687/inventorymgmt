using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMgmt.App.Entities
{
    public class Report
    {
        public int ReportId { get; set; }
        public decimal TotalValue { get; set; }

        public decimal Profit { get; set; }
    }
}
