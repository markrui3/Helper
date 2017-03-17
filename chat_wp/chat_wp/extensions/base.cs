using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace chat_wp.extensions
{
    abstract class @base
    {
        public string recvObj;
        public string retObj;
        public virtual bool shouldHandle(String recvObj)
        {
            this.recvObj = recvObj;

            return false;
        }

        public virtual string handle()
        {
            this.retObj =  "我是base";
            return getTextJson(this.retObj);
        }

        public string getTextJson(string text)
        {
            JObject obj = new JObject();
            obj.Add("MsgType", JValue.CreateString("text"));
            obj.Add("Content", JValue.CreateString(text));
            return obj.ToString();
        }

           public string getNewsJson(double num, string news)
           {
               JObject obj = new JObject();
               obj.Add("MsgType", JValue.CreateString("news"));
               obj.Add("ArticleCount", JValue.CreateString(num.ToString()));
               obj.Add("Articles", JValue.CreateString(news));
               return obj.ToString();
           }

           public string getMusicJson(string MusicTitle, string MusicDes, string MusicUrl)
           {
               JObject obj = new JObject();
               obj.Add("MsgType", JValue.CreateString("music"));
               obj.Add("MusicTitle", JValue.CreateString(MusicTitle));
               obj.Add("MusicDes", JValue.CreateString(MusicDes));
               obj.Add("MusicUrl", JValue.CreateString(MusicUrl));
               return obj.ToString();
           }

    }
}
