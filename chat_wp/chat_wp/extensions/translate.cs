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
        
        public override string handle()
        {
            this.recvObj = this.recvObj.Replace("翻译", "");
            if (this.recvObj == "")
            {
                this.retObj = "翻译啥啊？";
                return getTextJson(this.retObj);
            }
            this.language(this.recvObj);
            string content = this.retObj;

            if (content == null)
            {
                this.retObj = "抱歉，没有查到\"" + this.recvObj + "\"的翻译信息！";
            }
            else
            {
                try
                {
                    JObject obj = (JObject)JsonConvert.DeserializeObject(content);
                    JArray ja = (JArray)obj["trans_result"];
                    obj = (JObject)ja[0];
                    this.retObj = obj["dst"].ToString();
                }
                catch (Exception)
                {
                    this.retObj = null;
                }
            }
            return getTextJson(this.retObj);
        }

        public void language(string str, string from = "auto", string to = "auto")
        {
            string appid = "4RTGvorznzpxQDuM6bha4g1u";
            

            string url = "http://openapi.baidu.com/public/2.0/bmt/translate?client_id=" + appid + "&q=" + str + "&from=" + from + "&to=" + to;

            utils.webutils.file_get_contents(url);
            this.retObj = utils.webutils.contents;
            //this.retObj = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, Byte.Parse(this.retObj));
        }
    }
}
