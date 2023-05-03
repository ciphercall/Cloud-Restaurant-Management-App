using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Mvc_RestaurantAPP.Controllers;

namespace Mvc_RestaurantAPP.Models
{
    public class BillingReportViewModel
    {

        [Required(ErrorMessage = "From date field can not be empty!")]
        [DataType(DataType.Date)]
        public DateTime? FromDate { get; set; }

        [Required(ErrorMessage = "To Date field can not be empty!")]
        [DataType(DataType.Date)]
        public DateTime? ToDate { get; set; }


        public string Command { get; set; }

        public Int64? CompanyID { get; set; }
        public Int64? CategoryID { get; set; }
        public string CategoryName { get; set; }
        public Int64? ItemID { get; set; }
        public string ItemName { get; set; }
        
        //[DataType(DataType.Date)]
        public string Date { get; set; }
        public DateTime? DateTime { get; set; }
        public string MemoNo { get; set; }
        public decimal? Quantity { get; set; }
        public Int64? ItemQtyCount { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? Vat { get; set; }
        public decimal? Net { get; set; }
        public decimal? ServiceCharge { get; set; }

        public RMS_TRANS RmsTrans { get; set; }
        public RMS_TRANSMST RmsTransmst { get; set; }


        public RMS_ITEM RmsItem { get; set; }
        public POS_ITEMMST PosItemmst { get; set; }
    }
}