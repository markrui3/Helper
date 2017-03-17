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
    class id : @base
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
            if (this.recvObj.Length >= 3)
                str = recvObj.Substring(0, 3);
            if (str == ("身份证"))
                return true;
            return false;
        }

        public override string handle()
        {
            this.recvObj = this.recvObj.Replace("身份证", "");

            if (this.recvObj == "")
            {
                this.retObj = "嗯？说好的身份证号呢~";
                return getTextJson(this.retObj);
            }
            this.search(this.recvObj);
            string content = this.retObj;
            this.retObj = "";
            JObject obj = new JObject();
            if (content != null)
            {
                obj = JObject.Parse(content);

                if (obj["success"].ToString() == "1")
                {
                    JObject jsonObj = (JObject)obj["result"];
                    this.retObj += "性别：" + jsonObj["sex"] + "\n生日：" + jsonObj["born"] + "\n归属地：" + jsonObj["att"];
                }
                else
                {
                    this.retObj = obj["msg"].ToString();
                }
                return getTextJson(this.retObj);
            }
            else
            {
                this.retObj = "查不到身份证" + this.recvObj + "的信息啊，是不是网络出问题了？";
                return getTextJson(this.retObj);
            }
        }

        public void search(string id_number)
        {
            string url = "http://api.k780.com/?app=idcard.get&idcard=" + id_number + "&appkey=10021&sign=13e512adb1ec0e128ffa9c2ea00c6f77&format=json";

            utils.webutils.file_get_contents(url);
            this.retObj = utils.webutils.contents;
        }
    }
}
