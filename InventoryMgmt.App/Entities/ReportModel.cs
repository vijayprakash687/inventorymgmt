using System;

namespace InventoryMgmt.App.Entities
{
    public class ReportModel
    {
        public int ReportId { get; set; }

        public decimal TotalValue { get; set; }

        public decimal ProfitAmount { get; set; }

        public DateTime CreatedTime { get; }

        public ReportModel()
        {
            CreatedTime = DateTime.UtcNow;
        }
    }
}
