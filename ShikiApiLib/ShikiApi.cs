using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;

namespace ShikiApiLib
{
    /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApi/class[@name="ShikiApi"]/*' />
    public class ShikiApi
    {
        private static string DomenApi { get { return "https://shikimori.one/api/"; } }
        private const int rate_list_limit = 99999;
        private UserInfo _info;
        private List<AnimeRate> _anime_rates;
        private List<MangaRate> _manga_rates;

        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApi/property[@name="Nickname"]/*' />
        public string Nickname { get; private set; }
        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApi/property[@name="AccessToken"]/*' />
        public string AccessToken { get; private set; }
        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApi/property[@name="CurrentUserId"]/*' />
        public int CurrentUserId { get; private set; }
        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApi/property[@name="Info"]/*' />
        public UserInfo Info
        {
            get
            {
                if (_info != null)
                {
                    return _info;
                }
                else
                {
                    _info = GetUserInfo();
                    return _info;
                }
            }
        }
        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApi/property[@name="AnimeRates"]/*' />
        public List<AnimeRate> AnimeRates
        {
            get
            {
                if (_anime_rates != null)
                {
                    return _anime_rates;
                }
                else
                {
                    _anime_rates = GetAnimeRates();
                    return _anime_rates;
                }
            }
        }
        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApi/property[@name="MangaRates"]/*' />
        public List<MangaRate> MangaRates
        {
            get
            {
                if (_manga_rates != null)
                {
                    return _manga_rates;
                }
                else
                {
                    _manga_rates = GetMangaRates();
                    return _manga_rates;
                }
            }
        }
        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApi/constructor[@name="ShikiApi1"]/*' />
        public ShikiApi(string nickname, string password)
        {
            if (String.IsNullOrWhiteSpace(nickname) || String.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Введите логин/пароль!");
            }

            using (var httpClient = new HttpClient())
            {
                string url = DomenApi + "access_token?nickname=" + nickname + "&password=" + password;
                var response = httpClient.GetStringAsync(url).Result;
                var token = JsonConvert.DeserializeObject<_AccessTokenJson>(response);

                if (token.api_access_token == null)
                {
                    throw new Exception("Неверный логин/пароль!");
                }
                
                Nickname = nickname;
                AccessToken = token.api_access_token;
                CurrentUserId = Query.GET<_WhoAmI>(DomenApi + "users/whoami", this).id;
            }
        }

        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApi/constructor[@name="ShikiApi2"]/*' />
        public ShikiApi(string nickname, string access_token, int current_user_id)
        {
            Nickname = nickname;
            AccessToken = access_token;
            CurrentUserId = current_user_id;
        }

        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApi/method[@name="GetUserInfo"]/*' />
        public UserInfo GetUserInfo() //current user info
        {
            string url = DomenApi + "users/" + CurrentUserId;
            _info = new UserInfo(Query.GET<_UserFullInfo>(url, this));
            return _info;
        }

        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApi/method[@name="GetAnimeRates"]/*' />
        public List<AnimeRate> GetAnimeRates(int limit = rate_list_limit)
        {
            string url = DomenApi + "users/" + CurrentUserId + "/anime_rates?limit=" + limit;
            _anime_rates = Query.GET<List<_UserRate>>(url, this).Select(x => new AnimeRate(x)).ToList();
            return _anime_rates;
        }

        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApi/method[@name="GetMangaRates"]/*' />
        public List<MangaRate> GetMangaRates(int limit = rate_list_limit)
        {
            string url = DomenApi + "users/" + CurrentUserId + "/manga_rates?limit=" + limit;
            _manga_rates = Query.GET<List<_UserRate>>(url, this).Select(x => new MangaRate(x)).ToList();
            return _manga_rates;
        }

        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApi/method[@name="UpdateAnimeRate"]/*' />
        public AnimeRate UpdateAnimeRate(AnimeRate title, UserStatus status = (UserStatus)99, int score = -1, int episodes = -1)
        {
            if ((int)status == 99 && score == -1 && episodes == -1) { return title; }

            List<KeyValuePair<string, string>> keys = new List<KeyValuePair<string, string>>();

            //Необязательные
            if ((int)status != 99) { keys.Add(new KeyValuePair<string, string>("user_rate[status]",   status.ToString())); }
            if (score >= 0)        { keys.Add(new KeyValuePair<string, string>("user_rate[score]",    score.ToString())); }
            if (episodes >= 0)     { keys.Add(new KeyValuePair<string, string>("user_rate[episodes]", episodes.ToString())); }

            var args = new FormUrlEncodedContent(keys);

            string url = DomenApi + "v2/user_rates/" + title.UserRateId;
            var response = Query.PUT<_UserRate_v2>(url, args, this);
            return new AnimeRate(title, response);
        }

        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApi/method[@name="UpdateMangaRate"]/*' />
        public MangaRate UpdateMangaRate(MangaRate title, UserStatus status = (UserStatus)99, int score = -1, int volumes = -1, int chapters = -1)
        {
            if ((int)status == 99 && score == -1 && volumes == -1 && chapters == -1) { return title; }

            List<KeyValuePair<string, string>> keys = new List<KeyValuePair<string, string>>();

            //Необязательные
            if ((int)status != 99) { keys.Add(new KeyValuePair<string, string>("user_rate[status]",   status.ToString())); }
            if (score >= 0)        { keys.Add(new KeyValuePair<string, string>("user_rate[score]",    score.ToString())); }
            if (volumes >= 0)      { keys.Add(new KeyValuePair<string, string>("user_rate[volumes]",  volumes.ToString())); }
            if (chapters >= 0)     { keys.Add(new KeyValuePair<string, string>("user_rate[chapters]", chapters.ToString())); }

            var args = new FormUrlEncodedContent(keys);

            string url = DomenApi + "v2/user_rates/" + title.UserRateId;
            var response = Query.PUT<_UserRate_v2>(url, args, this);
            return new MangaRate(title, response);
        }

        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApi/method[@name="CreateAnimeRate"]/*' />
        public AnimeRate CreateAnimeRate(int title_id, UserStatus status = (UserStatus)99, int score = 0, int episodes = 0)
        {
            List<KeyValuePair<string, string>> keys = new List<KeyValuePair<string, string>>();

            //Обязательные 
            keys.AddRange(new[]
            {
                new KeyValuePair<string, string>("user_rate[user_id]",     CurrentUserId.ToString()),
                new KeyValuePair<string, string>("user_rate[target_id]",   title_id.ToString()),
                new KeyValuePair<string, string>("user_rate[target_type]", "Anime"),
            });

            //Необязательные
            if ((int)status != 99) { keys.Add(new KeyValuePair<string, string>("user_rate[status]",   status.ToString())); }
            if (score > 0)         { keys.Add(new KeyValuePair<string, string>("user_rate[score]",    score.ToString())); }
            if (episodes > 0)      { keys.Add(new KeyValuePair<string, string>("user_rate[episodes]", episodes.ToString())); }

            var args = new FormUrlEncodedContent(keys);

            string url = DomenApi + "v2/user_rates";
            var response = Query.POST<_UserRate_v2>(url, args, this);
            return new AnimeRate(response);
        }

        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApi/method[@name="CreateMangaRate"]/*' />
        public MangaRate CreateMangaRate(int title_id, UserStatus status = (UserStatus)99, int score = -1, int volumes = -1, int chapters = -1)
        {
            List<KeyValuePair<string, string>> keys = new List<KeyValuePair<string, string>>();

            //Обязательные 
            keys.AddRange(new[]
            {
                new KeyValuePair<string, string>("user_rate[user_id]",     CurrentUserId.ToString()),
                new KeyValuePair<string, string>("user_rate[target_id]",   title_id.ToString()),
                new KeyValuePair<string, string>("user_rate[target_type]", "Manga"),
            });

            //Необязательные
            if ((int)status != 99) { keys.Add(new KeyValuePair<string, string>("user_rate[status]",   status.ToString())); }
            if (score >= 0)        { keys.Add(new KeyValuePair<string, string>("user_rate[score]",    score.ToString())); }
            if (volumes >= 0)      { keys.Add(new KeyValuePair<string, string>("user_rate[volumes]",  volumes.ToString())); }
            if (chapters >= 0)     { keys.Add(new KeyValuePair<string, string>("user_rate[chapters]", chapters.ToString())); }

            var args = new FormUrlEncodedContent(keys);

            string url = DomenApi + "v2/user_rates";
            var response = Query.POST<_UserRate_v2>(url, args, this);
            return new MangaRate(response);
        }

        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApi/method[@name="DeleteRate"]/*' />
        public bool DeleteRate(int user_rate_id) //Work for Anime and Manga. True if Http Reques = 204
        {
            string url = DomenApi + "v2/user_rates/" + user_rate_id;
            return Query.DELETE(url, this) == System.Net.HttpStatusCode.NoContent;
        }

        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApi/method[@name="GetAnimeRate"]/*' />
        public AnimeRate GetAnimeRate(int user_rate_id)
        {
            string url = DomenApi + "v2/user_rates/" + user_rate_id;
            var response = Query.GET<_UserRate_v2>(url, this);
            return (response.target_type == "Anime") ? new AnimeRate(response) : null;
        }

        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApi/method[@name="GetMangaRate"]/*' />
        public MangaRate GetMangaRate(int user_rate_id)
        {
            string url = DomenApi + "v2/user_rates/" + user_rate_id;
            var response = Query.GET<_UserRate_v2>(url, this);
            return (response.target_type == "Manga") ? new MangaRate(response) : null;
        }

        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApi/method[@name="GetAnimeFullInfo"]/*' />
        public AnimeFullInfo GetAnimeFullInfo(int title_id)
        {
            string url = DomenApi + "animes/" + title_id;
            return Query.GET<AnimeFullInfo>(url, this);
        }

        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApi/method[@name="GetMangaFullInfo"]/*' />
        public MangaFullInfo GetMangaFullInfo(int title_id)
        {
            string url = DomenApi + "mangas/" + title_id;
            return Query.GET<MangaFullInfo>(url, this);
        }
    }
}