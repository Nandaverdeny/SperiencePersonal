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
    
    public partial class SubjectActionExpense
    {
        public int Id { get; set; }
        public int ActionId { get; set; }
        public int ExpenseTypeId { get; set; }
        public string ExpenseDescription { get; set; }
        public decimal ExpenseAmount { get; set; }
        public int CompanyId { get; set; }
        public int LocationId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int Createdby { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> Updatedby { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<int> Deletedby { get; set; }
    
        public virtual SubjectAction SubjectAction { get; set; }
    }
}
