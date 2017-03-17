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

        public override async Task<string> handle()
        {
            this.recvObj = this.recvObj.Replace("身份证", "");

            if (this.recvObj == "")
            {
                this.retObj = "不说谁的身份证我怎么给你查呢？请输入身份证+身份证号，如身份证 222222199208232222~";
                return getTextJson(this.retObj);
            }
            await this.search(this.recvObj);
            string content = this.retObj;
            this.retObj = "";
            JsonObject obj = new JsonObject();
            if (content != "")
            {
                obj = JsonObject.Parse(content);

                if (obj.GetNamedString("success") == "1")
                {
                    JsonObject jsonObj = obj.GetNamedObject("result");
                    this.retObj += "性别：" + jsonObj.GetNamedString("sex") + "\n生日：" + jsonObj.GetNamedString("born") + "\n归属地：" + jsonObj.GetNamedString("att");
                }
                else
                {
                    this.retObj = obj.GetNamedString("msg");
                }
                return getTextJson(this.retObj);
            }
            else
            {
                this.retObj = "查不到身份证" + this.recvObj + "的信息啊，是不是网络出问题了？";
                return getTextJson(this.retObj);
            }
        }

        public async Task search(string id_number)
        {
            string url = "http://api.k780.com/?app=idcard.get&idcard=" + id_number + "&appkey=10021&sign=13e512adb1ec0e128ffa9c2ea00c6f77&format=json";

            this.retObj = await utils.webutils.file_get_contents(url);
        }
    }
}
