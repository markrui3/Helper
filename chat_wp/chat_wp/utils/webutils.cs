using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace chat_wp.utils
{
    class webutils
    {
        public static string contents = null;

        public static void file_get_contents(string url)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.OpenReadAsync(new Uri(url, UriKind.RelativeOrAbsolute));
                webClient.OpenReadCompleted += new OpenReadCompletedEventHandler(webClient_OpenReadCompleted);
            }
            catch (Exception)
            {
                MessageBox.Show("网络通信不畅");
            }
        }

        public static void webClient_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            using (StreamReader reader = new StreamReader(e.Result)){
                string s = reader.ReadToEnd();
                contents = s.ToString();
            };
        }

        public static void sendGet(string url)
        {
            HttpWebRequest request = HttpWebRequest.CreateHttp(url);
            IAsyncResult result = (IAsyncResult)request.BeginGetResponse(GetCallBack, request);
        }

        public static void GetCallBack(IAsyncResult result)
        {
            HttpWebRequest request = (HttpWebRequest)result.AsyncState;
            WebResponse response = request.EndGetResponse(result);
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                contents = reader.ReadToEnd().ToString();
            }
        }

        public static void sendPost(byte[] data, string url)
        {
            #region 创建httpWebRequest对象

            WebRequest webRequest = WebRequest.Create(url);

            HttpWebRequest httpRequest = webRequest as HttpWebRequest;

            if (httpRequest == null)
            {

                throw new ApplicationException(

                    string.Format("Invalid url string: {0}", url)

                    );

            }

            #endregion



            #region 填充httpWebRequest的基本信息

            httpRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

            httpRequest.ContentType = "application/x-www-form-urlencoded";

            httpRequest.Method = "POST";

            #endregion



            #region 填充要post的内容

            httpRequest.ContentLength = data.Length;

            //Stream requestStream = httpRequest.GetRequestStream();

            //requestStream.Write(data, 0, data.Length);

            //requestStream.Close();

            #endregion



            //IAsyncResult result = (IAsyncResult)request.BeginGetRequestStream(PostCallBack, request);
        }

        public static void PostCallBack(IAsyncResult result)
        {
            HttpWebRequest request = (HttpWebRequest)result.AsyncState;
            WebResponse response = request.EndGetResponse(result);
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                contents = reader.ReadToEnd().ToString();

            }
        }
    }
}
