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
    
    public partial class ExchangeRate
    {
        public int Id { get; set; }
        public int FromCurrencyId { get; set; }
        public int ToCurrencyId { get; set; }
        public decimal ExRate { get; set; }
        public System.DateTime ExchangeRateDate { get; set; }
        public decimal BankSellingRate { get; set; }
        public decimal RiskTolerance { get; set; }
        public int CompanyId { get; set; }
        public int LocationId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int Createdby { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> Updatedby { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<int> Deletedby { get; set; }
    }
}
