using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chat_wp.extensions
{
    class @default : @base
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
            this.retObj = "目前可以查到的功能有：\n" + 
"-------------------------  \n" +
"查快递   翻译   天气查询  \n " +
"算人品   ip       身份证  \n"  +
"查百科   笑话   周公解梦  \n" +
"查公交   菜谱   成语词典  \n" +
"手机            空气质量  \n" +
"历史上的今天  \n" + 
"-------------------------  \n" +
"如果您想了解各功能的使用方法，请直接回复“帮助”或“h”获取相关信息。";
            return getTextJson(this.retObj);
        }
    }
    
    
}
