using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using TabkeFiveWebApplication.Models.Common;
using System.Web.Mvc;


namespace TabkeFiveWebApplication.Models.ViewModels
{
    public class ProductViewModels
    {
        ///<summary>
        ///PID 產品編號
        ///</summary>        
        public int PId { get; set; }

        ///<summary>
        ///kind  商品種類
        ///</summary>        
        [Display(Name = "種類")]
        public int SelectedKinds { get; set; }

        ///<summary>
        ///Category 類別 
        ///</summary>        
        [Display(Name = "類別")]
        public int SelectedCategories { get; set; }

        public System.Web.Mvc.SelectList Kinds { get; set; }
        public System.Web.Mvc.SelectList Categories { get; set; }
                

        ///<summary>
        ///mid  會員編號(所有者)       
        ///</summary>        
        public string MId { get; set; }



        ///<summary>
        ///商品 name
        ///</summary>  
        [Display(Name = "名稱")]
        public String Name { get; set; }


        ///<summary>
        ///intro  商品介紹      
        ///</summary>        
        [Display(Name = "介紹")]
        public String Intro { get; set; }


        ///<summary>
        ///Price  價格
        ///</summary>        
        [Display(Name = "價格")]
        public int Price { get; set; }



        ///<summary>
        ///State  狀態
        ///</summary>        
        [Display(Name = "狀態")]
        public int State { get; set; }


        ///<summary>
        ///Score  評份
        ///</summary>        
        [Display(Name = "評份")]
        public int Score { get; set; }


        ///<summary>
        ///Pic  圖片
        ///</summary>        
        [Display(Name = "圖片")]
        public byte[] Picture { get; set; }


        ///<summary>
        ///Vidiourl  錄像url
        ///</summary>        
        [Display(Name = "影像")]
        public String VidioUrl { get; set; }


    }


}