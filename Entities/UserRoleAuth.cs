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
    
    public partial class UserRoleAuth
    {
        public int UserRoleAuthId { get; set; }
        public System.Guid UserId { get; set; }
        public System.Guid RoleId { get; set; }
        public Nullable<bool> WholeOrg { get; set; }
        public Nullable<int> SiteGroupId { get; set; }
        public Nullable<int> SiteId { get; set; }
        public byte[] Concurrency { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateUpdated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    
        public virtual aspnet_Roles aspnet_Roles { get; set; }
        public virtual aspnet_Users aspnet_Users { get; set; }
        public virtual Site Site { get; set; }
        public virtual SiteGroup SiteGroup { get; set; }
    }
}
