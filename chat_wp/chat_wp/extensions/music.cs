using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace chat_wp.extensions
{
    class music : @base
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
            if (str == "音乐")
                return true;
            return false;
        }

        public override string handle()
        {
            this.recvObj = this.recvObj.Replace("音乐", "");
            if (this.recvObj == "")
            {
                this.retObj = "想听哪首歌啊~输入音乐+音乐名 或 音乐+音乐名@作者，如音乐龙的传人@王力宏试试吧~";
                return getTextJson(this.retObj);
            }

            string song, author;
            if (this.recvObj.IndexOf("@") > -1)
            {
                song = this.recvObj.Substring(0, this.recvObj.IndexOf("@"));
                author = this.recvObj.Replace(song + "@", "");
            }
            else
            {
                song = this.recvObj;
                author = "";
            }

            this.search(song, author);
            string content = this.retObj;

            if (content != "" || content != "null" || content != null)
            {
                content = content.Replace("<![CDATA[", "");
                content = content.Replace("]]>", "");
                XmlReader reader = XmlReader.Create(new StringReader(content), new XmlReaderSettings { DtdProcessing = DtdProcessing.Prohibit });

                reader.ReadToFollowing("count");
                int count = reader.ReadElementContentAsInt();
                if (count == 0)
                {
                    this.retObj = "抱歉，未找到歌曲" + this.recvObj + "的信息哦~";
                    return getTextJson(this.retObj);
                }
                else
                {
                    //reader.ReadToFollowing("p2p");
                    //reader.ReadToDescendant("url");
                    // string url = reader.ReadElementContentAsString();
                    try
                    {
                        string xml = Regex.Match(content, @"\<durl\>\<encode\>.*\<\/encode\>\<decode\>.*\<\/decode\>\<type\>.*\<\/type\>\<lrcid\>.*\<\/lrcid\>\<flag\>.*\<\/flag\>\<\/durl\>").Value;
                        string durl = Regex.Match(xml, @"\<decode\>.*\<\/decode\>").Value;
                        durl = durl.Replace("<decode>", "");
                        durl = durl.Replace("</decode>", "");
                        string id = durl.Substring(0, durl.IndexOf(".m"));
                        string url = "http://zhangmenshiting.baidu.com/data2/music/" + id + "/" + durl;

                        return getMusicJson(song, author, url);
                    }
                    catch (Exception)
                    {
                        this.retObj = "抱歉，未找到歌曲" + this.recvObj + "的信息哦~";
                        return getTextJson(this.retObj);
                    }
                }

                /*    try
                    {
                        string url = "";
                        JsonArray jsonArray = new JsonArray();
                        jsonArray = JsonArray.Parse(content);
                        foreach (JsonValue value in jsonArray)
                        {
                            var jsonObj = value.GetObject();
                            author = jsonObj["author"].GetString();
                            url = jsonObj["src"].GetString();
                            break;
                        }
                        return getMusicJson(song, author, url);
                    }
                    catch(Exception)
                    {
                        this.retObj = "抱歉，未找到歌曲" + this.recvObj + "的信息哦~";
                        return getTextJson(this.retObj);
                    }*/

            }
            else
            {
                this.retObj = "抱歉，未找到歌曲" + this.recvObj + "的信息哦~";
                return getTextJson(this.retObj);
            }
        }

        public void search(string song, string author)
        {
            string url;
            if (author != "")
                url = "http://box.zhangmen.baidu.com/x?op=12&count=1&title=" + song + "$$" + author + "$$$$";
            else
                url = "http://box.zhangmen.baidu.com/x?op=12&count=1&title=" + song + "$$$$$$";
            utils.webutils.file_get_contents(url);
            this.retObj = utils.webutils.contents;
            /* string url = "http://www.xiami.com/web/search-songs?key=" + song + " " + author + "&_xiamitoken=a";
             this.retObj = await utils.webutils.file_get_contents(url);*/
        }
    }
}
