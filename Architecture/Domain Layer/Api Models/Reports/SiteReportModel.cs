using System.Collections.Generic;

namespace Api.Architecture.DomainLayer.ApiModels.Reports
{
    public class SiteReportModel
    {
        public int SiteId { get; set; }

        public string TenantSiteIdentifier { get; set; }

        public string Name { get; set; }

        public IList<string> SiteOwner { get; set; }

        public float? Acres { get; set; }
    }
}
