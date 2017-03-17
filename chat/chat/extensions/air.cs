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
    class air : @base
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
            if (recvObj.Length >= 2)
                str = recvObj.Substring(recvObj.Length - 2);
            if (str == "空气")
                return true;
            return false;
        }

        public override async Task<string> handle()
        {
            this.recvObj = this.recvObj.Replace("空气", "");
            if (this.recvObj == "")
            {
                this.retObj = "哎呦，不说是哪里的空气质量我怎么给您查呢~输入地名+空气，如天津空气试试吧~";
                return getTextJson(this.retObj);
            }
            await this.search(this.recvObj);
            string content = this.retObj;

            if (content != "")
            {
                JArray ja = (JArray)JsonConvert.DeserializeObject(content);
                JObject jsonObj = (JObject)ja[0];
                this.retObj = jsonObj["Title"] + "\n" + jsonObj["Description"];
            }
            else
            {
                this.retObj = "抱歉，没有查到\"" + this.recvObj + "\"的空气质量！";
            }
            return getTextJson(this.retObj);
        }

        public async Task search(string city)
        {
            string appid = "%E8%8B%8F%E7%95%85Thug4Life";
            string url = "http://api100.duapp.com/airquality/?appkey=" + appid + "&city=" + city;
            this.retObj = await utils.webutils.file_get_contents(url);
        }
    }
}
