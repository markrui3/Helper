using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace chat_wp.extensions
{
    class simi : @base
    {
        public new string recvObj;
        public new string retObj;
        public override bool shouldHandle(String recvObj)
        {
            this.recvObj = recvObj;
            return true;
        }

        public override string handle()
        {
            if (this.recvObj == "")
            {
                this.retObj = "说什么啊？听不清~";
                return this.retObj;
            }

            this.search(this.recvObj);
            if (this.retObj.IndexOf("\\") != -1)
            {
                this.retObj = this.retObj.Replace("\\", "");
                this.retObj = this.retObj.Replace("n", "\n");
            }

            string content = this.retObj;

            if (content == "")
            {
                this.retObj = "404 \n Not Found";
            }
            else
            { }
            return getTextJson(this.retObj);
        }

        public void search(string key)
        {
            string url = "http://www.xiaojo.com/bot/chata.php";

            string data = "chat=" + key;
            Encoding encoding = Encoding.GetEncoding("ascii");
            byte[] bytesToPost = encoding.GetBytes(data);

            utils.webutils.sendPost(bytesToPost, url);
            this.retObj = utils.webutils.contents;
        }
    }
}
