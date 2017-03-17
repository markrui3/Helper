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
    class phone : @base
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
            if (str == ("手机"))
                return true;
            return false;
        }

        public override async Task<string> handle()
        {
            this.recvObj = this.recvObj.Replace("手机", "");

            if (this.recvObj == "")
            {
                this.retObj = "手机归属？不用愁，输入手机+号码，如手机 18722222222~";
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
                    this.retObj += "卡类型：" + jsonObj.GetNamedString("ctype") + "\n区号：" + jsonObj.GetNamedString("area") + "\n邮政编码：" + jsonObj.GetNamedString("postno") + "\n所在城市：" + jsonObj.GetNamedString("style_citynm");
                }
                else
                {
                    this.retObj = obj.GetNamedString("msg");
                }
            }
            else
            {
                this.retObj = "查不到手机号码" + this.recvObj + "的信息啊，是不是网络出问题了？";
            }
            return getTextJson(this.retObj);
        }

        public async Task search(string phone_number)
        {
            string url = "http://api.k780.com/?app=phone.get&phone=" + phone_number + "&appkey=10021&sign=13e512adb1ec0e128ffa9c2ea00c6f77&format=json";

            this.retObj = await utils.webutils.file_get_contents(url);
        }
    }
}
