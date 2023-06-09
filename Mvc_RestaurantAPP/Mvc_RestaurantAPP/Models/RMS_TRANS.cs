﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_RestaurantAPP.Models
{
    public class RMS_TRANS
    {
        public int RMS_TRANSid { get; set; }

        [Display(Name = "Company ID")]
        public Int64? COMPID { get; set; }

        public string TRANSTP { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? TRANSDT { get; set; }
        public Int64? TRANSYY { get; set; }

        public string TRANSNO { get; set; }
        public string TABLENO { get; set; }

        public Int64? ITEMSL { get; set; }
        public Int64? ITEMID { get; set; }

        public Int64? QTY { get; set; }
        public decimal? RATE { get; set; }
        public decimal? AMOUNT { get; set; }
        public decimal? DISCRT { get; set; }
        public decimal? DISCAMT { get; set; }
        public decimal? GROSSAMT { get; set; }

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