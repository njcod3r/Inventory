using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.IO;
using System.Linq;
using System.Web;

namespace MVC_ERP.Models
{
    public class MSDBContext : DbContext
    {

        //Adding system tables
        public DbSet<Sys_Companies>Companies{ get; set; }
        public DbSet<Sys_Branches>Branches{ get; set; }
        public DbSet<Sys_Countries>Sys_Countries{ get; set; }
        public DbSet<Sys_Cities>Sys_Cities{ get; set; }
        public DbSet<Sys_Currencies>Sys_Currencies { get; set; }
        public DbSet<Sys_Currencies_Rates> Sys_Currencies_Rates { get; set; }
        public DbSet<Sys_Activities> Sys_Activities { get; set; }
        public DbSet<Sys_PartnerGroups> Sys_PartnerGroups { get; set; }
        public DbSet<Sys_Partners> Sys_Partners { get; set; }
        public DbSet<Sys_Colors> Sys_Colors { get; set; }

        //Adding Privileges tables
        public DbSet<Priv_Menus> Priv_Menus { get; set; }
        public DbSet<Priv_Groups> Priv_Groups { get; set; }
        public DbSet<Priv_Groups_Rights> Priv_Groups_Rights { get; set; }
        public DbSet<Priv_Users> Priv_Users { get; set; }
        public DbSet<Priv_Users_Groups> Priv_Users_Groups { get; set; }
        public DbSet<Priv_Users_CompaniesBranches> Priv_Users_CompaniesBranches { get; set; }
        public DbSet<Priv_Users_FinancialYears> Priv_Users_FinancialYears { get; set; }

        //Adding GL tables
        public DbSet<GL_FinancialYears> GL_FinancialYears { get; set; }
        public DbSet<GL_FinancialYears_Periods> GL_FinancialYears_Periods { get; set; }
        public DbSet<GL_ChartOfCostCenter> GL_ChartOfCostCenter { get; set; }
        public DbSet<GL_ChartOfAccounts> GL_ChartOfAccounts { get; set; }
        public DbSet<GL_JournalTypes> GL_JournalTypes { get; set; }
        public DbSet<GL_Banks> GL_Banks { get; set; }
        public DbSet<GL_BankAccounts> GL_BankAccounts { get; set; }
        public DbSet<GL_DailyVoucher> GL_DailyVoucher { get; set; }
        public DbSet<GL_DailyVoucher_DTL> GL_DailyVoucher_DTL { get; set; }
        public DbSet<GL_DailyVoucher_CostCenter> GL_DailyVoucher_CostCenter { get; set; }
        public DbSet<GL_EstimateBudget> GL_EstimateBudget { get; set; }
        public DbSet<GL_EstimateBudget_DTL> GL_EstimateBudget_DTL { get; set; }
        public DbSet<GL_EstimateBudget_Periods> GL_EstimateBudget_Periods { get; set; }
        public DbSet<GL_PayVoucher> GL_PayVoucher { get; set; }
        public DbSet<GL_PayVoucher_DTL> GL_PayVoucher_DTL { get; set; }
        public DbSet<GL_PayVoucher_CostCenter> GL_PayVoucher_CostCenter { get; set; }
        public DbSet<GL_ReceiptVoucher> GL_ReceiptVoucher { get; set; }
        public DbSet<GL_ReceiptVoucher_DTL> GL_ReceiptVoucher_DTL { get; set; }
        public DbSet<GL_ReceiptVoucher_CostCenter> GL_ReceiptVoucher_CostCenter { get; set; }
        public DbSet<GL_Setting_Defaults> GL_Setting_Defaults { get; set; }
        public DbSet<GL_Setting_VoucherDirection> GL_Setting_VoucherDirection { get; set; }



        //Adding Stock tables
        public DbSet<Stock_Warehouses> Stock_Warehouses { get; set; }
        public DbSet<Stock_Warehouses_Shelves> Stock_Warehouses_Shelves { get; set; }
        public DbSet<Stock_Warehouses_Racks> Stock_Warehouses_Racks { get; set; }
        public DbSet<Stock_Warehouses_RackRows> Stock_Warehouses_RackRows { get; set; }
        public DbSet<Stock_Warehouses_RackCases> Stock_Warehouses_RackCases { get; set; }
        public DbSet<Stock_Units> Stock_Units { get; set; }
        public DbSet<Stock_Brands> Stock_Brands { get; set; }
        public DbSet<Stock_Setting_General> Stock_Setting_General { get; set; }
        public DbSet<Stock_Setting_Accounts> Stock_Setting_Accounts { get; set; }
        public DbSet<Stock_ProductGroups> Stock_ProductGroups { get; set; }
        public DbSet<Stock_Products> Stock_Products { get; set; }
        public DbSet<Stock_Products_Units> Stock_Products_Units { get; set; }
        public DbSet<Stock_Products_Suppliers> Stock_Products_Suppliers { get; set; }
        public DbSet<Stock_Products_Pricing> Stock_Products_Pricing { get; set; }
        public DbSet<Stock_Products_Alternative> Stock_Products_Alternative { get; set; }
        public DbSet<Stock_Products_RecomendeSP> Stock_Products_RecomendeSP { get; set; }
        public DbSet<Stock_Products_SPCollection> Stock_Products_SPCollection { get; set; }
        public DbSet<Stock_Products_DefaultStorageLocation> Stock_Products_DefaultStorageLocation { get; set; }
        public DbSet<Stock_TransInTransOut> Stock_TransInTransOut { get; set; }
        public DbSet<Stock_TransInTransOut_Location> Stock_TransInTransOut_Location { get; set; }
        public DbSet<Stock_TransInTransOut_Serial> Stock_TransInTransOut_Serial { get; set; }
        public DbSet<Stock_AddProperties_Location> Stock_AddProperties_Location { get; set; }
        public DbSet<Stock_AddProperties_Serial> Stock_AddProperties_Serial { get; set; }
        public DbSet<Stock_OpeningBalanceHdr> Stock_OpeningBalanceHdr { get; set; }
        public DbSet<Stock_OpeningBalanceDtl> Stock_OpeningBalanceDtl { get; set; }
     

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
       //  modelBuilder.Entity<Sys_Branches>().HasRequired(t => t.Sys_Companies).WithMany().HasForeignKey(d => d.CompanyId).WillCascadeOnDelete(false) ;
       //  modelBuilder.Entity<Sys_Cities>().HasRequired(t => t.Sys_Countries).WithMany().HasForeignKey(d => d.CountryId).WillCascadeOnDelete(false);
       //  modelBuilder.Entity<Sys_PartnerGroups>().HasRequired(t => t.Sys_Companies).WithMany().HasForeignKey(d => d.CompanyId).WillCascadeOnDelete(false);
       //  modelBuilder.Entity<Sys_Partners>().HasRequired(t => t.Sys_Companies).WithMany().HasForeignKey(d => d.CompanyId).WillCascadeOnDelete(false) ;

       //  //GL Module

       //  //GL_BankAccounts
       //  modelBuilder.Entity<GL_BankAccounts>().HasRequired(t => t.Sys_Companies).WithMany().HasForeignKey(d => d.CompanyId).WillCascadeOnDelete(false);
       //  modelBuilder.Entity<GL_BankAccounts>().HasRequired(t => t.GL_ChartOfAccounts).WithMany().HasForeignKey(d => d.AccountId).WillCascadeOnDelete(false);
       //  modelBuilder.Entity<GL_BankAccounts>().HasRequired(t => t.Sys_Currencies).WithMany().HasForeignKey(d => d.CurrencyId).WillCascadeOnDelete(false);
       //  modelBuilder.Entity<GL_BankAccounts>().HasRequired(t => t.Sys_Countries).WithMany().HasForeignKey(d => d.CountryId).WillCascadeOnDelete(false);
       //  modelBuilder.Entity<GL_BankAccounts>().HasRequired(t => t.Sys_Cities).WithMany().HasForeignKey(d => d.CityId).WillCascadeOnDelete(false);

       //  //GL_ChartOfAccounts
       //  modelBuilder.Entity<GL_ChartOfAccounts>().HasRequired(t => t.Sys_Companies).WithMany().HasForeignKey(d => d.CompanyId).WillCascadeOnDelete(false);


       //  //GL_DailyVoucher
       //  modelBuilder.Entity<GL_DailyVoucher>().HasRequired(t => t.Sys_Companies).WithMany().HasForeignKey(d => d.CompanyId).WillCascadeOnDelete(false);
       //  modelBuilder.Entity<GL_DailyVoucher>().HasRequired(t => t.Sys_Branches).WithMany().HasForeignKey(d => d.BranchId).WillCascadeOnDelete(false);
       //  modelBuilder.Entity<GL_DailyVoucher>().HasRequired(t => t.GL_JournalTypes).WithMany().HasForeignKey(d => d.JournalId).WillCascadeOnDelete(false);
       //  modelBuilder.Entity<GL_DailyVoucher>().HasRequired(t => t.GL_FinancialYears).WithMany().HasForeignKey(d => d.FinancialYearId).WillCascadeOnDelete(false);

       //  //GL_DailyVoucher_DTL
       //  modelBuilder.Entity<GL_DailyVoucher_DTL>().HasRequired(t => t.GL_ChartOfAccounts).WithMany().HasForeignKey(d => d.AccountId).WillCascadeOnDelete(false);
       //  modelBuilder.Entity<GL_DailyVoucher_DTL>().HasRequired(t => t.Sys_Currencies).WithMany().HasForeignKey(d => d.CurrencyId).WillCascadeOnDelete(false);

       //  //GL_DailyVoucher_CostCenter
       //  modelBuilder.Entity<GL_DailyVoucher_CostCenter>().HasRequired(t => t.GL_ChartOfCostCenter).WithMany().HasForeignKey(d => d.CostCenterId).WillCascadeOnDelete(false);


            
       //base.OnModelCreating(modelBuilder);
        }

    }


    public class MenueContextSeedInitializer : CreateDatabaseIfNotExists<MSDBContext>
    {

        protected override void Seed(MSDBContext context)
        {

            context.Priv_Menus.AddRange(StaticLists.AllMenueList);


            //Add Stored related to GL Model
            //for (int i = 0; i < GL_AddProcedureAndViews.GLAddProcedureAndViews.Length; i++)
            //{
            //    context.Database.ExecuteSqlCommand(GL_AddProcedureAndViews.GLAddProcedureAndViews[i]);
            //}

            var SqlFiles = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "\\SqlQuery\\GL", "*.sql");
            foreach(string file in SqlFiles)
            {
                context.Database.ExecuteSqlCommand(File.ReadAllText(file));
            }


            context.SaveChanges();
        }
    }
}