using Social.NetWork.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Social.NetWork.BLL.Services {
    public class ImageService : IImageService {
        public async Task<byte[]> GetImageData(HttpPostedFileBase image) {
            if (image == null)
                return null;

            byte[] data = new byte[image.ContentLength];
            Stream inputStream = image.InputStream;

            await inputStream.ReadAsync(data, 0, image.ContentLength);

            return data;
        }

        public async Task<byte[]> GetImageDataFromFS(string path) {
            byte[] data;
            using (FileStream fs = File.Open(path, FileMode.Open)) {
                data = new byte[fs.Length];
                await fs.ReadAsync(data, 0, (int)fs.Length);
            }
            return data;
        }
    }
}
