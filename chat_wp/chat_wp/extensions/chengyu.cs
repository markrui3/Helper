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

        public override string handle()
        {
            this.recvObj = this.recvObj.Replace("成语", "");
            if (this.recvObj == "")
            {
                this.retObj = "什么成语？没听清~";
                return getTextJson(this.retObj);
            }
            this.search(this.recvObj);
            string content = this.retObj;
            this.retObj = "";
            JArray jsonArray = new JArray();

            if (content != null)
            {
                if (content != "[]")
                    jsonArray = JArray.Parse(content);

                if (content != "[]")
                {
                    foreach (JObject jsonObj in jsonArray)
                    {
                        this.retObj += jsonObj["Title"].ToString() + jsonObj["Description"].ToString() + "\n";
                    }
                }
                else
                {
                    this.retObj = "Sorry~查不到成语" + this.recvObj + "的任何解释啊~";
                }
            }
            else
            {
                this.retObj = "抱歉，查不到相应的内容~ \n请检查网络连接";
            }
            
            return getTextJson(this.retObj);
        }

        public void search(string word)
        {
            string appid = "%EE7%95%85Thug4Life";
            string url = "http://api100.duapp.com/idiom/?appkey=" + appid + "&word=" + word;

            utils.webutils.file_get_contents(url);
            this.retObj = utils.webutils.contents;
        }
    }
}
