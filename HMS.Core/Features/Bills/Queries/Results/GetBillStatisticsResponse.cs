namespace HMS.Core.Features.Bills.Queries.Results
{
    public class GetBillStatisticsResponse
    {
        public int TotalBills { get; set; }
        public int PaidBills { get; set; }
        public int PendingBills { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal UnpaidAmount { get; set; }
        public double PaymentRate { get; set; }
        public decimal MonthlyRevenue { get; set; }
        public decimal WeeklyRevenue { get; set; }
        public int OverdueBillsCount { get; set; }
    }
}
