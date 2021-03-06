namespace Api.Architecture.DomainLayer.ApiModels.Reports
{
    public class BudgetReportModel
    {
        public int SiteId { get; set; }

        public string TenantSiteIdentifier { get; set; }

        public string SiteName { get; set; }

        public int ProjectId { get; set; }

        public string TenantProjectIdentifier { get; set; }

        public string ProjectName { get; set; }

        public string FiscalYearName { get; set; }

        public string SpendingTypeName { get; set; }

        public string FundingSourceTypeName { get; set; }

        public string FundingSourceName { get; set; }

        public string FundingSourceCode { get; set; }

        public float? AmountAllocated { get; set; }

        public float? AmountSpent { get; set; }

        public float? ProjectFYFundingSourceAmountApproved { get; set; }

        public float? ProjectFYFundingSourceAmountEncumbered { get; set; }

        public float? ProjectFYFundingSourceAmountRemaining { get; set; }
    }
}
