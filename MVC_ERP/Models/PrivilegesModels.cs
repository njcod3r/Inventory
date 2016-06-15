using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;



namespace MVC_ERP.Models
{
    public class Priv_Menus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string NameA { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string NameE { get; set; }
        public int? ParentId { get; set; }
        public Boolean Sold { get; set; }
        public Boolean Expanded { get; set; }
        public Boolean HasChildren { get; set; }
        public string UrlLink { get; set; }
        public string ImageUrlLink { get; set; }
        public string Target { get; set; }
        public string HtmlAttributes { get; set; }
        public Boolean OnlyForProgrammer { get; set; }
        public string Notes { get; set; }
        //Not used but wanted 
        public Boolean Checked { get; set; }
    }
    public class Priv_Groups
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id {get;set;}
        public int MemberShipId { get; set; }
        public string Code { get; set; }
         [Required(ErrorMessage = "Name required")]
        public string NameA { get; set; }
         [Required(ErrorMessage = "Name required")]
        public string NameE { get; set; }
        public string Notes { get; set; }
        public int? Create_Uid { get; set; }
        public Nullable<DateTime> Create_Date { get; set; }
        public int? Write_Uid { get; set; }
        public Nullable<DateTime> Write_Date { get; set; }
        public Boolean? Post { get; set; }
        public int? Post_Uid { get; set; }
        public Nullable<DateTime> Post_Date { get; set; }
        public Boolean? Deleted { get; set; }
        public int? Delete_Uid { get; set; }
        public Nullable<DateTime> Delete_Date { get; set; }
        [ForeignKey("GroupId")]
        public List<Priv_Groups_Rights> Priv_Groups_Rights { get; set; }
        //public List<Priv_Users_Groups> Priv_Users_Groups { get; set; }

    }
    public class Priv_Groups_Rights
    {
        [Key]
        public int Id { get; set; }
        public int MemberShipId { get; set; }
        public int GroupId { get; set; }
        public int MenuId { get; set; }
        public Boolean Open { get; set; }
        public Boolean Insert { get; set; }
        public Boolean Modifay { get; set; }
        public Boolean Reserve { get; set; }
        public Boolean Delete { get; set; }
        public Boolean Reuse { get; set; }
        public Boolean Post { get; set; }
        public Boolean UnPost { get; set; }
        public Boolean Share { get; set; }
        public Boolean Search { get; set; }
        public Boolean Preview { get; set; }
        public Boolean Print { get; set; }
        public Boolean Export { get; set; }
    
    
    }
    public class Priv_Users : IUser
    {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int id { get; set; }
    public int MemberShipId { get; set; }
    public string Code { get; set; }
    [Required(ErrorMessage = "Name required")]
    public string NameA { get; set; }
    [Required(ErrorMessage = "Name required")]
    public string NameE { get; set; }
    [Required(ErrorMessage = "Login Name required")]
    public string UserName { get; set; }
    [Required(ErrorMessage = "Pasword required")]
    public string Password { get; set; }
    public string Email { get; set; }
    public string Mobile { get; set; }
    public string Signature { get; set; }
    public bool Active { get; set; }
    public bool CanWorkRemotly  { get; set; }
    public string Notes { get; set; }
    public int? Default_LanguageId { get; set; }
    [ForeignKey("Default_FinancialYearId")]
    public GL_FinancialYears GL_FinancialYears { get; set; }
    public int? Default_FinancialYearId { get; set; }
    [ForeignKey("Default_CompanyId")]
    public Sys_Companies Sys_Companies { get; set; }
    public int? Default_CompanyId { get; set; }
    [ForeignKey("Default_BranchId")]
    public Sys_Branches Sys_Branches { get; set; }
    public int? Default_BranchId { get; set; }
    public int? Create_Uid { get; set; }
    public Nullable<DateTime> Create_Date { get; set; }
    public int? Write_Uid { get; set; }
    public Nullable<DateTime> Write_Date { get; set; }
    public Boolean? Post { get; set; }
    public int? Post_Uid { get; set; }
    public Nullable<DateTime> Post_Date { get; set; }
    public Boolean? Deleted { get; set; }
    public int? Delete_Uid { get; set; }
    public Nullable<DateTime> Delete_Date { get; set; }
    [NotMapped]
    public string PasswordHash { get; set; }
        //For Authentication
    [NotMapped]
    public string Id
    {
        get { return  Convert.ToString(id); }
    }
    

}
    public class Priv_Users_Groups
    {
        [Key]
        public int Id { get; set; }
        public int MemberShipId { get; set; }

        [ForeignKey("UserId")]
        public Priv_Users Priv_Users { get; set; }
        public int UserId { get; set; }

        [ForeignKey("GroupId")]
        public Priv_Groups Priv_Groups { get; set; }
        public int GroupId { get; set; }
        public bool IsAdmin { get; set; }

    }
    public class Priv_Users_CompaniesBranches
    {
        [Key]
        public int Id { get; set; }
        public int MemberShipId { get; set; }

        [ForeignKey("UserId")]
        public Priv_Users Priv_Users { get; set; }
        public int UserId { get; set; }

        [ForeignKey("CompanyId")]
        public Sys_Companies Sys_Companies { get; set; }
        public int CompanyId { get; set; }

        [ForeignKey("BranchId")]
        public Sys_Branches Sys_Branches { get; set; }
        public int BranchId { get; set; }
        public bool Show { get; set; }
        public int? ParentId { get; set; }
        public bool HasChildren { get; set; }
        public virtual string NameA { get; set; }
        public virtual string NameE { get; set; }

    }
    public class Priv_Users_FinancialYears
    {
        [Key]
        public int Id { get; set; }
        public int MemberShipId { get; set; }

        [ForeignKey("UserId")]
        public Priv_Users Priv_Users { get; set; }
        public int UserId { get; set; }

        [ForeignKey("FinancialYearId")]
        public GL_FinancialYears GL_FinancialYears { get; set; }
        public int FinancialYearId { get; set; }
        public bool Show { get; set; }
        public bool CanEdit { get; set; }
      
        //Not ~mapped column for memory and programming usage
        [NotMapped]
        public int FinancialYear { get; set; }

    }
     public class Priv_BacupName
     {
         public int Id { get; set; }
         public string FileName { get; set; }
         public DateTime CreateDate { get; set; }
         public string FilSize { get; set; }
     }

}
