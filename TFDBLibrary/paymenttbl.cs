//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace TFDBLibrary
{
    using System;
    using System.Collections.Generic;
    
    public partial class paymenttbl
    {
        public int payid { get; set; }
        public string mid { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string addr { get; set; }
        public Nullable<int> pay { get; set; }
        public Nullable<System.DateTime> paydate { get; set; }
        public Nullable<int> paykind { get; set; }
        public Nullable<int> creditkind { get; set; }
        public string creaditno { get; set; }
        public string vaildmonth { get; set; }
        public string vaildyear { get; set; }
        public string vaildcode { get; set; }
        public string memo { get; set; }
    }
}
