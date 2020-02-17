using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Social.NetWork.WEB.Controllers {
    /// <summary>
    /// Summary description for ChatHandler
    /// </summary>
    public class ChatHandler : IHttpHandler {

        public void ProcessRequest(HttpContext context) {
            if (context.IsWebSocketRequest)
                context.AcceptWebSocketRequest(WebSocketRequest);
        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}