using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TabkeFiveWebApplication.Models.ViewModels
{
    public class OrderViewModel
    {

        ///<summary>
        ///MName 持卡人姓名
        ///</summary> 
        [Display(Name = "姓名")]
        [StringLength(50, ErrorMessage = "{0} 的長度至少必須為 {2} 個字元。", MinimumLength = 2)]
        public String MName { get; set; }


        ///<summary>
        ///MPhone 持卡人地址
        ///</summary> 
        [Display(Name = "地址")]
        [StringLength(250, ErrorMessage = "{0} 的長度至少必須為 {2} 個字元。", MinimumLength = 8)]
        public String MAddr { get; set; }

        ///<summary>
        ///MPhone 持卡人電話
        ///</summary> 
        [Display(Name = "行動電話")]
        [RegularExpression(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "輸入的手機號碼格式不正確(ex: 0123-456-789)")]
        public String MPhone { get; set; }

        ///<summary>
        ///CreditKind 信用卡類別
        ///</summary> 
        [Display(Name = "信用卡類別")]
        public int CreditKind { get; set; }

        ///<summary>
        ///CreditKind 信用卡號碼-1
        ///</summary> 
        [Display(Name = "信用卡卡號")]
        public int CreditNumber1 { get; set; }

        ///<summary>
        ///CreditKind 信用卡號碼-2
        ///</summary> 
        [Display(Name = "信用卡卡號2")]
        public int CreditNumber2 { get; set; }


        ///<summary>
        ///CreditKind 信用卡號碼-3
        ///</summary> 
        [Display(Name = "信用卡卡號3")]
        public int CreditNumber3 { get; set; }

        ///<summary>
        ///CreditKind 信用卡號碼-4
        ///</summary> 
        [Display(Name = "信用卡卡號4")]
        public int CreditNumber4 { get; set; }


        ///<summary>
        ///VaildMonth 有效日期-月
        ///</summary> 
        [Display(Name = "有效日期-月")]        
        public int VaildMonth { get; set; }

        ///<summary>
        ///VaildYear 有效日期-年
        ///</summary> 
        [Display(Name = "有效日期-年")]        
        public int VaildYear { get; set; }


        ///<summary>
        ///VaildCode 驗證碼
        ///</summary>         
        [Display(Name = "驗證碼")]
        public int VaildCode { get; set; }


        ///<summary>
        ///Memo 備註
        ///</summary> 
        [Display(Name = "備註")]
        public String  Memo { get; set; }

        [Display(Name = "總額")]
        public int TotalPrice { get; set; }


        [Display(Name = "購買日期")]
        public DateTime BuyDate { get; set; }


        [Display(Name = "購買訂單號碼")]
        public int PayId { get; set; }

        [Display(Name = "訂單狀態")]
        public int Status { get; set; }

        
    }
}