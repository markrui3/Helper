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
    class history : @base
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
            if (str == ("历史"))
                return true;
            return false;
        }

        public override string handle()
        {
            this.search();
            this.retObj = this.retObj.Replace("\\", "");
            this.retObj = this.retObj.Replace("n", "\n");
            string content = this.retObj;

            if (content == null)
            {
                this.retObj = "404 \n Not Found";
            }
            else
            {
                JObject obj = JObject.Parse(content);
                this.retObj = obj.ToString();
            }
            return getTextJson(this.retObj);
        }

        public void search()
        {
            string appid = "Thug4Life";
            string url = "http://api100.duapp.com/history/?appkey=" + appid;

            utils.webutils.file_get_contents(url);
            this.retObj = utils.webutils.contents;
            //this.retObj = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, Byte.Parse(this.retObj));
        }
    }
}
