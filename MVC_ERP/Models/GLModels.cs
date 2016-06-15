using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;





namespace MVC_ERP.Models
{
   
    public class GL_FinancialYears
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id {get;set;}
        public int MemberShipId { get; set; }
        public string FinancialYear { get; set; }
        public UserControl_GHDate_NotNull StartDate { get; set; }
        public UserControl_GHDate_NotNull EndDate { get; set; }    
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

        [ForeignKey("FinancialYearId")]
        public List<GL_FinancialYears_Periods> GL_FinancialYears_Periods { get; set; }
    }
    public class GL_FinancialYears_Periods
    {
        [Key]
        public int Id { get; set; }
        public int MemberShipId { get; set; }
        public int FinancialYearId { get; set; }
        public string Code { get; set; }
          [Required(ErrorMessage = "Name required")]
        public string NameA { get; set; }
         [Required(ErrorMessage = "Name required")]
        public string NameE { get; set; }
          [DataType(DataType.Date)]
        public DateTime PeriodStartDate_Gregi { get; set; }
        [DefaultValue("1")]
        public string PeriodStartDate_Higri { get; set; }
          [DataType(DataType.Date)]
        public DateTime PeriodEndDate_Gregi { get; set; }
        public string PeriodEndDate_Higri { get; set; }
        public bool Closed { get; set; }
        public string Notes { get; set; }

       

    }
    public class GL_ChartOfCostCenter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int MemberShipId { get; set; }
        [ForeignKey("CompanyId")]
        public Sys_Companies Sys_Companies { get; set; }
        [Required]
        public int CompanyId { get; set; }
        public string Code { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string NameA { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string NameE { get; set; }
        public int? ParentId { get; set; }
        public Boolean HasChildren { get; set; }
        public int Levels { get; set; }
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
    public class GL_ChartOfAccounts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int MemberShipId { get; set; }
        [ForeignKey("CompanyId")]
        public Sys_Companies Sys_Companies { get; set; }
        [Required]
        public int CompanyId { get; set; }
        public string Code { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string NameA { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string NameE { get; set; }
        public int? ParentId { get; set; }
        public bool HasChildren { get; set; }
        public int AccType { get; set; }  // 1 Balance  2- Incom
        public int Levels { get; set; }
        public int AccNature { get; set; }
        [ForeignKey("CurrencyId")]
        public Sys_Currencies Sys_Currencies { get; set; }
        public int? CurrencyId { get; set; }
        [ForeignKey("CostCenterId")]
        public GL_ChartOfCostCenter GL_ChartOfCostCenter { get; set; }
        public int? CostCenterId { get; set; }
        public bool IsSecret { get; set; }
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
    public class GL_JournalTypes
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
        public Boolean AllowManuallyUse { get; set; }
        public Boolean IsDefault { get; set; }
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
    public class GL_Banks
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
        public string SwiftCode { get; set; }
        public string WebSite { get; set; }
        public string EMail { get; set; }
        public string UnifiedPhone { get; set; }
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

        [ForeignKey("BankId")]
        public List<GL_BankAccounts> GL_BankAccounts { get; set; }

    }
    public class GL_BankAccounts
    {
        [Key]
        public int Id { get; set; }
        public int MemberShipId { get; set; }
        public int BankId { get; set; }
        [ForeignKey("CompanyId")]
        public Sys_Companies Sys_Companies { get; set; }

        public int CompanyId { get; set; }
        [NotMapped]
        public string CompanyName { get; set; }
      
        [ForeignKey("AccountId")]
        public GL_ChartOfAccounts GL_ChartOfAccounts { get; set; }
        public int AccountId { get; set; }
        [NotMapped]
        public string AccountName { get; set; }
        [ForeignKey("CurrencyId")]
        public Sys_Currencies Sys_Currencies { get; set; }
        public int CurrencyId { get; set; }
        [NotMapped]
        public string CurrencyName { get; set; }
        public string BankBranchCode { get; set; }
        public string Iban { get; set; }
        [ForeignKey("CountryId")]
        public Sys_Countries Sys_Countries { get; set; }
        public int? CountryId { get; set; }
        [NotMapped]
        public string CountryName { get; set; }
        [ForeignKey("CityId")]
        public Sys_Cities Sys_Cities { get; set; }
        public int? CityId { get; set; }
        [NotMapped]
        public string CityName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Notes { get; set; }
      

    }
    public class GL_DailyVoucher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int MemberShipId { get; set; }
        [ForeignKey("CompanyId")]
        public Sys_Companies Sys_Companies { get; set; }
        public int CompanyId { get; set; }
        [ForeignKey("BranchId")]
        public Sys_Branches Sys_Branches { get; set; }
        public int BranchId { get; set; }
        [ForeignKey("FinancialYearId")]
        public GL_FinancialYears GL_FinancialYears { get; set; }
        public int? FinancialYearId { get; set; }
        [ForeignKey("JournalId")]
        public GL_JournalTypes GL_JournalTypes { get; set; }
        public int? JournalId { get; set; }
        //[NotMapped]
        //public string JournalName { get; set; }
        public string Code { get; set; }
        public UserControl_GHDate_NotNull TransactDate { get; set; }
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        public int Source { get; set; }
        [NotMapped]
        public string SourceName { get; set; }
        public int SourceId { get; set; }
        public string SourceNo { get; set; }
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

        [ForeignKey("DailyVoucherId")]
        public List<GL_DailyVoucher_DTL> GL_DailyVoucher_DTL { get; set; }

    }
    public class GL_DailyVoucher_DTL
    {
        [Key]
        public int Id { get; set; }
        public int DailyVoucherId { get; set; }
      
        [ForeignKey("AccountId")]
        public GL_ChartOfAccounts GL_ChartOfAccounts { get; set; }
        public int AccountId { get; set; }
        [NotMapped]
        public string AccountName { get; set; }
        [ForeignKey("CurrencyId")]
        public Sys_Currencies Sys_Currencies { get; set; }
        public int CurrencyId { get; set; }
        [NotMapped]
        public string CurrencyName { get; set; }
        public decimal Rate { get; set; }
          [DataType(DataType.Date)]
        public DateTime TransactDate_Gregi { get; set; }
        public string TransactDate_Hijri { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal DebitLocal { get; set; }
        public decimal CreditLocal { get; set; }
        public string Notes { get; set; }

        [ForeignKey("DailyVoucherRowId")]
        public List<GL_DailyVoucher_CostCenter> GL_DailyVoucher_CostCenter { get; set; }
      

    }
    public class GL_DailyVoucher_CostCenter
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("DailyVoucherId")]
        public GL_DailyVoucher GL_DailyVoucher { get; set; }
        public int DailyVoucherId { get; set; }
        public int DailyVoucherRowId { get; set; }
        public DateTime TransactDate_Gregi { get; set; }
        public string TransactDate_Hijri { get; set; }
        [ForeignKey("CostCenterId")]
        public GL_ChartOfCostCenter GL_ChartOfCostCenter { get; set; }
        public int? CostCenterId { get; set; }
        [NotMapped]
        public string CostCenterName { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal DebitLocal { get; set; }
        public decimal CreditLocal { get; set; }
        public string Notes { get; set; }


    }
    public class GL_EstimateBudget
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [ForeignKey("FinancialYearId")]
        public GL_FinancialYears GL_FinancialYears { get; set; }
        public int FinancialYearId { get; set; }
        public int MemberShipId { get; set; }
        [ForeignKey("CompanyId")]
        public Sys_Companies Sys_Companies { get; set; }
        public int CompanyId { get; set; }
        [ForeignKey("BranchId")]
        public Sys_Branches Sys_Branches { get; set; }
        public int BranchId { get; set; }
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
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

        [ForeignKey("EstimateBudgetId")]
        public List<GL_EstimateBudget_DTL> GL_EstimateBudget_DTL { get; set; }

    }
    public class GL_EstimateBudget_DTL
    {
        [Key]
        public int Id { get; set; }
        public int EstimateBudgetId { get; set; }
        [ForeignKey("AccountId")]
        public GL_ChartOfAccounts GL_ChartOfAccounts { get; set; }
        public int AccountId { get; set; }
        [NotMapped]
        public string AccountName { get; set; }
        [ForeignKey("CurrencyId")]
        public Sys_Currencies Sys_Currencies { get; set; }
        public int CurrencyId { get; set; }
        [NotMapped]
        public string CurrencyName { get; set; }
        public decimal Rate { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal DebitLocal { get; set; }
        public decimal CreditLocal { get; set; }
        public string Notes { get; set; }

        [ForeignKey("EstimateBudgetRowId")]
        public List<GL_EstimateBudget_Periods> GL_EstimateBudget_Periods { get; set; }


    }
    public class GL_EstimateBudget_Periods
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("EstimateBudgetId")]
        public GL_EstimateBudget GL_EstimateBudget { get; set; }
        public int EstimateBudgetId { get; set; }
        public int EstimateBudgetRowId { get; set; }
        [ForeignKey("PeriodId")]
        public GL_FinancialYears_Periods GL_FinancialYears_Periods { get; set; }
        public int PeriodId { get; set; }
        [NotMapped]
        public string PeriodName { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal DebitLocal { get; set; }
        public decimal CreditLocal { get; set; }
        public string Notes { get; set; }


    }
    public class GL_PayVoucher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int MemberShipId { get; set; }
        [ForeignKey("CompanyId")]
        public Sys_Companies Sys_Companies { get; set; }
        public int CompanyId { get; set; }
        [ForeignKey("BranchId")]
        public Sys_Branches Sys_Branches { get; set; }
        public int BranchId { get; set; }
        [ForeignKey("FinancialYearId")]
        public GL_FinancialYears GL_FinancialYears { get; set; }
        public int FinancialYearId { get; set; }
        public int Type { get; set; } // 1 General  2- Supplier Payment 3-Supplier Prepaid 4- Prepaid expenses 
        public int PayWay { get; set; }  // 1 Cash 2 check 3 Transfer
        public string Code { get; set; }
        public string PaperCode { get; set; }
        public string CodePerPayWay { get; set; }
        public UserControl_GHDate_NotNull TransactDate { get; set; }
      
        public string RecieptPerson { get; set; }
        public string RecieptPersonTel { get; set; }
        public string RecieptPersonAddress { get; set; }
        [ForeignKey("CreditAccountId")]
        public GL_ChartOfAccounts GL_ChartOfAccounts { get; set; }
        public int CreditAccountId { get; set; }
        [ForeignKey("BankId")]
        public GL_Banks GL_Banks { get; set; }
        public int? BankId { get; set; }
        public string CheckTransferNo { get; set; }
        public UserControl_GHDate_Null CheckTransferDate { get; set; }
        public string Beneficiary { get; set; }
        public string City { get; set; }
        public decimal Total { get; set; }
        [ForeignKey("DailyVoucherId")]
        public GL_DailyVoucher GL_DailyVoucher { get; set; }
        public int DailyVoucherId { get; set; }
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

        [ForeignKey("PayVoucherId")]
        public List<GL_PayVoucher_DTL> GL_PayVoucher_DTL { get; set; }
        

    }
    public class GL_PayVoucher_DTL
    {
        [Key]
        public int Id { get; set; }
        public int PayVoucherId { get; set; }

        [ForeignKey("AccountId")]
        public GL_ChartOfAccounts GL_ChartOfAccounts { get; set; }
        public int AccountId { get; set; }
        [NotMapped]
        public string AccountName { get; set; }
        [ForeignKey("CurrencyId")]
        public Sys_Currencies Sys_Currencies { get; set; }
        public int CurrencyId { get; set; }
        [NotMapped]
        public string CurrencyName { get; set; }
        public decimal Rate { get; set; }
        [DataType(DataType.Date)]
        public DateTime TransactDate_Gregi { get; set; }
        public string TransactDate_Hijri { get; set; }
        public decimal Debit { get; set; }
        public decimal DebitLocal { get; set; }
        public string Notes { get; set; }

        [ForeignKey("PayVoucherRowId")]
        public List<GL_PayVoucher_CostCenter> GL_PayVoucher_CostCenter { get; set; }


    }
    public class GL_PayVoucher_CostCenter
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("PayVoucherId")]
        public GL_PayVoucher GL_PayVoucher { get; set; }
        public int PayVoucherId { get; set; }
        public int PayVoucherRowId { get; set; }
        public DateTime TransactDate_Gregi { get; set; }
        public string TransactDate_Hijri { get; set; }
        [ForeignKey("CostCenterId")]
        public GL_ChartOfCostCenter GL_ChartOfCostCenter { get; set; }
        public int? CostCenterId { get; set; }
        [NotMapped]
        public string CostCenterName { get; set; }
        public decimal Debit { get; set; }
        public decimal DebitLocal { get; set; }
        public string Notes { get; set; }


    }
    public class GL_ReceiptVoucher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int MemberShipId { get; set; }
        [ForeignKey("CompanyId")]
        public Sys_Companies Sys_Companies { get; set; }
        public int CompanyId { get; set; }
        [ForeignKey("BranchId")]
        public Sys_Branches Sys_Branches { get; set; }
        public int BranchId { get; set; }
        [ForeignKey("FinancialYearId")]
        public GL_FinancialYears GL_FinancialYears { get; set; }
        public int FinancialYearId { get; set; }
        public int Type { get; set; } // 1 General  2- Customer Payment 3-Customer Prepaid 
        public int PayWay { get; set; }  // 1 Cash 2 check 3 Transfer
        public string Code { get; set; }
        public string PaperCode { get; set; }
        public string CodePerPayWay { get; set; }
        public UserControl_GHDate_NotNull TransactDate { get; set; }
        public string PayPerson { get; set; }
        public string PayPersonTel { get; set; }
        public string PayPersonAddress { get; set; }
        [ForeignKey("DebitAccountId")]
        public GL_ChartOfAccounts GL_ChartOfAccounts { get; set; }
        public int DebitAccountId { get; set; }
        [ForeignKey("BankId")]
        public GL_Banks GL_Banks { get; set; }
        public int? BankId { get; set; }
        public string CheckTransferNo { get; set; }
        public UserControl_GHDate_Null CheckTransferDate { get; set; }
        public string Beneficiary { get; set; }
        public string City { get; set; }
        public decimal Total { get; set; }
        [ForeignKey("DailyVoucherId")]
        public GL_DailyVoucher GL_DailyVoucher { get; set; }
        public int DailyVoucherId { get; set; }
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

        [ForeignKey("ReceiptVoucherId")]
        public List<GL_ReceiptVoucher_DTL> GL_ReceiptVoucher_DTL { get; set; }


    }
    public class GL_ReceiptVoucher_DTL
    {
        [Key]
        public int Id { get; set; }
        public int ReceiptVoucherId { get; set; }

        [ForeignKey("AccountId")]
        public GL_ChartOfAccounts GL_ChartOfAccounts { get; set; }
        public int AccountId { get; set; }
        [NotMapped]
        public string AccountName { get; set; }
        [ForeignKey("CurrencyId")]
        public Sys_Currencies Sys_Currencies { get; set; }
        public int CurrencyId { get; set; }
        [NotMapped]
        public string CurrencyName { get; set; }
        public decimal Rate { get; set; }
        [DataType(DataType.Date)]
        public DateTime TransactDate_Gregi { get; set; }
        public string TransactDate_Hijri { get; set; }
        public decimal Credit { get; set; }
        public decimal CreditLocal { get; set; }
        public string Notes { get; set; }

        [ForeignKey("ReceiptVoucherRowId")]
        public List<GL_ReceiptVoucher_CostCenter> GL_ReceiptVoucher_CostCenter { get; set; }


    }
    public class GL_ReceiptVoucher_CostCenter
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ReceiptVoucherId")]
        public GL_ReceiptVoucher GL_ReceiptVoucher { get; set; }
        public int ReceiptVoucherId { get; set; }
        public int ReceiptVoucherRowId { get; set; }
        public DateTime TransactDate_Gregi { get; set; }
        public string TransactDate_Hijri { get; set; }
        [ForeignKey("CostCenterId")]
        public GL_ChartOfCostCenter GL_ChartOfCostCenter { get; set; }
        public int? CostCenterId { get; set; }
        [NotMapped]
        public string CostCenterName { get; set; }
        public decimal Credit { get; set; }
        public decimal CreditLocal { get; set; }
        public string Notes { get; set; }


    }
    public class GL_Setting_Defaults
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int MemberShipId { get; set; }
        [ForeignKey("CompanyId")]
        public Sys_Companies Sys_Companies { get; set; }
        public int CompanyId { get; set; }
        [ForeignKey("BranchId")]
        public Sys_Branches Sys_Branches { get; set; }
        public int BranchId { get; set; }
       
        [ForeignKey("RevenueLossAccountId")]
        public GL_ChartOfAccounts GL_ChartOfAccounts { get; set; }
        public int? RevenueLossAccountId { get; set; }
        [ForeignKey("BoxAccountId")]
        public GL_ChartOfAccounts GL_ChartOfAccountsBox { get; set; }
        public int? BoxAccountId { get; set; }
        public int DailyVoucherSerial { get; set; }  //1 Connecte  2 Seperated from 1 every year
        public int PaymentVoucherSerial { get; set; }  //1 Connecte  2 Seperated from 1 every year
        public int ReceiptVoucherSerial { get; set; }  //1 Connecte  2 Seperated from 1 every year
        public bool HidDetailDate { get; set; }
        public string AssetsAccountIds { get; set; }
        public string LiabilitiesAccountIds { get; set; }
        public string ExpensesAccountIds { get; set; }
        public string RevenueAccountIds { get; set; }
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
    public class GL_Setting_VoucherDirection
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int MemberShipId { get; set; }
        [ForeignKey("CompanyId")]
        public Sys_Companies Sys_Companies { get; set; }
        public int CompanyId { get; set; }
        [ForeignKey("BranchId")]
        public Sys_Branches Sys_Branches { get; set; }
        public int BranchId { get; set; }
        public int? VoucherId { get; set; }
        [NotMapped]
        public string VoucherName { get; set; }
        [ForeignKey("JournalId")]
        public GL_JournalTypes GL_JournalTypes { get; set; }
        public int? JournalId { get; set; }
        [NotMapped]
        public string JournalName { get; set; }
        public string Notes { get; set; }
      
    
    }

    //Add object to data
    public class GL_AddProcedureAndViews
    {
        public static string[] GLAddProcedureAndViews = new string[]
        {
            //fn_Split
            " CREATE  FUNCTION [dbo].[fn_Split](@text varchar(max), @delimiter varchar(20) = ' ') " +
            " RETURNS @Strings TABLE (position int IDENTITY PRIMARY KEY,value varchar(8000)  ) " +
            " AS BEGIN DECLARE @index int SET @index = -1 WHILE (LEN(@text) > 0) " +
            " BEGIN SET @index = CHARINDEX(@delimiter , @text)  IF (@index = 0) AND (LEN(@text) > 0)  " +
            " BEGIN  INSERT INTO @Strings VALUES (@text) BREAK  END " +
            " IF (@index > 1)  BEGIN  INSERT INTO @Strings VALUES (LEFT(@text, @index - 1))   SET @text = RIGHT(@text, (LEN(@text) - @index)) END  " +
            " ELSE SET @text = RIGHT(@text, (LEN(@text) - @index)) END RETURN " +
            " END  ",

            //GLView_ChartOfAccounts
            " CREATE VIEW [dbo].[GLView_ChartOfAccounts] AS " +
            " SELECT        Id, MemberShipId, CompanyId, Code, NameA, NameE, ParentId, HasChildren, AccType, Levels, AccNature, CurrencyId, CostCenterId, IsSecret, Notes, " +
            "             Create_Uid, Create_Date, Write_Uid, Write_Date, Post, Post_Uid, Post_Date, Deleted, Delete_Uid, Delete_Date, Parent1, Parent1NameA, Parent1NameE, " +
            "             Parent2, Parent2NameA, Parent2NameE, Parent3, Parent3NameA, Parent3NameE, Parent4, Parent4NameA, Parent4NameE, Parent5, Parent5NameA, " +
            "             Parent5NameE, Parent6, Parent6NameA, Parent6NameE, Parent7, Parent7NameA, Parent7NameE, Parent8, Parent8NameA, Parent8NameE, Parent9, " +
            "             Parent9NameA, Parent9NameE " +
            " FROM            (SELECT        dbo.GL_ChartOfAccounts.Id, dbo.GL_ChartOfAccounts.MemberShipId, dbo.GL_ChartOfAccounts.CompanyId, dbo.GL_ChartOfAccounts.Code, " +
            "                                        dbo.GL_ChartOfAccounts.NameA, dbo.GL_ChartOfAccounts.NameE, dbo.GL_ChartOfAccounts.ParentId, dbo.GL_ChartOfAccounts.HasChildren, " +
            "                                        dbo.GL_ChartOfAccounts.AccType, dbo.GL_ChartOfAccounts.Levels, dbo.GL_ChartOfAccounts.AccNature, dbo.GL_ChartOfAccounts.CurrencyId, " +
            "                                        dbo.GL_ChartOfAccounts.CostCenterId, dbo.GL_ChartOfAccounts.IsSecret, dbo.GL_ChartOfAccounts.Notes, dbo.GL_ChartOfAccounts.Create_Uid, " +
            "                                        dbo.GL_ChartOfAccounts.Create_Date, dbo.GL_ChartOfAccounts.Write_Uid, dbo.GL_ChartOfAccounts.Write_Date, dbo.GL_ChartOfAccounts.Post, " +
            "                                        dbo.GL_ChartOfAccounts.Post_Uid, dbo.GL_ChartOfAccounts.Post_Date, dbo.GL_ChartOfAccounts.Deleted, dbo.GL_ChartOfAccounts.Delete_Uid, " +
            "                                        dbo.GL_ChartOfAccounts.Delete_Date, " +
            "                                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_1.Code WHEN 9 THEN GL_ChartOfAccounts_2.Code WHEN 8 THEN GL_ChartOfAccounts_3.Code " +
            "                                         WHEN 7 THEN GL_ChartOfAccounts_4.Code WHEN 6 THEN GL_ChartOfAccounts_5.Code WHEN 5 THEN GL_ChartOfAccounts_6.Code WHEN 4 THEN " +
            "                                         GL_ChartOfAccounts_7.Code WHEN 3 THEN GL_ChartOfAccounts_8.Code WHEN 2 THEN GL_ChartOfAccounts_9.Code WHEN 1 THEN '0' END AS " +
            "                                         Parent1,  " +
            "                                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_1.NameA WHEN 9 THEN GL_ChartOfAccounts_2.NameA WHEN 8 THEN GL_ChartOfAccounts_3.NameA " +
            "                                         WHEN 7 THEN GL_ChartOfAccounts_4.NameA WHEN 6 THEN GL_ChartOfAccounts_5.NameA WHEN 5 THEN GL_ChartOfAccounts_6.NameA WHEN " +
            "                                         4 THEN GL_ChartOfAccounts_7.NameA WHEN 3 THEN GL_ChartOfAccounts_8.NameA WHEN 2 THEN GL_ChartOfAccounts_9.NameA WHEN 1 THEN " +
            "                                         '' END AS Parent1NameA, " +
            "                                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_1.NameE WHEN 9 THEN GL_ChartOfAccounts_2.NameE WHEN 8 THEN GL_ChartOfAccounts_3.NameE " +
            "                                         WHEN 7 THEN GL_ChartOfAccounts_4.NameE WHEN 6 THEN GL_ChartOfAccounts_5.NameE WHEN 5 THEN GL_ChartOfAccounts_6.NameE WHEN " +
            "                                         4 THEN GL_ChartOfAccounts_7.NameE WHEN 3 THEN GL_ChartOfAccounts_8.NameE WHEN 2 THEN GL_ChartOfAccounts_9.NameE WHEN 1 THEN " +
            "                                         '' END AS Parent1NameE,  " +
            "                                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_2.Code WHEN 9 THEN GL_ChartOfAccounts_3.Code WHEN 8 THEN GL_ChartOfAccounts_4.Code " +
            "                                         WHEN 7 THEN GL_ChartOfAccounts_5.Code WHEN 6 THEN GL_ChartOfAccounts_6.Code WHEN 5 THEN GL_ChartOfAccounts_7.Code WHEN 4 THEN " +
            "                                         GL_ChartOfAccounts_8.Code WHEN 3 THEN GL_ChartOfAccounts_9.Code WHEN 2 THEN '0' WHEN 1 THEN '0' END AS Parent2, " +
            "                                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_2.NameA WHEN 9 THEN GL_ChartOfAccounts_3.NameA WHEN 8 THEN GL_ChartOfAccounts_4.NameA " +
            "                                         WHEN 7 THEN GL_ChartOfAccounts_5.NameA WHEN 6 THEN GL_ChartOfAccounts_6.NameA WHEN 5 THEN GL_ChartOfAccounts_7.NameA WHEN " +
            "                                         4 THEN GL_ChartOfAccounts_8.NameA WHEN 3 THEN GL_ChartOfAccounts_9.NameA WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent2NameA, " +
            "                                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_2.NameE WHEN 9 THEN GL_ChartOfAccounts_3.NameE WHEN 8 THEN GL_ChartOfAccounts_4.NameE " +
            "                                         WHEN 7 THEN GL_ChartOfAccounts_5.NameE WHEN 6 THEN GL_ChartOfAccounts_6.NameE WHEN 5 THEN GL_ChartOfAccounts_7.NameE WHEN " +
            "                                         4 THEN GL_ChartOfAccounts_8.NameE WHEN 3 THEN GL_ChartOfAccounts_9.NameE WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent2NameE, " +
            "                                         CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_3.Code WHEN 9 THEN GL_ChartOfAccounts_4.Code WHEN 8 THEN GL_ChartOfAccounts_5.Code " +
            "                                         WHEN 7 THEN GL_ChartOfAccounts_6.Code WHEN 6 THEN GL_ChartOfAccounts_7.Code WHEN 5 THEN GL_ChartOfAccounts_8.Code WHEN 4 THEN " +
            "                                         GL_ChartOfAccounts_9.Code WHEN 3 THEN '0' WHEN 2 THEN '0' WHEN 1 THEN '0' END AS Parent3, " +
            "                                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_3.NameA WHEN 9 THEN GL_ChartOfAccounts_4.NameA WHEN 8 THEN GL_ChartOfAccounts_5.NameA " +
            "                                         WHEN 7 THEN GL_ChartOfAccounts_6.NameA WHEN 6 THEN GL_ChartOfAccounts_7.NameA WHEN 5 THEN GL_ChartOfAccounts_8.NameA WHEN " +
            "                                         4 THEN GL_ChartOfAccounts_9.NameA WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent3NameA, " +
            "                                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_3.NameE WHEN 9 THEN GL_ChartOfAccounts_4.NameE WHEN 8 THEN GL_ChartOfAccounts_5.NameE " +
            "                                         WHEN 7 THEN GL_ChartOfAccounts_6.NameE WHEN 6 THEN GL_ChartOfAccounts_7.NameE WHEN 5 THEN GL_ChartOfAccounts_8.NameE WHEN " +
            "                                         4 THEN GL_ChartOfAccounts_9.NameE WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent3NameE, " +
            "                                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_4.Code WHEN 9 THEN GL_ChartOfAccounts_5.Code WHEN 8 THEN GL_ChartOfAccounts_6.Code " +
            "                                         WHEN 7 THEN GL_ChartOfAccounts_7.Code WHEN 6 THEN GL_ChartOfAccounts_8.Code WHEN 5 THEN GL_ChartOfAccounts_9.Code WHEN 4 THEN " +
            "                                         '0' WHEN 3 THEN '0' WHEN 2 THEN '0' WHEN 1 THEN '0' END AS Parent4, " +
            "                                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_4.NameA WHEN 9 THEN GL_ChartOfAccounts_5.NameA WHEN 8 THEN GL_ChartOfAccounts_6.NameA " +
            "                                         WHEN 7 THEN GL_ChartOfAccounts_7.NameA WHEN 6 THEN GL_ChartOfAccounts_8.NameA WHEN 5 THEN GL_ChartOfAccounts_9.NameA WHEN " +
            "                                         4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent4NameA, " +
            "                                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_4.NameE WHEN 9 THEN GL_ChartOfAccounts_5.NameE WHEN 8 THEN GL_ChartOfAccounts_6.NameE " +
            "                                         WHEN 7 THEN GL_ChartOfAccounts_7.NameE WHEN 6 THEN GL_ChartOfAccounts_8.NameE WHEN 5 THEN GL_ChartOfAccounts_9.NameE WHEN " +
            "                                         4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent4NameE, " +
            "                                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_5.Code WHEN 9 THEN GL_ChartOfAccounts_6.Code WHEN 8 THEN GL_ChartOfAccounts_7.Code " +
            "                                         WHEN 7 THEN GL_ChartOfAccounts_8.Code WHEN 6 THEN GL_ChartOfAccounts_9.Code WHEN 5 THEN '0' WHEN 4 THEN '0' WHEN 3 THEN '0' WHEN " +
            "                                         2 THEN '0' WHEN 1 THEN '0' END AS Parent5, " +
            "                                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_5.NameA WHEN 9 THEN GL_ChartOfAccounts_6.NameA WHEN 8 THEN GL_ChartOfAccounts_7.NameA " +
            "                                         WHEN 7 THEN GL_ChartOfAccounts_8.NameA WHEN 6 THEN GL_ChartOfAccounts_9.NameA WHEN 5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN " +
            "                                         2 THEN '' WHEN 1 THEN '' END AS Parent5NameA, " +
            "                                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_5.NameE WHEN 9 THEN GL_ChartOfAccounts_6.NameE WHEN 8 THEN GL_ChartOfAccounts_7.NameE " +
            "                                         WHEN 7 THEN GL_ChartOfAccounts_8.NameE WHEN 6 THEN GL_ChartOfAccounts_9.NameE WHEN 5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' " +
            "                                         WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent5NameE, " +
            "                                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_6.Code WHEN 9 THEN GL_ChartOfAccounts_7.Code WHEN 8 THEN GL_ChartOfAccounts_8.Code " +
            "                                         WHEN 7 THEN GL_ChartOfAccounts_9.Code WHEN 6 THEN '0' WHEN 5 THEN '0' WHEN 4 THEN '0' WHEN 3 THEN '0' WHEN 2 THEN '0' WHEN 1 " +
            "                                         THEN '0' END AS Parent6, " +
            "                                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_6.NameA WHEN 9 THEN GL_ChartOfAccounts_7.NameA WHEN 8 THEN GL_ChartOfAccounts_8.NameA " +
            "                                         WHEN 7 THEN GL_ChartOfAccounts_9.NameA WHEN 6 THEN '' WHEN 5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN " +
            "                                         '' END AS Parent6NameA, " +
            "                                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_6.NameE WHEN 9 THEN GL_ChartOfAccounts_7.NameE WHEN 8 THEN GL_ChartOfAccounts_8.NameE " +
            "                                         WHEN 7 THEN GL_ChartOfAccounts_9.NameE WHEN 6 THEN '' WHEN 5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN " +
            "                                         '' END AS Parent6NameE, " +
            "                                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_7.Code WHEN 9 THEN GL_ChartOfAccounts_8.Code WHEN 8 THEN GL_ChartOfAccounts_9.Code " +
            "                                         WHEN 7 THEN '0' WHEN 6 THEN '0' WHEN 5 THEN '0' WHEN 4 THEN '0' WHEN 3 THEN '0' WHEN 2 THEN '0' WHEN 1 THEN '0' END AS Parent7, " +
            "                                         CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_7.NameA WHEN 9 THEN GL_ChartOfAccounts_8.NameA WHEN 8 THEN GL_ChartOfAccounts_9.NameA " +
            "                                         WHEN 7 THEN '' WHEN 6 THEN '' WHEN 5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent7NameA, " +
            "                                         CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_7.NameE WHEN 9 THEN GL_ChartOfAccounts_8.NameE WHEN 8 THEN GL_ChartOfAccounts_9.NameE " +
            "                                         WHEN 7 THEN '' WHEN 6 THEN '' WHEN 5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent7NameE, " +
            "                                         CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_8.Code WHEN 9 THEN GL_ChartOfAccounts_9.Code WHEN 8 THEN '0' WHEN " +
            "                                         7 THEN '0' WHEN 6 THEN '0' WHEN 5 THEN '0' WHEN 4 THEN '0' WHEN 3 THEN '0' WHEN 2 THEN '0' WHEN 1 THEN '0' END AS Parent8, " +
            "                                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_8.NameA WHEN 9 THEN GL_ChartOfAccounts_9.NameA WHEN 8 THEN '' " +
            "                                         WHEN 7 THEN '' WHEN 6 THEN '' WHEN 5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent8NameA, " +
            "                                         CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_8.NameE WHEN 9 THEN GL_ChartOfAccounts_9.NameE WHEN 8 THEN '' " +
            "                                         WHEN 7 THEN '' WHEN 6 THEN '' WHEN 5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent8NameE, " +
            "                                         CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_9.Code WHEN 9 THEN '0' WHEN 8 THEN '0' WHEN 7 THEN '0' WHEN 6 THEN " +
            "                                         '0' WHEN 5 THEN '0' WHEN 4 THEN '0' WHEN 3 THEN '0' WHEN 2 THEN '0' WHEN 1 THEN '0' END AS Parent9,  " +
            "                                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_9.NameA WHEN 9 THEN '' WHEN 8 THEN '' WHEN 7 THEN '' WHEN 6 THEN " +
            "                                         '' WHEN 5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent9NameA, " +
            "                                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_9.NameE WHEN 9 THEN '' WHEN 8 THEN '' WHEN 7 THEN '' WHEN 6 THEN " +
            "                                         '' WHEN 5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent9NameE " +
            "               FROM            dbo.GL_ChartOfAccounts LEFT OUTER JOIN " +
            "                                        dbo.GL_ChartOfAccounts AS GL_ChartOfAccounts_9 ON dbo.GL_ChartOfAccounts.ParentId = GL_ChartOfAccounts_9.Id LEFT OUTER JOIN " +
            "                                        dbo.GL_ChartOfAccounts AS GL_ChartOfAccounts_8 ON GL_ChartOfAccounts_9.ParentId = GL_ChartOfAccounts_8.Id LEFT OUTER JOIN " +
            "                                        dbo.GL_ChartOfAccounts AS GL_ChartOfAccounts_7 ON GL_ChartOfAccounts_8.ParentId = GL_ChartOfAccounts_7.Id LEFT OUTER JOIN " +
            "                                        dbo.GL_ChartOfAccounts AS GL_ChartOfAccounts_6 ON GL_ChartOfAccounts_7.ParentId = GL_ChartOfAccounts_6.Id LEFT OUTER JOIN " +
            "                                        dbo.GL_ChartOfAccounts AS GL_ChartOfAccounts_5 ON GL_ChartOfAccounts_6.ParentId = GL_ChartOfAccounts_5.Id LEFT OUTER JOIN " +
            "                                        dbo.GL_ChartOfAccounts AS GL_ChartOfAccounts_4 ON GL_ChartOfAccounts_5.ParentId = GL_ChartOfAccounts_4.Id LEFT OUTER JOIN " +
            "                                        dbo.GL_ChartOfAccounts AS GL_ChartOfAccounts_3 ON GL_ChartOfAccounts_4.ParentId = GL_ChartOfAccounts_3.Id LEFT OUTER JOIN " +
            "                                        dbo.GL_ChartOfAccounts AS GL_ChartOfAccounts_2 ON GL_ChartOfAccounts_3.ParentId = GL_ChartOfAccounts_2.Id LEFT OUTER JOIN " +
            "                                        dbo.GL_ChartOfAccounts AS GL_ChartOfAccounts_1 ON GL_ChartOfAccounts_2.ParentId = GL_ChartOfAccounts_1.Id) AS Gl_Accounttree " ,


            //GLView_ChartOfCostCenter

            " CREATE VIEW [dbo].[GLView_ChartOfCostCenter] AS" +
            " SELECT        dbo.GL_ChartOfCostCenter.Id, dbo.GL_ChartOfCostCenter.MemberShipId, dbo.GL_ChartOfCostCenter.CompanyId, dbo.GL_ChartOfCostCenter.Code, " +
            "             dbo.GL_ChartOfCostCenter.NameA, dbo.GL_ChartOfCostCenter.NameE, dbo.GL_ChartOfCostCenter.ParentId, dbo.GL_ChartOfCostCenter.HasChildren, " +
            "             dbo.GL_ChartOfCostCenter.Levels, dbo.GL_ChartOfCostCenter.Notes, dbo.GL_ChartOfCostCenter.Create_Uid, dbo.GL_ChartOfCostCenter.Create_Date, " +
            "             dbo.GL_ChartOfCostCenter.Write_Uid, dbo.GL_ChartOfCostCenter.Write_Date, dbo.GL_ChartOfCostCenter.Post, dbo.GL_ChartOfCostCenter.Post_Uid, " +
            "             dbo.GL_ChartOfCostCenter.Post_Date, dbo.GL_ChartOfCostCenter.Deleted, dbo.GL_ChartOfCostCenter.Delete_Uid, dbo.GL_ChartOfCostCenter.Delete_Date, " +
            "             CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_1.Code WHEN 9 THEN GL_ChartOfCostCenter_2.Code WHEN 8 THEN GL_ChartOfCostCenter_3.Code" +
            "              WHEN 7 THEN GL_ChartOfCostCenter_4.Code WHEN 6 THEN GL_ChartOfCostCenter_5.Code WHEN 5 THEN GL_ChartOfCostCenter_6.Code WHEN 4 THEN GL_ChartOfCostCenter_7.Code" +
            "              WHEN 3 THEN GL_ChartOfCostCenter_8.Code WHEN 2 THEN GL_ChartOfCostCenter_9.Code WHEN 1 THEN 0 END AS Parent1, " +
            "             CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_1.NameA WHEN 9 THEN GL_ChartOfCostCenter_2.NameA WHEN 8 THEN GL_ChartOfCostCenter_3.NameA" +
            "              WHEN 7 THEN GL_ChartOfCostCenter_4.NameA WHEN 6 THEN GL_ChartOfCostCenter_5.NameA WHEN 5 THEN GL_ChartOfCostCenter_6.NameA WHEN 4 THEN" +
            "              GL_ChartOfCostCenter_7.NameA WHEN 3 THEN GL_ChartOfCostCenter_8.NameA WHEN 2 THEN GL_ChartOfCostCenter_9.NameA WHEN 1 THEN '' END AS Parent1NameA," +
            "              CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_1.NameE WHEN 9 THEN GL_ChartOfCostCenter_2.NameE WHEN 8 THEN GL_ChartOfCostCenter_3.NameE" +
            "              WHEN 7 THEN GL_ChartOfCostCenter_4.NameE WHEN 6 THEN GL_ChartOfCostCenter_5.NameE WHEN 5 THEN GL_ChartOfCostCenter_6.NameE WHEN 4 THEN" +
            "              GL_ChartOfCostCenter_7.NameE WHEN 3 THEN GL_ChartOfCostCenter_8.NameE WHEN 2 THEN GL_ChartOfCostCenter_9.NameE WHEN 1 THEN '' END AS Parent1NameE," +
            "              CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_2.Code WHEN 9 THEN GL_ChartOfCostCenter_3.Code WHEN 8 THEN GL_ChartOfCostCenter_4.Code" +
            "              WHEN 7 THEN GL_ChartOfCostCenter_5.Code WHEN 6 THEN GL_ChartOfCostCenter_6.Code WHEN 5 THEN GL_ChartOfCostCenter_7.Code WHEN 4 THEN GL_ChartOfCostCenter_8.Code" +
            "              WHEN 3 THEN GL_ChartOfCostCenter_9.Code WHEN 2 THEN 0 WHEN 1 THEN 0 END AS Parent2, " +
            "             CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_2.NameA WHEN 9 THEN GL_ChartOfCostCenter_3.NameA WHEN 8 THEN GL_ChartOfCostCenter_4.NameA" +
            "              WHEN 7 THEN GL_ChartOfCostCenter_5.NameA WHEN 6 THEN GL_ChartOfCostCenter_6.NameA WHEN 5 THEN GL_ChartOfCostCenter_7.NameA WHEN 4 THEN" +
            "              GL_ChartOfCostCenter_8.NameA WHEN 3 THEN GL_ChartOfCostCenter_9.NameA WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent2NameA, " +
            "             CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_2.NameE WHEN 9 THEN GL_ChartOfCostCenter_3.NameE WHEN 8 THEN GL_ChartOfCostCenter_4.NameE" +
            "              WHEN 7 THEN GL_ChartOfCostCenter_5.NameE WHEN 6 THEN GL_ChartOfCostCenter_6.NameE WHEN 5 THEN GL_ChartOfCostCenter_7.NameE WHEN 4 THEN" +
            "              GL_ChartOfCostCenter_8.NameE WHEN 3 THEN GL_ChartOfCostCenter_9.NameE WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent2NameE, " +
            "             CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_3.Code WHEN 9 THEN GL_ChartOfCostCenter_4.Code WHEN 8 THEN GL_ChartOfCostCenter_5.Code" +
            "              WHEN 7 THEN GL_ChartOfCostCenter_6.Code WHEN 6 THEN GL_ChartOfCostCenter_7.Code WHEN 5 THEN GL_ChartOfCostCenter_8.Code WHEN 4 THEN GL_ChartOfCostCenter_9.Code" +
            "              WHEN 3 THEN 0 WHEN 2 THEN 0 WHEN 1 THEN 0 END AS Parent3, " +
            "             CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_3.NameA WHEN 9 THEN GL_ChartOfCostCenter_4.NameA WHEN 8 THEN GL_ChartOfCostCenter_5.NameA" +
            "              WHEN 7 THEN GL_ChartOfCostCenter_6.NameA WHEN 6 THEN GL_ChartOfCostCenter_7.NameA WHEN 5 THEN GL_ChartOfCostCenter_8.NameA WHEN 4 THEN" +
            "              GL_ChartOfCostCenter_9.NameA WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent3NameA, " +
            "             CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_3.NameE WHEN 9 THEN GL_ChartOfCostCenter_4.NameE WHEN 8 THEN GL_ChartOfCostCenter_5.NameE" +
            "              WHEN 7 THEN GL_ChartOfCostCenter_6.NameE WHEN 6 THEN GL_ChartOfCostCenter_7.NameE WHEN 5 THEN GL_ChartOfCostCenter_8.NameE WHEN 4 THEN" +
            "              GL_ChartOfCostCenter_9.NameE WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent3NameE, " +
            "             CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_4.Code WHEN 9 THEN GL_ChartOfCostCenter_5.Code WHEN 8 THEN GL_ChartOfCostCenter_6.Code" +
            "              WHEN 7 THEN GL_ChartOfCostCenter_7.Code WHEN 6 THEN GL_ChartOfCostCenter_8.Code WHEN 5 THEN GL_ChartOfCostCenter_9.Code WHEN 4 THEN 0 WHEN" +
            "              3 THEN 0 WHEN 2 THEN 0 WHEN 1 THEN 0 END AS Parent4, " +
            "             CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_4.NameA WHEN 9 THEN GL_ChartOfCostCenter_5.NameA WHEN 8 THEN GL_ChartOfCostCenter_6.NameA" +
            "              WHEN 7 THEN GL_ChartOfCostCenter_7.NameA WHEN 6 THEN GL_ChartOfCostCenter_8.NameA WHEN 5 THEN GL_ChartOfCostCenter_9.NameA WHEN 4 THEN" +
            "              '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent4NameA, " +
            "             CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_4.NameE WHEN 9 THEN GL_ChartOfCostCenter_5.NameE WHEN 8 THEN GL_ChartOfCostCenter_6.NameE" +
            "              WHEN 7 THEN GL_ChartOfCostCenter_7.NameE WHEN 6 THEN GL_ChartOfCostCenter_8.NameE WHEN 5 THEN GL_ChartOfCostCenter_9.NameE WHEN 4 THEN" +
            "              '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent4NameE, " +
            "             CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_5.Code WHEN 9 THEN GL_ChartOfCostCenter_6.Code WHEN 8 THEN GL_ChartOfCostCenter_7.Code" +
            "              WHEN 7 THEN GL_ChartOfCostCenter_8.Code WHEN 6 THEN GL_ChartOfCostCenter_9.Code WHEN 5 THEN 0 WHEN 4 THEN 0 WHEN 3 THEN 0 WHEN 2 THEN" +
            "              0 WHEN 1 THEN 0 END AS Parent5, " +
            "             CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_5.NameA WHEN 9 THEN GL_ChartOfCostCenter_6.NameA WHEN 8 THEN GL_ChartOfCostCenter_7.NameA" +
            "              WHEN 7 THEN GL_ChartOfCostCenter_8.NameA WHEN 6 THEN GL_ChartOfCostCenter_9.NameA WHEN 5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2" +
            "              THEN '' WHEN 1 THEN '' END AS Parent5NameA, " +
            "             CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_5.NameE WHEN 9 THEN GL_ChartOfCostCenter_6.NameE WHEN 8 THEN GL_ChartOfCostCenter_7.NameE" +
            "              WHEN 7 THEN GL_ChartOfCostCenter_8.NameE WHEN 6 THEN GL_ChartOfCostCenter_9.NameE WHEN 5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2" +
            "              THEN '' WHEN 1 THEN '' END AS Parent5NameE, " +
            "             CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_6.Code WHEN 9 THEN GL_ChartOfCostCenter_7.Code WHEN 8 THEN GL_ChartOfCostCenter_8.Code" +
            "              WHEN 7 THEN GL_ChartOfCostCenter_9.Code WHEN 6 THEN 0 WHEN 5 THEN 0 WHEN 4 THEN 0 WHEN 3 THEN 0 WHEN 2 THEN 0 WHEN 1 THEN 0 END AS" +
            "              Parent6, " +
            "             CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_6.NameA WHEN 9 THEN GL_ChartOfCostCenter_7.NameA WHEN 8 THEN GL_ChartOfCostCenter_8.NameA" +
            "              WHEN 7 THEN GL_ChartOfCostCenter_9.NameA WHEN 6 THEN '' WHEN 5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS" +
            "              Parent6NameA, " +
            "             CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_6.NameE WHEN 9 THEN GL_ChartOfCostCenter_7.NameE WHEN 8 THEN GL_ChartOfCostCenter_8.NameE" +
            "              WHEN 7 THEN GL_ChartOfCostCenter_9.NameE WHEN 6 THEN '' WHEN 5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS" +
            "              Parent6NameE, " +
            "             CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_7.Code WHEN 9 THEN GL_ChartOfCostCenter_8.Code WHEN 8 THEN GL_ChartOfCostCenter_9.Code" +
            "              WHEN 7 THEN 0 WHEN 6 THEN 0 WHEN 5 THEN 0 WHEN 4 THEN 0 WHEN 3 THEN 0 WHEN 2 THEN 0 WHEN 1 THEN 0 END AS Parent7, " +
            "             CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_7.NameA WHEN 9 THEN GL_ChartOfCostCenter_8.NameA WHEN 8 THEN GL_ChartOfCostCenter_9.NameA" +
            "              WHEN 7 THEN '' WHEN 6 THEN '' WHEN 5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent7NameA, " +
            "             CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_7.NameE WHEN 9 THEN GL_ChartOfCostCenter_8.NameE WHEN 8 THEN GL_ChartOfCostCenter_9.NameE" +
            "              WHEN 7 THEN '' WHEN 6 THEN '' WHEN 5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent7NameE, " +
            "             CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_8.Code WHEN 9 THEN GL_ChartOfCostCenter_9.Code WHEN 8 THEN 0 WHEN 7 THEN" +
            "              0 WHEN 6 THEN 0 WHEN 5 THEN 0 WHEN 4 THEN 0 WHEN 3 THEN 0 WHEN 2 THEN 0 WHEN 1 THEN 0 END AS Parent8, " +
            "             CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_8.NameA WHEN 9 THEN GL_ChartOfCostCenter_9.NameA WHEN 8 THEN '' WHEN" +
            "              7 THEN '' WHEN 6 THEN '' WHEN 5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent8NameA, " +
            "             CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_8.NameE WHEN 9 THEN GL_ChartOfCostCenter_9.NameE WHEN 8 THEN '' WHEN" +
            "              7 THEN '' WHEN 6 THEN '' WHEN 5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent8NameE, " +
            "             CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_9.Code WHEN 9 THEN 0 WHEN 8 THEN 0 WHEN 7 THEN 0 WHEN 6 THEN 0 WHEN" +
            "              5 THEN 0 WHEN 4 THEN 0 WHEN 3 THEN 0 WHEN 2 THEN 0 WHEN 1 THEN 0 END AS Parent9, " +
            "             CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_9.NameA WHEN 9 THEN '' WHEN 8 THEN '' WHEN 7 THEN '' WHEN 6 THEN '' WHEN" +
            "              5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent9NameA, " +
            "             CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_9.NameE WHEN 9 THEN '' WHEN 8 THEN '' WHEN 7 THEN '' WHEN 6 THEN '' WHEN" +
            "              5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent9NameE" +
            " FROM            dbo.GL_ChartOfCostCenter LEFT OUTER JOIN" +
            "             dbo.GL_ChartOfCostCenter AS GL_ChartOfCostCenter_9 ON dbo.GL_ChartOfCostCenter.ParentId = GL_ChartOfCostCenter_9.Id LEFT OUTER JOIN" +
            "             dbo.GL_ChartOfCostCenter AS GL_ChartOfCostCenter_8 ON GL_ChartOfCostCenter_9.ParentId = GL_ChartOfCostCenter_8.Id LEFT OUTER JOIN" +
            "             dbo.GL_ChartOfCostCenter AS GL_ChartOfCostCenter_7 ON GL_ChartOfCostCenter_8.ParentId = GL_ChartOfCostCenter_7.Id LEFT OUTER JOIN" +
            "             dbo.GL_ChartOfCostCenter AS GL_ChartOfCostCenter_6 ON GL_ChartOfCostCenter_7.ParentId = GL_ChartOfCostCenter_6.Id LEFT OUTER JOIN" +
            "             dbo.GL_ChartOfCostCenter AS GL_ChartOfCostCenter_5 ON GL_ChartOfCostCenter_6.ParentId = GL_ChartOfCostCenter_5.Id LEFT OUTER JOIN" +
            "             dbo.GL_ChartOfCostCenter AS GL_ChartOfCostCenter_4 ON GL_ChartOfCostCenter_5.ParentId = GL_ChartOfCostCenter_4.Id LEFT OUTER JOIN" +
            "             dbo.GL_ChartOfCostCenter AS GL_ChartOfCostCenter_3 ON GL_ChartOfCostCenter_4.ParentId = GL_ChartOfCostCenter_3.Id LEFT OUTER JOIN" +
            "             dbo.GL_ChartOfCostCenter AS GL_ChartOfCostCenter_2 ON GL_ChartOfCostCenter_3.ParentId = GL_ChartOfCostCenter_2.Id LEFT OUTER JOIN" +
            "             dbo.GL_ChartOfCostCenter AS GL_ChartOfCostCenter_1 ON GL_ChartOfCostCenter_2.ParentId = GL_ChartOfCostCenter_1.Id ",


            //Create Type CodeName_Type
            "CREATE TYPE CodeName_Type AS TABLE " +
            "( Id int , " +
            "NameE VARCHAR(50)," +
            "NameA  VARCHAR(50) );",

            //GL_DailyVoucher_Search_SP
            " CREATE PROCEDURE [dbo].[GL_DailyVoucher_Search_SP] @SourceTable CodeName_Type READONLY,@Lang nvarchar(50), @CompanyId int,@BranchId int,@FinancialYearId int,@TxtSearchVal nvarchar(100)  AS " +
            " SELECT TOP (50) GL_DailyVoucher.Id, GL_DailyVoucher.Code, GL_JournalTypes.NameE as JournalName, GL_DailyVoucher.TransactDate_Gregi, GL_DailyVoucher.Notes, GL_DailyVoucher.SourceNo, GL_DailyVoucher.TotalDebit,Source.NameE as SourceName" +
            " FROM   GL_DailyVoucher INNER JOIN GL_JournalTypes ON GL_DailyVoucher.JournalId = GL_JournalTypes.Id " +
			" INNER JOIN @SourceTable AS Source ON GL_DailyVoucher.Source =  Source.Id " +
            " where GL_DailyVoucher.CompanyId=@CompanyId and GL_DailyVoucher.BranchId=@BranchId and isnull(GL_DailyVoucher.Deleted,0)=0  and " +
            " (GL_DailyVoucher.Code=@TxtSearchVal or CONVERT(varchar(10), TransactDate_Gregi, 103) like '%'+ @TxtSearchVal + '%'   or " +
            " GL_JournalTypes.NameE like '%'+ @TxtSearchVal + '%'  or  Source.NameE like '%'+ @TxtSearchVal + '%'   or GL_DailyVoucher.SourceNo =@TxtSearchVal or GL_DailyVoucher.Notes like '%'+ @TxtSearchVal + '%'  or  CONVERT(nvarchar(50),cast(GL_DailyVoucher.TotalDebit as float))= @TxtSearchVal) " +
            " order by GL_DailyVoucher.JournalId,GL_DailyVoucher.Id",

             ////GL_PeriodAccountBalance_SP
             //" CREATE PROCEDURE [dbo].[GL_PeriodAccountBalance_SP] @CompanyId int,@BranchId nvarchar(max),@AccountId int,@FinancialYearId int AS BEGIN " +
             //" declare @PeriodTbl table ( idx int identity(1,1), Id int,Code nvarchar(50) ,NameE nvarchar(50),NameA nvarchar(50), FinancialYearId int,PeriodStartDate_Gregi date,PeriodEndDate_Gregi date) " +
             //" declare @ResulTbl table ( AccountId int , AccountCode nvarchar(50),NameE nvarchar(50),NameA nvarchar(50),PeriodNameE nvarchar(50),PeriodNameA nvarchar(50),Debit float,Credit float,Balance float ) " +
             //" declare @PeriodRowsCount  int declare @CurrentRow     int declare @PeriodFirstDate     date declare @PeriodLastDate     date declare @NameE as nvarchar(50) declare @NameA as nvarchar(50) " +
             //" --First Balance " +
             //" insert into @ResulTbl ( AccountId  , AccountCode,NameE ,NameA ,PeriodNameE ,PeriodNameA ,Debit ,Credit ,Balance  ) " +
             //" select AccountId,AccountCode,NameE,NameA,PeriodNameE,PeriodNameA,sum(Debit) as Debit,sum(Credit) as Credit,sum(Debit)-sum(Credit) as Balance " +
             //" from( " +
             //" SELECT        GL_DailyVoucher_DTL.AccountId, GL_ChartOfAccounts.Code as AccountCode, GL_ChartOfAccounts.NameA, GL_ChartOfAccounts.NameE,'First Balance' as PeriodNameE,'رصيد سابق' as PeriodNameA, GL_DailyVoucher_DTL.DebitLocal as Debit, " +
             //" GL_DailyVoucher_DTL.CreditLocal as Credit " +
             //" FROM            GL_DailyVoucher INNER JOIN GL_DailyVoucher_DTL ON GL_DailyVoucher.Id = GL_DailyVoucher_DTL.DailyVoucherId INNER JOIN GL_ChartOfAccounts ON GL_DailyVoucher_DTL.AccountId = GL_ChartOfAccounts.Id " +
             //" where GL_DailyVoucher_DTL.AccountId=@AccountId and GL_DailyVoucher.CompanyId in (@CompanyId) and GL_DailyVoucher.BranchId in (SELECT Value FROM dbo.fn_Split(@BranchId,',')) and GL_DailyVoucher_DTL.TransactDate_Gregi<(select StartDate_Gregi from GL_FinancialYears where Id= @FinancialYearId) " +
             //" ) as Tbl group by  AccountId,AccountCode,NameE,NameA,PeriodNameE,PeriodNameA " +
             //" --Period Balance " +
             //" INSERT INTO @PeriodTbl (Id ,Code  ,NameE ,NameA , FinancialYearId ,PeriodStartDate_Gregi ,PeriodEndDate_Gregi ) " +
             //" SELECT Id ,Code  ,NameE ,NameA , FinancialYearId ,PeriodStartDate_Gregi ,PeriodEndDate_Gregi from GL_FinancialYears_Periods where FinancialYearId=@FinancialYearId " +
             //" --Get Period Count " +
             //" SET @PeriodRowsCount=@@ROWCOUNT SET @CurrentRow=0 WHILE @CurrentRow<@PeriodRowsCount " +
             //" BEGIN SET @CurrentRow=@CurrentRow+1 SELECT   @NameE=NameE, @NameA=NameA,  @PeriodFirstDate=PeriodStartDate_Gregi, @PeriodLastDate=PeriodEndDate_Gregi FROM @PeriodTbl WHERE idx=@CurrentRow " +
             //" insert into @ResulTbl ( AccountId  , AccountCode,NameE ,NameA ,PeriodNameE ,PeriodNameA ,Debit ,Credit ,Balance  ) " +
             //" select AccountId,AccountCode,NameE,NameA,PeriodNameE,PeriodNameA,sum(isnull(Debit,0)) as Debit,sum(isnull(Credit,0)) as Credit,sum(isnull(Debit,0))-sum(isnull(Credit,0)) as Balance " +
             //" from( " +
             //" SELECT        GL_ChartOfAccounts.Id as AccountId, GL_ChartOfAccounts.Code as AccountCode, GL_ChartOfAccounts.NameA, GL_ChartOfAccounts.NameE,@NameE as PeriodNameE,@NameA as PeriodNameA, DailyVoucher.DebitLocal as Debit, " +
             //" DailyVoucher.CreditLocal as Credit, DailyVoucher.CompanyId, DailyVoucher.BranchId " +
             //" FROM            (SELECT        GL_DailyVoucher_DTL.AccountId, GL_DailyVoucher_DTL.DebitLocal, GL_DailyVoucher_DTL.CreditLocal, GL_DailyVoucher.CompanyId, " +
             //"            GL_DailyVoucher.BranchId, GL_DailyVoucher_DTL.TransactDate_Gregi " +
             //" FROM            GL_DailyVoucher INNER JOIN GL_DailyVoucher_DTL ON GL_DailyVoucher.Id = GL_DailyVoucher_DTL.DailyVoucherId " +
             //"			 	WHERE GL_DailyVoucher.CompanyId IN (@CompanyId) AND GL_DailyVoucher.BranchId IN (SELECT Value FROM dbo.fn_Split(@BranchId,',')) AND  " +
             //"            GL_DailyVoucher_DTL.TransactDate_Gregi BETWEEN @PeriodFirstDate AND @PeriodLastDate	 " +
             //"			 ) AS DailyVoucher RIGHT OUTER JOIN GL_ChartOfAccounts ON DailyVoucher.AccountId = GL_ChartOfAccounts.Id where GL_ChartOfAccounts.Id = @AccountId " +
             //" ) as Tbl group by  AccountId,AccountCode,NameE,NameA,PeriodNameE,PeriodNameA END select * from  @ResulTbl END ",

             //GL_BudjetFollow_SP
             " CREATE PROCEDURE [dbo].[GL_BudjetFollow_SP] @CompanyId int, @BranchId nvarchar(max), @FinancialYearId int   " +
             " declare @PeriodTbl table ( idx int identity(1,1), Id int,Code nvarchar(50) ,NameE nvarchar(50),NameA nvarchar(50), FinancialYearId int,PeriodStartDate_Gregi date,PeriodEndDate_Gregi date)  " +
             " declare @ResulTbl table ( AccountId int , AccountCode nvarchar(50),NameE nvarchar(50),NameA nvarchar(50),PeriodId int,PeriodNameE nvarchar(50),PeriodNameA nvarchar(50),ActualBalance float,EstimateBalance float,CompleteRate float )  " +
             " declare @PeriodRowsCount  int declare @CurrentRow     int declare @PeriodFirstDate     date declare @PeriodLastDate     date declare @NameE as nvarchar(50) declare @NameA as nvarchar(50) declare @PeriodId     int  " +
             " --Period Balance  " +
             " INSERT INTO @PeriodTbl (Id ,Code  ,NameE ,NameA , FinancialYearId ,PeriodStartDate_Gregi ,PeriodEndDate_Gregi )  " +
             " SELECT Id ,Code  ,NameE ,NameA , FinancialYearId ,PeriodStartDate_Gregi ,PeriodEndDate_Gregi from GL_FinancialYears_Periods where FinancialYearId=@FinancialYearId  " +
             " --Get Period Count  " +
             " SET @PeriodRowsCount=@@ROWCOUNT SET @CurrentRow=0  " +
             " WHILE @CurrentRow<@PeriodRowsCount BEGIN  " +
             " SET @CurrentRow=@CurrentRow+1  " +
             " SELECT @PeriodId=Id, @NameE=NameE, @NameA=NameA, @PeriodFirstDate=PeriodStartDate_Gregi, @PeriodLastDate=PeriodEndDate_Gregi FROM @PeriodTbl  WHERE idx=@CurrentRow  " +
             " insert into @ResulTbl ( AccountId  , AccountCode,NameE ,NameA ,PeriodId,PeriodNameE ,PeriodNameA ,ActualBalance ,EstimateBalance,CompleteRate )  " +
             " select AccountIds,AccountCode,NameE,NameA,PeriodId,PeriodNameE,PeriodNameA,isnull(ActualBalance,0),isnull(EstimateBalance,0),case when isnull(EstimateBalance,0)=0 then 0 else isnull(ActualBalance,0)/isnull(EstimateBalance,0) end  " +
             " from(  " +
             " select AccountIds,AccountCode,NameE,NameA,PeriodId,PeriodNameE,PeriodNameA,sum(isnull(Debit,0))-sum(isnull(Credit,0)) as ActualBalance,  " +
             " (SELECT        SUM(isnull(GL_EstimateBudget_Periods.DebitLocal,0)) - sum(isnull(GL_EstimateBudget_Periods.CreditLocal,0)) as EstimateBalance  " +
             " FROM            GL_EstimateBudget INNER JOIN  " +
             "            GL_EstimateBudget_DTL ON GL_EstimateBudget.Id = GL_EstimateBudget_DTL.EstimateBudgetId INNER JOIN  " +
             "            GL_EstimateBudget_Periods ON GL_EstimateBudget.Id = GL_EstimateBudget_Periods.EstimateBudgetId AND   " +
             "            GL_EstimateBudget_DTL.Id = GL_EstimateBudget_Periods.EstimateBudgetRowId  " +
			 "			 where CompanyId in(@CompanyId) and BranchId in (SELECT Value FROM dbo.fn_Split(@BranchId,',')) and AccountId =AccountIds and PeriodId=@PeriodId) as EstimateBalance  " +
             " from(  " +
             " SELECT        GL_ChartOfAccounts.Id as AccountIds, GL_ChartOfAccounts.Code as AccountCode, GL_ChartOfAccounts.NameA, GL_ChartOfAccounts.NameE,@PeriodId as PeriodId ,@NameE as PeriodNameE,@NameA as PeriodNameA, DailyVoucher.DebitLocal as Debit,  " +
             "            DailyVoucher.CreditLocal as Credit, DailyVoucher.CompanyId, DailyVoucher.BranchId  " +
             " FROM            (SELECT        GL_DailyVoucher_DTL.AccountId, GL_DailyVoucher_DTL.DebitLocal, GL_DailyVoucher_DTL.CreditLocal, GL_DailyVoucher.CompanyId,  " +
             "            GL_DailyVoucher.BranchId, GL_DailyVoucher_DTL.TransactDate_Gregi  " +
             " FROM            GL_DailyVoucher INNER JOIN  " +
             "            GL_DailyVoucher_DTL ON GL_DailyVoucher.Id = GL_DailyVoucher_DTL.DailyVoucherId  " +
			 "			 	WHERE GL_DailyVoucher.CompanyId IN (@CompanyId) AND GL_DailyVoucher.BranchId IN (SELECT Value FROM dbo.fn_Split(@BranchId,',')) AND   " +
             "            cast(GL_DailyVoucher_DTL.TransactDate_Gregi as date) BETWEEN @PeriodFirstDate AND @PeriodLastDate	 " +
			 "			 ) AS DailyVoucher RIGHT OUTER JOIN  " +
			 "			 GL_ChartOfAccounts  ON DailyVoucher.AccountId = GL_ChartOfAccounts.Id  " +
			 "			where GL_ChartOfAccounts.Id in(select distinct GL_EstimateBudget_DTL.AccountId from  GL_EstimateBudget_DTL inner join GL_EstimateBudget on GL_EstimateBudget_DTL.EstimateBudgetId =GL_EstimateBudget.Id  " +
			 "			 where GL_EstimateBudget.CompanyId in(@CompanyId) and GL_EstimateBudget.BranchId in (@BranchId) and GL_EstimateBudget.FinancialYearId=@FinancialYearId)  " +
             " ) as Tbl group by  AccountIds,AccountCode,NameE,NameA,PeriodId,PeriodNameE,PeriodNameA) as TBL2 END   " +
             "select * from  @ResulTbl order by AccountCode,PeriodId  "

          
        
        
        };

      

    }
    
     
          
    
}
