using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TabkeFiveWebApplication.Models.ViewModels
{
    public class OrderDetailViewModel
    {

        ///<summary>
        ///MName 持卡人姓名
        ///</summary> 
        [Display(Name = "產品名稱")]
        public String PName { get; set; }


        ///<summary>
        ///MPhone 持卡人地址
        ///</summary> 
        [Display(Name = "種類")]
        public int PKind { get; set; }

        ///<summary>
        ///MPhone 持卡人電話
        ///</summary> 
        [Display(Name = "類別")]
        public int PCategory { get; set; }

        ///<summary>
        ///MPhone 持卡人電話
        ///</summary> 
        [Display(Name = "數量")]
        public int BQty { get; set; }

        ///<summary>
        ///MPhone 持卡人電話
        ///</summary> 
        [Display(Name = "價格")]
        public int BPrice { get; set; }
                     
    }
}