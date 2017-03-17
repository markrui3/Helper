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
    class chengyu : @base
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
            if (str == ("成语"))
                return true;
            return false;
        }

        public override async Task<string> handle()
        {
            this.recvObj = this.recvObj.Replace("成语", "");
            if (this.recvObj == "")
            {
                this.retObj = "什么成语？没听清~对我大声说成语+内容，如成语 千言万语~";
                return getTextJson(this.retObj);
            }
            await this.search(this.recvObj);
            string content = this.retObj;
            this.retObj = "";
            JsonArray jsonArray = new JsonArray();      

            if (content != "[]" && content != "")
            {
                jsonArray = JsonArray.Parse(content);
                foreach (JsonValue value in jsonArray)
                {
                    var jsonObj = value.GetObject();
                    this.retObj += jsonObj["Title"].GetString() + jsonObj["Description"].GetString() + "\n";
                }
            }
            else
            {
                this.retObj = "Sorry~查不到成语" + this.recvObj + "的任何解释啊~";
            }
            return getTextJson(this.retObj);
        }

        public async Task search(string word)
        {
            string appid = "%EE7%95%85Thug4Life";
            string url = "http://api100.duapp.com/idiom/?appkey=" + appid + "&word=" + word;

            this.retObj = await utils.webutils.file_get_contents(url);
        }
    }
}
