using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace chat_wp.extensions
{
    class fun : @base
    {
        public new string recvObj;
        public new string retObj;
        public override bool shouldHandle(String recvObj)
        {
            recvObj.Trim();
            recvObj = recvObj.Replace(" ", "");
            recvObj = recvObj.Replace("  ", "");
            recvObj = recvObj.Replace("\t", "");
            recvObj = recvObj.Replace("\n", "");
            recvObj = recvObj.Replace("\r", "");
            this.recvObj = recvObj;
            string str = "";
            if (this.recvObj.Length >= 2)
                str = recvObj.Substring(0, 2);
            if (str == ("笑话"))
                return true;
            return false;
        }

        public override string handle()
        {
            this.xiaohua();
            string content = this.retObj;

            if (content == null)
            {
                this.retObj = "404 \n Not Found";
            }
            else
            {
                this.retObj = this.retObj.Replace("\"", "");
            }
            return getTextJson(this.retObj);
        }

        public void xiaohua()
        {
            string appid = "%E8%8B%8F%E7%95%85Thug4Life";
            string url = "http://api100.duapp.com/joke/?appkey=" + appid;

            utils.webutils.file_get_contents(url);
            this.retObj = utils.webutils.contents;
            //this.retObj = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, Byte.Parse(this.retObj));
        }
    }
}
