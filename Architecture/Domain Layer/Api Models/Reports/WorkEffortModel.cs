using System;

namespace Api.Architecture.DomainLayer.ApiModels.Reports
{
    public class WorkEffortModel
    {
        public int WorkEffortId { get; set; }

        public int ProjectId { get; set; }

        public int SiteId { get; set; }

        public string TenantProjectIdentifier { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string WorkEffortLead { get; set; }

        public string ContractorContact { get; set; }

        public string CwsContractNumber { get; set; }

        public string PurchaseOrderNumber { get; set; }

        public string WorkEffortType { get; set; }

        public string WorkEffortStatus { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Agreement { get; set; }

        public string ProjectName { get; set; }

        public string RateType { get; set; }

        public int TaskCount { get; set; }

        public float? TaskCostSubtotal { get; set; }

        public float? WorkEffortTotal { get; set; }

        public float? InvoiceCount { get; set; }

        public float? TotalInvoiceAmount { get; set; }

        public float? ApprovedAmount { get; set; }
    }
}
