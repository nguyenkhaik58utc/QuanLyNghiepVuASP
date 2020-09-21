using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BigExample.Areas.Admin.Model
{
    public class MenuModel
    {
        public int menu_id { get; set; }
        public string menu_name { get; set; }
        public string menu_url { get; set; }
        public string menu_screen { get; set; }
    }
}