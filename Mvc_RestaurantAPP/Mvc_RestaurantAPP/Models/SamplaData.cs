using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace Mvc_RestaurantAPP.Models
{
    public class SamplaData : DropCreateDatabaseIfModelChanges<RestaurantDbContext>
    {
        protected override void Seed(RestaurantDbContext context)
        {
            context.AslCompanyDbSet.Add(new AslCompany() { COMPID = 101, COMPNM = "CAFF 101", ADDRESS = "jamalkhan road,Chittagong", CONTACTNO = "8801745555555", EMAILID = "caff101@gmail.com", WEBID = "www.caff101@gmail.com", STATUS = "A" });
            context.AslCompanyDbSet.Add(new AslCompany() { COMPID = 102, COMPNM = "CAFF 102", ADDRESS = "GEC road,Chittagong", CONTACTNO = "8801744444444", EMAILID = "caff102@gmail.com", WEBID = "www.caff102@gmail.com", STATUS = "A" });

            context.SaveChanges();

            context.AslUsercoDbSet.Add(new AslUserco() { COMPID = 001, USERID = 10001, USERNM = "Alchemy Software(Piash)", DEPTNM = "Admin", OPTP = "AslSuperadmin", ADDRESS = "Goal pahar,Suborna, 203/b,Chittagong", MOBNO = "8801740545009", EMAILID = "superadmin01@gmail.com", LOGINBY = "EMAIL", LOGINID = "superadmin01@gmail.com", LOGINPW = "123", TIMEFR = "00:00", TIMETO = "23:59", STATUS = "A" });
            context.AslUsercoDbSet.Add(new AslUserco() { COMPID = 002, USERID = 10002, USERNM = "Alchemy Software(Shamim)", DEPTNM = "Admin", OPTP = "AslSuperadmin", ADDRESS = "Goal pahar, 203/b,Chittagong", MOBNO = "8801775222222", EMAILID = "superadmin02@gmail.com", LOGINBY = "EMAIL", LOGINID = "superadmin02@gmail.com", LOGINPW = "123", TIMEFR = "00:00", TIMETO = "23:59", STATUS = "A" });

            context.AslUsercoDbSet.Add(new AslUserco() { COMPID = 101, USERID = 10101, USERNM = "Raju Chowdhury", DEPTNM = "Admin", OPTP = "CompanyAdmin", ADDRESS = "jamalkhan road,Chittagong", MOBNO = "8801745555555", EMAILID = "caff10101@gmail.com", LOGINBY = "EMAIL", LOGINID = "caff10101@gmail.com", LOGINPW = "123", TIMEFR = "00:00", TIMETO = "23:59", STATUS = "A" });
            context.AslUsercoDbSet.Add(new AslUserco() { COMPID = 101, USERID = 10102, USERNM = "Shamin ullah", DEPTNM = "Account", OPTP = "User", ADDRESS = "jamalkhan road,Chittagong", MOBNO = "8801744444445", EMAILID = "caff10102@gmail.com", LOGINBY = "EMAIL", LOGINID = "caff10102@gmail.com", LOGINPW = "123", TIMEFR = "00:00", TIMETO = "23:59", STATUS = "A" });
            context.AslUsercoDbSet.Add(new AslUserco() { COMPID = 102, USERID = 10201, USERNM = "Riaz Talukdar", DEPTNM = "Admin", OPTP = "CompanyAdmin", ADDRESS = "GEC road,Chittagong", MOBNO = "8801744444444", EMAILID = "caff10201@gmail.com", LOGINBY = "EMAIL", LOGINID = "caff10201@gmail.com", LOGINPW = "123", TIMEFR = "00:00", TIMETO = "23:59", STATUS = "A" });

            //context.SaveChanges();

            context.AslMenumstDbSet.Add(new ASL_MENUMST() { MODULEID = "01", MODULENM = "User Module" });
            context.AslMenumstDbSet.Add(new ASL_MENUMST() { MODULEID = "02", MODULENM = "Transaction Module" });


            context.AslMenuDbSet.Add(new ASL_MENU() { MODULEID = "01", MENUTP = "F", MENUID = "F0101", MENUNM = "User Information", ACTIONNAME = "Index", CONTROLLERNAME = "AslUserCo" });
            context.AslMenuDbSet.Add(new ASL_MENU() { MODULEID = "02", MENUTP = "F", MENUID = "F0201", MENUNM = "Item Creation", ACTIONNAME = "Index", CONTROLLERNAME = "CategoryItem" });
            context.AslMenuDbSet.Add(new ASL_MENU() { MODULEID = "01", MENUTP = "R", MENUID = "R0101", MENUNM = "User Log Data List", ACTIONNAME = "GetCompanyUserLogData", CONTROLLERNAME = "UserReport" });
            context.AslMenuDbSet.Add(new ASL_MENU() { MODULEID = "02", MENUTP = "R", MENUID = "R0201", MENUNM = "Listed Items", ACTIONNAME = "GetCategoryReport", CONTROLLERNAME = "BillingReport" });


            context.AslRoleDbSet.Add(new ASL_ROLE() { COMPID = 101, USERID = 10101, MODULEID = "01", MENUTP = "F", MENUID = "F0101", MENUNAME = "User Information", ACTIONNAME = "Index", CONTROLLERNAME = "AslUserCo", STATUS = "A", INSERTR = "A", UPDATER = "A", DELETER = "A" });
            context.AslRoleDbSet.Add(new ASL_ROLE() { COMPID = 101, USERID = 10101, MODULEID = "02", MENUTP = "F", MENUID = "F0201", MENUNAME = "Item Creation", ACTIONNAME = "Index", CONTROLLERNAME = "CategoryItem", STATUS = "A", INSERTR = "A", UPDATER = "A", DELETER = "A" });
            context.AslRoleDbSet.Add(new ASL_ROLE() { COMPID = 101, USERID = 10101, MODULEID = "01", MENUTP = "R", MENUID = "R0101", MENUNAME = "User Log Data List", ACTIONNAME = "GetCompanyUserLogData", CONTROLLERNAME = "UserReport", STATUS = "A", INSERTR = "I", UPDATER = "I", DELETER = "I" });
            context.AslRoleDbSet.Add(new ASL_ROLE() { COMPID = 101, USERID = 10101, MODULEID = "02", MENUTP = "R", MENUID = "R0201", MENUNAME = "Listed Items", ACTIONNAME = "GetCategoryReport", CONTROLLERNAME = "BillingReport", STATUS = "A", INSERTR = "I", UPDATER = "I", DELETER = "I" });
            //.....................................
            context.AslRoleDbSet.Add(new ASL_ROLE() { COMPID = 101, USERID = 10102, MODULEID = "01", MENUTP = "F", MENUID = "F0101", MENUNAME = "User Information", ACTIONNAME = "Index", CONTROLLERNAME = "AslUserCo", STATUS = "I", INSERTR = "I", UPDATER = "I", DELETER = "I" });
            context.AslRoleDbSet.Add(new ASL_ROLE() { COMPID = 101, USERID = 10102, MODULEID = "02", MENUTP = "F", MENUID = "F0201", MENUNAME = "Item Creation", ACTIONNAME = "Index", CONTROLLERNAME = "CategoryItem", STATUS = "I", INSERTR = "I", UPDATER = "I", DELETER = "I" });
            context.AslRoleDbSet.Add(new ASL_ROLE() { COMPID = 101, USERID = 10102, MODULEID = "01", MENUTP = "R", MENUID = "R0101", MENUNAME = "User Log Data List", ACTIONNAME = "GetCompanyUserLogData", CONTROLLERNAME = "UserReport", STATUS = "I", INSERTR = "I", UPDATER = "I", DELETER = "I" });
            context.AslRoleDbSet.Add(new ASL_ROLE() { COMPID = 101, USERID = 10102, MODULEID = "02", MENUTP = "R", MENUID = "R0201", MENUNAME = "Listed Items", ACTIONNAME = "GetCategoryReport", CONTROLLERNAME = "BillingReport", STATUS = "I", INSERTR = "I", UPDATER = "I", DELETER = "I" });
            //.....................................
            context.AslRoleDbSet.Add(new ASL_ROLE() { COMPID = 102, USERID = 10201, MODULEID = "01", MENUTP = "F", MENUID = "F0101", MENUNAME = "User Information", ACTIONNAME = "Index", CONTROLLERNAME = "AslUserCo", STATUS = "A", INSERTR = "A", UPDATER = "A", DELETER = "A" });
            context.AslRoleDbSet.Add(new ASL_ROLE() { COMPID = 102, USERID = 10201, MODULEID = "02", MENUTP = "F", MENUID = "F0201", MENUNAME = "Item Creation", ACTIONNAME = "Index", CONTROLLERNAME = "CategoryItem", STATUS = "A", INSERTR = "A", UPDATER = "A", DELETER = "A" });
            context.AslRoleDbSet.Add(new ASL_ROLE() { COMPID = 102, USERID = 10201, MODULEID = "01", MENUTP = "R", MENUID = "R0101", MENUNAME = "User Log Data List", ACTIONNAME = "GetCompanyUserLogData", CONTROLLERNAME = "UserReport", STATUS = "A", INSERTR = "I", UPDATER = "I", DELETER = "I" });
            context.AslRoleDbSet.Add(new ASL_ROLE() { COMPID = 102, USERID = 10201, MODULEID = "02", MENUTP = "R", MENUID = "R0201", MENUNAME = "Listed Items", ACTIONNAME = "GetCategoryReport", CONTROLLERNAME = "BillingReport", STATUS = "A", INSERTR = "I", UPDATER = "I", DELETER = "I" });

            //context.PosItemMstDbSet.Add(new POS_ITEMMST(){COMPID = 101,CATID = 10101,CATNM = "Food",REMARKS = "10 items included",INSUSERID = 10101});
            //context.PosItemMstDbSet.Add(new POS_ITEMMST() { COMPID = 101, CATID = 10102, CATNM = "Lunch", REMARKS = "Varieties items included", INSUSERID = 10101 });
            //context.PosItemMstDbSet.Add(new POS_ITEMMST() { COMPID = 101, CATID = 10103, CATNM = "Drink", REMARKS = "Varieties of drink", INSUSERID = 10101 });

            //context.RmsItemDbSet.Add(new RMS_ITEM() { COMPID = 101, CATID = 10101, ITEMID = 10101001, ITEMNM = "Burger", BRAND = "good", UNIT = "1Kg", BUYRT = 100, SALRT = 120, STKMIN = 20, DISCOUNT = 20, REMARKS = "delicious" });
            //context.RmsItemDbSet.Add(new RMS_ITEM() { COMPID = 101, CATID = 10101, ITEMID = 10101002, ITEMNM = "Hot Dog", BRAND = "better", UNIT = "50 grms", BUYRT = 120, SALRT = 140, STKMIN = 20, DISCOUNT = 20, REMARKS = "delicious food" });
            //context.RmsItemDbSet.Add(new RMS_ITEM() { COMPID = 101, CATID = 10101, ITEMID = 10101003, ITEMNM = "Coka cola", BRAND = "Drink", UNIT = "1 kgm", BUYRT = 60, SALRT = 80, STKMIN = 20, DISCOUNT = 20, REMARKS = "delicious cold" });

            //context.RmsItemDbSet.Add(new RMS_ITEM() { COMPID = 101, CATID = 10102, ITEMID = 10102001, ITEMNM = "Rice", BRAND = "better", UNIT = "1 plate", BUYRT = 40, SALRT = 50, STKMIN = 20, DISCOUNT = 20, REMARKS = "delicious.2 types of item" });
            //context.RmsItemDbSet.Add(new RMS_ITEM() { COMPID = 101, CATID = 10102, ITEMID = 10102002, ITEMNM = "Chicken", BRAND = "better", UNIT = "2 plate", BUYRT = 70, SALRT = 80, STKMIN = 20, DISCOUNT = 20, REMARKS = "Korma" });
            //context.RmsItemDbSet.Add(new RMS_ITEM() { COMPID = 101, CATID = 10102, ITEMID = 10102003, ITEMNM = "Mutton", BRAND = "better", UNIT = "3 pieces ", BUYRT = 60, SALRT = 90, STKMIN = 20, DISCOUNT = 20, REMARKS = "Casta" });
            //context.RmsItemDbSet.Add(new RMS_ITEM() { COMPID = 101, CATID = 10102, ITEMID = 10102004, ITEMNM = "Chicken Mashla", BRAND = "Hot", UNIT = "4 pieces", BUYRT = 90, SALRT = 110, STKMIN = 20, DISCOUNT = 20, REMARKS = "Masla resala" });

            //context.RmsItemDbSet.Add(new RMS_ITEM() { COMPID = 101, CATID = 10103, ITEMID = 10103001, ITEMNM = "Cocktel", BRAND = "Cold", UNIT = "1 bottle ", BUYRT = 1000, SALRT = 1200, STKMIN = 20, DISCOUNT = 20, REMARKS = "Hot Drink" });
            //context.RmsItemDbSet.Add(new RMS_ITEM() { COMPID = 101, CATID = 10103, ITEMID = 10103002, ITEMNM = "whisky ", BRAND = "xtz", UNIT = "1 bottle ", BUYRT = 1400, SALRT = 2000, STKMIN = 20, DISCOUNT = 20, REMARKS = "Cold Drink" });
            //context.RmsItemDbSet.Add(new RMS_ITEM() { COMPID = 101, CATID = 10103, ITEMID = 10103003, ITEMNM = "Coka cola", BRAND = "Uniliber", UNIT = "1 bottle ", BUYRT = 60, SALRT = 80, STKMIN = 20, DISCOUNT = 20, REMARKS = "Cold Drink" });

            context.SaveChanges();
            //base.Seed(context);
        }
    }
}