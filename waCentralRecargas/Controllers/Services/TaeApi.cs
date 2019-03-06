﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;

namespace waCentralRecargas.Controllers.Services
{
    public class TaeApi
    {
        private string keyTaeCel = "24bdfd7a160b7b86eb11d1cf03c77f59";
        private string nipTaeCel = "dbd68bcfd4d3529307279b7f0add29cb";
        private string keyTaeCel1 = "24bdfd7a160b7b86eb11d1cf03c77f59";
        private string nipTaeCel2 = "dbd68bcfd4d3529307279b7f0add29cb";
        public Models.TaecelModel.RequestTXNModel SendServiceRE(string producto, string monto, string referencia,string op)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var client = new RestClient("https://taecel.com/app/api/RequestTXN");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Connection", "Keep-Alive");
            request.AddParameter("undefined", "key=" + op=="1"?keyTaeCel:keyTaeCel1 + "&nip=" + op=="1"?nipTaeCel:nipTaeCel2 + "&producto=" + producto + "&referencia=" + referencia, ParameterType.RequestBody);
            request.Timeout = 180000;
            request.ReadWriteTimeout = 180000;
            client.Timeout = 180000;
            client.ReadWriteTimeout = 180000;
            Models.TaecelModel.RequestTXNModel x = new Models.TaecelModel.RequestTXNModel();
            try
            {
                IRestResponse response = client.Execute(request);
                var content = JsonConvert.DeserializeObject(response.Content);
                x = (Models.TaecelModel.RequestTXNModel)Newtonsoft.Json.JsonConvert.DeserializeObject(content.ToString(), typeof(Models.TaecelModel.RequestTXNModel));
            }
            catch
            {
                return new Models.TaecelModel.RequestTXNModel();
            }
            return x;
        }
        public Models.TaecelModel.StatusTXNRE GetStatusTXNRE(string transID,string op)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var client = new RestClient("https://taecel.com/app/api/StatusTXN");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Connection", "Keep-Alive");
            request.AddParameter("undefined", "key=" + op == "1" ? keyTaeCel : keyTaeCel1 + "&nip=" + op=="1"?nipTaeCel:nipTaeCel2 + "&transID=" + transID, ParameterType.RequestBody);
            request.Timeout = 180000;
            request.ReadWriteTimeout = 180000;
            client.Timeout = 180000;
            client.ReadWriteTimeout = 180000;
            Models.TaecelModel.StatusTXNRE x = new Models.TaecelModel.StatusTXNRE();
            try
            {
                IRestResponse response = client.Execute(request);
                var content = JsonConvert.DeserializeObject(response.Content);
                x = (Models.TaecelModel.StatusTXNRE)Newtonsoft.Json.JsonConvert.DeserializeObject(content.ToString(), typeof(Models.TaecelModel.StatusTXNRE));

            }
            catch
            {
                System.Threading.Thread.Sleep(2000);
                IRestResponse response = client.Execute(request);
                var content = JsonConvert.DeserializeObject(response.Content);
                x = (Models.TaecelModel.StatusTXNRE)Newtonsoft.Json.JsonConvert.DeserializeObject(content.ToString(), typeof(Models.TaecelModel.StatusTXNRE));
            }
            return x;
        }
        public Models.TaecelModel.RequestTXNModel SendServicePS(string producto, string monto, string referencia,string op)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var client = new RestClient("https://taecel.com/app/api/RequestTXN");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Connection", "Keep-Alive");
            request.AddParameter("undefined", "key=" + op == "1" ? keyTaeCel : keyTaeCel1 + "&nip=" + op=="1"?nipTaeCel:nipTaeCel2 + "&producto=" + producto + "&referencia=" + referencia + "&monto=" + monto, ParameterType.RequestBody);
            request.Timeout = 180000;
            request.ReadWriteTimeout = 180000;
            client.Timeout = 180000;
            client.ReadWriteTimeout = 180000;
            Models.TaecelModel.RequestTXNModel x = new Models.TaecelModel.RequestTXNModel();
            try
            {
                IRestResponse response = client.Execute(request);
                var content = JsonConvert.DeserializeObject(response.Content);
                x = (Models.TaecelModel.RequestTXNModel)Newtonsoft.Json.JsonConvert.DeserializeObject(content.ToString(), typeof(Models.TaecelModel.RequestTXNModel));
            }
            catch
            {
                return new Models.TaecelModel.RequestTXNModel();
            }
            return x;
        }
    }
}
