using System;
using System.Collections.Generic;

namespace Api.Architecture.DomainLayer.ApiModels.Reports
{
    public class ProjectReportModel
    {
        public int ProjectId { get; set; }

        public int SiteId { get; set; }

        public string Name { get; set; }

        public string TenantProjectIdentifier { get; set; }

        public string TenantSiteIdentifier { get; set; }

        public string ProgramLead { get; set; }

        public string PeerReviewer { get; set; }

        public string PrimaryOrganization { get; set; }

        public string PrimaryOrganizationContact { get; set; }

        public IList<string> OtherContacts { get; set; }

        public string ProjectStatus { get; set; }

        public string ProjectType { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Description { get; set; }

        public float Acres { get; set; }
    }
}
