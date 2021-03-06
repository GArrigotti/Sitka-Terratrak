using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Architecture.DomainLayer.ApiModels.Reports
{
    public class TaskReportModel
    {
        public int TaskId { get; set; }

        public int WorkEffortId { get; set; }

        public int ProjectId { get; set; }

        public int SiteId { get; set; }

        public string Name { get; set; }

        public string TaskType { get; set; }

        public string TaskStatus { get; set; }

        public bool IsComplete { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string BillingMethod { get; set; }

        public float TaskUnits { get; set; }

        public string TaskUnitOfMeasure { get; set; }

        public float TaskUnitCost { get; set; }

        public float TaskCost { get; set; }

        [NotMapped]
        public string Equipment { get; set; }

        [NotMapped]
        public string EquipmentUnits { get; set; }

        [NotMapped]
        public string EquipmentUnitOfMeasure { get; set; }

        [NotMapped]
        public string EquipmentUnitCost { get; set; }

        [NotMapped]
        public string EquipmentCost { get; set; }

        [NotMapped]
        public string OperatorUnits { get; set; }

        [NotMapped]
        public string OperatorUnitOfMeasure { get; set; }

        [NotMapped]
        public float OperatorUnitCost { get; set; }

        [NotMapped]
        public string OperatorCost { get; set; }

        public string ContractedCost { get; set; }

        public float MobilizationCost { get; set; }

        public string MaterialsCost { get; set; }

        public float TotalCost { get; set; }

        public float InvoicedTaskUnits { get; set; }

        [NotMapped]
        public string InvoicedEquipmentUnits { get; set; }

        [NotMapped]
        public string InvoicedOperatorUnits { get; set; }

        public float InvoicedTaskAmount { get; set; }

        [NotMapped]
        public string InvoicedEquipmentAmount { get; set; }

        [NotMapped]
        public string InvoicedOperatorAmount { get; set; }

        public float InvoicedContractedAmount { get; set; }

        public float InvoicedMobilizationAmount { get; set; }

        public float InvoicedMaterialsAmount { get; set; }

        public float InvoicedAdjustmentAmount { get; set; }

        public float InvoicedTotalAmount { get; set; }
    }
}
