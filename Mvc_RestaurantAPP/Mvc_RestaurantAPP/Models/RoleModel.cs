using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc_RestaurantAPP.Models
{
    public class RoleModel
    {
        public RoleModel()
        {
            this.AslMenu = new ASL_MENU();
            this.AslMenumst = new ASL_MENUMST();
            this.AslRole = new ASL_ROLE();
            this.AslUserco = new AslUserco();
        }

        public AslUserco AslUserco { get; set; }
        public ASL_MENUMST AslMenumst { get; set; }
        public ASL_MENU AslMenu { get; set; }
        public ASL_ROLE AslRole { get; set; }
    }
}