using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace chat_wp.utils
{
class JsonObj
    {
        public string MsgType;
        public string Content;
        public double ArticleCount;
        public string [] Title = new string[20];
        //public string [] Description;
        //public string [] PicUrl;
        public string[] Url = new string[20];
        public string MusicTitle;
        public string MusicDes;
        public string MusicUrl;

        public JsonObj(string str)
        {
            JObject obj = new JObject();
            obj = JObject.Parse(str);
            this.MsgType = obj["MsgType"].ToString();
            if (this.MsgType == "text")
            {
                this.Content = obj["Content"].ToString();
            }
            else if (this.MsgType == "news")
            {
                this.ArticleCount = double.Parse(obj["ArticleCount"].ToString());
                string articleObj = obj["Articles"].ToString();
                JArray jsonArray = JArray.Parse(articleObj);
                int i = 0;
                foreach (JObject value in jsonArray)
                {
                    //var jsonObj = value.GetObject();
                    //this.Description[i] = jsonObj["Description"].GetString();
                    //this.PicUrl[i] = jsonObj["PicUrl"].GetString();
                    try
                    {
                        this.Title[i] = value["Title"].ToString();
                        this.Url[i] = value["Url"].ToString();
                    }
                    catch(Exception)
                    {
                        this.Title[i] = "sada";
                        this.Url[i] = "asda";
                    }
                      
                    i++;
                }
            }
            else if (this.MsgType == "music")
            {
                this.MusicTitle = obj["MusicTitle"].ToString();
                this.MusicDes = obj["MusicDes"].ToString();
                this.MusicUrl = obj["MusicUrl"].ToString();
            }
        }
    }
}
