using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TabkeFiveWebApplication.ViewModels;
using TFDBLibrary;

namespace TabkeFiveWebApplication.Controllers
{
    public class SampleUserController : Controller
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
        public ActionResult Index(SampleUserViewModel model)
        {
            if (string.IsNullOrEmpty(model.selectModel.UserName))
            {
                model = GetViewModel(1, string.Empty);
            }
            else
            {
                model = GetViewModel(1, model.selectModel.UserName);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(string id)
        {
            var data = db.AspNetUsers.Where(x => x.Id.Equals(id)).FirstOrDefault();

            if (data == null)
            {
                return View(new RequestModel());
            }

            var model = new RequestModel
            {
                Id = data.Id,
                Birthday = data.Birthday,
                Email = data.Email,
                Name = data.Name,
                phNumber = data.phNumber,
                UserName = data.UserName
            };


            return View(model);
        }

        [HttpPost]
        public ActionResult PostEdit(RequestModel model)
        {
            AspNetUsers aspNetUsers = new AspNetUsers();

            aspNetUsers.Id = model.Id;
            aspNetUsers.Name = model.Name;
            aspNetUsers.UserName = model.UserName;
            aspNetUsers.phNumber = model.phNumber;
            aspNetUsers.Birthday = model.Birthday;
            aspNetUsers.Email = model.Email;
            aspNetUsers.AccessFailedCount = 0;

            db.Entry(aspNetUsers).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            var data = db.AspNetUsers.Where(x => x.Id.Equals(aspNetUsers.Id)).Select(x => new RequestModel
            {
                Id = x.Id,
                Birthday = x.Birthday,
                Email = x.Email,
                Name = x.Name,
                phNumber = x.phNumber,
                UserName = x.UserName
            }).OrderBy(x => x.Id).ToPagedList(1, l_pageSize);

            var viewModel = new SampleUserViewModel
            {
                requestModel = data,
                PageIndex = 1
            };


            return View("Index", viewModel);
        }

        SampleUserViewModel GetViewModel(int pageIndex, string whereName)
        {
            SampleUserViewModel model = new SampleUserViewModel();

            var data = db.AspNetUsers.Select(x => new RequestModel
            {
                Id = x.Id,
                Birthday = x.Birthday,
                Email = x.Email,
                Name = x.Name,
                phNumber = x.phNumber,
                UserName = x.UserName
            }).OrderBy(x => x.Id);

            if (!string.IsNullOrEmpty(whereName))
            {
                model = new SampleUserViewModel
                {
                    requestModel = data.Where(x => x.UserName.Contains(whereName)).ToPagedList(pageIndex, l_pageSize),
                    PageIndex = pageIndex
                };
            }
            else
            {
                model = new SampleUserViewModel
                {
                    requestModel = data.ToPagedList(pageIndex, l_pageSize),
                    PageIndex = pageIndex
                };
            }


            return model;
        }
    }
}