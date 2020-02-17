using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Social.NetWork.BLL.Interfaces {
    public interface IImageService {
        Task<byte[]> GetImageData(HttpPostedFileBase image);
        Task<byte[]> GetImageDataFromFS(string path);
    }
}
