//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class SiteGroupSite
    {
        public int SiteGroupSiteId { get; set; }
        public int SiteId { get; set; }
        public int SiteGroupId { get; set; }
        public byte[] Concurrency { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateUpdated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    
        public virtual Site Site { get; set; }
        public virtual SiteGroup SiteGroup { get; set; }
    }
}
