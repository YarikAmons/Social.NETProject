using System;
using System.Web.Mvc;

namespace Social.NetWork.WEB.Helpers {
    public static class ImageHelper {
        public static MvcHtmlString ImageFromArray(this HtmlHelper html, byte[] image, string classes = "", string width="", string height = "", string id = "", string alt = "") {
            string imgStr = "";
            string dataType = "";

            TagBuilder img = new TagBuilder("img");

            if (image != null) {
                imgStr = Convert.ToBase64String(image);
                dataType = "data:image/jpg;base64";
            }

            string src = image != null ? $"{dataType}, {imgStr}" : "";
            img.Attributes.Add("id", id);
            img.Attributes.Add("src", src);
            img.Attributes.Add("alt", alt);
            img.Attributes.Add("width", width);
            img.Attributes.Add("height", height);
            img.AddCssClass(classes);

            return MvcHtmlString.Create(img.ToString(TagRenderMode.SelfClosing));
        }
    }
}