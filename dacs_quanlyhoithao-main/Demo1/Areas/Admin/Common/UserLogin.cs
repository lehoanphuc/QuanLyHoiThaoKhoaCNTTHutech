using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo1.Areas.Admin.Common
{
    [Serializable]
    public class UserLogin
    {
        public string UserName { set; get; }
        public string TenQuyen { set; get; }
    }
}