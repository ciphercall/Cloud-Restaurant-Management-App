using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using Mvc_RestaurantAPP.Models;

namespace Mvc_RestaurantAPP.Controllers
{
    public class CategoryItemController : AppController
    {

        //Datetime formet
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        public DateTime td = DateTime.Now;

        RestaurantDbContext db = new RestaurantDbContext();
        //Get Ip ADDRESS,Time & user PC Name
        public string strHostName;
        public IPHostEntry ipHostInfo;
        public IPAddress ipAddress;

        public CategoryItemController()
        {
            strHostName = System.Net.Dns.GetHostName();
            ipHostInfo = Dns.Resolve(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[0];
            td = DateTime.Now;
        }




        // Create ASL_LOG object and it used to this Insert_POSItemmst_LogData, Update_POSItemmst_LogData, Delete_POSItemmst_LogData (POS_ITEMMST posItemmst).
        public ASL_LOG aslLog = new ASL_LOG();

        // SAVE ALL INFORMATION from CategoryItemModel TO Asl_lOG Database Table.
        public void Insert_POSItemmst_LogData(CategoryItemModel aCategoryItemModel)
        {
            var date = Convert.ToString(DateTime.Now.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(DateTime.Now.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == aCategoryItemModel.PosItemmstModel.COMPID && n.USERID == aCategoryItemModel.PosItemmstModel.INSUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(aCategoryItemModel.PosItemmstModel.COMPID);
            aslLog.USERID = aCategoryItemModel.PosItemmstModel.INSUSERID;
            aslLog.LOGTYPE = "INSERT";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = aCategoryItemModel.PosItemmstModel.INSIPNO;
            aslLog.LOGLTUDE = aCategoryItemModel.PosItemmstModel.INSLTUDE;
            aslLog.TABLEID = "POS_ITEMMST";
            aslLog.LOGDATA = Convert.ToString("Category ID: " + aCategoryItemModel.PosItemmstModel.CATID + ",\nCategory Name: " + aCategoryItemModel.PosItemmstModel.CATNM + ",\nRemarks: " + aCategoryItemModel.PosItemmstModel.REMARKS + ".");
            aslLog.USERPC = aCategoryItemModel.PosItemmstModel.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }

        // Edit ALL INFORMATION from POS_ITEMMST TO Asl_lOG Database Table.
        public void Update_POSItemmst_LogData(POS_ITEMMST posItemmst)
        {
            var date = Convert.ToString(DateTime.Now.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(DateTime.Now.ToString("hh:mm:ss tt"));


            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == posItemmst.COMPID && n.USERID == posItemmst.UPDUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(posItemmst.COMPID);
            aslLog.USERID = posItemmst.UPDUSERID;
            aslLog.LOGTYPE = "UPDATE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = posItemmst.UPDIPNO;
            aslLog.LOGLTUDE = posItemmst.UPDLTUDE;
            aslLog.TABLEID = "POS_ITEMMST";
            aslLog.LOGDATA = Convert.ToString("Category ID: " + posItemmst.CATID + ",\nCategory Name: " + posItemmst.CATNM + ",\nRemarks: " + posItemmst.REMARKS + ".");
            aslLog.USERPC = posItemmst.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }


        // Delete ALL INFORMATION from POS_ITEMMST TO Asl_lOG Database Table.
        public void Delete_POSItemmst_LogData(POS_ITEMMST posItemmst)
        {
            var date = Convert.ToString(DateTime.Now.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(DateTime.Now.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == posItemmst.COMPID && n.USERID == posItemmst.UPDUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(posItemmst.COMPID);
            aslLog.USERID = posItemmst.UPDUSERID;
            aslLog.LOGTYPE = "DELETE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = posItemmst.UPDIPNO;
            aslLog.LOGLTUDE = posItemmst.UPDLTUDE;
            aslLog.TABLEID = "POS_ITEMMST";
            aslLog.LOGDATA = Convert.ToString("Category ID: " + posItemmst.CATID + ",\nCategory Name: " + posItemmst.CATNM + ",\nRemarks: " + posItemmst.REMARKS + ".");
            aslLog.USERPC = posItemmst.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }



        // SAVE ALL INFORMATION from RMS_ITEM TO Asl_lOG Database Table.
        public void Insert_RMS_ITEM_LogData(RMS_ITEM rmsItem)
        {
            var date = Convert.ToString(DateTime.Now.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(DateTime.Now.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == rmsItem.COMPID && n.USERID == rmsItem.INSUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(rmsItem.COMPID);
            aslLog.USERID = rmsItem.INSUSERID;
            aslLog.LOGTYPE = "INSERT";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = rmsItem.INSIPNO;
            aslLog.LOGLTUDE = rmsItem.INSLTUDE;
            aslLog.TABLEID = "RMS_ITEM";
            aslLog.LOGDATA = Convert.ToString("Category ID: " + rmsItem.CATID + ",\nIntem ID: " + rmsItem.ITEMID + ",\nItem Name: " + rmsItem.ITEMNM + ",\nBrand: " + rmsItem.BRAND + ",\nUnit: " + rmsItem.UNIT + ",\nBuy Rate: " + rmsItem.BUYRT + ",\nSale Rate: " + rmsItem.SALRT + ",\nStoke Minimum: " + rmsItem.STKMIN + ",\nDisscount: " + rmsItem.DISCOUNT + ",\nRemarks: " + rmsItem.REMARKS + ".");
            aslLog.USERPC = rmsItem.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }


        // Edit ALL INFORMATION from RMS_ITEM TO Asl_lOG Database Table.
        public void Update_RMS_ITEM_LogData(ASL_LOG aslLOGRef)
        {
            var date = Convert.ToString(DateTime.Now.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(DateTime.Now.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == aslLOGRef.COMPID && n.USERID == aslLOGRef.USERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(aslLOGRef.COMPID);
            aslLog.USERID = aslLOGRef.USERID;
            aslLog.LOGTYPE = "UPDATE";
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = aslLOGRef.LOGIPNO;
            aslLog.LOGLTUDE = aslLOGRef.LOGLTUDE;
            aslLog.TABLEID = "RMS_ITEM";
            aslLog.LOGDATA = aslLOGRef.LOGDATA;
            aslLog.USERPC = strHostName;
            db.AslLogDbSet.Add(aslLog);
        }


        // Delete ALL INFORMATION from RMS_ITEM TO Asl_lOG Database Table.
        public void Delete_RMS_ITEM_LogData(RMS_ITEM rmsItem)
        {
            var date = Convert.ToString(DateTime.Now.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(DateTime.Now.ToString("hh:mm:ss tt"));


            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == rmsItem.COMPID && n.USERID == rmsItem.UPDUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(rmsItem.COMPID);
            aslLog.USERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
            aslLog.LOGTYPE = "DELETE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = rmsItem.UPDIPNO;
            aslLog.LOGLTUDE = rmsItem.UPDLTUDE;
            aslLog.TABLEID = "RMS_ITEM";
            aslLog.LOGDATA = Convert.ToString("Category ID: " + rmsItem.CATID + ",\nIntem ID: " + rmsItem.ITEMID + ",\nItem Name: " + rmsItem.ITEMNM + ",\nBrand: " + rmsItem.BRAND + ",\nUnit: " + rmsItem.UNIT + ",\nBuy Rate: " + rmsItem.BUYRT + ",\nSale Rate: " + rmsItem.SALRT + ",\nStoke Minimum: " + rmsItem.STKMIN + ",\nDisscount: " + rmsItem.DISCOUNT + ",\nRemarks: " + rmsItem.REMARKS + ".");
            aslLog.USERPC = rmsItem.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }




        // Create ASL_DELETE object and it used to this Delete_ASL_DELETE (AslUserco aslUsercos).
        public ASL_DELETE AslDelete = new ASL_DELETE();

        // Delete ALL INFORMATION from POS_ITEMMST TO ASL_DELETE Database Table.
        public void Delete_ASL_DELETE_POS_ITEMMST(POS_ITEMMST posItemmst)
        {
            var date = Convert.ToString(DateTime.Now.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(DateTime.Now.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslDeleteDbSet where n.COMPID == posItemmst.COMPID && n.USERID == posItemmst.UPDUSERID select n.DELSLNO).Max());
            if (maxSerialNo == 0)
            {
                AslDelete.DELSLNO = Convert.ToInt64("1");
            }
            else
            {
                AslDelete.DELSLNO = maxSerialNo + 1;
            }

            AslDelete.COMPID = Convert.ToInt64(posItemmst.COMPID);
            AslDelete.USERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
            AslDelete.DELSLNO = AslDelete.DELSLNO;
            AslDelete.DELDATE = Convert.ToString(date);
            AslDelete.DELTIME = Convert.ToString(time);
            AslDelete.DELIPNO = posItemmst.UPDIPNO;
            AslDelete.DELLTUDE = posItemmst.UPDLTUDE;
            AslDelete.TABLEID = "POS_ITEMMST";
            AslDelete.DELDATA = Convert.ToString("Category ID: " + posItemmst.CATID + ",\nCategory Name: " + posItemmst.CATNM + ",\nRemarks: " + posItemmst.REMARKS + ".");
            AslDelete.USERPC = posItemmst.USERPC;
            db.AslDeleteDbSet.Add(AslDelete);
        }


        // Delete ALL INFORMATION from RMS_ITEM TO ASL_DELETE Database Table.
        public void Delete_ASL_DELETE_RMS_ITEM(RMS_ITEM rmsItem)
        {
            var date = Convert.ToString(DateTime.Now.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(DateTime.Now.ToString("hh:mm:ss tt"));


            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslDeleteDbSet where n.COMPID == rmsItem.COMPID && n.USERID == rmsItem.UPDUSERID select n.DELSLNO).Max());
            if (maxSerialNo == 0)
            {
                AslDelete.DELSLNO = Convert.ToInt64("1");
            }
            else
            {
                AslDelete.DELSLNO = maxSerialNo + 1;
            }

            AslDelete.COMPID = Convert.ToInt64(rmsItem.COMPID);
            AslDelete.USERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
            AslDelete.DELSLNO = AslDelete.DELSLNO;
            AslDelete.DELDATE = Convert.ToString(date);
            AslDelete.DELTIME = Convert.ToString(time);
            AslDelete.DELIPNO = rmsItem.UPDIPNO;
            AslDelete.DELLTUDE = rmsItem.UPDLTUDE;
            AslDelete.TABLEID = "RMS_ITEM";
            AslDelete.DELDATA = Convert.ToString("Category ID: " + rmsItem.CATID + ",\nIntem ID: " + rmsItem.ITEMID + ",\nItem Name: " + rmsItem.ITEMNM + ",\nBrand: " + rmsItem.BRAND + ",\nUnit: " + rmsItem.UNIT + ",\nBuy Rate: " + rmsItem.BUYRT + ",\nSale Rate: " + rmsItem.SALRT + ",\nStoke Minimum: " + rmsItem.STKMIN + ",\nDisscount: " + rmsItem.DISCOUNT + ",\nRemarks: " + rmsItem.REMARKS + ".");
            AslDelete.USERPC = rmsItem.USERPC;
            db.AslDeleteDbSet.Add(AslDelete);
        }





        // GET: /CategoryItem/
        [AcceptVerbs("GET")]
        [ActionName("Index")]
        public ActionResult Index()
        {
            var dt = (CategoryItemModel)TempData["category"];
            return View(dt);
        }



        [AcceptVerbs("POST")]
        [ActionName("Index")]
        public ActionResult IndexPost(CategoryItemModel aCategoryItemModel, string command)
        {
            if (command == "Add")
            {
                //.........................................................Create Permission Check
                var LoggedCompId = System.Web.HttpContext.Current.Session["loggedCompID"].ToString();
                var loggedUserID = System.Web.HttpContext.Current.Session["loggedUserID"].ToString();

                var createStatus = "";

                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RestaurantDbContext"].ToString());
                string query = string.Format("SELECT STATUS, INSERTR, UPDATER, DELETER FROM ASL_ROLE WHERE  CONTROLLERNAME='CategoryItem' AND COMPID='{0}' AND USERID = '{1}'", LoggedCompId, loggedUserID);
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conn);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                da.Fill(ds);

                foreach (DataRow row in ds.Rows)
                {
                    createStatus = row["INSERTR"].ToString();
                }

                conn.Close();

                if (createStatus == 'I'.ToString())
                {
                    TempData["ShowAddButton"] = "Show Add Button";
                    TempData["category"] = aCategoryItemModel;
                    TempData["categoryId"] = aCategoryItemModel.PosItemmstModel.CATID;
                    TempData["message"] = "Permission not granted!";
                    return RedirectToAction("Index");
                }
                //...............................................................................................

                aCategoryItemModel.RmsItemModel.COMPID = aCategoryItemModel.PosItemmstModel.COMPID;
                aCategoryItemModel.RmsItemModel.CATID = aCategoryItemModel.PosItemmstModel.CATID;
                if (aCategoryItemModel.RmsItemModel.CATID == null)
                {
                    TempData["message"] = "Enter Category First";
                    return View("Index");
                }
                aCategoryItemModel.RmsItemModel.USERPC = strHostName;
                aCategoryItemModel.RmsItemModel.INSIPNO = ipAddress.ToString();
                aCategoryItemModel.RmsItemModel.INSTIME = td;
                aCategoryItemModel.RmsItemModel.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);


                try
                {
                    if (ModelState.IsValid)
                    {

                        POS_ITEMMST pos_itemmst_CompID = db.PosItemMstDbSet.FirstOrDefault(r => (r.COMPID == aCategoryItemModel.PosItemmstModel.COMPID));
                        Int64 catagoryID = Convert.ToInt64(aCategoryItemModel.PosItemmstModel.CATID);
                        POS_ITEMMST posItemmst_CATID = db.PosItemMstDbSet.FirstOrDefault(r => (r.CATID == catagoryID));

                        if (pos_itemmst_CompID == null && posItemmst_CATID == null)
                        {
                            TempData["ShowAddButton"] = "Show Add Button";
                            TempData["message"] = "Catagory ID not found ";
                            return View("Index");
                        }
                        else
                        {
                            Int64 maxData = Convert.ToInt64((from n in db.RmsItemDbSet where n.COMPID == aCategoryItemModel.PosItemmstModel.COMPID && n.CATID == aCategoryItemModel.PosItemmstModel.CATID select n.ITEMID).Max());

                            Int64 R = Convert.ToInt64(catagoryID + "999");

                            if (maxData == 0)
                            {
                                aCategoryItemModel.RmsItemModel.ITEMID = Convert.ToInt64(catagoryID + "001");
                                Insert_RMS_ITEM_LogData(aCategoryItemModel.RmsItemModel);

                                db.RmsItemDbSet.Add(aCategoryItemModel.RmsItemModel);
                                if (db.SaveChanges() > 0)
                                {
                                    TempData["message"] = "Item Successfully Saved";
                                    aCategoryItemModel.RmsItemModel.ITEMNM = "";
                                    aCategoryItemModel.RmsItemModel.BRAND = "";
                                    aCategoryItemModel.RmsItemModel.UNIT = "";
                                    aCategoryItemModel.RmsItemModel.BUYRT = 0;
                                    aCategoryItemModel.RmsItemModel.SALRT = 0;
                                    aCategoryItemModel.RmsItemModel.STKMIN = 0;
                                    aCategoryItemModel.RmsItemModel.DISCOUNT = 0;
                                    aCategoryItemModel.RmsItemModel.REMARKS = "";



                                }

                            }
                            else if (maxData <= R)
                            {

                                aCategoryItemModel.RmsItemModel.ITEMID = maxData + 1;
                                Insert_RMS_ITEM_LogData(aCategoryItemModel.RmsItemModel);

                                db.RmsItemDbSet.Add(aCategoryItemModel.RmsItemModel);
                                db.SaveChanges();
                                TempData["message"] = "Item Successfully Saved";
                                aCategoryItemModel.RmsItemModel.ITEMNM = "";
                                aCategoryItemModel.RmsItemModel.BRAND = "";
                                aCategoryItemModel.RmsItemModel.UNIT = "";
                                aCategoryItemModel.RmsItemModel.BUYRT = 0;
                                aCategoryItemModel.RmsItemModel.SALRT = 0;
                                aCategoryItemModel.RmsItemModel.STKMIN = 0;
                                aCategoryItemModel.RmsItemModel.DISCOUNT = 0;
                                aCategoryItemModel.RmsItemModel.REMARKS = "";


                            }
                            else
                            {
                                TempData["message"] = "Item entry not possible";
                                TempData["ShowAddButton"] = "Show Add Button";

                            }
                        }

                    }
                }
                catch (Exception ex)
                {

                }
                TempData["ShowAddButton"] = "Show Add Button";
                TempData["category"] = aCategoryItemModel;
                TempData["categoryId"] = aCategoryItemModel.PosItemmstModel.CATID;
                return RedirectToAction("Index");
            }


            if (command == "Submit")
            {
                if (ModelState.IsValid)
                {
                    if (aCategoryItemModel.PosItemmstModel.CATNM != null)
                    {
                        //Get Ip ADDRESS,Time & user PC Name
                        string strHostName = System.Net.Dns.GetHostName();
                        IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                        IPAddress ipAddress = ipHostInfo.AddressList[0];


                        aCategoryItemModel.PosItemmstModel.USERPC = strHostName;
                        aCategoryItemModel.PosItemmstModel.INSIPNO = ipAddress.ToString();
                        aCategoryItemModel.PosItemmstModel.INSTIME = Convert.ToDateTime(DateTime.Now.ToString("d"));
                        //Insert User ID save POS_ITEMMST table attribute (INSUSERID) field
                        aCategoryItemModel.PosItemmstModel.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

                        Int64 companyID = Convert.ToInt64(aCategoryItemModel.PosItemmstModel.COMPID);


                        Int64 minCategoryId = Convert.ToInt64((from n in db.PosItemMstDbSet where n.COMPID == companyID select n.CATID).Min());
                        //if (aCategoryItemModel.PosItemmstModel.CATID == null)
                        //{
                        //    aCategoryItemModel.PosItemmstModel.CATID = minCategoryId;
                        //}


                        var result = db.PosItemMstDbSet.Count(d => d.CATNM == aCategoryItemModel.PosItemmstModel.CATNM
                                                                  && d.COMPID == companyID);
                        if (result == 0)
                        {

                            //.........................................................Create Permission Check
                            var LoggedCompId = System.Web.HttpContext.Current.Session["loggedCompID"].ToString();
                            var loggedUserID = System.Web.HttpContext.Current.Session["loggedUserID"].ToString();

                            var createStatus = "";

                            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RestaurantDbContext"].ToString());
                            string query = string.Format("SELECT STATUS, INSERTR, UPDATER, DELETER FROM ASL_ROLE WHERE  CONTROLLERNAME='CategoryItem' AND COMPID='{0}' AND USERID = '{1}'", LoggedCompId, loggedUserID);
                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conn);
                            conn.Open();
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataTable ds = new DataTable();
                            da.Fill(ds);

                            foreach (DataRow row in ds.Rows)
                            {
                                createStatus = row["INSERTR"].ToString();
                            }

                            conn.Close();

                            if (createStatus == 'I'.ToString())
                            {
                                TempData["category"] = aCategoryItemModel;
                                TempData["categoryId"] = aCategoryItemModel.PosItemmstModel.CATID;
                                TempData["ShowAddButton"] = "Show Add Button";
                                TempData["message"] = "Permission not granted!";
                                return RedirectToAction("Index");
                            }
                            //...............................................................................................


                            AslUserco aslUserco = db.AslUsercoDbSet.FirstOrDefault(r => (r.COMPID == companyID));
                            if (aslUserco == null)
                            {
                                TempData["message"] = " User ID not found ";
                                TempData["ShowAddButton"] = "Show Add Button";
                            }
                            else
                            {
                                Int64 maxData = Convert.ToInt64((from n in db.PosItemMstDbSet where n.COMPID == aCategoryItemModel.PosItemmstModel.COMPID select n.CATID).Max());

                                Int64 R = Convert.ToInt64(aCategoryItemModel.PosItemmstModel.COMPID + "99");

                                if (maxData == 0)
                                {
                                    aCategoryItemModel.PosItemmstModel.CATID = Convert.ToInt64(aCategoryItemModel.PosItemmstModel.COMPID + "01");

                                    Insert_POSItemmst_LogData(aCategoryItemModel);

                                    db.PosItemMstDbSet.Add(aCategoryItemModel.PosItemmstModel);
                                    db.SaveChanges();

                                    TempData["message"] = "Category Name: '" + aCategoryItemModel.PosItemmstModel.CATNM + "' successfully saved.\n Please Create the item List.";
                                    TempData["ShowAddButton"] = "Show Add Button";
                                    TempData["category"] = aCategoryItemModel;
                                    TempData["categoryId"] = aCategoryItemModel.PosItemmstModel.CATID;
                                    return RedirectToAction("Index");
                                }
                                else if (maxData <= R)
                                {
                                    aCategoryItemModel.PosItemmstModel.CATID = maxData + 1;

                                    Insert_POSItemmst_LogData(aCategoryItemModel);

                                    db.PosItemMstDbSet.Add(aCategoryItemModel.PosItemmstModel);
                                    db.SaveChanges();

                                    TempData["message"] = "Category Name: '" + aCategoryItemModel.PosItemmstModel.CATNM + "' successfully saved.\n Please Create the item List. ";
                                    TempData["ShowAddButton"] = "Show Add Button";
                                    TempData["category"] = aCategoryItemModel;
                                    TempData["categoryId"] = aCategoryItemModel.PosItemmstModel.CATID;
                                    return RedirectToAction("Index");
                                }
                                else
                                {
                                    TempData["ShowAddButton"] = "Show Add Button";
                                    TempData["message"] = "Not possible entry ";
                                    return RedirectToAction("Index");
                                }
                            }
                        }
                        else if (result > 0)
                        {
                            //TempData["message"] = "Get the Item List";
                            TempData["ShowAddButton"] = "Show Add Button";
                            TempData["category"] = aCategoryItemModel;
                            TempData["categoryId"] = aCategoryItemModel.PosItemmstModel.CATID;
                            TempData["latitute_CategoryList"] = aCategoryItemModel.PosItemmstModel.INSLTUDE;
                            return RedirectToAction("Index");
                        }
                    }

                    else if (aCategoryItemModel.PosItemmstModel.CATNM == null && aCategoryItemModel.PosItemmstModel.REMARKS != null)
                    {
                        ViewBag.CategoryMsg = "Please Enter Category Name.";
                        return View("Index");
                    }
                    else if (aCategoryItemModel.PosItemmstModel.CATNM == null && aCategoryItemModel.PosItemmstModel.REMARKS == null)
                    {
                        ViewBag.NullField = "Please Enter Category Name & Remarks.";
                        return View("Index");
                    }

                }
            }

            if (command == "Update")
            {
                var query =
                    from a in db.RmsItemDbSet
                    where (a.ITEMID == aCategoryItemModel.RmsItemModel.ITEMID && a.COMPID == aCategoryItemModel.PosItemmstModel.COMPID && a.CATID == aCategoryItemModel.PosItemmstModel.CATID)
                    select a;
                aCategoryItemModel.RmsItemModel.COMPID = aCategoryItemModel.PosItemmstModel.COMPID;
                aCategoryItemModel.RmsItemModel.CATID = aCategoryItemModel.PosItemmstModel.CATID;


                foreach (RMS_ITEM a in query)
                {
                    // Insert any additional changes to column values.
                    a.ITEMNM = aCategoryItemModel.RmsItemModel.ITEMNM;
                    a.BRAND = aCategoryItemModel.RmsItemModel.BRAND;
                    a.UNIT = aCategoryItemModel.RmsItemModel.UNIT;
                    a.BUYRT = aCategoryItemModel.RmsItemModel.BUYRT;
                    a.SALRT = aCategoryItemModel.RmsItemModel.SALRT;
                    a.STKMIN = aCategoryItemModel.RmsItemModel.STKMIN;
                    a.DISCOUNT = aCategoryItemModel.RmsItemModel.DISCOUNT;
                    a.REMARKS = aCategoryItemModel.RmsItemModel.REMARKS;
                    a.UPDIPNO = ipAddress.ToString();
                    a.UPDTIME = td;
                    a.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    a.UPDLTUDE = aCategoryItemModel.PosItemmstModel.INSLTUDE;
                    TempData["RmsItemLogData"] = Convert.ToString("Category ID: " + a.CATID + ",\nIntem ID: " + a.ITEMID + ",\nItem Name: " + a.ITEMNM + ",\nBrand: " + a.BRAND + ",\nUnit: " + a.UNIT + ",\nBuy Rate: " + a.BUYRT + ",\nSale Rate: " + a.SALRT + ",\nStoke Minimum: " + a.STKMIN + ",\nDisscount: " + a.DISCOUNT + ",\nRemarks: " + a.REMARKS + ".");

                }

                ASL_LOG aslLogref = new ASL_LOG();

                aslLogref.LOGIPNO = ipAddress.ToString();
                aslLogref.COMPID = aCategoryItemModel.RmsItemModel.COMPID;
                aslLogref.LOGLTUDE = aCategoryItemModel.RmsItemModel.INSLTUDE;

                //Update User ID save ASL_ROLE table attribute UPDUSERID
                aslLogref.USERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                aslLogref.LOGDATA = TempData["RmsItemLogData"].ToString();
                Update_RMS_ITEM_LogData(aslLogref);

                db.SaveChanges();

                TempData["category"] = aCategoryItemModel;
                TempData["categoryId"] = aCategoryItemModel.RmsItemModel.CATID;
                TempData["ShowAddButton"] = "Show Add Button";
                aCategoryItemModel.RmsItemModel.ITEMNM = "";
                aCategoryItemModel.RmsItemModel.BRAND = "";
                aCategoryItemModel.RmsItemModel.UNIT = "";
                aCategoryItemModel.RmsItemModel.BUYRT = 0;
                aCategoryItemModel.RmsItemModel.SALRT = 0;
                aCategoryItemModel.RmsItemModel.STKMIN = 0;
                aCategoryItemModel.RmsItemModel.DISCOUNT = 0;
                aCategoryItemModel.RmsItemModel.REMARKS = "";
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }


        public ActionResult EditItemUpdate(Int64 id, Int64 cid, Int64 catid, Int64 itemId, string itemname, CategoryItemModel model)
        {
            //.........................................................Create Permission Check
            var LoggedCompId = System.Web.HttpContext.Current.Session["loggedCompID"].ToString();
            var loggedUserID = System.Web.HttpContext.Current.Session["loggedUserID"].ToString();

            var updateStatus = "";

            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RestaurantDbContext"].ToString());
            string query1 = string.Format("SELECT STATUS, INSERTR, UPDATER, DELETER FROM ASL_ROLE WHERE  CONTROLLERNAME='CategoryItem' AND COMPID='{0}' AND USERID = '{1}'", LoggedCompId, loggedUserID);
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query1, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);

            foreach (DataRow row in ds.Rows)
            {
                updateStatus = row["UPDATER"].ToString();
            }
            conn.Close();

            //add the data from database to model
            var categoryName_Remarks = from m in db.PosItemMstDbSet where m.CATID == catid && m.COMPID == cid select m;
            foreach (var categoryResult in categoryName_Remarks)
            {
                model.PosItemmstModel.COMPID = cid;
                model.PosItemmstModel.CATID = catid;
                model.PosItemmstModel.CATNM = categoryResult.CATNM;
                model.PosItemmstModel.REMARKS = categoryResult.REMARKS;
            }
            TempData["category"] = model;
            TempData["categoryId"] = catid;
            TempData["ShowAddButton"] = null;
            if (updateStatus == 'I'.ToString())
            {
                TempData["message"] = "Permission not granted!";
                return RedirectToAction("Index");
            }
            //...............................................................................................

            model.RmsItemModel = db.RmsItemDbSet.Find(id);

            var item = from r in db.RmsItemDbSet where r.RMS_ITEMid == id select r.ITEMNM;
            foreach (var it in item)
            {
                model.RmsItemModel.ITEMNM = it.ToString();
            }

            return RedirectToAction("Index");

        }


        public ActionResult ItemDelete(Int64 id, Int64 cid, Int64 catid, Int64 itemId, string itemname, CategoryItemModel model)
        {
            //.........................................................Create Permission Check
            var LoggedCompId = System.Web.HttpContext.Current.Session["loggedCompID"].ToString();
            var loggedUserID = System.Web.HttpContext.Current.Session["loggedUserID"].ToString();

            var deleteStatus = "";

            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RestaurantDbContext"].ToString());
            string query = string.Format("SELECT STATUS, INSERTR, UPDATER, DELETER FROM ASL_ROLE WHERE  CONTROLLERNAME='CategoryItem' AND COMPID='{0}' AND USERID = '{1}'", LoggedCompId, loggedUserID);
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);

            foreach (DataRow row in ds.Rows)
            {
                deleteStatus = row["DELETER"].ToString();
            }
            conn.Close();
            //add the data from database to model
            var categoryName_Remarks = from m in db.PosItemMstDbSet where m.CATID == catid && m.COMPID == cid select m;
            foreach (var categoryResult in categoryName_Remarks)
            {
                model.PosItemmstModel.COMPID = cid;
                model.PosItemmstModel.CATID = catid;
                model.PosItemmstModel.CATNM = categoryResult.CATNM;
                model.PosItemmstModel.REMARKS = categoryResult.REMARKS;
            }
            TempData["category"] = model;
            TempData["categoryId"] = catid;
            TempData["ShowAddButton"] = "Show Add Button";
            if (deleteStatus == 'I'.ToString())
            {
                TempData["message"] = "Permission not granted!";
                return RedirectToAction("Index");

            }
            //...............................................................................................

            RMS_ITEM rmsitem = db.RmsItemDbSet.Find(id);
            //Get Ip ADDRESS,Time & user PC Name
            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            rmsitem.USERPC = strHostName;
            rmsitem.UPDIPNO = ipAddress.ToString();
            rmsitem.UPDTIME = Convert.ToDateTime(DateTime.Now.ToString("d"));
            //Delete User ID save POS_ITEMMST table attribute (UPDUSERID) field
            rmsitem.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

            if (TempData["latitute_CategoryList"] != null)
            {
                //Get current LOGLTUDE data 
                rmsitem.UPDLTUDE = TempData["latitute_CategoryList"].ToString();
            }

            Delete_RMS_ITEM_LogData(rmsitem);
            Delete_ASL_DELETE_RMS_ITEM(rmsitem);
            db.RmsItemDbSet.Remove(rmsitem);
            db.SaveChanges();

            return RedirectToAction("Index");
        }



        //
        // GET: /CategoryItemModel/
        public ActionResult ShowCategoryList()
        {
            Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
            var result = (from n in db.PosItemMstDbSet
                          where n.COMPID == compid
                          select n
                     );
            return View(result);
        }

        //
        // GET: /CategoryItemModel

        public ActionResult EditCategoryList(int id = 0)
        {
            POS_ITEMMST posItemmst = db.PosItemMstDbSet.Find(id);
            if (posItemmst == null)
            {
                return HttpNotFound();
            }
            return View(posItemmst);
        }

        //
        // POST: /CategoryItemModel

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategoryList(POS_ITEMMST posItemmst)
        {
            if (ModelState.IsValid)
            {
                //Get Ip ADDRESS,Time & user PC Name
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];


                posItemmst.UPDIPNO = ipAddress.ToString();
                posItemmst.UPDTIME = Convert.ToDateTime(DateTime.Now.ToString("d"));

                //Insert User ID save ASL_MENUMST table attribute INSUSERID
                posItemmst.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                Update_POSItemmst_LogData(posItemmst);

                db.Entry(posItemmst).State = EntityState.Modified;
                db.SaveChanges();

                TempData["UpdateCategoryInfo"] = "Category Name: '" + posItemmst.CATNM + "' update successfully!";
                return RedirectToAction("ShowCategoryList");
            }
            return View(posItemmst);
        }




        //
        // GET: /CategoryItemModel

        public ActionResult DeleteCategory(int id = 0)
        {
            POS_ITEMMST posItemmst = db.PosItemMstDbSet.Find(id);
            if (posItemmst == null)
            {
                return HttpNotFound();
            }
            return View(posItemmst);
        }

        //
        // POST: /CategoryItemModel

        [HttpPost, ActionName("DeleteCategory")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategoryConfirmed(int id, POS_ITEMMST posItemmstDelete)
        {
            POS_ITEMMST posItemmst = db.PosItemMstDbSet.Find(id);
            //Get Ip ADDRESS,Time & user PC Name 
            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            posItemmst.USERPC = strHostName;
            posItemmst.UPDIPNO = ipAddress.ToString();
            posItemmst.UPDTIME = Convert.ToDateTime(DateTime.Now.ToString("d"));
            //Delete User ID save POS_ITEMMST table attribute (UPDUSERID) field
            posItemmst.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
            //Get current LOGLTUDE data 
            posItemmst.UPDLTUDE = posItemmstDelete.UPDLTUDE;

            //Search all information from Menu Table,when it match to the Module ID
            var menuList = (from sub in db.RmsItemDbSet select sub)
               .Where(sub => sub.CATID == posItemmst.CATID);

            var date = Convert.ToString(DateTime.Now.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(DateTime.Now.ToString("hh:mm:ss tt"));

            Int64 maxSerialNoDelete = Convert.ToInt64((from n in db.AslDeleteDbSet where n.COMPID == posItemmst.COMPID && n.USERID == posItemmst.UPDUSERID select n.DELSLNO).Max());
            if (maxSerialNoDelete == 0)
            {
                AslDelete.DELSLNO = Convert.ToInt64("1");
            }
            else
            {
                AslDelete.DELSLNO = maxSerialNoDelete + 1;
            }
            // Delete ALL INFORMATION from RMS_ITEM TO Asl_Delete Database Table.
            AslDelete.COMPID = Convert.ToInt64(posItemmst.COMPID);
            AslDelete.USERID = Convert.ToInt64(posItemmst.UPDUSERID);
            AslDelete.DELSLNO = AslDelete.DELSLNO;
            AslDelete.DELDATE = Convert.ToString(date);
            AslDelete.DELTIME = Convert.ToString(time);
            AslDelete.DELIPNO = posItemmst.UPDIPNO;
            AslDelete.DELLTUDE = posItemmst.UPDLTUDE;
            AslDelete.TABLEID = "RMS_ITEM";
            AslDelete.USERPC = posItemmst.USERPC;
            AslDelete.DELDATA = " ";


            Int64 maxSerialNoLog = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == posItemmst.COMPID && n.USERID == posItemmst.UPDUSERID select n.LOGSLNO).Max());
            if (maxSerialNoLog == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNoLog + 1;
            }
            // Delete ALL INFORMATION from RMS_ITEM TO Asl_lOG Database Table.
            aslLog.COMPID = Convert.ToInt64(posItemmst.COMPID);
            aslLog.USERID = Convert.ToInt64(posItemmst.UPDUSERID);
            aslLog.LOGTYPE = "DELETE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = posItemmst.UPDIPNO;
            aslLog.LOGLTUDE = posItemmst.UPDLTUDE;
            aslLog.TABLEID = "RMS_ITEM";
            aslLog.USERPC = posItemmst.USERPC;
            aslLog.LOGDATA = "";

            foreach (var n in menuList)
            {
                AslDelete.DELDATA = AslDelete.DELDATA + Convert.ToString("Category ID: " + n.CATID + ",\nIntem ID: " + n.ITEMID + ",\nItem Name: " + n.ITEMNM + ",\nBrand: " + n.BRAND + ",\nUnit: " + n.UNIT + ",\nBuy Rate: " + n.BUYRT + ",\nSale Rate: " + n.SALRT + ",\nStoke Minimum: " + n.STKMIN + ",\nDisscount: " + n.DISCOUNT + ",\nRemarks: " + n.REMARKS + " .\n..................\n");
                aslLog.LOGDATA = aslLog.LOGDATA + Convert.ToString("Category ID: " + n.CATID + ",\nIntem ID: " + n.ITEMID + ",\nItem Name: " + n.ITEMNM + ",\nBrand: " + n.BRAND + ",\nUnit: " + n.UNIT + ",\nBuy Rate: " + n.BUYRT + ",\nSale Rate: " + n.SALRT + ",\nStoke Minimum: " + n.STKMIN + ",\nDisscount: " + n.DISCOUNT + ",\nRemarks: " + n.REMARKS + " .\n..................\n");

                db.RmsItemDbSet.Remove(n);

            }
            db.AslDeleteDbSet.Add(AslDelete);
            db.AslLogDbSet.Add(aslLog);
            db.SaveChanges();

            Delete_POSItemmst_LogData(posItemmst);
            Delete_ASL_DELETE_POS_ITEMMST(posItemmst);

            db.PosItemMstDbSet.Remove(posItemmst);
            db.SaveChanges();

            TempData["DeleteCategoryInfo"] = "Category Name: '" + posItemmst.CATNM + "' delete successfully!";
            return RedirectToAction("ShowCategoryList");
        }


        //// Get All Item Information
        //public ActionResult ShowItemList()
        //{
        //    Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
        //    var result = (from n in db.RmsItemDbSet
        //                  where n.COMPID == compid
        //                  select n
        //             );
        //    return View(result);
        //}





        //AutoComplete 
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult ItemNameChanged(string changedText)
        {
            var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());
            String itemId = "";
            string remarks = "";
            var rt = db.PosItemMstDbSet.Where(n => n.CATNM == changedText &&
                                                         n.COMPID == compid).Select(n => new
                                                         {
                                                             catid = n.CATID,
                                                             rmks = n.REMARKS
                                                         });
            foreach (var n in rt)
            {
                itemId = Convert.ToString(n.catid);
                remarks = Convert.ToString(n.rmks);
            }

            var result = new { catid = itemId, rmrks = remarks };
            return Json(result, JsonRequestBehavior.AllowGet);

        }




        //AutoComplete
        public JsonResult TagSearch(string term)
        {
            var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());


            var tags = from p in db.PosItemMstDbSet
                       where p.COMPID == compid
                       select p.CATNM;

            return this.Json(tags.Where(t => t.StartsWith(term)),
                            JsonRequestBehavior.AllowGet);
        }



        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}



