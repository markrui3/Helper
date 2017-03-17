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
    class caipu : @base
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
                str = recvObj.Substring(0, 2);
            if (str == "菜谱")
                return true;
            return false;
        }

        public override async Task<string> handle()
        {
            this.recvObj = this.recvObj.Replace("菜谱", "");
            if (this.recvObj == "")
            {
                this.retObj = "哎呦，不说是哪道菜我怎么给您查呢~对我大声说菜谱+菜名，如菜谱 剁椒鱼头~";
                return getTextJson(this.retObj);
            }
            await this.search(this.recvObj);
            string content = this.retObj;
            this.retObj = "";
            try
            {
                JsonArray obj = new JsonArray();
                if (content != "")
                    obj = JsonArray.Parse(content);
                foreach (JsonValue value in obj)
                {
                    var jsonObj = value.GetObject();
                    this.retObj += jsonObj["Title"].GetString() + "\n" + jsonObj["Description"].GetString();
                }
            }
            catch(Exception)
            {
                this.retObj = "未找到任何关于" + this.recvObj + "的菜谱!";
            }
            if (content == "")
                this.retObj = "未找到任何关于" + this.recvObj + "的菜谱!";
            return getTextJson(this.retObj);
        }

        public async Task search(string name)
        {
            string appid = "Thug4Life";
            string url = "http://api100.duapp.com/recipe/?appkey=" + appid + "&name=" + name;
            this.retObj = await utils.webutils.file_get_contents(url);
        }
    }
}
