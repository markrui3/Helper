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
    class dream : @base
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
            if (str == ("梦见"))
                return true;
            return false;
        }

        public override async Task<string> handle()
        {
            this.recvObj = this.recvObj.Replace("梦见", "");
            if (this.recvObj == "")
            {
                this.retObj = "梦见啥了？周公解梦请输入梦见+内容，如梦见 蛇~";
                return getTextJson(this.retObj);
            }
            await this.meng(this.recvObj);
            string content = this.retObj;

            if (content == "")
            {
                this.retObj = "周公不知道你为什么梦见" + this.recvObj;
            }
            else
            {
                this.retObj = this.retObj.Replace("\"", "");
            }
            return getTextJson(this.retObj);
        }

        public async Task meng(string content)
        {
            string appid = "%E8%8B%8F%E7%95%85Thug4Life";
            string url = "http://api100.duapp.com/dream/?appkey=" + appid + "&content=" + content;

            this.retObj = await utils.webutils.file_get_contents(url);
            //this.retObj = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, Byte.Parse(this.retObj));
        }
    }
}
