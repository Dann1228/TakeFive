using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TabkeFiveWebApplication.Models.Common
{
    public class ProductEnumLists
    {

               
        public static SelectList GetInitKinds()
        {

            List<SelectListItem> Kinds = new List<SelectListItem>()
            {
                new SelectListItem(){ Value="1", Text="課程"},
                new SelectListItem(){ Value="2", Text="影片"}
                //,new SelectListItem(){ Value="3", Text="二手書"}
            };

            return new SelectList(Kinds, "Value", "Text");
            
        }

        public static SelectList GetInitCategories()
        {
            return new SelectList(new List<SelectListItem>(), "Value", "Text");
        }

        public static SelectList  GetCategories(string KindsId)
        {
            int id = int.Parse(KindsId);
            List<SelectListItem> list;
            if (id == 1)
            {

                list = new List<SelectListItem>() {
                new SelectListItem(){ Value="1", Text="英文"},
                new SelectListItem(){ Value="2", Text="日文"},
                new SelectListItem(){ Value="3", Text="韓文"}
                 };

            }
            else if (id == 2)
            {

                list = new List<SelectListItem>() {
                new SelectListItem(){ Value="1", Text="英文影片"},
                new SelectListItem(){ Value="2", Text="日文影片"},
                new SelectListItem(){ Value="3", Text="韓文影片"}
                 };

            }
            else
            {

                list = new List<SelectListItem>();

                //list = new List<SelectListItem>()
                //{
                //    new SelectListItem(){ Value="1", Text="英文二手書"},
                //    new SelectListItem(){ Value="2", Text="日文二手書"},
                //    new SelectListItem(){ Value="3", Text="韓文二手書"}
                //};

            }

            return new SelectList(list, "Value", "Text");

        }


    }
}