using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TabkeFiveWebApplication.Models.ViewModels
{
    public class DiscussViewModel
    {
        /// <summary>
        ///  DId 文章編號
        /// </summary>
        [Display(Name ="文章編號")]
        public int DId { get; set; }

        /// <summary>
        ///  Pid 產品編號
        /// </summary>
        [Display(Name ="產品編號")]
        public Nullable<int> PId { get; set; }

        /// <summary>
        ///  MId 發文者編號
        /// </summary>
        public string MId { get; set; }

        /// <summary>
        ///  DContent 文章內容
        /// </summary>
        public string DContent { get; set; }

        /// <summary>
        ///  DGreat 文章評分
        /// </summary>
        public Nullable<int> DGreat { get; set; }

        /// <summary>
        /// DCrateDate 發文日期
        /// </summary>
        public string DCrateDate { get; set; }

        /// <summary>
        ///  DModifyDate 修改文日期
        /// </summary>
        public string DModifyDate { get; set; }
    }
}