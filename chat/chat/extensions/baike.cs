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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace chat.extensions
{
    class baike : @base
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
            if (str == ("百科"))
                return true;
            return false;
        }

        public override async Task<string> handle()
        {
            this.recvObj = this.recvObj.Replace("百科", "");

            if (this.recvObj == "")
            {
                this.retObj = "要百科什么啊，你不告诉我我也不告诉你~输入百科+内容，如百科 微信试试吧~";
                return getTextJson(this.retObj);
            }
            await this.search(this.recvObj);
            if (this.retObj.IndexOf("Title") == -1)
            {
                this.retObj = this.retObj.Replace("\"", "");
                return getTextJson(this.retObj);
            }
            else
            {
                double ArticleCount = 0;
                JArray jsonArray = JArray.Parse(this.retObj);
                foreach (JValue value in jsonArray)
                {
                    ArticleCount++;
                }
                return getNewsJson(ArticleCount, this.retObj);
            }
        }

        public async Task search(string word)
        {
            string appid = "Thug4Life";
            string url = "http://api100.duapp.com/encyclopedia/?appkey=" + appid + "&word=" + word;

            this.retObj = await utils.webutils.file_get_contents(url);
        }
    }
}
