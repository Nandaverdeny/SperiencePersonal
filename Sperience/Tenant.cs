//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sperience
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tenant
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string TenantName { get; set; }
        public string TenantDescription { get; set; }
        public int CommonCurrencyId { get; set; }
        public bool Active { get; set; }
        public Nullable<int> AuthenticationMethod { get; set; }
        public string OAuthAppId { get; set; }
        public string OAuthAppSecret { get; set; }
        public string LDAPServer { get; set; }
        public int LocationId { get; set; }
        public int CompanyId { get; set; }
        public int Createdby { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> Updatedby { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> Deletedby { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    }
}
