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
    class simi : @base
    {
        public new string recvObj;
        public new string retObj;
        public override bool shouldHandle(String recvObj)
        {
            this.recvObj = recvObj;
            return true;
        }

        public override async Task<string> handle()
        {
            if (this.recvObj == "")
            {
                this.retObj = "说什么啊？听不清~";
                return this.retObj;
            }

            await this.search(this.recvObj);
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

        public async Task search(string key)
        {
            string url = "http://www.xiaojo.com/bot/chata.php";

            this.retObj = await utils.webutils.post(key, url);
        }
    }
}
