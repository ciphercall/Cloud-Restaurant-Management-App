﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Mvc_RestaurantAPP.Models;
using Newtonsoft.Json.Serialization;

namespace Mvc_RestaurantAPP.Controllers
{
    public class ASL_ROLEController : AppController
    {
        //Datetime formet
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        public DateTime td = DateTime.Now;

        RestaurantDbContext db = new RestaurantDbContext();
        //Get Ip ADDRESS,Time & user PC Name
        public string strHostName;
        public IPHostEntry ipHostInfo;
        public IPAddress ipAddress;

        public ASL_ROLEController()
        {
            strHostName = System.Net.Dns.GetHostName();
            ipHostInfo = Dns.Resolve(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[0];

            td = DateTime.Now;
        }


        public ASL_LOG aslLog = new ASL_LOG();
        // Insert ALL INFORMATION when User update the permission list.
        public void Update_ASl_Role_LogData(ASL_LOG aslLogRef)
        {
            var date = Convert.ToString(DateTime.Now.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(DateTime.Now.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == aslLogRef.COMPID && n.USERID == aslLogRef.USERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(aslLogRef.COMPID);
            aslLog.USERID = aslLogRef.USERID;
            aslLog.LOGTYPE = "UPDATE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = aslLogRef.LOGIPNO;
            aslLog.LOGLTUDE = aslLogRef.LOGLTUDE;
            aslLog.TABLEID = "ASL_ROLE";
            aslLog.LOGDATA = aslLogRef.LOGDATA;
            aslLog.USERPC = aslLogRef.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }




        // GET: /ASL_ROLE/
        public ActionResult ViewRoleList()
        {
            Exception exception;
            HandleErrorInfo errorInfo;
            try
            {
                Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
                var optp = System.Web.HttpContext.Current.Session["LoggedUserType"].ToString();

                if (optp == "AslSuperadmin")
                {
                    return View(db.AslRoleDbSet);
                }
                else
                {
                    List<ASL_ROLE> aslRoles;

                    var result = (from n in db.AslRoleDbSet
                                  where n.COMPID == compid
                                  select n
                        );

                    return View(result);
                }

            }

            catch
            {
                ViewBag.Message = "Unable to recognize the Client!";
                exception = new Exception("Unable to recognize the Client!");
                errorInfo = new HandleErrorInfo(exception, "Home", "Index");
                return View("Error", errorInfo);
            }

            return View(db.AslRoleDbSet);
        }


        //
        // GET: /ASL_ROLE/ + GridView load
        [AcceptVerbs("GET")]
        [ActionName("Index")]
        public ActionResult Index()
        {
            if (TempData["ErrorFieldMessage"] != null)
            {
                return View();
            }

            if (TempData["data_F"] != null)
            {
                var dt = (RoleModel)TempData["data_F"];
                return View(dt);
            }
            else if (TempData["data_R"] != null)
            {
                var dt = (RoleModel)TempData["data_R"];
                return View(dt);
            }
            else
            {
                var dt = (RoleModel)TempData["data"];
                return View(dt);
            }

        }



        [AcceptVerbs("POST")]
        [ActionName("Index")]
        public ActionResult IndexPost(RoleModel roleModel, string command)
        {
            if (command == "Update")
            {
                string msg;
                try
                {
                    var query =
                            from a in db.AslRoleDbSet
                            where (a.MENUID == roleModel.AslRole.MENUID && a.USERID == roleModel.AslRole.USERID
                            && a.COMPID == roleModel.AslRole.COMPID && a.MODULEID == roleModel.AslRole.MODULEID)
                            select a;

                    if (roleModel.AslRole.STATUS == null)
                    {
                        roleModel.AslRole.STATUS = "I";
                    }
                    if (roleModel.AslRole.INSERTR == null)
                    {
                        roleModel.AslRole.INSERTR = "I";
                    }
                    if (roleModel.AslRole.UPDATER == null)
                    {
                        roleModel.AslRole.UPDATER = "I";
                    }
                    if (roleModel.AslRole.DELETER == null)
                    {
                        roleModel.AslRole.DELETER = "I";
                    }

                    foreach (ASL_ROLE a in query)
                    {
                        // Insert any additional changes to column values.
                        a.STATUS = roleModel.AslRole.STATUS;
                        a.INSERTR = roleModel.AslRole.INSERTR;
                        a.UPDATER = roleModel.AslRole.UPDATER;
                        a.DELETER = roleModel.AslRole.DELETER;

                        //Get Ip ADDRESS,Time & user PC Name
                        string strHostName = System.Net.Dns.GetHostName();
                        IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                        IPAddress ipAddress = ipHostInfo.AddressList[0];

                        a.INSUSERID = a.INSUSERID;
                        a.INSIPNO = a.INSIPNO;
                        a.INSTIME = a.INSTIME;
                        a.USERPC = strHostName;
                        a.UPDIPNO = ipAddress.ToString();
                        a.UPDTIME = Convert.ToDateTime(DateTime.Now.ToString("d"));
                        a.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                        TempData["aslRoleUserPc"] = a.USERPC;
                        TempData["aslRoleUpdateIPNo"] = a.UPDIPNO;
                        TempData["aslRoleCompanyID"] = a.COMPID;

                        TempData["aslRoleLogData"] = Convert.ToString("Company ID: " + a.COMPID + ",\nUser ID: " + a.USERID + ",\nModuleID: " + a.MODULEID + ",\nMenu Type: " + a.MENUTP + ",\nMenu ID: " + a.MENUID + ",\nStatus: " + a.STATUS + ",\nInsert: " + a.INSERTR + ",\nUpdate: " + a.UPDATER + ",\nDelete: " + a.DELETER + ",\nMenuName: " + a.MENUNAME + ",\nActionName: " + a.ACTIONNAME + ",\nControllerName: " + a.CONTROLLERNAME + ".");

                    }

                    ASL_LOG aslLogref = new ASL_LOG();
                    aslLogref.USERPC = TempData["aslRoleUserPc"].ToString();
                    aslLogref.LOGIPNO = TempData["aslRoleUpdateIPNo"].ToString();
                    aslLogref.COMPID = Convert.ToInt64(TempData["aslRoleCompanyID"]);
                    aslLogref.LOGLTUDE = roleModel.AslUserco.UPDLTUDE;
                    //Update User ID save ASL_ROLE table attribute UPDUSERID
                    aslLogref.USERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    aslLogref.LOGDATA = TempData["aslRoleLogData"].ToString();

                    Update_ASl_Role_LogData(aslLogref);

                    db.SaveChanges();
                    TempData["message"] = "Saved Sucessfully!";
                }

                catch (Exception ex)
                {
                    TempData["message"] = "Error occured:" + ex.Message;
                }

                TempData["AslRoleModel"] = null;
                TempData["data"] = roleModel;
                TempData["menuType"] = roleModel.AslRole.MENUTP;
                TempData["userId"] = roleModel.AslRole.USERID;
                TempData["moduleID"] = roleModel.AslRole.MODULEID;
                return RedirectToAction("Index");
            }

            if (roleModel.AslUserco.OPTP == "User" && roleModel.AslMenumst.MODULEID == "01")
            {
                //ViewBag.ErrorFieldMessage = "Permission not granted ";
                TempData["ErrorFieldMessage"] = "User can not access permission for this User Module ";
                //return RedirectToAction("Index");
                return View("Index");
            }

            TempData["AslRoleModel"] = null;

            if (roleModel.AslMenu.MENUTP != null && roleModel.AslMenumst.MODULEID != null && roleModel.AslUserco.USERNM != null && roleModel.AslMenu.MENUTP != null)
            {
                TempData["data"] = roleModel;
                TempData["menuType"] = roleModel.AslMenu.MENUTP;
                TempData["userId"] = roleModel.AslRole.USERID;
                TempData["moduleID"] = roleModel.AslMenumst.MODULEID;

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorFieldMessage = "Please select all DropdownList value ";
                return View("Index");
            }

        }


        public ActionResult EditRoleUpdate(Int64 asl_roleId, Int64 companyID, string moduleId, string menuId, Int64 userId, string ForR, RoleModel roleModel)
        {
            roleModel.AslRole = db.AslRoleDbSet.Find(asl_roleId);
            var result = from user in db.AslUsercoDbSet where user.USERID == userId && user.COMPID == companyID select new { user.USERNM, user.OPTP, user.COMPID };
            foreach (var variable in result)
            {
                roleModel.AslUserco.COMPID = Convert.ToInt64(variable.COMPID);
                roleModel.AslUserco.USERNM = variable.USERNM.ToString();
                roleModel.AslUserco.OPTP = variable.OPTP.ToString();

            }
            var result2 = from n in db.AslMenumstDbSet where n.MODULEID == moduleId select new { n.MODULENM, n.MODULEID };
            foreach (var v in result2)
            {
                TempData["moduleName"] = v.MODULENM;
                roleModel.AslMenumst.MODULENM = v.MODULENM;
                roleModel.AslMenumst.MODULEID = v.MODULEID;
            }

            var result3 = from n in db.AslMenuDbSet where n.MENUID == menuId && n.MENUTP == ForR select new { n.MENUTP, n.MODULEID, n.MENUNM };
            foreach (var v in result3)
            {
                roleModel.AslMenu.MENUTP = v.MENUTP;
                //roleModel.AslMenu.MODULEID = v.MODULEID;
                roleModel.AslMenu.MENUNM = v.MENUNM;
            }





            var resultRoleModel = from m in db.AslRoleDbSet
                                  where m.COMPID == companyID && m.USERID == userId
                                        && m.MODULEID == moduleId && m.MENUID == menuId && m.MENUTP == ForR
                                  select new { m.ASL_ROLEId, m.COMPID, m.USERID, m.MODULEID, m.MENUTP, m.MENUID, m.STATUS, m.INSERTR, m.UPDATER, m.DELETER };
            foreach (var VARIABLE in resultRoleModel)
            {
                roleModel.AslRole.ASL_ROLEId = VARIABLE.ASL_ROLEId;
                roleModel.AslRole.COMPID = VARIABLE.COMPID;
                roleModel.AslRole.USERID = VARIABLE.USERID;
                roleModel.AslRole.MODULEID = VARIABLE.MODULEID;
                roleModel.AslRole.MENUTP = VARIABLE.MENUTP;
                roleModel.AslRole.MENUID = VARIABLE.MENUID;
                roleModel.AslRole.STATUS = VARIABLE.STATUS;
                roleModel.AslRole.INSERTR = VARIABLE.INSERTR;
                roleModel.AslRole.UPDATER = VARIABLE.UPDATER;
                roleModel.AslRole.DELETER = VARIABLE.DELETER;
            }
            TempData["AslRoleModel"] = roleModel.AslRole;


            TempData["data"] = roleModel;
            TempData["menuType"] = ForR;
            TempData["userId"] = roleModel.AslRole.USERID;
            TempData["moduleID"] = roleModel.AslMenumst.MODULEID;
            TempData["menuId"] = roleModel.AslRole.MENUID;

            return RedirectToAction("Index");
        }





        //Get User Name when Username Dropdownlist Changed.
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult UserNameChanged_Name(Int64 changedDropDown)
        {
            // var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());

            string userNameField = "", operationType = "";
            var rt = db.AslUsercoDbSet.Where(n => n.USERID == changedDropDown).Select(n => new
            {
                userName = n.USERNM,
                opTp = n.OPTP

            });

            foreach (var n in rt)
            {
                userNameField = n.userName;
                operationType = n.opTp;
            }

            var result = new { USERNM = userNameField, OPTP = operationType };
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        ////Load dropdown list
        //[HttpPost]
        //public ActionResult GetModuleName()
        //{
        //    string operationType = Request.Params.Get("opType");

        //    if (operationType == "User")
        //    {
        //        List<SelectListItem> listModuleName = new List<SelectListItem>();
        //        var minID = ((from a in db.AslMenumstDbSet select a.MODULEID).Min());
        //        var resultModule = (from n in db.AslMenumstDbSet where n.MODULEID != minID select n).ToList();
        //        foreach (var n in resultModule)
        //        {
        //            //list.Add(new SelectListItem { Text = bd["buildid"].ToString(), Value = bd["buildid"].ToString() });
        //            ASL_MENUMST aslModuleId = db.AslMenumstDbSet.FirstOrDefault(e => e.MODULEID == n.MODULEID);
        //            listModuleName.Add(new SelectListItem { Text = aslModuleId.MODULENM.ToString(), Value = aslModuleId.MODULEID.ToString() });
        //        }
        //        return Json(listModuleName, JsonRequestBehavior.AllowGet);
        //    }
        //    else 
        //    {
        //        List<SelectListItem> listModuleName = new List<SelectListItem>();
        //        var resultModule = (from n in db.AslMenumstDbSet select n).ToList();
        //        foreach (var n in resultModule)
        //        {
        //            ASL_MENUMST aslModuleId = db.AslMenumstDbSet.FirstOrDefault(e => e.MODULEID == n.MODULEID);
        //            listModuleName.Add(new SelectListItem { Text = aslModuleId.MODULENM.ToString(), Value = aslModuleId.MODULEID.ToString() });
        //        }
        //        return Json(listModuleName, JsonRequestBehavior.AllowGet);
        //    }

        //}


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
