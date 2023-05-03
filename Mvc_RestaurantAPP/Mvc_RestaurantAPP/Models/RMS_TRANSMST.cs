using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_RestaurantAPP.Models
{
    public class RMS_TRANSMST
    {
        public int RMS_TRANSMSTid { get; set; }

        [Display(Name = "Company ID")]
        public Int64? COMPID { get; set; }

        public string TRANSTP { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? TRANSDT { get; set; }

        public Int64? TRANSYY { get; set; }
        public string TRANSNO { get; set; }
        public string TABLENO { get; set; }
        public string TRMODE { get; set; }
        public string PSID { get; set; }
        public string CUSTNM { get; set; }


        [Display(Name = "Mobile Number")]
        //[RegularExpression(@"^(8{2})([0-9]{11})", ErrorMessage = "Insert a valid phone Number like 8801900000000")]
        public string CMOBNO { get; set; }

        [Display(Name = "Email Address")]
        //[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid Email Address")]
        public string CEMAILID { get; set; }

        public decimal? TOTAMT { get; set; }
        public decimal? DISCRTG { get; set; }
        public decimal? DISCAMTG { get; set; }
        public decimal? TOTGROSS { get; set; }
        public decimal? VATRT { get; set; }
        public decimal? VATAMT { get; set; }
        public decimal? SCHARGE { get; set; }
        public decimal? TOTNET { get; set; }
        public decimal? CASHAMT { get; set; }
        public decimal? CREDITAMT { get; set; }

        [Display(Name = "Remarks")]
        public string REMARKS { get; set; }

        public Int64? INSUSERID { get; set; }
        public Int64? UPDUSERID { get; set; }

        [Display(Name = "User PC")]
        public string USERPC { get; set; }


        [Display(Name = "Insert Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? INSTIME { get; set; }

        [Display(Name = "Inesrt IP ADDRESS")]
        public string INSIPNO { get; set; }
        public string INSLTUDE { get; set; }


        [Display(Name = "Update Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? UPDTIME { get; set; }

        [Display(Name = "Update IP ADDRESS")]
        public string UPDIPNO { get; set; }
        public string UPDLTUDE { get; set; }
    }
}