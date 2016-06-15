using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace MVC_ERP.Models
{

    public class UserControl_GHDate_NotNull
    {
        [DataType(DataType.Date)]
        public DateTime? Gregi { get; set; }
        //[Column(TypeName = "DateTime2")]
        public string Hijri { get; set; }
       }

    public class UserControl_GHDate_Null
    {
        [DataType(DataType.Date)]
        public DateTime? Gregi { get; set; }
        //[Column(TypeName = "DateTime2")]
        public string Hijri { get; set; }
    }

     public class Source_Name
    {
        public int Id { get; set; }
        public string NameE { get; set; }
       public string NameA { get; set; }

    }

     public class Voucher_Name
     {
         public int Id { get; set; }
         public string NameE { get; set; }
         public string NameA { get; set; }

     }

     public class DiscountType
     {
         public int Id { get; set; }
         public string NameE { get; set; }
         public string NameA { get; set; }

     }

     public class PurchasePricingPolicy
     {
         public int Id { get; set; }
         public string NameE { get; set; }
         public string NameA { get; set; }

     }

     public class SalesPricingPolicy
     {
         public int Id { get; set; }
         public string NameE { get; set; }
         public string NameA { get; set; }

     }
     public class Scope
     {
         public int Id { get; set; }
         public string NameE { get; set; }
         public string NameA { get; set; }

     }


}