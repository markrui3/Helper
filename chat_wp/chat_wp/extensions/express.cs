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
    class express: @base
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

            if (this.recvObj.IndexOf("快递") > -1 || this.recvObj.IndexOf("物流") > -1)
                return true;
            return false;
        }

        public override string handle()
        {
            this.recvObj = this.recvObj.Replace("快递", " ");
            this.recvObj = this.recvObj.Replace("物流", " ");
            string com = this.recvObj.Substring(0, this.recvObj.IndexOf(" "));
            string num = this.recvObj.Substring(this.recvObj.IndexOf(" ") + 1);
           // $str_key1 = mb_substr ( $this->msg, 0, strrpos($this->msg," ")-4, "UTF-8" );

            if (this.recvObj == " ")
            {
                this.retObj = "查啥快递啊？给个提示被~";
                return getTextJson(this.retObj);
            }
            this.search(com, num);
            string content = this.retObj;
            this.retObj = "";
            JObject obj = new JObject();
            if (content != null)
            {
                obj = JObject.Parse(content);

                if (obj["errCode"].ToString() == "0")
                {
                    JArray jsonArray = (JArray)obj["data"];
                    foreach (JValue jsonObj in jsonArray)
                    {
                        this.retObj += jsonObj["time"].ToString() + jsonObj["context"].ToString() + "\n";
                    }
                }
                else
                {
                    this.retObj = obj["message"].ToString();
                }
            }
            else
            {
                this.retObj = "抱歉，找不到您的快递消息~";
            }
            return getTextJson(this.retObj);
        }

        public void search(string com, string num)
        {
            string text = "{\"AAE\":\"aae\",\"安捷\":\"安捷anjie\",\"安信达\":\"anxinda\",\"Aramex\":\"aramex\",\"CCES\":\"cces\",\"长通\":\"changtong\",\"程光\":\"chengguang\",\"传喜\":\"chuanxi\",\"传志\":\"chuanzhi\",\"CityLink\":\"citylink\",\"东方\":\"coe\",\"城市之星\":\"cszx\",\"大田\":\"datian\",\"德邦\":\"debang\",\"DHL\":\"dhl\",\"递四方\":\"disifang\",\"DPEX\":\"dpex\",\"D速\":\"dsu\",\"百福东方\":\"ees\",\"国际Fedex\":\"fedex\",\"Fedex国内\":\"fedexcn\",\"飞邦\":\"feibang\",\"飞豹\":\"feibao\",\"飞航\":\"feihang\",\"飞远\":\"feiyuan\",\"丰达\":\"fengda\",\"飞康达\":\"fkd\",\"飞快达\":\"fkdex\",\"广东邮政\":\"gdyz\",\"共速达\":\"gongsuda\",\"天地华宇\":\"huayu\",\"华宇\":\"huayu\",\"汇通\":\"huitong\",\"佳吉\":\"jiaji\",\"佳怡\":\"jiayi\",\"加运美\":\"jiayunmei\",\"京广\":\"jingguang\",\"晋越\":\"jinyue\",\"嘉里大通\":\"jldt\",\"快捷\":\"kuaijie\",\"蓝镖\":\"lanbiao\",\"乐捷递\":\"lejiedi\",\"联昊通\":\"lianhaotong\",\"龙邦\":\"longbang\",\"民航\":\"minhang\",\"港中能达\":\"nengda\",\"能达\":\"nengda\",\"OCS\":\"ocs\",\"平安达\":\"pinganda\",\"全晨\":\"quanchen\",\"全峰\":\"quanfeng\",\"全际通\":\"quanjitong\",\"全日通\":\"quanritong\",\"全一\":\"quanyi\",\"RPX\":\"rpx\",\"保时达\":\"rpx\",\"如风达\":\"rufeng\",\"三态\":\"santai\",\"伟邦\":\"scs\",\"盛丰\":\"shengfeng\",\"盛辉\":\"shenghui\",\"申通\":\"shentong\",\"顺丰\":\"shunfeng\",\"速尔\":\"sure\",\"天天\":\"tiantian\",\"TNT\":\"tnt\",\"通成\":\"tongcheng\",\"UPS\":\"ups\",\"USPS\":\"usps\",\"万家\":\"wanjia\",\"新邦\":\"xinbang\",\"鑫飞鸿\":\"xinfeihong\",\"信丰\":\"xinfeng\",\"源安达\":\"yad\",\"亚风\":\"yafeng\",\"一邦\":\"yibang\",\"银捷\":\"yinjie\",\"优速\":\"yousu\",\"一统飞鸿\":\"ytfh\",\"远成\":\"yuancheng\",\"圆通\":\"yuantong\",\"元智捷诚\":\"yuanzhi\",\"越丰\":\"yuefeng\",\"韵达\":\"yunda\",\"运通\":\"yuntong\",\"源伟丰\":\"ywfex\",\"宅急送\":\"zhaijisong\",\"中铁\":\"zhongtie\",\"中通\":\"zhongtong\",\"忠信达\":\"zhongxinda\",\"中邮\":\"zhongyou\",\"EMS\":\"ems\"}";
            JObject jsonObject = JObject.Parse(text);
            
            try
            {
                com = jsonObject[com].ToString();
            }
            catch (Exception)
            {
                com = "";
            }

            string uri = "http://api.ickd.cn/";                    //快递查询api的uri
            string authId = "E077B7C6D3F2DA9C92D13D7BD36CD171";    //个人注册的key
            string type = "json";                                  //api返回值类型
            string encode = "utf8";                                //数据返回的字符集
		    string url = uri + "?com=" + com + "&nu=" + num + "&id=" + authId + "&type=" + type + "&encode=" + encode;

            utils.webutils.file_get_contents(url);
            this.retObj = utils.webutils.contents;
        }
    }
}
