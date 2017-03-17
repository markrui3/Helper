using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using Windows.UI.Popups;
using Windows.Web.Syndication;
using Windows.Networking.Connectivity;

namespace chat.utils
{
    class webutils
    {
        public static string result;

        public static async Task<string> file_get_contents(string url)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                Boolean statusCode = response.IsSuccessStatusCode;
                if (!statusCode)
                {
                    Windows.UI.Popups.MessageDialog dlg = new Windows.UI.Popups.MessageDialog("网络通信不畅");
                    await dlg.ShowAsync();
                    return null;
                }
                result = await httpClient.GetStringAsync(url);
                return result;
            }
            catch (Exception)
            {
                result = "";
                return "";
            }
        }

        public static async Task<string> post(string key, string url)
        {
            try
            {
                HttpClient client = new HttpClient();
                //准备POST的数据
                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("chat", key));
                HttpContent httpcontent = new FormUrlEncodedContent(postData);
                HttpResponseMessage response = await client.PostAsync(url, httpcontent);
                Boolean statusCode = response.IsSuccessStatusCode;
                if (!statusCode)
                {
                    Windows.UI.Popups.MessageDialog dlg = new Windows.UI.Popups.MessageDialog("网络通信不畅");
                    await dlg.ShowAsync();
                    return null;
                }
                //返回的信息
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
