using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc_RestaurantAPP.Models
{
    public class CategoryItemModel
    {
        public CategoryItemModel()
        {
            this.RmsTrans = new RMS_TRANS();
            this.RmsTransMst = new RMS_TRANSMST();
            this.PosItemmstModel = new POS_ITEMMST();
            this.RmsItemModel = new RMS_ITEM();
        }

        public POS_ITEMMST PosItemmstModel { get; set; }
        public RMS_ITEM RmsItemModel { get; set; }
        public RMS_TRANSMST RmsTransMst { get; set; }
        public RMS_TRANS RmsTrans { get; set; }
        
        public decimal? Total { get; set; }
        public string Empty { get; set; } //It used for readonly value(HtmlTextBoxfor) hold.
    }
}