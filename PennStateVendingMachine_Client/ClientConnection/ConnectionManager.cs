using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClientConnection
{
    public class ConnectionManager
    {
        static HttpClient client = new HttpClient();
        static string path = "http://api.solmon.belgiumcampus.ac.za/vendingmachinestatistics";
        //public async Task<string> TestConnection()
        //{
        //    client.BaseAddress = new Uri("http://api.solmon.belgiumcampus.ac.za");
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    HttpResponseMessage response = await client.GetAsync(path);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return await response.Content.ReadAsStringAsync();
        //    }
        //    return null;
        //}
        public string write(string postData)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://api.solmon.belgiumcampus.ac.za/purchase/AddNewPurchase");
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }
        public string getCodes(string postData)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://api.solmon.belgiumcampus.ac.za/vendingmachinestatistics");
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }
    }
}
