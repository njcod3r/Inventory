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


    public class Stock_Warehouses
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
        public int WarehouseType { get; set; }  //1 -Store 2- Front Store
        public string Adress { get; set; }
        public string Tel { get; set; }
        public string Warehouseman { get; set; }
      
        public int? Height { get; set; }
        public int? Area { get; set; }  //مساحة المخزن بالمتر
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
        [ForeignKey("WarehouseId")]
        public List<Stock_Warehouses_Shelves> Stock_Warehouses_Shelves { get; set; }


    }
    public class Stock_Warehouses_Shelves // الصف بالكامل بكامل طول المخزن
    {
        [Key]
        public int Id { get; set; }
        public int WarehouseId { get; set; }
        public string ShelveNo { get; set; }
        public string Notes { get; set; }

        [ForeignKey("ShelveId")]
        public List<Stock_Warehouses_Racks> Stock_Warehouses_Racks { get; set; }

    }
    public class Stock_Warehouses_Racks //الاعمدة 
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("WarehouseId")]
        public Stock_Warehouses Stock_Warehouses { get; set; }
        public int WarehouseId { get; set; }
        public int ShelveId { get; set; }
        public string RackNo { get; set; }
        public string Notes { get; set; }
        [ForeignKey("RackId")]
        public List<Stock_Warehouses_RackRows> Stock_Warehouses_RackRows { get; set; }

    }
    public class Stock_Warehouses_RackRows  // الصفوف
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("WarehouseId")]
        public Stock_Warehouses Stock_Warehouses { get; set; }
        public int WarehouseId { get; set; }
        [ForeignKey("ShelveId")]
        public Stock_Warehouses_Shelves Stock_Warehouses_Shelves { get; set; }
        public int ShelveId { get; set; }
        public int RackId { get; set; }
        public string RowNo { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public int? Depth { get; set; } 
        public string Notes { get; set; }

        [ForeignKey("RowId")]
        public List<Stock_Warehouses_RackCases> Stock_Warehouses_RackCases { get; set; }

    }
    public class Stock_Warehouses_RackCases   //الادراج
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("WarehouseId")]
        public Stock_Warehouses Stock_Warehouses { get; set; }
        public int WarehouseId { get; set; }
        [ForeignKey("ShelveId")]
        public Stock_Warehouses_Shelves Stock_Warehouses_Shelves { get; set; }
        public int ShelveId { get; set; }
        [ForeignKey("RackId")]
        public Stock_Warehouses_Racks Stock_Warehouses_Racks { get; set; }
        public int RackId { get; set; }
        public int RowId { get; set; }
        public string CaseNo { get; set; }
        public string Notes { get; set; }

    }
    public class Stock_Units
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id {get;set;}
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
    public class Stock_ProductGroups
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
        public string Code { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string NameA { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string NameE { get; set; }
        public string Notes { get; set; }
        public Boolean HasChildren { get; set; }
        public int Levels { get; set; }
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
    public class Stock_Brands
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
    public class Stock_Products
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int MemberShipId { get; set; }
        [ForeignKey("CompanyId")]
        public Sys_Companies Sys_Companies { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [ForeignKey("GroupId")]
        public Stock_ProductGroups Stock_ProductGroups { get; set; }
        [Required]
        public int GroupId { get; set; }
        public string Code { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string NameA { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string NameE { get; set; }
        public int ItemType { get; set; }
        public int CostType { get; set; }
        public Boolean HasValidDate { get; set; }
        public Boolean HasColor { get; set; }
        public Boolean HasSerial { get; set; }
        public Boolean SerialNeedInReciept { get; set; }
        public Boolean SerialNeedInDeliver { get; set; }
        public decimal? MaxLimit { get; set; }
        public decimal? RequireLimit { get; set; }
        public decimal? MinLimit { get; set; }
        public decimal? slumpRate { get; set; }
        [ForeignKey("BrandId")]
        public Stock_Brands Stock_Brands { get; set; }
        public int? BrandId { get; set; }
        [NotMapped]
        public string BrandName { get; set; }
        public int? Warranty { get; set; } //Months
        public int? CustomerLeadTime { get; set; } //Days مهلة العملاء
        public bool Active { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public int? Depth { get; set; } 
        public byte[] Image { get; set; }
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
        [ForeignKey("ProductId")]
        public List<Stock_Products_Units> Stock_Products_Units { get; set; }
        [ForeignKey("ProductId")]
        public List<Stock_Products_Suppliers> Stock_Products_Suppliers { get; set; }     
        [ForeignKey("ProductId")]
        public List<Stock_Products_Pricing> Stock_Products_Pricing { get; set; }   
        [ForeignKey("ProductId")]
        public List<Stock_Products_Alternative> Stock_Products_Alternative { get; set; }
        [ForeignKey("ProductId")]
        public List<Stock_Products_RecomendeSP> Stock_Products_RecomendeSP { get; set; }
        [ForeignKey("ProductId")]
        public List<Stock_Products_SPCollection> Stock_Products_SPCollection { get; set; }
       [ForeignKey("ProductId")]
        public List<Stock_Products_DefaultStorageLocation> Stock_Products_DefaultStorageLocation { get; set; }

    }
    public class Stock_Products_Units
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("UnitId")]
        public Stock_Units Stock_Units { get; set; }
        public int UnitId { get; set; }
        [NotMapped]
        public string UnitName { get; set; }
        public bool IsBaseUnit { get; set; }
        public bool IsDefault { get; set; }
        public decimal Rate { get; set; }
        public string BarCode { get; set; }

    }
    public class Stock_Products_Suppliers
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("SupplierId")]
        public Sys_Partners Sys_Partners { get; set; }
        public int SupplierId { get; set; }
        [NotMapped]
        public string SupplierName { get; set; }
        public string ProductCode { get; set; }
        public int? Warranty { get; set; } //Months
        public int? DeliveryLeadTime { get; set; } //days
        public decimal MinimalQuantity { get; set; }
    

    }
    public class Stock_Products_Pricing
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("UnitId")]
        public Stock_Products_Units Stock_Products_Units { get; set; }
        public int UnitId { get; set; }
        [NotMapped]
        public string UnitName { get; set; }
        public int PurchasePolicy { get; set; }
        [NotMapped]
        public string PurchasePolicyName { get; set; }

        [ForeignKey("PCurrencyId")]
        public Sys_Currencies PSys_Currencies { get; set; }
        public int PCurrencyId { get; set; }
        [NotMapped]
        public string PCurrencyName { get; set; }
        public decimal PurchaseValue { get; set; }
        public int SalesPolicy { get; set; }
        [NotMapped]
        public string SalesPolicyName { get; set; }

        [ForeignKey("SCurrencyId")]
        public Sys_Currencies SSys_Currencies { get; set; }
        public int SCurrencyId { get; set; }
        [NotMapped]
        public string SCurrencyName { get; set; }
        public decimal SalesValue { get; set; }
      

    }
    public class Stock_Products_Alternative
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("AlternateId")]
        public Stock_Products Stock_Products { get; set; }
        public int AlternateId { get; set; }
        [NotMapped]
        public string AlternateName { get; set; }
        public string Notes { get; set; }
    


    }
    public class Stock_Products_RecomendeSP  //قطع الغيار الموصى بتواجدها بالمخازن لهذا المنتج اثناء فترة الضمان
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("SPId")]
        public Stock_Products Stock_Products { get; set; }
        public int SPId { get; set; }
        [NotMapped]
        public string SPName { get; set; }
        [ForeignKey("UnitId")]
        public Stock_Products_Units Stock_Products_Units { get; set; }
        public int UnitId { get; set; }
        [NotMapped]
        public string UnitName { get; set; }
        public decimal? Quantity { get; set; }
        public string Notes { get; set; }



    }
    public class Stock_Products_SPCollection  //Product Group     تفاصيل الاصناف المجمعه    
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("SPId")]
        public Stock_Products Stock_Products { get; set; }
        public int SPId { get; set; }
        [NotMapped]
        public string SPName { get; set; }
        [ForeignKey("UnitId")]
        public Stock_Products_Units Stock_Products_Units { get; set; }
        public int UnitId { get; set; }
        [NotMapped]
        public string UnitName { get; set; }
        public decimal? Quantity { get; set; }
        public string Notes { get; set; }



    }
    public class Stock_Products_DefaultStorageLocation  //موقع التخزين الافتراضي للصنف داخل كل مخزن    
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("WarehouseId")]
        public Stock_Warehouses Stock_Warehouses { get; set; }
        public int WarehouseId { get; set; }
        [NotMapped]
        public string WarehouseName { get; set; }
        [ForeignKey("ShelveId")]
        public Stock_Warehouses_Shelves Stock_Warehouses_Shelves { get; set; }
        public int ShelveId { get; set; }
        [NotMapped]
        public string ShelveName { get; set; }
        [ForeignKey("RackId")]
        public Stock_Warehouses_Racks Stock_Warehouses_Racks { get; set; }
        public int? RackId { get; set; }
        [NotMapped]
        public string RackName { get; set; }
        [ForeignKey("RowId")]
        public Stock_Warehouses_RackRows Stock_Warehouses_RackRows { get; set; }
        public int? RowId { get; set; }
        [NotMapped]
        public string RowName { get; set; }
        [ForeignKey("CaseId")]
        public Stock_Warehouses_RackCases Stock_Warehouses_RackCases { get; set; }
        public int? CaseId { get; set; }
        [NotMapped]
        public string CaseName { get; set; }
        public string Notes { get; set; }



    }
    public class Stock_TransInTransOut
    {
        [Key]
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
        public UserControl_GHDate_NotNull TransactDate { get; set; }
        public int Type { get; set; }  //  ا اضافة  2 صرف
        public int RDId { get; set; } // رقم اذن الصرف او اذن الاضافة -1 فى حالة الارصدة الافتتاحية
        public string RDNo { get; set; } //كود اذن الصرف او اذن الاضافة
        public int Source { get; set; } //المصدر اذا كان الاذن مستقل يتم كتابة رقمة واذا كان من فاتورة مبيعات او مشتريات او امر شراء او مرتجع او تحويل او تجميع اصناف او فك تجميع يتم كتابة ارقامهم
        public int SourceId { get; set; }
        public string SourceNo { get; set; }
        [ForeignKey("WarehouseId")]
        public Stock_Warehouses Stock_Warehouses { get; set; }
        public int WarehouseId { get; set; }
        [ForeignKey("ProductId")]
        public Stock_Products Stock_Products { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("UnitId")]
        public Stock_Units Stock_Units { get; set; }
        public int UnitId { get; set; }
        public decimal UnitRate { get; set; } // معدل التغيير مع الوحدة الرئيسية
        public decimal Quantity { get; set; }
        public decimal UnitCost { get; set; } // سعر التكلفة  
        public decimal TotalCost { get; set; }
        [ForeignKey("BaseUnitId")]
        public Stock_Units BaseStock_Units { get; set; }
        public int BaseUnitId { get; set; }
        public decimal BaseQuantity { get; set; }
        public decimal BaseUnitCost { get; set; }
        //بيانات تخص الفواتير التى تخص هذا السطر سواء كان المصدر يعتمد على الفواتير او لا وذلك لمعرفة ما الذى تم توريدة من امر شراء على سبيل المثال
        public int? PurchaseOrderId { get; set; }
        public int? PurchaseInvoiceId { get; set; }
        public int? ReturnPurchaseInvoiceId { get; set; }
        public int? SalesOrderId { get; set; }
        public int? SalesInvoiceId { get; set; }
        public int? ReturnSalesInvoiceId { get; set; }

        [ForeignKey("DTLRowId")]
        public List<Stock_TransInTransOut_Location> Stock_TransInTransOut_Location { get; set; }
        [ForeignKey("DTLRowId")]
        public List<Stock_TransInTransOut_Serial> Stock_TransInTransOut_Serial { get; set; }
      
    }
    public class Stock_TransInTransOut_Location
    {
        [Key]
        public int Id { get; set; }
        public int Source { get; set; } //المصدر اذا كان الاذن مستقل يتم كتابة رقمة واذا كان من فاتورة مبيعات او مشتريات او امر شراء او مرتجع يتم كتابة ارقامهم
        public int SourceId { get; set; }
        public string SourceNo { get; set; }
        public int DTLRowId { get; set; }

        [ForeignKey("ShelveId")]
        public Stock_Warehouses_Shelves Stock_Warehouses_Shelves { get; set; }
        public int ShelveId { get; set; }
        [NotMapped]
        public string ShelveName { get; set; }
        [ForeignKey("RackId")]
        public Stock_Warehouses_Racks Stock_Warehouses_Racks { get; set; }
        public int? RackId { get; set; }
        [NotMapped]
        public string RackName { get; set; }
        [ForeignKey("RowId")]
        public Stock_Warehouses_RackRows Stock_Warehouses_RackRows { get; set; }
        public int? RowId { get; set; }
        [NotMapped]
        public string RowName { get; set; }
        [ForeignKey("CaseId")]
        public Stock_Warehouses_RackCases Stock_Warehouses_RackCases { get; set; }
        public int? CaseId { get; set; }
        [NotMapped]
        public string CaseName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ValidDate { get; set; }
        [ForeignKey("ColorId")]
        public Sys_Colors Sys_Colors { get; set; }
        public int? ColorId { get; set; }
        [NotMapped]
        public string ColorName { get; set; }
        public decimal Quantity { get; set; }
        public decimal BaseQuantity { get; set; }
        public string Notes { get; set; }



    }
    public class Stock_TransInTransOut_Serial
    {
        [Key]
        public int Id { get; set; }
        public int Source { get; set; } //المصدر اذا كان الاذن مستقل يتم كتابة رقمة واذا كان من فاتورة مبيعات او مشتريات او امر شراء او مرتجع يتم كتابة ارقامهم
        public int SourceId { get; set; }
        public string SourceNo { get; set; }
        public int DTLRowId { get; set; }
        public string Serial { get; set; }
    }
    //هذة الجداول لادخال تفاصيل الاصناف فى اى شاشة ادخال او اخراج اصناف
    public class Stock_AddProperties_Location
    {
        [Key]
        public int Id { get; set; }
        public int Source { get; set; } //المصدر اذا كان الاذن مستقل يتم كتابة رقمة واذا كان من فاتورة مبيعات او مشتريات او امر شراء او مرتجع يتم كتابة ارقامهم
        public int SourceId { get; set; }
        public string SourceNo { get; set; }
        public int? DTLRowIdOpeningBalance { get; set; }
        public int? DTLRowIdRecieptOrder { get; set; }
        [ForeignKey("ShelveId")]
        public Stock_Warehouses_Shelves Stock_Warehouses_Shelves { get; set; }
        public int ShelveId { get; set; }
        [NotMapped]
        public string ShelveName { get; set; }
        [ForeignKey("RackId")]
        public Stock_Warehouses_Racks Stock_Warehouses_Racks { get; set; }
        public int? RackId { get; set; }
        [NotMapped]
        public string RackName { get; set; }
        [ForeignKey("RowId")]
        public Stock_Warehouses_RackRows Stock_Warehouses_RackRows { get; set; }
        public int? RowId { get; set; }
        [NotMapped]
        public string RowName { get; set; }
        [ForeignKey("CaseId")]
        public Stock_Warehouses_RackCases Stock_Warehouses_RackCases { get; set; }
        public int? CaseId { get; set; }
        [NotMapped]
        public string CaseName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ValidDate { get; set; }
        [ForeignKey("ColorId")]
        public Sys_Colors Sys_Colors { get; set; }
        public int? ColorId { get; set; }
        [NotMapped]
        public string ColorName { get; set; }
        public decimal Quantity { get; set; }
        public string Notes { get; set; }
      


    }
    public class Stock_AddProperties_Serial
      {
     [Key]
     public int Id { get; set; }
     public int Source { get; set; } //المصدر اذا كان الاذن مستقل يتم كتابة رقمة واذا كان من فاتورة مبيعات او مشتريات او امر شراء او مرتجع يتم كتابة ارقامهم
     public int SourceId { get; set; }
     public string SourceNo { get; set; }
     public int? DTLRowIdOpeningBalance { get; set; }
     public int? DTLRowIdRecieptOrder { get; set; }
     public string Serial { get; set; }
     }
    public class Stock_OpeningBalanceHdr
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
        [ForeignKey("WarehouseId")]
        public Stock_Warehouses Stock_Warehouses { get; set; }
        public int WarehouseId { get; set; }
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

        [ForeignKey("MasterId")]
        public List<Stock_OpeningBalanceDtl> Stock_OpeningBalanceDtl { get; set; }


    }
    public class Stock_OpeningBalanceDtl
    {
        [Key]
        public int Id { get; set; }
        public int MasterId { get; set; }
        [ForeignKey("ProductId")]
        public Stock_Products Stock_Products { get; set; }
        public int ProductId { get; set; }
        [NotMapped]
        public string ProductName { get; set; }
        [ForeignKey("UnitId")]
        public Stock_Units Stock_Units { get; set; }
        public int UnitId { get; set; }
        [NotMapped]
        public string UnitName { get; set; }
        public decimal UnitRate { get; set; } // معدل التغيير مع الوحدة الرئيسية
        public decimal Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal TotalCost { get; set; }
        [ForeignKey("BaseUnitId")]
        public Stock_Units BaseStock_Units { get; set; }
        public int BaseUnitId { get; set; }
        public decimal BaseQuantity { get; set; }
        public decimal BaseUnitCost { get; set; }
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

        [ForeignKey("DTLRowIdOpeningBalance")]
        public List<Stock_AddProperties_Location> Stock_AddProperties_Location { get; set; }
        [ForeignKey("DTLRowIdOpeningBalance")]
        public List<Stock_AddProperties_Serial> Stock_AddProperties_Serial { get; set; }

    }
    public class Stock_RecieptOrderHdr
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
        [ForeignKey("WarehouseId")]
        public Stock_Warehouses Stock_Warehouses { get; set; }
        public int WarehouseId { get; set; }
        public string Code { get; set; }
        public UserControl_GHDate_NotNull TransactDate { get; set; }
        public int Source { get; set; } //  بدون 1 امر شراء 2 فاتورة مشتريات 3 مرتجع مبيعات 4 اذن تحويل 5   جرد مخزون 6 
        public int? SourceId { get; set; }
        public string SourceNo { get; set; }
        [ForeignKey("ProjectId")]
        public ProjMang_Projects ProjMang_Projects { get; set; }
        public int? ProjectId { get; set; }
        public int RecieptFrom { get; set; } //1 supplier   2- customer   3- None
        [ForeignKey("PartnerId")]
        public Sys_Partners Sys_Partners { get; set; }
        public int? PartnerId { get; set; }
        [ForeignKey("DailyVoucherId")]
        public GL_DailyVoucher GL_DailyVoucher { get; set; }
        public int? DailyVoucherId { get; set; }
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

        [ForeignKey("MasterId")]
        public List<Stock_RecieptOrderDtl> Stock_RecieptOrderDtl { get; set; }


    }
    public class Stock_RecieptOrderDtl
    {
        [Key]
        public int Id { get; set; }
        public int MasterId { get; set; }
        [ForeignKey("ProductId")]
        public Stock_Products Stock_Products { get; set; }
        public int ProductId { get; set; }
        [NotMapped]
        public string ProductName { get; set; }
        [ForeignKey("UnitId")]
        public Stock_Units Stock_Units { get; set; }
        public int UnitId { get; set; }
        [NotMapped]
        public string UnitName { get; set; }
        public decimal UnitRate { get; set; } // معدل التغيير مع الوحدة الرئيسية
        public decimal Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal TotalCost { get; set; }
        [ForeignKey("BaseUnitId")]
        public Stock_Units BaseStock_Units { get; set; }
        public int BaseUnitId { get; set; }
        public decimal BaseQuantity { get; set; }
        public decimal BaseUnitCost { get; set; }
        public string Notes { get; set; }
       

        [ForeignKey("DTLRowIdRecieptOrder")]
        public List<Stock_AddProperties_Location> Stock_AddProperties_Location { get; set; }
        [ForeignKey("DTLRowIdRecieptOrder")]
        public List<Stock_AddProperties_Serial> Stock_AddProperties_Serial { get; set; }

    }
    public class Stock_Setting_General
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
        public int AccountBindMethod { get; set; } //الفرع 1  المخزن 2  المجموعة 3 
        [ForeignKey("BoxAccountId")]
        public GL_ChartOfAccounts GL_ChartOfAccountsBox { get; set; }
        public int? BoxAccountId { get; set; }
        [ForeignKey("VisaAccountId")]
        public GL_ChartOfAccounts GL_ChartOfAccountsVisa { get; set; }
        public int? VisaAccountId { get; set; }
        public int StocktackingType { get; set; } // مستمر 1 دورى 2
        public int? ReceiptDeleviryPolicy { get; set; } //  مع الفاتورة 1 قبل الفاتورة 2 بعد الفاتورة 3
        public int? DefaultPayMethod { get; set; } // اجل 1 نقدى 2
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

        [ForeignKey("MasterId")]
        public List<Stock_Setting_Accounts> Stock_Setting_Accounts { get; set; }


    }
    public class Stock_Setting_Accounts
    {
        [Key]
        public int Id { get; set; }
        public int MasterId { get; set; }
        [ForeignKey("BranchId")]
        public Sys_Branches Sys_Branches { get; set; }
        public int? BranchId { get; set; }
        [ForeignKey("WarehouseId")]
        public Stock_Warehouses Stock_Warehouses { get; set; }
        public int? WarehouseId { get; set; }
        [ForeignKey("GroupId")]
        public Stock_ProductGroups Stock_ProductGroups { get; set; }
        public int? GroupId { get; set; }
        [ForeignKey("SalesAccountId")]
        public GL_ChartOfAccounts GL_ChartOfAccountsSales { get; set; }
        public int? SalesAccountId { get; set; }

        [ForeignKey("PurchaseAccountId")]
        public GL_ChartOfAccounts GL_ChartOfAccountsPurchase { get; set; }
        public int? PurchaseAccountId { get; set; }

        [ForeignKey("ReturnSalesAccountId")]
        public GL_ChartOfAccounts GL_ChartOfAccountsReturnSales { get; set; }
        public int? ReturnSalesAccountId { get; set; }

        [ForeignKey("ReturnPurchaseAccountId")]
        public GL_ChartOfAccounts GL_ChartOfAccountsReturnPurchase { get; set; }
        public int? ReturnPurchaseAccountId { get; set; }

        [ForeignKey("StoreAccountId")]
        public GL_ChartOfAccounts GL_ChartOfAccountsStore { get; set; }
        public int? StoreAccountId { get; set; }

        [ForeignKey("CostGoodsAccountId")]
        public GL_ChartOfAccounts GL_ChartOfAccountsCostGoods { get; set; }
        public int? CostGoodsAccountId { get; set; }

        [ForeignKey("GoodsInTransitAccountId")]
        public GL_ChartOfAccounts GL_ChartOfAccountsGoodsInTransit { get; set; }
        public int? GoodsInTransitAccountId { get; set; }

        [ForeignKey("AdjustAccountId")]
        public GL_ChartOfAccounts GL_ChartOfAccountsAdjust { get; set; }
        public int? AdjustAccountId { get; set; }

        [ForeignKey("DestoryAccountId")]
        public GL_ChartOfAccounts GL_ChartOfAccountsDestory { get; set; }
        public int? DestoryAccountId { get; set; }

        [ForeignKey("AllowDiscountAccountId")]
        public GL_ChartOfAccounts GL_ChartOfAccountsAllowDiscount { get; set; }
        public int? AllowDiscountAccountId { get; set; }

        [ForeignKey("EarnDiscountAccountId")]
        public GL_ChartOfAccounts GL_ChartOfAccountsEarnDiscount { get; set; }
        public int? EarnDiscountAccountId { get; set; }

    }
   
    
}
