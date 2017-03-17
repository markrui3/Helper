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
    class translate : @base
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
            if (str == ("翻译"))
                return true;
            return false;
        }

        public override async Task<string> handle()
        {
            this.recvObj = this.recvObj.Replace("翻译", "");
            if (this.recvObj == "")
            {
                this.retObj = "想找翻译助手？请输入翻译+内容，如翻译 我爱你~";
                return getTextJson(this.retObj);
            }
            await this.language(this.recvObj);
            string content = this.retObj;
            JsonObject obj = new JsonObject();
            if (content != "")
                obj = JsonObject.Parse(content);

            if (content == "")
            {
                this.retObj = "抱歉，没有查到\"" + this.recvObj + "\"的翻译信息！";
            }
            else
            {
                try
                {
                    JsonArray array = obj.GetNamedArray("trans_result");
                    foreach (JsonValue value in array)
                    {
                        var jsonObj = value.GetObject();
                        this.retObj = jsonObj["dst"].GetString();
                    }
                }
                catch (Exception)
                {
                    this.retObj = null;
                }
            }
            return getTextJson(this.retObj);
        }

        public async Task language(string str, string from = "auto", string to = "auto")
        {
            string appid = "4RTGvorznzpxQDuM6bha4g1u";
            

            string url = "http://openapi.baidu.com/public/2.0/bmt/translate?client_id=" + appid + "&q=" + str + "&from=" + from + "&to=" + to;

            this.retObj = await utils.webutils.file_get_contents(url);
            //this.retObj = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, Byte.Parse(this.retObj));
        }
    }
}
