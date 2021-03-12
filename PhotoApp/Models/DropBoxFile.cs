using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoApp.Models
{
    public class DropBoxFile
    {
        public string Name { get; set; }
        public string ModifiedAt { get; set; }
        public string Size { get; set; }
        public string Path { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}