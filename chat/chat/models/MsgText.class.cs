using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chat.models
{
    class MsgText
    {
        public Queue<extensions.@base> extObjs = new Queue<extensions.@base>();
        public string ques;
        public string retObj;
        public MsgText(String recvObj)
        {
            this.ques = recvObj;

            //将写好的处理类的实例压入队列
            extObjs.Enqueue(new extensions.translate());
            extObjs.Enqueue(new extensions.weather());
            extObjs.Enqueue(new extensions.chengyu());
            extObjs.Enqueue(new extensions.caipu()); 
            extObjs.Enqueue(new extensions.air());           
            extObjs.Enqueue(new extensions.baike());
            extObjs.Enqueue(new extensions.express());
            extObjs.Enqueue(new extensions.fun());
            extObjs.Enqueue(new extensions.dream());
            extObjs.Enqueue(new extensions.id());
            extObjs.Enqueue(new extensions.rp());
            extObjs.Enqueue(new extensions.phone());
            extObjs.Enqueue(new extensions.ip_address());
            extObjs.Enqueue(new extensions.bus());
            extObjs.Enqueue(new extensions.history());
            extObjs.Enqueue(new extensions.music());
            extObjs.Enqueue(new extensions.help());
            extObjs.Enqueue(new extensions.simi());
            extObjs.Enqueue(new extensions.@default());
        }

        public async Task getText()
        {
            IEnumerator<extensions.@base> enumerator = this.extObjs.GetEnumerator();
            enumerator.Reset();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current.shouldHandle(this.ques))
                {
                    this.retObj = await enumerator.Current.handle();
                    break;
                }
            }
        }
    }
}
