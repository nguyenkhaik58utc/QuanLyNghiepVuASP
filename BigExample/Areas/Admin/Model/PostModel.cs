using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BigExample.Areas.Admin.Model
{
    public class PostModel
    {
        private int idPost { get; set; }
        private string theme { get; set; }
        private string content { get; set; }
        private string urlFile { get; set; }
        private string idEmp { get; set; }
        private string nameEmp { get; set; }

        public PostModel(int idPost, string theme, string content, string urlFile, string nameEmp)
        {
            this.idPost = idPost;
            this.theme = theme;
            this.content = content;
            this.urlFile = urlFile;
            this.nameEmp = nameEmp;
        }
    }
}