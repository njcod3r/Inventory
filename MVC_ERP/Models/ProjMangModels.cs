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


    public class ProjMang_Projects
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int MemberShipId { get; set; }
        [ForeignKey("CompanyId")]
        public Sys_Companies Sys_Companies { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [ForeignKey("BranchId")]
        public Sys_Branches Sys_Branches { get; set; }
        [Required]
        public int BranchId { get; set; }
        public string Code { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string NameA { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string NameE { get; set; }
        [ForeignKey("PartnerId")]
        public Sys_Partners Sys_Partners { get; set; }
        public int PartnerId { get; set; }
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
