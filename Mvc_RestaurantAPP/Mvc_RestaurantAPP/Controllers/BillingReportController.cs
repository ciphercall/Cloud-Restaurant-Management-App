using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Mvc_RestaurantAPP.Models;
using RazorPDF;
using WebGrease.Css.Ast.Selectors;

namespace Mvc_RestaurantAPP.Controllers
{
    public class BillingReportController : AppController
    {

        private RestaurantDbContext db = new RestaurantDbContext();


        //Get Category Report
        public ActionResult GetCategoryReport()
        {
            var pdf = new PdfResult(null, "GetCategoryReport");
            return pdf;
        }


        public ActionResult CategoryWiseSalesSummery()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CategoryWiseSalesSummery(BillingReportViewModel billingReportViewModel)
        {

            if (billingReportViewModel.FromDate > billingReportViewModel.ToDate)
            {
                ViewBag.ErrorFromDateMessage = "Please select correct 'From Date' field!";
                ViewBag.ErrorToDateMessage = "Please select correct 'To Date' field!";
                return View("CategoryWiseSalesSummery");
            }
            else if (billingReportViewModel.FromDate == null && billingReportViewModel.ToDate == null)
            {
                ViewBag.ErrorFromDateMessage = "Please select correct 'From Date' field!";
                ViewBag.ErrorToDateMessage = "Please select correct 'To Date' field!";
                return View("CategoryWiseSalesSummery");
            }

            //string query = string.Format("SELECT RMS_TRANS.ITEMID, RMS_ITEM.CATID, RMS_TRANS.COMPID, POS_ITEMMST.CATNM, RMS_ITEM.ITEMNM, SUM(ISNULL(RMS_TRANS.QTY,0)) AS QTY, SUM(ISNULL(RMS_TRANS.GROSSAMT,0)) AS GROSSAMT " +
            //                             "FROM RMS_TRANS INNER JOIN RMS_ITEM ON RMS_TRANS.ITEMID = RMS_ITEM.ITEMID AND RMS_TRANS.COMPID = RMS_ITEM.COMPID INNER JOIN POS_ITEMMST ON RMS_ITEM.CATID = POS_ITEMMST.CATID AND RMS_ITEM.COMPID = POS_ITEMMST.COMPID " +
            //                             "where RMS_TRANS.TRANSDT between '{0}' AND '{1}' " +
            //                             "GROUP BY RMS_TRANS.ITEMID, RMS_ITEM.CATID, RMS_TRANS.COMPID, RMS_ITEM.COMPID, POS_ITEMMST.CATNM, RMS_ITEM.ITEMNM", billingReportViewModel.FromDate, billingReportViewModel.ToDate);

            Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
            var getCategoryWiseSalesSummery = (from RmsTrans in db.RmsTransDbSet
                                               join RmsItem in db.RmsItemDbSet on RmsTrans.ITEMID equals RmsItem.ITEMID
                                               join PosItemmst in db.PosItemMstDbSet on RmsItem.CATID equals PosItemmst.CATID
                                               where
                                                   RmsItem.COMPID == PosItemmst.COMPID && RmsTrans.COMPID == RmsItem.COMPID &&
                                                   RmsTrans.COMPID == compid &&
                                                   (RmsTrans.TRANSDT >= billingReportViewModel.FromDate &&
                                                    RmsTrans.TRANSDT <= billingReportViewModel.ToDate)
                                               group new { RmsTrans, RmsItem, PosItemmst } by
                                                   new { RmsTrans.ITEMID, RmsItem.CATID, RmsTrans.COMPID, PosItemmst.CATNM, RmsItem.ITEMNM }
                                                   into grp
                                                   select new
                                                   {
                                                       Count = grp.Count(),
                                                       grp.Key.ITEMID,
                                                       grp.Key.CATID,
                                                       grp.Key.COMPID,
                                                       grp.Key.CATNM,
                                                       grp.Key.ITEMNM,
                                                       Quantity = grp.Sum(x => x.RmsTrans.QTY),
                                                       Rate = grp.Sum(x => x.RmsTrans.RATE),
                                                       Amount = grp.Sum(x => x.RmsTrans.AMOUNT),
                                                       DisAmount = grp.Sum(x => x.RmsTrans.DISCAMT),
                                                       GrossAmount = grp.Sum(x => x.RmsTrans.GROSSAMT)
                                                   });




            IList<BillingReportViewModel> billingReportViewModels = new List<BillingReportViewModel>();

            var GetList = getCategoryWiseSalesSummery.ToList();

            if (GetList.Count == 0)
            {
                TempData["ErrorMessageItemWiseSalesSummery"] = "Category wise sales data empty!";
                return RedirectToAction("CategoryWiseSalesSummery");
            }

            foreach (var a in GetList)
            {
                billingReportViewModels.Add(new BillingReportViewModel()
                {
                    CategoryID = a.CATID,
                    ItemID = a.ITEMID,
                    CategoryName = a.CATNM,
                    ItemName = a.ITEMNM,
                    Quantity = Convert.ToDecimal(a.Quantity),
                    Rate = Convert.ToDecimal(a.Rate),
                    Amount = Convert.ToDecimal(a.Amount),
                    DiscountAmount = Convert.ToDecimal(a.DisAmount),
                    GrossAmount = Convert.ToDecimal(a.GrossAmount)

                });
            }

            var pdf = new PdfResult(billingReportViewModels, "GetCategoryWiseSalesSummaryReport");
            //From Date
            string FromDate = billingReportViewModel.FromDate.ToString();
            DateTime datefrom = DateTime.Parse(FromDate);
            string fromDate = datefrom.ToString("dd-MMM-yyyy");
            //To Date
            string ToDate = billingReportViewModel.ToDate.ToString();
            DateTime dateto = DateTime.Parse(ToDate);
            string toDate = dateto.ToString("dd-MMM-yyyy");



            pdf.ViewBag.FromDate = fromDate;
            pdf.ViewBag.ToDate = toDate;
            return pdf;
        }



        public ActionResult ItemWiseSalesSummary()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ItemWiseSalesSummary(BillingReportViewModel billingReportViewModel)
        {
            if (billingReportViewModel.FromDate > billingReportViewModel.ToDate)
            {
                ViewBag.ErrorFromDateMessage = "Please select correct 'From Date' field!";
                ViewBag.ErrorToDateMessage = "Please select correct 'To Date' field!";
                return View("ItemWiseSalesSummary");
            }
            else if (billingReportViewModel.FromDate == null && billingReportViewModel.ToDate == null)
            {
                ViewBag.ErrorFromDateMessage = "Please select correct 'From Date' field!";
                ViewBag.ErrorToDateMessage = "Please select correct 'To Date' field!";
                return View("ItemWiseSalesSummary");
            }

            else if (billingReportViewModel.FromDate == null && billingReportViewModel.ToDate == null && billingReportViewModel.RmsItem.ITEMID == null)
            {
                ViewBag.ErrorFromDateMessage = "Please select correct 'From Date' field!";
                ViewBag.ErrorToDateMessage = "Please select correct 'To Date' field!";
                ViewBag.ErrorRmsItemITEMID = "Please select the Item Name field!";
                return View("ItemWiseSalesSummary");
            }
            else if (billingReportViewModel.RmsItem.ITEMID == null)
            {
                ViewBag.ErrorRmsItemITEMID = "Please select the Item Name field!";
                return View("ItemWiseSalesSummary");
            }


            //SELECT RMS_TRANS.ITEMID,RMS_ITEM.ITEMNM, RMS_TRANS.TRANSNO, RMS_TRANS.TRANSDT, RMS_TRANS.QTY, RMS_TRANS.RATE, RMS_TRANS.AMOUNT
            //FROM RMS_ITEM INNER JOIN RMS_TRANS ON RMS_ITEM.COMPID = RMS_TRANS.COMPID AND RMS_ITEM.ITEMID = RMS_TRANS.ITEMID
            //where RMS_TRANS.ITEMID = '10103001' 


            Int64 compid = Convert.ToInt64(billingReportViewModel.RmsTrans.COMPID);
            var getItemWiseSalesSummary = (from RmsTrans in db.RmsTransDbSet
                                           join RmsItem in db.RmsItemDbSet on RmsTrans.COMPID equals RmsItem.COMPID
                                           where
                                               RmsItem.ITEMID == RmsTrans.ITEMID && RmsTrans.COMPID == compid
                                               && RmsTrans.ITEMID == billingReportViewModel.RmsItem.ITEMID &&
                                               (RmsTrans.TRANSDT >= billingReportViewModel.FromDate && RmsTrans.TRANSDT <= billingReportViewModel.ToDate)
                                           select new
                                           {
                                               Date = RmsTrans.TRANSDT,
                                               MemoNO = RmsTrans.TRANSNO,
                                               Quantity = RmsTrans.QTY,
                                               Rate = RmsTrans.RATE,
                                               Amount = RmsTrans.AMOUNT,
                                               DiscountAmount = RmsTrans.DISCAMT,
                                               GrossAmount = RmsTrans.GROSSAMT,
                                           });




            IList<BillingReportViewModel> billingReportViewModels = new List<BillingReportViewModel>();

            var GetList = getItemWiseSalesSummary.ToList();

            if (GetList.Count == 0)
            {
                TempData["ErrorMessageItemWiseSalesSummery"] = "Item wise sales data empty!";
                return RedirectToAction("ItemWiseSalesSummary");
            }

            foreach (var a in GetList)
            {
                string x = a.Date.ToString();
                DateTime y = DateTime.Parse(x);
                string date = y.ToString("dd-MMM-yyyy");

                billingReportViewModels.Add(new BillingReportViewModel()
                {

                    Date = date,
                    MemoNo = a.MemoNO,
                    Quantity = Convert.ToDecimal(a.Quantity),
                    Rate = Convert.ToDecimal(a.Rate),
                    Amount = Convert.ToDecimal(a.Amount),
                    DiscountAmount = Convert.ToDecimal(a.DiscountAmount),
                    GrossAmount = Convert.ToDecimal(a.GrossAmount),

                });
            }

            var pdf = new PdfResult(billingReportViewModels, "GetItemWiseSalesSummaryReports");
            //From Date
            string FromDate = billingReportViewModel.FromDate.ToString();
            DateTime datefrom = DateTime.Parse(FromDate);
            string fromDate = datefrom.ToString("dd-MMM-yyyy");
            //To Date
            string ToDate = billingReportViewModel.ToDate.ToString();
            DateTime dateto = DateTime.Parse(ToDate);
            string toDate = dateto.ToString("dd-MMM-yyyy");



            string ItemName = "";
            var ItemNameSearch = from n in db.RmsItemDbSet
                                 where n.ITEMID == billingReportViewModel.RmsItem.ITEMID
                                 select new { ItemNameFound = n.ITEMNM };
            foreach (var VARIABLE in ItemNameSearch)
            {
                ItemName = VARIABLE.ItemNameFound;
            }

            pdf.ViewBag.ItemName = ItemName.ToString();
            pdf.ViewBag.FromDate = fromDate;
            pdf.ViewBag.ToDate = toDate;
            return pdf;
        }



        public ActionResult SalesStatementSummarized()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SalesStatementSummarized(BillingReportViewModel billingReportViewModel)
        {
            if (billingReportViewModel.FromDate > billingReportViewModel.ToDate)
            {
                ViewBag.ErrorFromDateMessage = "Please select correct 'From Date' field!";
                ViewBag.ErrorToDateMessage = "Please select correct 'To Date' field!";
                return View("SalesStatementSummarized");
            }
            else if (billingReportViewModel.FromDate == null && billingReportViewModel.ToDate == null)
            {
                ViewBag.ErrorFromDateMessage = "Please select correct 'From Date' field!";
                ViewBag.ErrorToDateMessage = "Please select correct 'To Date' field!";
                return View("SalesStatementSummarized");
            }


            //SELECT RMS_TRANSMST.TRANSNO,RMS_TRANSMST.TOTAMT, RMS_TRANSMST.DISCAMTG, RMS_TRANSMST.TOTGROSS, RMS_TRANSMST.VATAMT, RMS_TRANSMST.TOTNET, RMS_TRANSMST.TRANSDT,count(ISNULL(RMS_TRANS.QTY,0)) AS qty 
            //FROM RMS_TRANS INNER JOIN RMS_TRANSMST ON RMS_TRANS.TRANSNO = RMS_TRANSMST.TRANSNO
            //where RMS_TRANSMST.TRANSDT between '2014-12-01' AND '2014-12-03' 
            //GROUP BY RMS_TRANSMST.TRANSNO,RMS_TRANSMST.TOTAMT, RMS_TRANSMST.DISCAMTG, RMS_TRANSMST.TOTGROSS, RMS_TRANSMST.VATAMT, RMS_TRANSMST.TOTNET,RMS_TRANSMST.TRANSDT


            Int64 compid = Convert.ToInt64(billingReportViewModel.RmsTrans.COMPID);
            var getSalesStatementSummarized = (from RmsTrans in db.RmsTransDbSet
                                               join RmsTrans_Mst in db.RmsTransmstDbSet on RmsTrans.TRANSNO equals RmsTrans_Mst.TRANSNO
                                               where
                                                   RmsTrans.COMPID == RmsTrans_Mst.COMPID && RmsTrans_Mst.COMPID == compid
                                                   && (RmsTrans_Mst.TRANSDT >= billingReportViewModel.FromDate && RmsTrans_Mst.TRANSDT <= billingReportViewModel.ToDate)
                                               group new { RmsTrans, RmsTrans_Mst } by
                                                  new { RmsTrans_Mst.TRANSDT, RmsTrans_Mst.TRANSNO, RmsTrans_Mst.TOTAMT, RmsTrans_Mst.DISCAMTG, RmsTrans_Mst.TOTGROSS, RmsTrans_Mst.VATAMT, RmsTrans_Mst.SCHARGE, RmsTrans_Mst.TOTNET }
                                                   into grp
                                                   select new
                                                   {
                                                       Count = grp.Count(),
                                                       grp.Key.TRANSDT,
                                                       grp.Key.TRANSNO,
                                                       grp.Key.TOTAMT,
                                                       grp.Key.DISCAMTG,
                                                       grp.Key.TOTGROSS,
                                                       grp.Key.VATAMT,
                                                       grp.Key.SCHARGE,
                                                       grp.Key.TOTNET,
                                                       ItemQtyCount = grp.Select(x => x.RmsTrans.QTY).Count(),
                                                       //ItemQtyCount = grp.Select(x => x.RmsTrans.QTY).Distinct()Count(),
                                                   });

            IList<BillingReportViewModel> billingReportViewModels = new List<BillingReportViewModel>();

            var GetList = getSalesStatementSummarized.ToList();

            if (GetList.Count == 0)
            {
                TempData["ErrorMessageItemWiseSalesSummery"] = "Sales statement (Summarized) data empty!";
                return RedirectToAction("SalesStatementSummarized");
            }

            foreach (var a in GetList)
            {
                string x = a.TRANSDT.ToString();
                DateTime y = DateTime.Parse(x);
                string date = y.ToString("dd-MMM-yyyy");

                billingReportViewModels.Add(new BillingReportViewModel()
                {

                    Date = date,
                    MemoNo = a.TRANSNO,
                    ItemQtyCount = a.ItemQtyCount,
                    Amount = Convert.ToDecimal(a.TOTAMT),
                    DiscountAmount = Convert.ToDecimal(a.DISCAMTG),
                    GrossAmount = Convert.ToDecimal(a.TOTGROSS),
                    Vat = Convert.ToDecimal(a.VATAMT),
                    ServiceCharge = Convert.ToDecimal(a.SCHARGE),
                    Net = Convert.ToDecimal(a.TOTNET),
                });
            }

            var pdf = new PdfResult(billingReportViewModels, "GetSalesStatementSummarizedReports");
            //From Date
            string FromDate = billingReportViewModel.FromDate.ToString();
            DateTime datefrom = DateTime.Parse(FromDate);
            string fromDate = datefrom.ToString("dd-MMM-yyyy");
            //To Date
            string ToDate = billingReportViewModel.ToDate.ToString();
            DateTime dateto = DateTime.Parse(ToDate);
            string toDate = dateto.ToString("dd-MMM-yyyy");

            pdf.ViewBag.FromDate = fromDate;
            pdf.ViewBag.ToDate = toDate;
            return pdf;
        }


        public ActionResult SalesStatementDetails()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SalesStatementDetails(BillingReportViewModel billingReportViewModel)
        {
            if (billingReportViewModel.FromDate > billingReportViewModel.ToDate)
            {
                ViewBag.ErrorFromDateMessage = "Please select correct 'From Date' field!";
                ViewBag.ErrorToDateMessage = "Please select correct 'To Date' field!";
                return View("SalesStatementDetails");
            }
            else if (billingReportViewModel.FromDate == null && billingReportViewModel.ToDate == null)
            {
                ViewBag.ErrorFromDateMessage = "Please select correct 'From Date' field!";
                ViewBag.ErrorToDateMessage = "Please select correct 'To Date' field!";
                return View("SalesStatementDetails");
            }


            // SELECT RMS_TRANS.TRANSDT, RMS_TRANS.TRANSNO, RMS_ITEM.ITEMNM, RMS_TRANS.ITEMID, Sum(ISNULL(RMS_TRANS.QTY,0))As Qty,
            //(SUM(ISNULL(RMS_TRANS.AMOUNT,0))/Sum(Isnull(RMS_TRANS.Qty,0)))As Rate,Sum(ISNULL(RMS_TRANS.AMOUNT,0))AS Amount, 
            //SUM(ISNULL(RMS_TRANS.DISCAMT,0)) AS Discount, Sum(ISNULL(RMS_TRANS.GROSSAMT,0))AS Gross
            //FROM RMS_TRANS INNER JOIN RMS_ITEM ON RMS_TRANS.ITEMID = RMS_ITEM.ITEMID
            //Group by  RMS_TRANS.TRANSDT,RMS_TRANS.TRANSNO, RMS_ITEM.ITEMNM,RMS_TRANS.ITEMID;

            Int64 compid = Convert.ToInt64(billingReportViewModel.RmsTrans.COMPID);
            var getSalesStatementDetails = (from RmsTrans in db.RmsTransDbSet
                                            join RmsItem in db.RmsItemDbSet on RmsTrans.ITEMID equals RmsItem.ITEMID
                                            where
                                                RmsTrans.COMPID == RmsItem.COMPID && RmsTrans.COMPID == compid
                                                && (RmsTrans.TRANSDT >= billingReportViewModel.FromDate && RmsTrans.TRANSDT <= billingReportViewModel.ToDate)
                                            group new { RmsTrans, RmsItem } by
                                                  new { RmsTrans.TRANSDT, RmsTrans.TRANSNO, RmsTrans.ITEMID, RmsItem.ITEMNM }
                                                into grp
                                                select new
                                                {
                                                    Count = grp.Count(),
                                                    grp.Key.TRANSDT,
                                                    grp.Key.TRANSNO,
                                                    grp.Key.ITEMNM,
                                                    grp.Key.ITEMID,
                                                    Quantity = grp.Sum(x => x.RmsTrans.QTY),
                                                    Rate = grp.Sum(x => x.RmsTrans.AMOUNT) / grp.Sum(x => x.RmsTrans.QTY),
                                                    Amount = grp.Sum(x => x.RmsTrans.AMOUNT),
                                                    DisAmount = grp.Sum(x => x.RmsTrans.DISCAMT),
                                                    GrossAmount = grp.Sum(x => x.RmsTrans.GROSSAMT),
                                                });

            IList<BillingReportViewModel> billingReportViewModels = new List<BillingReportViewModel>();

            var GetList = getSalesStatementDetails.ToList();

            if (GetList.Count == 0)
            {
                TempData["ErrorMessageItemWiseSalesSummery"] = "Sales statement (Details) data empty!";
                return RedirectToAction("SalesStatementSummarized");
            }

            foreach (var a in GetList)
            {
                //string x = a.TRANSDT.ToString();
                //DateTime y = DateTime.Parse(x);
                //string date = y.ToString("dd-MMM-yyyy");

                billingReportViewModels.Add(new BillingReportViewModel()
                {
                    DateTime = a.TRANSDT,
                    MemoNo = a.TRANSNO,
                    ItemID = a.ITEMID,
                    ItemName = a.ITEMNM,
                    Quantity = Convert.ToDecimal(a.Quantity),
                    Rate = Convert.ToDecimal(a.Rate),
                    Amount = Convert.ToDecimal(a.Amount),
                    DiscountAmount = Convert.ToDecimal(a.DisAmount),
                    GrossAmount = Convert.ToDecimal(a.GrossAmount),
                });
            }

            var pdf = new PdfResult(billingReportViewModels, "GetSalesStatementDetailsReports");
            //From Date
            string FromDate = billingReportViewModel.FromDate.ToString();
            DateTime datefrom = DateTime.Parse(FromDate);
            string fromDate = datefrom.ToString("dd-MMM-yyyy");
            //To Date
            string ToDate = billingReportViewModel.ToDate.ToString();
            DateTime dateto = DateTime.Parse(ToDate);
            string toDate = dateto.ToString("dd-MMM-yyyy");

            pdf.ViewBag.FromDate = fromDate;
            pdf.ViewBag.ToDate = toDate;
            return pdf;
        }






        //................................................................................................
        //Billing From (Transaction)
        public ActionResult transactionMemos(BillingReportViewModel billingReportViewModel)
        {
            if (billingReportViewModel.MemoNo == null)
            {
                return RedirectToAction("Index","Transaction");
            }
           
            //SELECT RMS_TRANS.TRANSDT, RMS_TRANS.TRANSNO, RMS_ITEM.ITEMNM, RMS_TRANS.ITEMID,RMS_TRANS.Qty,RMS_TRANS.RATE,RMS_TRANS.AMOUNT
            //FROM RMS_TRANS INNER JOIN RMS_ITEM ON RMS_TRANS.ITEMID = RMS_ITEM.ITEMID
            //where RMS_TRANS.TRANSDT ='2014-12-02' and RMS_TRANS.TRANSNO='291114144';

            Int64 compid = Convert.ToInt64(billingReportViewModel.CompanyID);
            var getSalesStatementDetails = (from RmsTrans in db.RmsTransDbSet
                                            join RmsItem in db.RmsItemDbSet on RmsTrans.ITEMID equals RmsItem.ITEMID
                                            where
                                                RmsTrans.COMPID == RmsItem.COMPID && RmsTrans.COMPID == compid
                                                && RmsTrans.TRANSDT == billingReportViewModel.DateTime && RmsTrans.TRANSNO == billingReportViewModel.MemoNo
                                            select new
                                            {
                                                RmsTrans.TRANSDT,
                                                RmsTrans.TRANSNO,
                                                RmsItem.ITEMNM,
                                                RmsItem.ITEMID,
                                                RmsTrans.QTY,
                                                RmsTrans.RATE,
                                                RmsTrans.AMOUNT,
                                                RmsTrans.DISCAMT,
                                                RmsTrans.GROSSAMT
                                            });

            IList<BillingReportViewModel> billingReportViewModels = new List<BillingReportViewModel>();

            var GetList = getSalesStatementDetails.ToList();

            foreach (var a in GetList)
            {
                string x = a.TRANSDT.ToString();
                DateTime y = DateTime.Parse(x);
                string date = y.ToString("dd-MMM-yyyy");

                billingReportViewModels.Add(new BillingReportViewModel()
                {
                    Date = date,
                    MemoNo = a.TRANSNO,
                    ItemID = a.ITEMID,
                    ItemName = a.ITEMNM,
                    Quantity = Convert.ToDecimal(a.QTY),
                    Rate = Convert.ToDecimal(a.RATE),
                    Amount = Convert.ToDecimal(a.AMOUNT),
                    DiscountAmount = Convert.ToDecimal(a.DISCAMT),
                    GrossAmount = Convert.ToDecimal(a.GROSSAMT),
                });
            }


            if (billingReportViewModel.Command == "A4")
            {
                var pdf = new PdfResult(billingReportViewModels, "GetTransactionMemoReports_BigSize");
                // Date
                string Date = billingReportViewModel.DateTime.ToString();
                DateTime dateParse = DateTime.Parse(Date);
                string getDate = dateParse.ToString("dd-MMM-yyyy");


                pdf.ViewBag.Date = getDate;
                pdf.ViewBag.InvoiceNo = billingReportViewModel.MemoNo.ToString();
                return pdf;
            }
            else if (billingReportViewModel.Command == "POS")
            {
                var pdf = new PdfResult(billingReportViewModels, "GetTransactionMemoReports_SmallSize");
                // Date
                string Date = billingReportViewModel.DateTime.ToString();
                DateTime dateParse = DateTime.Parse(Date);
                string getDate = dateParse.ToString("dd-MMM-yyyy");


                pdf.ViewBag.Date = getDate;
                pdf.ViewBag.InvoiceNo = billingReportViewModel.MemoNo.ToString();
                return pdf;
            }
            else
            {
                return RedirectToAction("Index","Transaction");
            }


        }


    }
}
