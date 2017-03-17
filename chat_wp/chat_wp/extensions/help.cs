using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chat_wp.extensions
{
    class help : @base
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
            if (this.recvObj.Equals("h") || this.recvObj.Equals("帮助"))
                return true;
            return false;
        }

        public override string handle()
        {
            this.retObj = "★学习类：\n"+

"【翻译】发送“翻译+中文/英文/日文”，例如：“翻译 study”。（支持英汉互译、日译汉）\n" +

"【成语】发送“成语+成语内容”，例如：“成语 方兴未艾”。获取成语词典信息。\n" +

"【百度百科】发送“百科+内容”，例如：“百科 微信”。获取要查询的信息。\n" +

"★生活类：\n"+

"【天气查询】发送“城市名+天气”，例如：“北京 天气”，获取对应城市天气信息。\n"+

"【空气质量】发送“城市名+空气”，例如：“北京 空气”，获取对应城市空气质量信息。\n" +

"【查快递】发送“某某快递+快递单号”，例如：“顺丰快递 966902008817”，获取物流信息。\n"+

"【菜谱】发送“菜谱+菜名”，例如：“菜谱 红烧狮子头”，获取红烧狮子头的做法。\n" +

"【公交】发送“地名+公交+菜名”，例如：“天津 公交 50”，获取天津公交50路车经过的车站。\n" +
 
"【身份证】发送“身份证+身份证号”，例如：“身份证 220882199311170024”，获取身份证信息。\n"+
 
"【IP地址】发送“ip+身份证号”，例如：“ip 255.255.0.0”，获取ip地址信息。\n"+
 
"【手机归属】发送“手机+手机号”，例如：“手机 18722661212”，获取手机号码信息。\n"+

"★娱乐类：\n"+

"【笑话】直接发送“笑话”，获取笑话一则。\n"+

"【周公解梦】发送“梦见+所梦内容”，例如：“梦见 火”，获取周公解梦内容。\n"+

"【人品计算】发送“姓名+人品”，例如：“张三人品”，获取张三的人品计算。\n" +

"【历史上的今天】发送“历史”，例如：“历史”，获取历史上今天发生的大事。";
            return getTextJson(this.retObj);
        }
    }


}
