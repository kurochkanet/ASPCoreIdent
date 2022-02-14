using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Utils
{
   public class ContentPathBuilder
    {
        public static string ContentRootPath { get; set; }
        public static string ContentGoodsFolder { get; set; }
        public static string ContentThumbnailsFolder { get; set; }
        public static string ContentWebFolder { get; set; }

        public static string GetGoodImageFolderPath()
        {
            return Path.Combine(ContentRootPath, ContentWebFolder, ContentGoodsFolder);
        }
        public static string GetGoodThumbNailsImageFolderPath()
        {
            return Path.Combine(ContentRootPath, ContentWebFolder, ContentThumbnailsFolder);
        }
    }
}
