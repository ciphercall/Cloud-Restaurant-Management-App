using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc_RestaurantAPP.Models
{
    public class PageModel
    {
        public ASL_MENUMST aslMenumst { get; set; }
        public ASL_MENU aslMenu { get; set; }

        public AslUserco aslUserco { get; set; }
        public AslCompany aslCompany { get; set; }
        public ASL_LOG aslLog { get; set; }

    }
}