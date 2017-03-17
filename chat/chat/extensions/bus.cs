using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Runtime.InteropServices;
using Windows.Data.Json;

namespace chat.extensions
{
    class bus : @base
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
            
            if (this.recvObj.IndexOf("公交") > -1)
                return true;
            return false;
        }

        public override async Task<string> handle()
        {
            this.recvObj = this.recvObj.Replace("公交", " ");
            string city = this.recvObj.Substring(0, this.recvObj.IndexOf(" "));
            string line = this.recvObj.Substring(this.recvObj.IndexOf(" ") + 1);
            if (this.recvObj == " ")
            {
                this.retObj = "什么公交？请输入地名+公交+路线，如天津公交50路~";
                return getTextJson(this.retObj);
            }
            await this.search(city, line);
            this.retObj = this.retObj.Replace("\\", "");
            this.retObj = this.retObj.Replace("n", "\n");
            string content = this.retObj;
            
            if (content == "")
            {
                this.retObj = "查不到" + this.recvObj + "的相关数据耶~/(^_^)(^_^)/";
            }
            else
            {
                this.retObj = this.retObj.Replace("\"", "");
            }
            return getTextJson(this.retObj);
        }

        public async Task search(string city, string line)
        {
            string appid = "Thug4Life";
            string url = "http://api100.duapp.com/busline/?appkey=" + appid + "&city=" + city + "&line=" + line;

            this.retObj = await utils.webutils.file_get_contents(url);
            //this.retObj = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, Byte.Parse(this.retObj));
        }
    }
}
