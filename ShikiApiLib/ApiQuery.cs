using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;

namespace ShikiApiLib
{
    internal static class Query
    {
        internal static T GET<T>(string url, ShikiApi user = null)
        {
            using (var httpClient = new HttpClient())
            {
                if (user != null)
                {
                    httpClient.DefaultRequestHeaders.Add("X-User-Nickname", user.Nickname);
                    httpClient.DefaultRequestHeaders.Add("X-User-Api-Access-Token", user.AccessToken);
                }
                httpClient.DefaultRequestHeaders.Add("User-Agent", ShikiApiStatic.ClientName);

                var response = httpClient.GetStringAsync(url).Result;
                //json_setting для работы сериализации IDictionary в полях rates_scores_stats и rates_statuses_stats в классах AnimeFullInfo и MangaFullInfo
                var json_setting = new JsonSerializerSettings { Converters = new JsonConverter[] { new JsonGenericDictionaryOrArrayConverterNameValueMod() } };
                var result = JsonConvert.DeserializeObject<T>(response, json_setting);
                return result;
            }
        }

        internal static T POST<T>(string url, FormUrlEncodedContent args, ShikiApi user = null)
        {
            using (var httpClient = new HttpClient())
            {
                if (user != null)
                {
                    httpClient.DefaultRequestHeaders.Add("X-User-Nickname", user.Nickname);
                    httpClient.DefaultRequestHeaders.Add("X-User-Api-Access-Token", user.AccessToken);
                }
                httpClient.DefaultRequestHeaders.Add("User-Agent", ShikiApiStatic.ClientName);

                var response = httpClient.PostAsync(url, args).Result.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<T>(response);
                return result;
            }
        }

        internal static T PUT<T>(string url, FormUrlEncodedContent args, ShikiApi user = null)
        {
            using (var httpClient = new HttpClient())
            {
                if (user != null)
                {
                    httpClient.DefaultRequestHeaders.Add("X-User-Nickname", user.Nickname);
                    httpClient.DefaultRequestHeaders.Add("X-User-Api-Access-Token", user.AccessToken);
                }
                httpClient.DefaultRequestHeaders.Add("User-Agent", ShikiApiStatic.ClientName);

                var response = httpClient.PutAsync(url, args).Result.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<T>(response);
                return result;
            }
        }

        internal static HttpStatusCode DELETE(string url, ShikiApi user = null)
        {
            using (var httpClient = new HttpClient())
            {
                if (user != null)
                {
                    httpClient.DefaultRequestHeaders.Add("X-User-Nickname", user.Nickname);
                    httpClient.DefaultRequestHeaders.Add("X-User-Api-Access-Token", user.AccessToken);
                }
                httpClient.DefaultRequestHeaders.Add("User-Agent", ShikiApiStatic.ClientName);

                var response = httpClient.DeleteAsync(url);
                return response.Result.StatusCode;
            }
        }
    }
}