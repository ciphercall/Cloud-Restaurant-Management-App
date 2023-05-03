using System.Security.Cryptography;
using Mvc_RestaurantAPP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Mvc_RestaurantAPP.Controllers
{
    public class TransactionController : AppController
    {

        //Datetime formet
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        public DateTime td = DateTime.Now;
        public string transdate = Convert.ToString(DateTime.Now.ToString("d"));

        RestaurantDbContext db = new RestaurantDbContext();
        //Get Ip ADDRESS,Time & user PC Name
        public string strHostName;
        public IPHostEntry ipHostInfo;
        public IPAddress ipAddress;

        Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);

        public TransactionController()
        {
            strHostName = System.Net.Dns.GetHostName();
            ipHostInfo = Dns.Resolve(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[0];
            td = DateTime.Now;
        }



        // Create ASL_LOG object and it used to this Insert_Asl_LogData (RMS_TRANSMST rmsTransmst).
        public ASL_LOG aslLog = new ASL_LOG();

        // SAVE ALL INFORMATION from RmsTransMst TO Asl_lOG Database Table.
        public void Insert_RmsTransMst_LogData(RMS_TRANSMST rmsTransmst)
        {
            var date = Convert.ToString(DateTime.Now.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(DateTime.Now.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == compid && n.USERID == rmsTransmst.INSUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(compid);
            aslLog.USERID = rmsTransmst.INSUSERID;
            aslLog.LOGTYPE = "INSERT";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = rmsTransmst.INSIPNO;
            aslLog.LOGLTUDE = rmsTransmst.INSLTUDE;
            aslLog.TABLEID = "RMS_TRANSMST";
            aslLog.LOGDATA = Convert.ToString("Transaction Date:" + rmsTransmst.TRANSDT + ",\nYear:" + rmsTransmst.TRANSYY + ",\nMemo NO:" + rmsTransmst.TRANSNO + ",\nTable NO:" + rmsTransmst.TABLENO + ",\nTotal Amount:" + rmsTransmst.TOTAMT + ",\nDiscount Rate: " + rmsTransmst.DISCRTG + ",\nDiscount Amount: " + rmsTransmst.DISCAMTG + ",\nTotal Gross: " + rmsTransmst.TOTGROSS + ",\nVat Rate: " + rmsTransmst.VATRT + ",\nVat Amount: " + rmsTransmst.VATAMT + ",\nService Charge: " + rmsTransmst.SCHARGE + ",\nTotal Net: " + rmsTransmst.TOTNET + ",\nPayment Mode: " + rmsTransmst.TRMODE + ",\nCustomer Name: " + rmsTransmst.CUSTNM + ",\nCustomer Mobile No: " + rmsTransmst.CMOBNO + ",\nCustomer Email ID: " + rmsTransmst.CEMAILID + ".");
            aslLog.USERPC = rmsTransmst.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }


        public void Update_RmsTransMst_LogData(RMS_TRANSMST rmsTransmst)
        {
            var date = Convert.ToString(DateTime.Now.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(DateTime.Now.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == compid && n.USERID == rmsTransmst.INSUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(compid);
            aslLog.USERID = rmsTransmst.INSUSERID;
            aslLog.LOGTYPE = "UPDATE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = rmsTransmst.INSIPNO;
            aslLog.LOGLTUDE = rmsTransmst.INSLTUDE;
            aslLog.TABLEID = "RMS_TRANSMST";
            aslLog.LOGDATA = Convert.ToString("Transaction Date:" + rmsTransmst.TRANSDT + ",\nYear:" + rmsTransmst.TRANSYY + ",\nMemo NO:" + rmsTransmst.TRANSNO + ",\nTable NO:" + rmsTransmst.TABLENO + ",\nTotal Amount:" + rmsTransmst.TOTAMT + ",\nDiscount Rate: " + rmsTransmst.DISCRTG + ",\nDiscount Amount: " + rmsTransmst.DISCAMTG + ",\nTotal Gross: " + rmsTransmst.TOTGROSS + ",\nVat Rate: " + rmsTransmst.VATRT + ",\nVat Amount: " + rmsTransmst.VATAMT + ",\nService Charge: " + rmsTransmst.SCHARGE + ",\nTotal Net: " + rmsTransmst.TOTNET + ",\nPayment Mode: " + rmsTransmst.TRMODE + ",\nCustomer Name: " + rmsTransmst.CUSTNM + ",\nCustomer Mobile No: " + rmsTransmst.CMOBNO + ",\nCustomer Email ID: " + rmsTransmst.CEMAILID + ".");
            aslLog.USERPC = rmsTransmst.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }


        // GET: /Transaction/
        [AcceptVerbs("GET")]
        [ActionName("Index")]
        public ActionResult Index()
        {
            var dt = (CategoryItemModel)TempData["data"];
            return View(dt);
        }


        [AcceptVerbs("POST")]
        [ActionName("Index")]
        public ActionResult IndexPost(CategoryItemModel model, string command)
        {
            if (command == "Add")
            {
                //if (model.RmsTrans.QTY == null && (model.RmsTrans.ITEMID == null || model.RmsTrans.ITEMID == 0))
                //{
                //    ViewBag.errorQty = "Please select a valid quantity !";
                //    ViewBag.errorItemid = "Please Select a Valid Item Name !";
                //    TempData["data"] = model;
                //    TempData["transno"] = model.RmsTrans.TRANSNO;
                //    return View("EditOrder");
                //}
                //else if (model.RmsTrans.ITEMID == null || model.RmsTrans.ITEMID == 0)
                //{
                //    ViewBag.errorItemid = "Please Select a Valid Item Name !";
                //    TempData["data"] = model;
                //    TempData["transno"] = model.RmsTrans.TRANSNO;
                //    return View("EditOrder");
                //}
                //else if (model.RmsTrans.QTY == null)
                //{
                //    ViewBag.errorQty = "Please select a valid quantity !";
                //    TempData["data"] = model;
                //    TempData["transno"] = model.RmsTrans.TRANSNO;
                //    return View("EditOrder");
                //}
                 
                //Permission Check
                Int64 loggedUserID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                var checkPermission = from role in db.AslRoleDbSet
                                      where role.USERID == loggedUserID && role.CONTROLLERNAME == "Transaction" && role.ACTIONNAME == "Index"
                                      select new { role.INSERTR};
                string Insert = "";
                foreach (var VARIABLE in checkPermission)
                {
                    Insert = VARIABLE.INSERTR;
                }

                if (Insert == "I")
                {
                    ViewBag.InsertPermission = "Permission Denied !";
                    return View("Index");
                }
            
                //Validation Check
                if (model.RmsTrans.TABLENO == null && (model.RmsTrans.ITEMID == null || model.RmsTrans.ITEMID == 0) && model.RmsTrans.QTY == null)
                {
                    ViewBag.TableNO = "Table name required!";
                    ViewBag.errorItemid = "Please select a valid item name!";
                    ViewBag.errorQty = "Please select a valid quantity !";
                    return View("Index");
                }
                else if (model.RmsTrans.QTY == null && (model.RmsTrans.ITEMID == null || model.RmsTrans.ITEMID == 0))
                {
                    ViewBag.errorQty = "Please select a valid quantity !";
                    ViewBag.errorItemid = "Please Select a Valid Item Name !";
                    return View("Index");
                }
                else if (model.RmsTrans.TABLENO == null && (model.RmsTrans.ITEMID == null || model.RmsTrans.ITEMID == 0))
                {
                    ViewBag.TableNO = "Table name required!";
                    ViewBag.errorItemid = "Please select a valid item name!";
                    return View("Index");
                }
                else if (model.RmsTrans.TABLENO == null)
                {
                    ViewBag.TableNO = "Table name required!";
                    TempData["data"] = model;
                    TempData["transno"] = model.RmsTrans.TRANSNO;
                    return View("Index");
                }
                else if (model.RmsTrans.ITEMID == null || model.RmsTrans.ITEMID == 0)
                {
                    ViewBag.errorItemid = "Please select a valid item name!";
                    TempData["data"] = model;
                    TempData["transno"] = model.RmsTrans.TRANSNO;
                    return View("Index");
                }
                else if (model.RmsTrans.QTY == null)
                {
                    ViewBag.errorQty = "Please select a valid quantity !";
                    TempData["data"] = model;
                    TempData["transno"] = model.RmsTrans.TRANSNO;
                    return View("Index");
                }

                model.RmsTrans.USERPC = strHostName;
                model.RmsTrans.INSIPNO = ipAddress.ToString();
                model.RmsTrans.INSTIME = td;
                model.RmsTrans.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                model.RmsTrans.TRANSDT = Convert.ToDateTime(transdate);
                model.RmsTrans.TRANSYY = Convert.ToInt64(td.ToString("yyyy"));
                model.RmsTrans.TRANSTP = "sale";

                var res = db.RmsTransDbSet.Where(a => a.TRANSNO == model.RmsTrans.TRANSNO && a.COMPID == compid).Count(a => a.ITEMID == model.RmsTrans.ITEMID) == 0;

                if (res == true)
                {
                    var sid = db.RmsTransDbSet.Where(x => x.TRANSNO == model.RmsTrans.TRANSNO && x.COMPID == compid)
                                  .Max(o => o.ITEMSL);
                    var transno = db.RmsTransDbSet.Where(x => x.TRANSDT == model.RmsTrans.TRANSDT && x.COMPID == compid)
                        .Max(s => s.TRANSNO);
                    if (model.RmsTrans.TRANSNO == null)
                    {
                        if (transno == null)
                        {
                            string date = td.ToString("dd");

                            //if (Convert.ToInt64(date) < 10)
                            //{
                            //    date = "0" + date;
                            //}
                            string month = td.ToString("MM");
                            string years = td.ToString("yy");

                            transno = Convert.ToString(date + month + years + "001");
                            model.RmsTrans.TRANSNO = transno;
                            TempData["transno"] = transno;
                            //Session["transno"] = transno;
                        }
                        else
                        {
                            Int64 convertTransNO = Convert.ToInt64(transno.Substring(6, 3));
                            Int64 transNO_Int = convertTransNO + 1;
                            if (transNO_Int < 10)
                            {
                                model.RmsTrans.TRANSNO = Convert.ToString(transno.Substring(0, 6) + "00" + transNO_Int.ToString());
                            }
                            else if ((10 <= transNO_Int) && (transNO_Int <= 99))
                            {
                                model.RmsTrans.TRANSNO =
                                    Convert.ToString(transno.Substring(0, 6) + "0" + transNO_Int.ToString());
                            }
                            else
                            {
                                model.RmsTrans.TRANSNO = Convert.ToString(transno.Substring(0, 6) + transNO_Int.ToString());
                            }

                            TempData["transno"] = model.RmsTrans.TRANSNO;
                            //Session["transno"] = transno;
                        }

                        if (sid == null)
                        {
                            model.RmsTrans.ITEMSL = 1;
                        }
                        else
                        {
                            model.RmsTrans.ITEMSL = sid + 1;
                        }

                        db.RmsTransDbSet.Add(model.RmsTrans);
                        db.SaveChanges();

                        model.RmsTrans.ITEMSL = 0;
                        model.RmsTrans.ITEMID = 0;
                        model.RmsTrans.QTY = 0;
                        model.RmsTrans.RATE = 0;
                        model.RmsTrans.AMOUNT = 0;
                        model.RmsTrans.DISCAMT = 0;
                        model.RmsTrans.DISCRT = 0;
                        model.RmsTrans.GROSSAMT = 0;

                        model.RmsItemModel.ITEMNM = "";

                        TempData["data"] = model;
                        return RedirectToAction("Index");
                    }

                    else
                    {
                        TempData["transno"] = model.RmsTrans.TRANSNO;
                        model.RmsTrans.ITEMSL = Convert.ToInt64(sid) + 1;


                        db.RmsTransDbSet.Add(model.RmsTrans);
                        db.SaveChanges();

                        model.RmsTrans.ITEMSL = 0;
                        model.RmsTrans.ITEMID = 0;
                        model.RmsTrans.QTY = 0;
                        model.RmsTrans.RATE = 0;
                        model.RmsTrans.AMOUNT = 0;
                        model.RmsTrans.DISCAMT = 0;
                        model.RmsTrans.DISCRT = 0;
                        model.RmsTrans.GROSSAMT = 0;

                        model.RmsItemModel.ITEMNM = "";

                        TempData["data"] = model;
                        return RedirectToAction("Index");
                    }

                }

                else
                {
                    //var some = db.AslRmsTransDbSet.Where(a => a.TRANSNO == model.RmsTrans.TRANSNO && a.COMPID == compid && a.ITEMID == model.RmsTrans.ITEMID && a.ITEMSL == model.RmsTrans.ITEMSL).Select(a => a.RMS_TRANSid);
                    var result = (from n in db.RmsTransDbSet
                                  where n.TRANSNO == model.RmsTrans.TRANSNO &&
                                        n.COMPID == compid &&
                                        n.ITEMID == model.RmsTrans.ITEMID
                                  select new
                                  {
                                      transPID = n.RMS_TRANSid,
                                      sl = n.ITEMSL
                                  }
                           );

                    foreach (var item in result)
                    {
                        model.RmsTrans.RMS_TRANSid = item.transPID;
                        model.RmsTrans.ITEMSL = item.sl;
                    }

                    db.Entry(model.RmsTrans).State = EntityState.Modified;
                    db.SaveChanges();

                    //model.RmsTrans.RMS_TRANSid = 0;
                    model.RmsTrans.ITEMSL = 0;
                    model.RmsTrans.ITEMID = 0;
                    model.RmsTrans.QTY = 0;
                    model.RmsTrans.RATE = 0;
                    model.RmsTrans.AMOUNT = 0;
                    model.RmsTrans.DISCAMT = 0;
                    model.RmsTrans.DISCRT = 0;
                    model.RmsTrans.GROSSAMT = 0;

                    model.RmsItemModel.ITEMNM = "";

                    TempData["transno"] = model.RmsTrans.TRANSNO;
                    TempData["data"] = model;

                    return RedirectToAction("Index");

                }
                //}

            }

            else if (command == "Save")
            {
                if (model.RmsTrans.TABLENO == null && model.RmsTransMst.TRMODE == null && model.RmsTrans.ITEMID == null)
                {
                    ViewBag.TableNO = "Table name required!";
                    ViewBag.TransactionType = "Transaction Type required!";
                    ViewBag.errorItemid = "Please select a valid item name!";
                    return View("Index");
                }
                else if (model.RmsTrans.TABLENO == null && model.RmsTrans.ITEMID == null)
                {
                    ViewBag.TableNO = "Table name required!";
                    ViewBag.errorItemid = "Please select a valid item name!";
                    return View("Index");
                }
                else if (model.RmsTransMst.TRMODE == null && model.RmsTrans.ITEMID == null)
                {
                    ViewBag.TransactionType = "Transaction Type required!";
                    ViewBag.errorItemid = "Please select a valid item name!";
                    return View("Index");
                }
                else if (model.RmsTransMst.TRMODE == null)
                {
                    ViewBag.TransactionType = "Transaction Type required!";
                    return View("Index");
                }
                else if (model.RmsTransMst.TOTNET == 0)
                {
                    ViewBag.addItemList = "Please Add item list!";
                    return View("Index");
                }
                else if (model.RmsTrans.TRANSNO == null)
                {
                    ViewBag.addItemList = "Please Add item list!";
                    return View("Index");
                }


                model.RmsTransMst.USERPC = strHostName;
                model.RmsTransMst.INSIPNO = ipAddress.ToString();
                model.RmsTransMst.INSTIME = td;
                model.RmsTransMst.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                model.RmsTransMst.TRANSDT = Convert.ToDateTime(transdate);
                model.RmsTransMst.TRANSYY = Convert.ToInt64(td.ToString("yyyy"));

                model.RmsTransMst.COMPID = model.RmsTrans.COMPID;
                model.RmsTransMst.TRANSNO = Convert.ToString(model.RmsTrans.TRANSNO);
                model.RmsTransMst.TABLENO = model.RmsTrans.TABLENO;
                model.RmsTransMst.TRANSTP = "sale";

                Insert_RmsTransMst_LogData(model.RmsTransMst);
                db.RmsTransmstDbSet.Add(model.RmsTransMst);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            else if (command == "Complete")
            {
                if (model.RmsTrans.TABLENO == null && model.RmsTrans.ITEMID == null)
                {
                    ViewBag.TableNO = "Table name required!";
                    ViewBag.errorItemid = "Please select a valid item name!";
                    return View("Index");
                }
                else if (model.RmsTrans.ITEMID == null)
                {
                    ViewBag.TransactionType = "Transaction Type required!";
                    ViewBag.errorItemid = "Please select a valid item name!";
                    return View("Index");
                }
                else if (model.RmsTransMst.TOTNET == 0)
                {
                    ViewBag.addItemList = "Please Add item list!";
                    return View("Index");
                }
                else if (model.RmsTrans.TRANSNO == null)
                {
                    ViewBag.addItemList = "Please Add item list!";
                    return View("Index");
                }


                if (model.RmsTransMst.CUSTNM == null && model.RmsTransMst.CMOBNO == null && model.RmsTransMst.CEMAILID == null)
                {
                    ViewBag.CustomerName = "Customer name required !";
                    ViewBag.MobileNo = "Customer mobile no. required !";

                    TempData["transno"] = model.RmsTrans.TRANSNO;
                    TempData["data"] = model;
                    return View("Index");
                }
                else if (model.RmsTransMst.CUSTNM == null)
                {
                    ViewBag.CustomerName = "Customer name required !";

                    TempData["transno"] = model.RmsTrans.TRANSNO;
                    TempData["data"] = model;
                    return View("Index");
                }
                else if (model.RmsTransMst.CMOBNO == null)
                {
                    ViewBag.MobileNo = "Customer mobile no. required !";

                    TempData["transno"] = model.RmsTrans.TRANSNO;
                    TempData["data"] = model;
                    return View("Index");
                }


                model.RmsTransMst.USERPC = strHostName;
                model.RmsTransMst.INSIPNO = ipAddress.ToString();
                model.RmsTransMst.INSTIME = td;
                model.RmsTransMst.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                model.RmsTransMst.TRANSDT = Convert.ToDateTime(transdate);
                model.RmsTransMst.TRANSYY = Convert.ToInt64(td.ToString("yyyy"));

                model.RmsTransMst.COMPID = model.RmsTrans.COMPID;
                model.RmsTransMst.TRANSNO = Convert.ToString(model.RmsTrans.TRANSNO);
                model.RmsTransMst.TABLENO = model.RmsTrans.TABLENO;
                model.RmsTransMst.TRANSTP = "sale";

                Insert_RmsTransMst_LogData(model.RmsTransMst);
                db.RmsTransmstDbSet.Add(model.RmsTransMst);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            else if (command == "Edit")
            {
                //Permission Check
                Int64 loggedUserID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                var checkPermission = from role in db.AslRoleDbSet
                                      where role.USERID == loggedUserID && role.CONTROLLERNAME == "Transaction" && role.ACTIONNAME == "Index"
                                      select new { role.UPDATER };
                string Update = "";
                foreach (var VARIABLE in checkPermission)
                {
                    Update = VARIABLE.UPDATER;
                }

                if (Update == "I")
                {
                    ViewBag.UpdatePermission = "Permission Denied !";
                    return View("Index");
                }
            
                return RedirectToAction("EditOrder");
            }

            else if (command == "A4")
            {
                if (model.RmsTrans.TABLENO == null && model.RmsTransMst.TRMODE == null && model.RmsTrans.ITEMID == null)
                {
                    ViewBag.TableNO = "Table name required!";
                    ViewBag.TransactionType = "Transaction Type required!";
                    ViewBag.errorItemid = "Please select a valid item name!";
                    return View("Index");
                }
                else if (model.RmsTransMst.TRMODE == null && model.RmsTrans.ITEMID == null)
                {
                    ViewBag.TransactionType = "Transaction Type required!";
                    ViewBag.errorItemid = "Please select a valid item name!";
                    return View("Index");
                }
                else if (model.RmsTransMst.TRMODE == null)
                {
                    ViewBag.TransactionType = "Transaction Type required!";
                    return View("Index");
                }
                else if (model.RmsTransMst.TOTNET == 0)
                {
                    ViewBag.addItemList = "Please Add item list!";
                    return View("Index");
                }
                else if (model.RmsTrans.TRANSNO == null)
                {
                    ViewBag.addItemList = "Please Add item list!";
                    return View("Index");
                }

                model.RmsTransMst.USERPC = strHostName;
                model.RmsTransMst.INSIPNO = ipAddress.ToString();
                model.RmsTransMst.INSTIME = td;
                model.RmsTransMst.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                model.RmsTransMst.TRANSDT = Convert.ToDateTime(transdate);
                model.RmsTransMst.TRANSYY = Convert.ToInt64(td.ToString("yyyy"));

                model.RmsTransMst.COMPID = model.RmsTrans.COMPID;
                model.RmsTransMst.TRANSNO = Convert.ToString(model.RmsTrans.TRANSNO);
                model.RmsTransMst.TABLENO = model.RmsTrans.TABLENO;
                model.RmsTransMst.TRANSTP = "sale";

                Insert_RmsTransMst_LogData(model.RmsTransMst);
                db.RmsTransmstDbSet.Add(model.RmsTransMst);
                db.SaveChanges();

                BillingReportViewModel aBillingReportViewModel = new BillingReportViewModel();
                aBillingReportViewModel.MemoNo = model.RmsTrans.TRANSNO;
                aBillingReportViewModel.DateTime = Convert.ToDateTime(transdate);
                aBillingReportViewModel.Command = command;
                aBillingReportViewModel.CompanyID = model.RmsTrans.COMPID;
                return RedirectToAction("transactionMemos", "BillingReport", aBillingReportViewModel);
            }

            else if (command == "POS")
            {
                if (model.RmsTrans.TABLENO == null && model.RmsTransMst.TRMODE == null && model.RmsTrans.ITEMID == null)
                {
                    ViewBag.TableNO = "Table name required!";
                    ViewBag.TransactionType = "Transaction Type required!";
                    ViewBag.errorItemid = "Please select a valid item name!";
                    return View("Index");
                }
                else if (model.RmsTransMst.TRMODE == null && model.RmsTrans.ITEMID == null)
                {
                    ViewBag.TransactionType = "Transaction Type required!";
                    ViewBag.errorItemid = "Please select a valid item name!";
                    return View("Index");
                }
                else if (model.RmsTransMst.TRMODE == null)
                {
                    ViewBag.TransactionType = "Transaction Type required!";
                    return View("Index");
                }
                else if (model.RmsTransMst.TOTNET == 0)
                {
                    ViewBag.addItemList = "Please Add item list!";
                    return View("Index");
                }
                else if (model.RmsTrans.TRANSNO == null)
                {
                    ViewBag.addItemList = "Please Add item list!";
                    return View("Index");
                }

                model.RmsTransMst.USERPC = strHostName;
                model.RmsTransMst.INSIPNO = ipAddress.ToString();
                model.RmsTransMst.INSTIME = td;
                model.RmsTransMst.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                model.RmsTransMst.TRANSDT = Convert.ToDateTime(transdate);
                model.RmsTransMst.TRANSYY = Convert.ToInt64(td.ToString("yyyy"));

                model.RmsTransMst.COMPID = model.RmsTrans.COMPID;
                model.RmsTransMst.TRANSNO = Convert.ToString(model.RmsTrans.TRANSNO);
                model.RmsTransMst.TABLENO = model.RmsTrans.TABLENO;
                model.RmsTransMst.TRANSTP = "sale";

                Insert_RmsTransMst_LogData(model.RmsTransMst);
                db.RmsTransmstDbSet.Add(model.RmsTransMst);
                db.SaveChanges();

                BillingReportViewModel aBillingReportViewModel = new BillingReportViewModel();
                aBillingReportViewModel.MemoNo = model.RmsTrans.TRANSNO;
                aBillingReportViewModel.DateTime = Convert.ToDateTime(transdate);
                aBillingReportViewModel.Command = command;
                aBillingReportViewModel.CompanyID = model.RmsTrans.COMPID;
                return RedirectToAction("transactionMemos", "BillingReport", aBillingReportViewModel);
            }

            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult OrderDelete(int tid, string orderNo, Int64 itemsl, CategoryItemModel model)
        {
            RMS_TRANS rmsTrans = db.RmsTransDbSet.Find(tid);

            db.RmsTransDbSet.Remove(rmsTrans);
            db.SaveChanges();

            var result = (from t in db.RmsTransDbSet
                          where t.COMPID == compid && t.TRANSNO == orderNo
                          select new
                          {
                              tableNO = t.TABLENO,
                          }
             ).Distinct().ToList();


            foreach (var n in result)
            {
                model.RmsTrans.TABLENO = n.tableNO;
            }


            var sid = db.RmsTransDbSet.Where(x => x.TRANSNO == model.RmsTrans.TRANSNO)
                              .Max(o => o.ITEMSL);

            model.RmsTrans.TRANSNO = orderNo;
            model.RmsTrans.ITEMSL = sid;

            TempData["data"] = model;
            //TempData["tableName"] = model.RmsTrans.TABLENO;
            TempData["transno"] = orderNo;

            return RedirectToAction("Index");
            //return View("Index");

        }

        public ActionResult OrderUpdate(int tid, int orderNo, Int64 itemsl, Int64 itemId, CategoryItemModel model)
        {
            model.RmsTrans = db.RmsTransDbSet.Find(tid);
            var item = from r in db.RmsItemDbSet where r.ITEMID == itemId select r.ITEMNM;

            //var iID = db.RmsItemDbSet.Where(x => x.ITEMID == itemId && x.COMPID==compid)
            //                  .Select(x => x.ITEMNM);

            foreach (var it in item)
            {
                model.RmsItemModel.ITEMNM = it.ToString();
            }

            TempData["data"] = model;
            TempData["transno"] = model.RmsTrans.TRANSNO;
            //db.SaveChanges();
            return RedirectToAction("Index");

        }

        [AcceptVerbs("POST")]
        public ActionResult OrderComplete(CategoryItemModel model)
        {
            return RedirectToAction("Index");
        }





        [AcceptVerbs("GET")]
        [ActionName("EditOrder")]
        public ActionResult EditOrder()
        {
            var dt = (CategoryItemModel)TempData["data"];
            return View(dt);
        }


        [AcceptVerbs("POST")]
        [ActionName("EditOrder")]
        public ActionResult EditOrder(CategoryItemModel model, string command)
        {
            if (command == "Add")
            {
                //Validation Check
                if (model.RmsTrans.TRANSDT == null && model.RmsTrans.QTY == null && (model.RmsTrans.ITEMID == null || model.RmsTrans.ITEMID == 0) && model.RmsTrans.TRANSNO == null)
                {
                    ViewBag.TransactionDate = "Transaction date required!";
                    ViewBag.errorQty = "Please select a valid quantity !";
                    ViewBag.errorItemid = "Please Select a Valid Item Name !";
                    ViewBag.MemoNO = "Please select a Memo NO!";
                    //TempData["data"] = model;
                    //TempData["transno"] = model.RmsTrans.TRANSNO;
                    return View("EditOrder");
                }
                else if (model.RmsTrans.TRANSDT == null && model.RmsTrans.QTY == null && (model.RmsTrans.ITEMID == null || model.RmsTrans.ITEMID == 0))
                {
                    ViewBag.TransactionDate = "Transaction date required!";
                    ViewBag.errorQty = "Please select a valid quantity !";
                    ViewBag.errorItemid = "Please Select a Valid Item Name !";
                    TempData["data"] = model;
                    return View("EditOrder");
                }
                else if (model.RmsTrans.QTY == null && (model.RmsTrans.ITEMID == null || model.RmsTrans.ITEMID == 0) && model.RmsTrans.TRANSNO == " ")
                {
                    ViewBag.errorQty = "Please select a valid quantity !";
                    ViewBag.errorItemid = "Please Select a Valid Item Name !";
                    ViewBag.MemoNO = "Please select a Memo NO!";
                    ViewBag.MemoNO = "Please select a Memo NO!";
                    //string date = model.RmsTrans.TRANSDT.ToString();
                    //string currentDate = date.Substring(0, 9);
                    string date = model.RmsTrans.TRANSDT.ToString();
                    DateTime MyDateTime = DateTime.Parse(date);
                    string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
                    TempData["transdate"] = currentDate;
                    TempData["data"] = model;
                    return View("EditOrder");
                }
                else if (model.RmsTrans.TRANSDT == null)
                {
                    ViewBag.TransactionDate = "Transaction date required!";
                    TempData["data"] = model;
                    TempData["transno"] = model.RmsTrans.TRANSNO;
                    return View("EditOrder");
                }
                else if (model.RmsTrans.ITEMID == null || model.RmsTrans.ITEMID == 0)
                {
                    ViewBag.errorItemid = "Please Select a Valid Item Name !";
                    //string date = model.RmsTrans.TRANSDT.ToString();
                    //string currentDate = date.Substring(0, 9);
                    string date = model.RmsTrans.TRANSDT.ToString();
                    DateTime MyDateTime = DateTime.Parse(date);
                    string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
                    TempData["transdate"] = currentDate;
                    TempData["data"] = model;
                    TempData["transno"] = model.RmsTrans.TRANSNO;
                    return View("EditOrder");
                }
                else if (model.RmsTrans.QTY == null)
                {
                    ViewBag.errorQty = "Please select a valid quantity !";
                    //string date = model.RmsTrans.TRANSDT.ToString();
                    //string currentDate = date.Substring(0, 9);
                    string date = model.RmsTrans.TRANSDT.ToString();
                    DateTime MyDateTime = DateTime.Parse(date);
                    string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
                    TempData["transdate"] = currentDate;
                    TempData["data"] = model;
                    TempData["transno"] = model.RmsTrans.TRANSNO;
                    return View("EditOrder");
                }
                else if (model.RmsTrans.TRANSNO == " ")
                {
                    ViewBag.MemoNO = "Please select a Memo NO!";
                    //string date = model.RmsTrans.TRANSDT.ToString();
                    //string currentDate = date.Substring(0, 9);
                    string date = model.RmsTrans.TRANSDT.ToString();
                    DateTime MyDateTime = DateTime.Parse(date);
                    string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
                    TempData["transdate"] = currentDate;
                    TempData["data"] = model;
                    return View("EditOrder");
                }
                else if (model.RmsTrans.TRANSNO == null)
                {
                    ViewBag.MemoNO = "Please select a Memo NO!";
                    //string date = model.RmsTrans.TRANSDT.ToString();
                    //string currentDate = date.Substring(0, 9);
                    string date = model.RmsTrans.TRANSDT.ToString();
                    DateTime MyDateTime = DateTime.Parse(date);
                    string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
                    TempData["transdate"] = currentDate;
                    TempData["data"] = model;
                    return View("EditOrder");
                }

                var res = db.RmsTransDbSet.Where(a => a.TRANSNO == model.RmsTrans.TRANSNO && a.COMPID == compid).Count(a => a.ITEMID == model.RmsTrans.ITEMID) == 0;

                if (res == true)
                {
                    model.RmsTrans.USERPC = strHostName;
                    model.RmsTrans.INSIPNO = ipAddress.ToString();
                    model.RmsTrans.INSTIME = td;
                    model.RmsTrans.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    model.RmsTrans.TRANSDT = Convert.ToDateTime(transdate);
                    model.RmsTrans.TRANSYY = Convert.ToInt64(td.ToString("yyyy"));
                    model.RmsTrans.TRANSTP = "sale";

                    var sid = db.RmsTransDbSet.Where(x => x.TRANSNO == model.RmsTrans.TRANSNO && x.COMPID == compid)
                                  .Max(o => o.ITEMSL);
                    var transno = db.RmsTransDbSet.Where(x => x.TRANSDT == model.RmsTrans.TRANSDT && x.COMPID == compid)
                        .Max(s => s.TRANSNO);
                    if (model.RmsTrans.TRANSNO == null)
                    {
                        if (transno == null)
                        {
                            string date = td.ToString("dd");

                            //if (Convert.ToInt64(date) < 10)
                            //{
                            //    date = "0" + date;
                            //}
                            string month = td.ToString("MM");
                            string years = td.ToString("yy");

                            transno = Convert.ToString(date + month + years + "001");
                            model.RmsTrans.TRANSNO = transno;
                            TempData["transno"] = transno;
                            //Session["transno"] = transno;
                        }
                        else
                        {
                            Int64 convertTransNO = Convert.ToInt64(transno.Substring(6, 3));
                            Int64 transNO_Int = convertTransNO + 1;
                            if (transNO_Int < 10)
                            {
                                model.RmsTrans.TRANSNO = Convert.ToString(transno.Substring(0, 6) + "00" + transNO_Int.ToString());
                            }
                            else if ((10 <= transNO_Int) && (transNO_Int <= 99))
                            {
                                model.RmsTrans.TRANSNO =
                                    Convert.ToString(transno.Substring(0, 6) + "0" + transNO_Int.ToString());
                            }
                            else
                            {
                                model.RmsTrans.TRANSNO = Convert.ToString(transno.Substring(0, 6) + transNO_Int.ToString());
                            }
                            TempData["transno"] = model.RmsTrans.TRANSNO;
                            //Session["transno"] = transno;
                        }

                        if (sid == null)
                        {
                            model.RmsTrans.ITEMSL = 1;
                        }
                        else
                        {
                            model.RmsTrans.ITEMSL = sid + 1;
                        }

                        db.RmsTransDbSet.Add(model.RmsTrans);
                        db.SaveChanges();

                        model.RmsTrans.ITEMSL = 0;
                        model.RmsTrans.ITEMID = 0;
                        model.RmsTrans.QTY = 0;
                        model.RmsTrans.RATE = 0;
                        model.RmsTrans.AMOUNT = 0;
                        model.RmsTrans.DISCAMT = 0;
                        model.RmsTrans.DISCRT = 0;
                        model.RmsTrans.GROSSAMT = 0;

                        model.RmsItemModel.ITEMNM = "";

                        TempData["data"] = model;
                        return RedirectToAction("EditOrder");
                    }

                    else
                    {
                        TempData["transno"] = model.RmsTrans.TRANSNO;
                        model.RmsTrans.ITEMSL = Convert.ToInt64(sid) + 1;

                        db.RmsTransDbSet.Add(model.RmsTrans);
                        db.SaveChanges();

                        //Discount rate, Vat rate, Service charge pass the value.
                        var transMst = (from m in db.RmsTransmstDbSet
                                        where m.COMPID == compid && m.TRANSNO == model.RmsTrans.TRANSNO
                                        select new { m.RMS_TRANSMSTid, m.TOTAMT, m.DISCRTG, m.DISCAMTG, m.TOTGROSS, m.VATRT, m.VATAMT, m.SCHARGE, m.TOTNET, m.TRMODE, m.CUSTNM, m.CMOBNO, m.CEMAILID }).ToList();

                        if (transMst.Count != 0)
                        {
                            foreach (var a in transMst)
                            {
                                TempData["HidendiscountRate"] = a.DISCRTG;
                                TempData["HidenVatRate"] = a.VATRT;
                                TempData["HidenServiceCharge"] = a.SCHARGE;
                                TempData["CustomerName"] = a.CUSTNM;
                                TempData["CMobileNO"] = a.CMOBNO;
                                TempData["CEmailID"] = a.CEMAILID;

                            }

                        }

                        model.RmsTrans.ITEMSL = 0;
                        model.RmsTrans.ITEMID = 0;
                        model.RmsTrans.QTY = 0;
                        model.RmsTrans.RATE = 0;
                        model.RmsTrans.AMOUNT = 0;
                        model.RmsTrans.DISCAMT = 0;
                        model.RmsTrans.DISCRT = 0;
                        model.RmsTrans.GROSSAMT = 0;

                        model.RmsItemModel.ITEMNM = "";

                        //string date = model.RmsTrans.TRANSDT.ToString();
                        //string currentDate = date.Substring(0, 9);
                        string date = model.RmsTrans.TRANSDT.ToString();
                        DateTime MyDateTime = DateTime.Parse(date);
                        string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
                        TempData["transdate"] = currentDate;
                        TempData["data"] = model;
                        return RedirectToAction("EditOrder");
                    }

                }

                else
                {
                    //var some = db.AslRmsTransDbSet.Where(a => a.TRANSNO == model.RmsTrans.TRANSNO && a.COMPID == compid && a.ITEMID == model.RmsTrans.ITEMID && a.ITEMSL == model.RmsTrans.ITEMSL).Select(a => a.RMS_TRANSid);
                    var result = (from n in db.RmsTransDbSet
                                  where n.TRANSNO == model.RmsTrans.TRANSNO &&
                                        n.COMPID == compid &&
                                        n.ITEMID == model.RmsTrans.ITEMID
                                  select new
                                  {
                                      transPID = n.RMS_TRANSid,
                                      sl = n.ITEMSL,
                                      year = n.TRANSYY,
                                      type = n.TRANSTP,
                                      InsertUserId = n.INSUSERID,
                                      InsertTime = n.INSTIME,
                                      InsertIpNo = n.INSIPNO,
                                  }
                           );

                    foreach (var item in result)
                    {
                        model.RmsTrans.RMS_TRANSid = item.transPID;
                        model.RmsTrans.ITEMSL = item.sl;
                        model.RmsTrans.TRANSYY = item.year;
                        model.RmsTrans.TRANSTP = item.type;

                        model.RmsTrans.USERPC = strHostName;
                        model.RmsTrans.INSUSERID = item.InsertUserId;
                        model.RmsTrans.INSTIME = item.InsertTime;
                        model.RmsTrans.INSIPNO = item.InsertIpNo;
                        model.RmsTrans.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                        model.RmsTrans.UPDLTUDE = model.RmsTrans.INSLTUDE;
                        model.RmsTrans.UPDIPNO = ipAddress.ToString();
                        model.RmsTrans.UPDTIME = td;
                    }

                    db.Entry(model.RmsTrans).State = EntityState.Modified;
                    db.SaveChanges();


                    //Discount rate, Vat rate, Service charge pass the value.
                    var transMst = (from m in db.RmsTransmstDbSet
                                    where m.COMPID == compid && m.TRANSNO == model.RmsTrans.TRANSNO
                                    select new { m.RMS_TRANSMSTid, m.TOTAMT, m.DISCRTG, m.DISCAMTG, m.TOTGROSS, m.VATRT, m.VATAMT, m.SCHARGE, m.TOTNET, m.TRMODE, m.CUSTNM, m.CMOBNO, m.CEMAILID }).ToList();

                    if (transMst.Count != 0)
                    {
                        foreach (var a in transMst)
                        {
                            TempData["HidendiscountRate"] = a.DISCRTG;
                            TempData["HidenVatRate"] = a.VATRT;
                            TempData["HidenServiceCharge"] = a.SCHARGE;
                            TempData["CustomerName"] = a.CUSTNM;
                            TempData["CMobileNO"] = a.CMOBNO;
                            TempData["CEmailID"] = a.CEMAILID;

                        }

                    }

                    //model.RmsTrans.RMS_TRANSid = 0;
                    model.RmsTrans.ITEMSL = 0;
                    model.RmsTrans.ITEMID = 0;
                    model.RmsTrans.QTY = 0;
                    model.RmsTrans.RATE = 0;
                    model.RmsTrans.AMOUNT = 0;
                    model.RmsTrans.DISCAMT = 0;
                    model.RmsTrans.DISCRT = 0;
                    model.RmsTrans.GROSSAMT = 0;

                    model.RmsItemModel.ITEMNM = "";

                    //string date = model.RmsTrans.TRANSDT.ToString();
                    //string currentDate = date.Substring(0, 9);
                    string date = model.RmsTrans.TRANSDT.ToString();
                    DateTime MyDateTime = DateTime.Parse(date);
                    string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
                    TempData["transdate"] = currentDate;
                    TempData["transno"] = model.RmsTrans.TRANSNO;
                    TempData["data"] = model;

                    return RedirectToAction("EditOrder");

                }
                //}

            }


            else if (command == "Save")
            {
                //Validation Check
                if (model.RmsTrans.TRANSDT == null && model.RmsTrans.TRANSNO == null && model.RmsTransMst.TOTNET == 0)
                {
                    ViewBag.TransactionDate = "Transaction date required!";
                    //ViewBag.TransactionNO = "Transaction no required!";
                    return View("EditOrder");
                }
                else if (model.RmsTrans.TRANSNO == null)
                {
                    ViewBag.addItemList = "Please Add item list!";
                    return View("EditOrder");
                }

                //update RmstransMaster table
                model.RmsTransMst.TRANSDT = Convert.ToDateTime(transdate);
                var findTransNO = (from n in db.RmsTransmstDbSet
                                   where n.TRANSNO == model.RmsTrans.TRANSNO && n.COMPID == model.RmsTrans.COMPID &&
                          n.TRANSDT == model.RmsTransMst.TRANSDT
                                   select n).ToList();
                if (findTransNO.Count != 0)
                {
                    foreach (RMS_TRANSMST rmsTransmst in findTransNO)
                    {
                        rmsTransmst.USERPC = strHostName;
                        rmsTransmst.UPDLTUDE = rmsTransmst.INSLTUDE;
                        rmsTransmst.UPDIPNO = ipAddress.ToString();
                        rmsTransmst.UPDTIME = td;
                        rmsTransmst.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                        rmsTransmst.TRANSDT = Convert.ToDateTime(transdate);
                        rmsTransmst.TRANSYY = Convert.ToInt64(td.ToString("yyyy"));

                        rmsTransmst.COMPID = model.RmsTrans.COMPID;
                        rmsTransmst.TRANSNO = Convert.ToString(model.RmsTrans.TRANSNO);
                        rmsTransmst.TABLENO = model.RmsTrans.TABLENO;
                        rmsTransmst.TRANSTP = "sale";

                        rmsTransmst.TOTAMT = model.RmsTransMst.TOTAMT;
                        rmsTransmst.DISCRTG = model.RmsTransMst.DISCRTG;
                        rmsTransmst.DISCAMTG = model.RmsTransMst.DISCAMTG;
                        rmsTransmst.TOTGROSS = model.RmsTransMst.TOTGROSS;
                        rmsTransmst.VATRT = model.RmsTransMst.VATRT;
                        rmsTransmst.VATAMT = model.RmsTransMst.VATAMT;
                        rmsTransmst.SCHARGE = model.RmsTransMst.SCHARGE;
                        rmsTransmst.TOTNET = model.RmsTransMst.TOTNET;

                        rmsTransmst.TRMODE = model.RmsTransMst.TRMODE;
                        rmsTransmst.CUSTNM = "";
                        rmsTransmst.CMOBNO = "";
                        rmsTransmst.CEMAILID = "";
                        Update_RmsTransMst_LogData(rmsTransmst);
                    }

                    db.SaveChanges();
                    return RedirectToAction("EditOrder");
                }


                model.RmsTransMst.USERPC = strHostName;
                model.RmsTransMst.INSIPNO = ipAddress.ToString();
                model.RmsTransMst.INSTIME = td;
                model.RmsTransMst.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                model.RmsTransMst.TRANSDT = Convert.ToDateTime(transdate);
                model.RmsTransMst.TRANSYY = Convert.ToInt64(td.ToString("yyyy"));

                model.RmsTransMst.COMPID = model.RmsTrans.COMPID;
                model.RmsTransMst.TRANSNO = Convert.ToString(model.RmsTrans.TRANSNO);
                model.RmsTransMst.TABLENO = model.RmsTrans.TABLENO;
                model.RmsTransMst.TRANSTP = "sale";
                model.RmsTransMst.TRMODE = model.RmsTransMst.TRMODE;
                model.RmsTransMst.CUSTNM = "";
                model.RmsTransMst.CMOBNO = "";
                model.RmsTransMst.CEMAILID = "";

                Insert_RmsTransMst_LogData(model.RmsTransMst);
                db.RmsTransmstDbSet.Add(model.RmsTransMst);
                db.SaveChanges();
                return RedirectToAction("EditOrder");



            }

            else if (command == "Complete")
            {
                //Validation Check
                if (model.RmsTrans.TRANSDT == null && model.RmsTrans.TRANSNO == null && model.RmsTransMst.TOTNET == 0 && model.RmsTransMst.CUSTNM == null && model.RmsTransMst.CMOBNO == null)
                {
                    ViewBag.TransactionDate = "Transaction date required!";
                    ViewBag.addItemList = "Please Add item list!";
                    ViewBag.CustomerName = "Customer name required !";
                    ViewBag.MobileNo = "Customer mobile no. required !";
                    //ViewBag.TransactionNO = "Transaction no required!";
                    return View("EditOrder");
                }
                else if (model.RmsTrans.TRANSNO == null)
                {
                    ViewBag.addItemList = "Please Add item list!";
                    return View("EditOrder");
                }


                //Validation Check
                if (model.RmsTransMst.CUSTNM == null && model.RmsTransMst.CMOBNO == null)
                {
                    ViewBag.CustomerName = "Customer name required !";
                    ViewBag.MobileNo = "Customer mobile no. required !";

                    //string date = model.RmsTrans.TRANSDT.ToString();
                    //string currentDate = date.Substring(0, 9);
                    string date = model.RmsTrans.TRANSDT.ToString();
                    DateTime MyDateTime = DateTime.Parse(date);
                    string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
                    TempData["transdate"] = currentDate;
                    TempData["transno"] = model.RmsTrans.TRANSNO;
                    TempData["data"] = model;
                    return View("EditOrder");
                }
                else if (model.RmsTransMst.CUSTNM == null)
                {
                    ViewBag.CustomerName = "Customer name required !";

                    //string date = model.RmsTrans.TRANSDT.ToString();
                    //string currentDate = date.Substring(0, 9);
                    string date = model.RmsTrans.TRANSDT.ToString();
                    DateTime MyDateTime = DateTime.Parse(date);
                    string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
                    TempData["transdate"] = currentDate;
                    TempData["transno"] = model.RmsTrans.TRANSNO;
                    TempData["data"] = model;
                    return View("EditOrder");
                }
                else if (model.RmsTransMst.CMOBNO == null)
                {
                    ViewBag.MobileNo = "Customer mobile no. required !";

                    //string date = model.RmsTrans.TRANSDT.ToString();
                    //string currentDate = date.Substring(0, 9);
                    string date = model.RmsTrans.TRANSDT.ToString();
                    DateTime MyDateTime = DateTime.Parse(date);
                    string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
                    TempData["transdate"] = currentDate;
                    TempData["transno"] = model.RmsTrans.TRANSNO;
                    TempData["data"] = model;
                    return View("EditOrder");
                }



                //update RmstransMaster table
                model.RmsTransMst.TRANSDT = Convert.ToDateTime(transdate);
                var findTransNO = (from n in db.RmsTransmstDbSet
                                   where n.TRANSNO == model.RmsTrans.TRANSNO && n.COMPID == model.RmsTrans.COMPID &&
                          n.TRANSDT == model.RmsTransMst.TRANSDT
                                   select n).ToList();
                if (findTransNO.Count != 0)
                {
                    foreach (RMS_TRANSMST rmsTransmst in findTransNO)
                    {
                        rmsTransmst.USERPC = strHostName;
                        rmsTransmst.UPDLTUDE = rmsTransmst.INSLTUDE;
                        rmsTransmst.UPDIPNO = ipAddress.ToString();
                        rmsTransmst.UPDTIME = td;
                        rmsTransmst.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                        rmsTransmst.TRANSDT = Convert.ToDateTime(transdate);
                        rmsTransmst.TRANSYY = Convert.ToInt64(td.ToString("yyyy"));

                        rmsTransmst.COMPID = model.RmsTrans.COMPID;
                        rmsTransmst.TRANSNO = Convert.ToString(model.RmsTrans.TRANSNO);
                        rmsTransmst.TABLENO = model.RmsTrans.TABLENO;
                        rmsTransmst.TRANSTP = "sale";

                        rmsTransmst.TOTAMT = model.RmsTransMst.TOTAMT;
                        rmsTransmst.DISCRTG = model.RmsTransMst.DISCRTG;
                        rmsTransmst.DISCAMTG = model.RmsTransMst.DISCAMTG;
                        rmsTransmst.TOTGROSS = model.RmsTransMst.TOTGROSS;
                        rmsTransmst.VATRT = model.RmsTransMst.VATRT;
                        rmsTransmst.VATAMT = model.RmsTransMst.VATAMT;
                        rmsTransmst.SCHARGE = model.RmsTransMst.SCHARGE;
                        rmsTransmst.TOTNET = model.RmsTransMst.TOTNET;

                        rmsTransmst.TRMODE = model.RmsTransMst.TRMODE;
                        rmsTransmst.CUSTNM = model.RmsTransMst.CUSTNM;
                        rmsTransmst.CMOBNO = model.RmsTransMst.CMOBNO;
                        rmsTransmst.CEMAILID = model.RmsTransMst.CEMAILID;
                        Update_RmsTransMst_LogData(rmsTransmst);
                    }
                    db.SaveChanges();
                    return RedirectToAction("EditOrder");
                }


                model.RmsTransMst.USERPC = strHostName;
                model.RmsTransMst.INSIPNO = ipAddress.ToString();
                model.RmsTransMst.INSTIME = td;
                model.RmsTransMst.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                model.RmsTransMst.TRANSDT = Convert.ToDateTime(transdate);
                model.RmsTransMst.TRANSYY = Convert.ToInt64(td.ToString("yyyy"));

                model.RmsTransMst.COMPID = model.RmsTrans.COMPID;
                model.RmsTransMst.TRANSNO = Convert.ToString(model.RmsTrans.TRANSNO);
                model.RmsTransMst.TABLENO = model.RmsTrans.TABLENO;
                model.RmsTransMst.TRANSTP = "sale";

                Insert_RmsTransMst_LogData(model.RmsTransMst);
                db.RmsTransmstDbSet.Add(model.RmsTransMst);
                db.SaveChanges();
                return RedirectToAction("EditOrder");
            }

            else if (command == "search")
            {
                model.RmsTrans.ITEMSL = 0;
                model.RmsTrans.ITEMID = 0;
                model.RmsTrans.QTY = 0;
                model.RmsTrans.RATE = 0;
                model.RmsTrans.AMOUNT = 0;
                model.RmsTrans.DISCAMT = 0;
                model.RmsTrans.DISCRT = 0;
                model.RmsTrans.GROSSAMT = 0;
                model.RmsItemModel.ITEMNM = "";

                var transMst = (from m in db.RmsTransmstDbSet
                                where m.TRANSDT == model.RmsTrans.TRANSDT && m.COMPID == compid && m.TRANSNO == model.RmsTrans.TRANSNO
                                select new { m.RMS_TRANSMSTid, m.TOTAMT, m.DISCRTG, m.DISCAMTG, m.TOTGROSS, m.VATRT, m.VATAMT, m.SCHARGE, m.TOTNET, m.TRMODE, m.CUSTNM, m.CMOBNO, m.CEMAILID }).ToList();

                if (transMst != null)
                {
                    foreach (var a in transMst)
                    {
                        TempData["totalAmount"] = a.TOTAMT;
                        TempData["discountRate"] = a.DISCRTG;
                        TempData["discountAmount"] = a.DISCAMTG;
                        TempData["GrossAmount"] = a.TOTGROSS;
                        TempData["VatRate"] = a.VATRT;
                        TempData["VatAmount"] = a.VATAMT;
                        TempData["ServiceCharge"] = a.SCHARGE;
                        TempData["NetAmount"] = a.TOTNET;
                        TempData["TrMode"] = a.TRMODE;
                        TempData["CustomerName"] = a.CUSTNM;
                        TempData["CMobileNO"] = a.CMOBNO;
                        TempData["CEmailID"] = a.CEMAILID;

                    }

                }


                string date = model.RmsTrans.TRANSDT.ToString();
                DateTime MyDateTime = DateTime.Parse(date);
                string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
                TempData["transdate"] = currentDate;
                TempData["transno"] = model.RmsTrans.TRANSNO;
                TempData["data"] = model;
                return RedirectToAction("EditOrder");
            }

            else if (command == "A4")
            {
                //Validation Check
                if (model.RmsTrans.TRANSDT == null && model.RmsTrans.TRANSNO == null && model.RmsTransMst.TOTNET == 0)
                {
                    ViewBag.TransactionDate = "Transaction date required!";
                    //ViewBag.TransactionNO = "Transaction no required!";
                    return View("EditOrder");
                }
                else if (model.RmsTrans.TRANSNO == null)
                {
                    ViewBag.addItemList = "Please Add item list!";
                    return View("EditOrder");
                }


                //update RmstransMaster table
                model.RmsTransMst.TRANSDT = Convert.ToDateTime(transdate);
                var findTransNO = (from n in db.RmsTransmstDbSet
                                   where n.TRANSNO == model.RmsTrans.TRANSNO && n.COMPID == model.RmsTrans.COMPID &&
                          n.TRANSDT == model.RmsTransMst.TRANSDT
                                   select n).ToList();
                if (findTransNO.Count != 0)
                {
                    foreach (RMS_TRANSMST rmsTransmst in findTransNO)
                    {
                        rmsTransmst.USERPC = strHostName;
                        rmsTransmst.UPDLTUDE = rmsTransmst.INSLTUDE;
                        rmsTransmst.UPDIPNO = ipAddress.ToString();
                        rmsTransmst.UPDTIME = td;
                        rmsTransmst.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                        rmsTransmst.TRANSDT = Convert.ToDateTime(transdate);
                        rmsTransmst.TRANSYY = Convert.ToInt64(td.ToString("yyyy"));

                        rmsTransmst.COMPID = model.RmsTrans.COMPID;
                        rmsTransmst.TRANSNO = Convert.ToString(model.RmsTrans.TRANSNO);
                        rmsTransmst.TABLENO = model.RmsTrans.TABLENO;
                        rmsTransmst.TRANSTP = "sale";

                        rmsTransmst.TOTAMT = model.RmsTransMst.TOTAMT;
                        rmsTransmst.DISCRTG = model.RmsTransMst.DISCRTG;
                        rmsTransmst.DISCAMTG = model.RmsTransMst.DISCAMTG;
                        rmsTransmst.TOTGROSS = model.RmsTransMst.TOTGROSS;
                        rmsTransmst.VATRT = model.RmsTransMst.VATRT;
                        rmsTransmst.VATAMT = model.RmsTransMst.VATAMT;
                        rmsTransmst.SCHARGE = model.RmsTransMst.SCHARGE;
                        rmsTransmst.TOTNET = model.RmsTransMst.TOTNET;

                        rmsTransmst.TRMODE = model.RmsTransMst.TRMODE;
                        rmsTransmst.CUSTNM = "";
                        rmsTransmst.CMOBNO = "";
                        rmsTransmst.CEMAILID = "";
                        Update_RmsTransMst_LogData(rmsTransmst);
                    }
                    db.SaveChanges();

                }
                else
                {
                    model.RmsTransMst.USERPC = strHostName;
                    model.RmsTransMst.INSIPNO = ipAddress.ToString();
                    model.RmsTransMst.INSTIME = td;
                    model.RmsTransMst.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    model.RmsTransMst.TRANSDT = Convert.ToDateTime(transdate);
                    model.RmsTransMst.TRANSYY = Convert.ToInt64(td.ToString("yyyy"));

                    model.RmsTransMst.COMPID = model.RmsTrans.COMPID;
                    model.RmsTransMst.TRANSNO = Convert.ToString(model.RmsTrans.TRANSNO);
                    model.RmsTransMst.TABLENO = model.RmsTrans.TABLENO;
                    model.RmsTransMst.TRANSTP = "sale";

                    Insert_RmsTransMst_LogData(model.RmsTransMst);
                    db.RmsTransmstDbSet.Add(model.RmsTransMst);
                    db.SaveChanges();
                }

                BillingReportViewModel aBillingReportViewModel = new BillingReportViewModel();
                aBillingReportViewModel.MemoNo = model.RmsTrans.TRANSNO;
                aBillingReportViewModel.DateTime = Convert.ToDateTime(transdate);
                aBillingReportViewModel.Command = command;
                aBillingReportViewModel.CompanyID = model.RmsTrans.COMPID;
                return RedirectToAction("transactionMemos", "BillingReport", aBillingReportViewModel);
            }

            else if (command == "POS")
            {
                //Validation Check
                if (model.RmsTrans.TRANSDT == null && model.RmsTrans.TRANSNO == null && model.RmsTransMst.TOTNET == 0)
                {
                    ViewBag.TransactionDate = "Transaction date required!";
                    //ViewBag.TransactionNO = "Transaction no required!";
                    return View("EditOrder");
                }
                else if (model.RmsTrans.TRANSNO == null)
                {
                    ViewBag.addItemList = "Please Add item list!";
                    return View("EditOrder");
                }


                //update RmstransMaster table
                model.RmsTransMst.TRANSDT = Convert.ToDateTime(transdate);
                var findTransNO = (from n in db.RmsTransmstDbSet
                                   where n.TRANSNO == model.RmsTrans.TRANSNO && n.COMPID == model.RmsTrans.COMPID &&
                          n.TRANSDT == model.RmsTransMst.TRANSDT
                                   select n).ToList();
                if (findTransNO.Count != 0)
                {
                    foreach (RMS_TRANSMST rmsTransmst in findTransNO)
                    {
                        rmsTransmst.USERPC = strHostName;
                        rmsTransmst.UPDLTUDE = rmsTransmst.INSLTUDE;
                        rmsTransmst.UPDIPNO = ipAddress.ToString();
                        rmsTransmst.UPDTIME = td;
                        rmsTransmst.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                        rmsTransmst.TRANSDT = Convert.ToDateTime(transdate);
                        rmsTransmst.TRANSYY = Convert.ToInt64(td.ToString("yyyy"));

                        rmsTransmst.COMPID = model.RmsTrans.COMPID;
                        rmsTransmst.TRANSNO = Convert.ToString(model.RmsTrans.TRANSNO);
                        rmsTransmst.TABLENO = model.RmsTrans.TABLENO;
                        rmsTransmst.TRANSTP = "sale";

                        rmsTransmst.TOTAMT = model.RmsTransMst.TOTAMT;
                        rmsTransmst.DISCRTG = model.RmsTransMst.DISCRTG;
                        rmsTransmst.DISCAMTG = model.RmsTransMst.DISCAMTG;
                        rmsTransmst.TOTGROSS = model.RmsTransMst.TOTGROSS;
                        rmsTransmst.VATRT = model.RmsTransMst.VATRT;
                        rmsTransmst.VATAMT = model.RmsTransMst.VATAMT;
                        rmsTransmst.SCHARGE = model.RmsTransMst.SCHARGE;
                        rmsTransmst.TOTNET = model.RmsTransMst.TOTNET;

                        rmsTransmst.TRMODE = model.RmsTransMst.TRMODE;
                        rmsTransmst.CUSTNM = "";
                        rmsTransmst.CMOBNO = "";
                        rmsTransmst.CEMAILID = "";
                        Update_RmsTransMst_LogData(rmsTransmst);
                    }
                    db.SaveChanges();

                }
                else
                {
                    model.RmsTransMst.USERPC = strHostName;
                    model.RmsTransMst.INSIPNO = ipAddress.ToString();
                    model.RmsTransMst.INSTIME = td;
                    model.RmsTransMst.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    model.RmsTransMst.TRANSDT = Convert.ToDateTime(transdate);
                    model.RmsTransMst.TRANSYY = Convert.ToInt64(td.ToString("yyyy"));

                    model.RmsTransMst.COMPID = model.RmsTrans.COMPID;
                    model.RmsTransMst.TRANSNO = Convert.ToString(model.RmsTrans.TRANSNO);
                    model.RmsTransMst.TABLENO = model.RmsTrans.TABLENO;
                    model.RmsTransMst.TRANSTP = "sale";

                    Insert_RmsTransMst_LogData(model.RmsTransMst);
                    db.RmsTransmstDbSet.Add(model.RmsTransMst);
                    db.SaveChanges();
                }

                BillingReportViewModel aBillingReportViewModel = new BillingReportViewModel();
                aBillingReportViewModel.MemoNo = model.RmsTrans.TRANSNO;
                aBillingReportViewModel.DateTime = Convert.ToDateTime(transdate);
                aBillingReportViewModel.Command = command;
                aBillingReportViewModel.CompanyID = model.RmsTrans.COMPID;
                return RedirectToAction("transactionMemos", "BillingReport", aBillingReportViewModel);
            }

            else if (command == "New")
            {
                return RedirectToAction("Index");
            }

            else
            {
                return RedirectToAction("EditOrder");
            }

        }

        public ActionResult EditOrderDelete(int tid, string orderNo, Int64 itemsl, CategoryItemModel model)
        {
            //Permission Check
            Int64 loggedUserID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
            var checkPermission = from role in db.AslRoleDbSet
                                  where role.USERID == loggedUserID && role.CONTROLLERNAME == "Transaction" && role.ACTIONNAME == "Index"
                                  select new { role.DELETER };
            string Delete = "";
            foreach (var VARIABLE in checkPermission)
            {
                Delete = VARIABLE.DELETER;
            }
            if (Delete == "I")
            {
                TempData["DeletePermission"] = "Delete Permission Denied !";
                //TempData["data"] = model;
                //TempData["transno"] = orderNo;
                return RedirectToAction("EditOrder");
            }

            RMS_TRANS rmsTrans = db.RmsTransDbSet.Find(tid);
            db.RmsTransDbSet.Remove(rmsTrans);
            db.SaveChanges();
            var result = (from t in db.RmsTransDbSet
                          where t.COMPID == compid && t.TRANSNO == orderNo
                          select new { date = t.TRANSDT }).ToList();

            foreach (var n in result)
            {
                //model.RmsTrans.TRANSNO =n.transno;
                //model.RmsTrans.COMPID = n.comid;
                //model.RmsTrans.ITEMSL = n.sl;
                model.RmsTrans.TRANSDT = n.date;
            }


            //Minimum RmsTrans Table MemoNO delete then RmsTransMst tabel data delete(key-> memoNO, compid)
            if (result.Count == 0)
            {
                var searchRmsTransMst = from n in db.RmsTransmstDbSet where n.COMPID == compid && n.TRANSNO == orderNo select n;
                foreach (var m in searchRmsTransMst)
                {
                    db.RmsTransmstDbSet.Remove(m);
                }
                db.SaveChanges();
            }

            //Discount rate, Vat rate, Service charge pass the value.
            var transMst = (from m in db.RmsTransmstDbSet
                            where m.COMPID == compid && m.TRANSNO == orderNo
                            select new { m.RMS_TRANSMSTid, m.TOTAMT, m.DISCRTG, m.DISCAMTG, m.TOTGROSS, m.VATRT, m.VATAMT, m.SCHARGE, m.TOTNET, m.TRMODE, m.CUSTNM, m.CMOBNO, m.CEMAILID }).ToList();

            if (transMst.Count != 0)
            {
                foreach (var a in transMst)
                {
                    TempData["HidendiscountRate"] = a.DISCRTG;
                    TempData["HidenVatRate"] = a.VATRT;
                    TempData["HidenServiceCharge"] = a.SCHARGE;
                    TempData["CustomerName"] = a.CUSTNM;
                    TempData["CMobileNO"] = a.CMOBNO;
                    TempData["CEmailID"] = a.CEMAILID;

                }

            }


            var sid = db.RmsTransDbSet.Where(x => x.TRANSNO == model.RmsTrans.TRANSNO).Max(o => o.ITEMSL);
            model.RmsTrans.TRANSNO = orderNo;
            model.RmsTrans.ITEMSL = sid;

            if (model.RmsTrans.TRANSDT != null)
            {
                //string date = model.RmsTrans.TRANSDT.ToString();
                //string currentDate = date.Substring(0, 9);
                string date = model.RmsTrans.TRANSDT.ToString();
                DateTime MyDateTime = DateTime.Parse(date);
                string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
                TempData["transdate"] = currentDate;
            }

            TempData["data"] = model;
            TempData["transno"] = orderNo;

            return RedirectToAction("EditOrder");
            //return View("Index");

        }

        public ActionResult EditOrderUpdate(int tid, string orderNo, Int64 itemsl, Int64 itemId, CategoryItemModel model)
        {
            model.RmsTrans = db.RmsTransDbSet.Find(tid);
            var item = from r in db.RmsItemDbSet where r.ITEMID == itemId select r.ITEMNM;

            //var iID = db.RmsItemDbSet.Where(x => x.ITEMID == itemId && x.COMPID==compid)
            //                  .Select(x => x.ITEMNM);

            foreach (var it in item)
            {
                model.RmsItemModel.ITEMNM = it.ToString();
            }

            //Discount rate, Vat rate, Service charge pass the value.
            var transMst = (from m in db.RmsTransmstDbSet
                            where m.COMPID == compid && m.TRANSNO == orderNo
                            select new { m.RMS_TRANSMSTid, m.TOTAMT, m.DISCRTG, m.DISCAMTG, m.TOTGROSS, m.VATRT, m.VATAMT, m.SCHARGE, m.TOTNET, m.TRMODE, m.CUSTNM, m.CMOBNO, m.CEMAILID }).ToList();

            if (transMst.Count != 0)
            {
                foreach (var a in transMst)
                {
                    TempData["HidendiscountRate"] = a.DISCRTG;
                    TempData["HidenVatRate"] = a.VATRT;
                    TempData["HidenServiceCharge"] = a.SCHARGE;
                    TempData["CustomerName"] = a.CUSTNM;
                    TempData["CMobileNO"] = a.CMOBNO;
                    TempData["CEmailID"] = a.CEMAILID;

                }

            }



            //string date = model.RmsTrans.TRANSDT.ToString();
            //string currentDate = date.Substring(0, 9);
            string date = model.RmsTrans.TRANSDT.ToString();
            DateTime MyDateTime = DateTime.Parse(date);
            string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
            TempData["transdate"] = currentDate;
            TempData["data"] = model;
            TempData["transno"] = model.RmsTrans.TRANSNO;
            //db.SaveChanges();
            return RedirectToAction("EditOrder");

        }

        public JsonResult DateChanged(string theDate)
        {

            DateTime dt = Convert.ToDateTime(theDate);

            DateTime dd = DateTime.Parse(theDate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);

            DateTime datetm = Convert.ToDateTime(dd);

            List<SelectListItem> trans = new List<SelectListItem>();

            //var transres = db.AslRmsTransDbSet
            //    .Where(n=>DateTime.Parse(n.TRANSNO)==datetm && n.COMPID==compid)
            //                   .Select(c =>c.TRANSNO )
            //                   .Distinct()
            //                   .ToList();


            var transres = (from n in db.RmsTransDbSet
                            where n.TRANSDT == dd && n.COMPID == compid
                            select new
                            {
                                n.TRANSNO
                            }
                            )
                            .Distinct()
                            .ToList();

            //var transMst = (from m in db.RmsTransmstDbSet
            //                where m.TRANSDT == dd && m.COMPID == compid
            //                select new { m.TRANSNO }).ToList();

            //var finalList = from item1 in transres
            //                where !(transMst.Any(item2 => item2.TRANSNO == item1.TRANSNO))
            //                orderby item1.TRANSNO
            //                select item1;

            string transNO = null;
            foreach (var f in transres)
            {
                if (transNO == null)
                {
                    transNO = Convert.ToString(f.TRANSNO);
                }
                trans.Add(new SelectListItem { Text = f.TRANSNO.ToString(), Value = f.TRANSNO.ToString() });
            }

            var FirsttransNO = new { TransNO = transNO };
            //return Json(new SelectList(trans, "Value", "Text"), FirsttransNO.ToString());
            return Json(new SelectList(trans, "Value", "Text"));
            //return Json(transres, JsonRequestBehavior.AllowGet);
        }



        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult TransNoChanged(string changedDropDown)
        {
            // var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());

            string tableNumber = "", transType = "";
            DateTime trandate;

            string transno = Convert.ToString(changedDropDown);

            TempData["transno"] = transno;

            var rt = db.RmsTransDbSet.Where(n => n.TRANSNO == transno && n.COMPID == compid).Select(n => new
            {
                tableno = n.TABLENO,
                transtype = n.TRANSTP


            });

            foreach (var n in rt)
            {
                tableNumber = n.tableno;
                transType = n.transtype;

            }

            var result = new { TABLENO = tableNumber, TRANSTP = transType, TRANSNO = transno };
            return Json(result, JsonRequestBehavior.AllowGet);

        }




        //[HttpPost]
        //public ActionResult TransNoChanged(Int64 changedDropDown)
        //{
        //    string tableNumber = "", transType = "";

        //    Int64 transno = Convert.ToInt64(changedDropDown);

        //    CategoryItemModel model = new CategoryItemModel();

        //    var rt = db.AslRmsTransDbSet.Where(n => n.TRANSNO == transno && n.COMPID == compid).Select(n => new
        //    {
        //        tableno = n.TABLENO,
        //        transtype = n.TRANSTP,
        //        transdate = n.TRANSDT,
        //        trans = n.TRANSNO

        //    });

        //    foreach (var n in rt)
        //    {
        //        model.RmsTrans.TRANSDT = n.transdate;
        //        model.RmsTrans.TABLENO = n.tableno;
        //        model.RmsTrans.TRANSTP = n.transtype;
        //        model.RmsTrans.TRANSNO = n.trans;
        //    }

        //    model.RmsTrans.QTY = 0;
        //    model.RmsTrans.RATE = 0;
        //    model.RmsTrans.AMOUNT = 0;
        //    model.RmsTrans.DISCRT = 0;
        //    model.RmsTrans.DISCAMT = 0;
        //    model.RmsTrans.GROSSAMT = 0;

        //    TempData["transno"] = transno;
        //    TempData["data"] = model;

        //    return RedirectToAction("EditOrder");


        //}




        //AutoComplete 
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult ItemNameChanged(string changedText)
        {
            var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());
            string itemId = "";
            string rate = "";
            string discount = "";
            decimal qty = 0;
            var rt = db.RmsItemDbSet.Where(n => n.ITEMNM == changedText &&
                                                         n.COMPID == compid).Select(n => new
                                                         {
                                                             itemid = n.ITEMID,
                                                             rate = n.SALRT,
                                                             discount = n.DISCOUNT
                                                         });
            foreach (var n in rt)
            {
                itemId = Convert.ToString(n.itemid);
                rate = Convert.ToString(n.rate);
                discount = Convert.ToString(n.discount);
            }

            var result = new { itemid = itemId, rate = rate, discount = discount, qty = 1 };
            return Json(result, JsonRequestBehavior.AllowGet);

        }




        //AutoComplete
        public JsonResult TagSearch(string term, string changedDropDown)
        {
            var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());

            //Int64 categoryID = Convert.ToInt64(changedDropDown);
            if (changedDropDown == "")
            {
                var tags = from p in db.RmsItemDbSet
                           where p.COMPID == compid
                           select p.ITEMNM;

                return this.Json(tags.Where(t => t.StartsWith(term)),
                           JsonRequestBehavior.AllowGet);
            }
            else
            {
                var catid = Convert.ToInt64(changedDropDown);

                var tags = from p in db.RmsItemDbSet
                           where p.COMPID == compid && p.CATID == catid
                           select p.ITEMNM;

                return this.Json(tags.Where(t => t.StartsWith(term)),
                           JsonRequestBehavior.AllowGet);
            }



        }




    }
}
