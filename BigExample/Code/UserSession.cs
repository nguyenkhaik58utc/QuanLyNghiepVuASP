using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BigExample.Code
{
    [Serializable]
    public class UserSession
    {
        public static string UserName { get; set; }

        public static int roleId { get; set; }

        public static int idEmployee { get; set; }
    }
}