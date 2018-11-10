using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TabkeFiveWebApplication.ViewModels;
using TFDBLibrary;

namespace TabkeFiveWebApplication.Controllers
{
    public class paymenttblsController : Controller
    {
        TakeFiveDBEntities db = new TakeFiveDBEntities();

        /// <summary>
        /// 一頁幾筆資料
        /// </summary>
        private const int l_pageSize = 10;

        [HttpGet]
        public ActionResult Index(int pageIndex = 0)
        {
            pageIndex = pageIndex == 0 ? 1 : pageIndex;

            return View(GetViewModel(pageIndex, string.Empty));
        }

        [HttpPost]
        public ActionResult Index(paymenttblsViewModel model)
        {
            if (string.IsNullOrEmpty(model.SelectPay.name))
            {
                model = GetViewModel(1, string.Empty);
            }
            else
            {
                model = GetViewModel(1, model.SelectPay.name);
            }

            return View(model);
        }

        paymenttblsViewModel GetViewModel(int pageIndex, string whereName)
        {
            paymenttblsViewModel model = new paymenttblsViewModel();

            var data = db.paymenttbl.Select(x => new RequestPay
            {
                payid = x.payid,
                mid = x.mid,
                name = x.name,
                phone = x.phone,
                addr = x.addr,
                pay = x.payid,
                paydate = x.paydate,
                memo = x.memo
            }).OrderBy(x => x.mid);

            if (!string.IsNullOrEmpty(whereName))
            {
                model = new paymenttblsViewModel
                {
                    RequestPay = data.Where(x => x.name.Contains(whereName)).ToPagedList(pageIndex, l_pageSize),
                    PageIndex = pageIndex
                };
            }
            else
            {
                model = new paymenttblsViewModel
                {
                    RequestPay = data.ToPagedList(pageIndex, l_pageSize),
                    PageIndex = pageIndex
                };
            }
            return model;
        }
        // POST: paymenttbls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        // GET: paymenttbls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paymenttbl paymenttbl = db.paymenttbl.Find(id);
            if (paymenttbl == null)
            {
                return HttpNotFound();
            }
            return View(paymenttbl);
        }

        [HttpPost]
        public ActionResult Edit(int id)
        {
            var data = db.paymenttbl.Where(x => x.payid.Equals(id)).FirstOrDefault();

            if (data == null)
            {
                return View(new RequestPay());
            }

            var model = new RequestPay
            {
                payid = data.payid,
                mid = data.mid,
                name = data.name,
                phone = data.phone,
                addr = data.addr,
                pay = data.payid,
                paydate = data.paydate,
                memo = data.memo
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult PostEdit(RequestPay model)
        {
            paymenttbl paymenttbl = new paymenttbl();

            paymenttbl.payid = model.payid;
            paymenttbl.mid = model.mid;
            paymenttbl.name = model.name;
            paymenttbl.phone = model.phone;
            paymenttbl.addr = model.addr;
            paymenttbl.pay = model.payid;
            paymenttbl.paydate = model.paydate;
            paymenttbl.memo = model.memo;

            db.Entry(paymenttbl).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            var data = db.paymenttbl.Where(x => x.payid.Equals(paymenttbl.payid)).Select(x => new RequestPay
            {
                payid = x.payid,
                mid = x.mid,
                name = x.name,
                phone = x.phone,
                addr = x.addr,
                pay = x.payid,
                paydate = x.paydate,
                memo = x.memo
            }).OrderBy(x => x.payid).ToPagedList(1, l_pageSize);

            var viewModel = new paymenttblsViewModel
            {
                RequestPay = data,
                PageIndex = 1
            };

            return View("Index", viewModel);
        }
        // GET: paymenttbls/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    paymenttbl paymenttbl = db.paymenttbl.Find(id);
        //    if (paymenttbl == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(paymenttbl);
        //}

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var data = db.paymenttbl.Where(x => x.payid.Equals(id)).FirstOrDefault();

            if (data == null)
            {
                return View(new RequestPay());
            }

            var model = new RequestPay
            {
                payid = data.payid,
                mid = data.mid,
                name = data.name,
                phone = data.phone,
                addr = data.addr,
                pay = data.payid,
                paydate = data.paydate,
                memo = data.memo
            };

            return View(model);
        }

        // POST: paymenttbls/Delete/5
        [HttpPost]
        public ActionResult DeleteCheck(RequestPay model)
        {
            paymenttbl paymenttbl = new paymenttbl();

            paymenttbl.payid = model.payid;
            paymenttbl.mid = model.mid;
            paymenttbl.name = model.name;
            paymenttbl.phone = model.phone;
            paymenttbl.addr = model.addr;
            paymenttbl.pay = model.payid;
            paymenttbl.paydate = model.paydate;
            paymenttbl.memo = model.memo;

            db.Entry(paymenttbl).State = System.Data.Entity.EntityState.Deleted;
            db.paymenttbl.Remove(paymenttbl);
            db.SaveChanges();

            var data = db.paymenttbl.Where(x => x.payid.Equals(paymenttbl.payid)).Select(x => new RequestPay
            {
                payid = x.payid,
                mid = x.mid,
                name = x.name,
                phone = x.phone,
                addr = x.addr,
                pay = x.payid,
                paydate = x.paydate,
                memo = x.memo
            }).OrderBy(x => x.payid).ToPagedList(1, l_pageSize);

            var viewModel = new paymenttblsViewModel
            {
                RequestPay = data,
                PageIndex = 1
            };
            return View("Index", viewModel);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}