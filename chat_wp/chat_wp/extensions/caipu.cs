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

        public override string handle()
        {
            this.recvObj = this.recvObj.Replace("菜谱", "");
            if (this.recvObj == "")
            {
                this.retObj = "哎呦，不说是哪道菜我怎么给您查呢~";
                return getTextJson(this.retObj);
            }
            this.search(this.recvObj);
            string content = this.retObj;
            this.retObj = "";
            try
            {
                JArray obj = new JArray();
                if (content != null)
                    obj = JArray.Parse(content);
                foreach (JValue jsonObj in obj)
                {
                    this.retObj += jsonObj["Title"].ToString() + "\n" + jsonObj["Description"].ToString();
                }
            }
            catch(Exception)
            {
                this.retObj = "未找到任何关于" + this.recvObj + "的菜谱!";
            }

            return getTextJson(this.retObj);
        }

        public void search(string name)
        {
            string appid = "Thug4Life";
            string url = "http://api100.duapp.com/recipe/?appkey=" + appid + "&name=" + name;
            utils.webutils.file_get_contents(url);
            this.retObj = utils.webutils.contents;
        }
    }
}
