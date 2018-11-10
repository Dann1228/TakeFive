using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TabkeFiveWebApplication.ViewModels
{
    public class SampleUserViewModel
    {
        public SelectModel selectModel { get; set; }

        public IPagedList<RequestModel> requestModel { get; set; }

        public int PageIndex { get; set; }
    }

    public class SelectModel
    {
        [Display(Name = "使用者名稱")]
        public string UserName { get; set; }
    }

    public class RequestModel
    {
        [Required]
        public string Id { get; set; }

        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Display(Name = "使用者名稱")]
        public string UserName { get; set; }

        [Display(Name = "電話號碼")]
        public string phNumber { get; set; }

        [Display(Name = "生日")]
        public string Birthday { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}