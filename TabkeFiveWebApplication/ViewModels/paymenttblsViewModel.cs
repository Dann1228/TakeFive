using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TabkeFiveWebApplication.ViewModels
{
    public class paymenttblsViewModel
    {
        public SelectPay SelectPay { get; set; }

        public IPagedList<RequestPay> RequestPay { get; set; }

        public int PageIndex { get; set; }
    }
    public class SelectPay
    {
        [Display(Name = "顧客姓名")]
        public string name { get; set; }
    }
    public class RequestPay
    {
        [Required]
        public int payid { get; set; }

        [Display(Name = "會員帳號")]
        public string mid { get; set; }

        [Display(Name = "顧客姓名")]
        public string name { get; set; }

        [Display(Name = "電話")]
        public string phone { get; set; }

        [Display(Name = "地址")]
        public string addr { get; set; }

        [Display(Name = "付款金額")]
        public Nullable<int> pay { get; set; }

        [Display(Name = "付款日期")]
        public Nullable<System.DateTime> paydate { get; set; }

        [Display(Name = "備註")]
        public string memo { get; set; }
    }
}