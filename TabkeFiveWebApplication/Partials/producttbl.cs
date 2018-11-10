using System;
using System.ComponentModel.DataAnnotations;

namespace BackgroundStage2.Models
{
    [MetadataType(typeof(MetaDataproducttbl))]
    public partial class producttbl
    {
        public class MetaDataproducttbl
        {
            public int pid { get; set; }
            public string mid { get; set; }

            [Display(Name = "影片名稱")]
            public string name { get; set; }

            [Display(Name = "介紹")]
            public string intro { get; set; }

            [Display(Name = "圖片")]
            public byte[] picture { get; set; }

            [Display(Name = "影片位子")]
            public string vidiourl { get; set; }

            public Nullable<int> kind { get; set; }
            public Nullable<int> category { get; set; }
            public Nullable<int> price { get; set; }
            public Nullable<int> state { get; set; }
            public Nullable<int> score { get; set; }
        }
    }
}