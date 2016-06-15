using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MVC_ERP.Models
{
    public class Sys_Companies
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
        public string RegisterNo { get; set; }
        public Nullable<DateTime>EstablishDate{ get; set; }
        public string WorkActivity { get; set; }
        public string Address { get; set; }
        public string WebSite { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string BankAccount { get; set; }
        [ForeignKey("CurrencyId")]
        public Sys_Currencies Sys_Currencies { get; set; }
        public int? CurrencyId { get; set; }
        public byte[] Logo { get; set; }
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

    }
    public class Sys_Branches
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int MemberShipId { get; set; }

        [ForeignKey("CompanyId")]
        public Sys_Companies Sys_Companies { get; set; }

        [Required]
        public   int CompanyId { get; set; }
        public string Code { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string NameA { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string NameE { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string BankAccount { get; set; }
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

       
    }
    public class Sys_Currencies
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int MemberShipId { get; set; }
        public string Code { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string NameA { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string NameE { get; set; }
        public string SymbolA { get; set; }
        public string SymbolE { get; set; }

        [Required(ErrorMessage = "Check local required")]
        public Boolean BaseCurrency { get; set; }

        [DisplayFormat(DataFormatString = "{0:F3}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Rate required")]
        public decimal Rate { get; set; }
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
        [ForeignKey("CurrencyId")]
        public List<Sys_Currencies_Rates> Sys_Currencies_Rates { get; set; }
       
    }
    public class Sys_Currencies_Rates
    {
        [Key]
        public int Id { get; set; }
        public int CurrencyId { get; set; }
        [Required(ErrorMessage = "Date required")]
        [DataType(DataType.Date)]
        public DateTime ChangeDate { get; set; }
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Rate required")]
        public decimal Rate { get; set; }
    }
    public class Sys_Countries
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int MemberShipId { get; set; }
        public string Code { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string NameA { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string NameE { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string NationalityA { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string NationalityE { get; set; }
        public string ShortCode { get; set; }
        [ForeignKey("CurrencyId")]
        public Sys_Currencies Sys_Currencies { get; set; }
        public int? CurrencyId { get; set; }
        public string TelKey { get; set; }
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

    }
    public class Sys_Cities
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int MemberShipId { get; set; }
        [ForeignKey("CountryId")]
        public Sys_Countries Sys_Countries { get; set; }
        [Required]
        public int CountryId { get; set; }
        public string Code { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string NameA { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string NameE { get; set; }
        public string ZipCode { get; set; }
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

    }
    public class Sys_Activities
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
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

    }
    public class Sys_PartnerGroups
    {
    
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int MemberShipId { get; set; }

        [ForeignKey("CompanyId")]
        public Sys_Companies Sys_Companies { get; set; }

        [Required]
        public int CompanyId { get; set; }
        public int? ParentId { get; set; }
        [Required]
        public int GroupType { get; set; } // 1 Clients  2  Suppliers  3    4 Agents
        public string Code { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string NameA { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string NameE { get; set; }
        [ForeignKey("MainAccountId")]
        public GL_ChartOfAccounts GL_ChartOfAccounts { get; set; }
        public int? MainAccountId { get; set; } // الحساب الرئيسي الذي سيتم فتح حسابات العملاء او الموردين تحتة وبذلك نستطيع تقسيم مجموعات للعملاء والموردين كما نريد بالدليل المحاسبي
        public string Notes { get; set; }
        public Boolean HasSub { get; set; }
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
   
    }
    public class Sys_Partners
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int MemberShipId { get; set; }
        [ForeignKey("CompanyId")]
        public Sys_Companies Sys_Companies { get; set; }
        [Required]
        public int CompanyId { get; set; }
        public int PartnerType { get; set; } // 1 Clients  2  Suppliers  3    4 Agents
        public Boolean IsCustomerSupplier { get; set; }
        public string Code { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string NameA { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string NameE { get; set; }
        [ForeignKey("PartnerGroup_Id")]
        public Sys_PartnerGroups Sys_PartnerGroups { get; set; }
        public int? PartnerGroup_Id { get; set; }
        [ForeignKey("ActivityId")]
        public Sys_Activities Sys_Activities { get; set; }
        public int? ActivityId { get; set; }
        public DateTime? DealDate { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string WebSite { get; set; }
        public string RegisterNo { get; set; }
        public UserControl_GHDate_Null RegisterDates{ get; set; }
        public UserControl_GHDate_Null DealDates { get; set; }
        public DateTime? RegisterEndDate { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime? RegisterEndDateHijri { get; set; }

        [ForeignKey("CountryId")]
        public Sys_Countries Sys_Countries { get; set; }
        public int? CountryId { get; set; }
     
        [ForeignKey("CityId")]
        public Sys_Cities Sys_Cities { get; set; }
        public int? CityId { get; set; }
        public int? Zip { get; set; }
        public int? P_O_Box { get; set; }
        public int AccountId { get; set; }
        [ForeignKey("CurrencyId")]
        public Sys_Currencies Sys_Currencies { get; set; }
        public int? CurrencyId { get; set; }
        public decimal? OpeningBalance { get; set; }
        public int OpeningBalanceNature { get; set; } // 1 debit   2 credit
        public int? DayLimit { get; set; }
        public decimal? MoneyLimit { get; set; }
        public int? MainPartnerId { get; set; }
        public int? SalesDelegatorId { get; set; }
        public string Notes { get; set; }
        public Boolean Stoped { get; set; }
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

    }
    public class Sys_Colors
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
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


    }

}