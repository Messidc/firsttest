using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace WulianwangAPITest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            string serviceAddress = "http://open.189iot.cn:8832/wlwManage/api/auth.do";
            //string body = "{ \"userName\": \"DHBSQ\",\"secret\": \"hbgz.890\"}";
            string body = "{ \"userName\": \"ttrr\",\"secret\": \"Chen@190\"}";
            string sss=Post(serviceAddress,body);
            JAuth jp = JsonConvert.DeserializeObject<JAuth>(sss);



            //string body1 = "{ \"imei\": \"864183031179194\"}";
            //string sss1 = Post1("http://open.189iot.cn:8832/wlwManage/api/getDeviceInfo.do", body1, jp.accessToken);




            //调用魔豆的sourceCommand
            string body2 = "{ \"imei\": \"869029030511454\",\"orderList\": [{ \"orderName\": \"命令下发\",\"orderValue\": {\"command\":\"0212fefefefe6899999999999968010263e9b516\"}}]}";
            string sss2 = Post2("http://open.189iot.cn:8832/wlwManage/api/orderCommand.do", body2, jp.accessToken);


            Console.WriteLine(sss2);
            Console.ReadKey();
        }

        /// <summary>
        /// {"tokenType":"gzwl","scope":"default","expiresIn":3600,"accessToken":"ee46c105162445a4b21bfb7c57e80bf7","refreshToken":"6497e691a214449594842c487607def3"}
        /// </summary>
        public class JAuth
        {
            public string tokenType;
            public string scope;
            public string expiresIn;
            public string accessToken;
            public string refreshToken;
        }

        /// <summary>
        /// 指定Post地址使用Get 方式获取全部字符串
        /// </summary>
        /// <param name="url">请求后台地址</param>
        /// <returns></returns>
        private string Post(string url, Dictionary<string, string> dic)
        {
            string result = string.Empty;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            #region 添加Post 参数
            StringBuilder builder = new StringBuilder();
            int i = 0;
            foreach (var item in dic)
            {
                if (i > 0)
                    builder.Append("&");
                builder.AppendFormat("{0}={1}", item.Key, item.Value);
                i++;
            }
            byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            #endregion

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        /// <summary>
        /// POST整个字符串到URL地址中 、、获取key  accessToken
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="jsonParas"></param>
        /// <returns></returns>
        public static string Post(string Url, string jsonParas)
        {
            string strURL = Url;

            //创建一个HTTP请求 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strURL);
            //Post请求方式 
            request.Method = "POST";
            //内容类型
            request.ContentType = "application/x-www-form-urlencoded";

            //设置参数，并进行URL编码

            string paraUrlCoded = jsonParas;//System.Web.HttpUtility.UrlEncode(jsonParas);  

            byte[] payload;
            //将Json字符串转化为字节 
            payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
            //设置请求的ContentLength  
            request.ContentLength = payload.Length;
            //发送请求，获得请求流

            Stream writer;
            try
            {
                writer = request.GetRequestStream();//获取用于写入请求数据的Stream对象
            }
            catch (Exception)
            {
                writer = null;
                Console.Write("连接服务器失败!");
            }
            //将请求参数写入流
            writer.Write(payload, 0, payload.Length);
            writer.Close();//关闭请求流

            //String strValue = "";//strValue为http响应所返回的字符流
            HttpWebResponse response;
            try
            {
                //获得响应流
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = ex.Response as HttpWebResponse;
            }

            Stream s = response.GetResponseStream();

            //Stream postData = Request.InputStream;
            StreamReader sRead = new StreamReader(s);
            string postContent = sRead.ReadToEnd();
            sRead.Close();

            return postContent;//返回Json数据
        }





        /// <summary>
        ///DC  查询设备参数
        ///
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="jsonParas"></param>
        /// <returns></returns>
        public static string Post1(string Url,string jsonParas, string para)
        {
            string strURL = Url;

            //创建一个HTTP请求 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strURL);
            //Post请求方式 
            request.Method = "POST";
            //内容类型
            request.ContentType = "application/json";

            request.Headers.Add("app_key", "DHBSQ");
            request.Headers.Add("Authorization", "GZWL "+para);

            //设置参数，并进行URL编码

            string paraUrlCoded = jsonParas;//System.Web.HttpUtility.UrlEncode(jsonParas);  

            byte[] payload;
            //将Json字符串转化为字节 
            payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
            //设置请求的ContentLength  
            request.ContentLength = payload.Length;




            //发送请求，获得请求流

            Stream writer;
            try
            {
                writer = request.GetRequestStream();//获取用于写入请求数据的Stream对象
            }
            catch (Exception)
            {
                writer = null;
                Console.Write("连接服务器失败!");
            }
            //将请求参数写入流
            writer.Write(payload, 0, payload.Length);
            writer.Close();//关闭请求流

            //String strValue = "";//strValue为http响应所返回的字符流
            HttpWebResponse response;
            try
            {
                //获得响应流
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = ex.Response as HttpWebResponse;
            }

            Stream s = response.GetResponseStream();

            //Stream postData = Request.InputStream;
            StreamReader sRead = new StreamReader(s);
            string postContent = sRead.ReadToEnd();
            sRead.Close();

            return postContent;//返回Json数据
        }



        /// <summary>
        ///DC2  调用命令
        ///
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="jsonParas"></param>
        /// <returns></returns>
        public static string Post2(string Url, string jsonParas, string para)
        {
            string strURL = Url;

            //创建一个HTTP请求 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strURL);
            //Post请求方式 
            request.Method = "POST";
            //内容类型
            request.ContentType = "application/json";

            request.Headers.Add("app_key", "ttrr");
            request.Headers.Add("Authorization", "GZWL " + para);

            //设置参数，并进行URL编码

            string paraUrlCoded = jsonParas;//System.Web.HttpUtility.UrlEncode(jsonParas);  

            byte[] payload;
            //将Json字符串转化为字节 
            payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
            //设置请求的ContentLength  
            request.ContentLength = payload.Length;




            //发送请求，获得请求流

            Stream writer;
            try
            {
                writer = request.GetRequestStream();//获取用于写入请求数据的Stream对象
            }
            catch (Exception)
            {
                writer = null;
                Console.Write("连接服务器失败!");
            }
            //将请求参数写入流
            writer.Write(payload, 0, payload.Length);
            writer.Close();//关闭请求流

            //String strValue = "";//strValue为http响应所返回的字符流
            HttpWebResponse response;
            try
            {
                //获得响应流
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = ex.Response as HttpWebResponse;
            }

            Stream s = response.GetResponseStream();

            //Stream postData = Request.InputStream;
            StreamReader sRead = new StreamReader(s);
            string postContent = sRead.ReadToEnd();
            sRead.Close();

            return postContent;//返回Json数据
        }

    }
}
