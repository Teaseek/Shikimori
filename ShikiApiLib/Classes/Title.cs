using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShikiApiLib
{
    public class TitleImage
    {
        private string _original;
        private string _preview;
        private string _x96;
        private string _x48;

        public string original { get { return ShikiApiStatic.Domen + _original; } set { _original = value.Split('?')[0]; } }
        public string preview { get { return ShikiApiStatic.Domen + _preview; } set { _preview = value.Split('?')[0]; } }
        public string x96 { get { return ShikiApiStatic.Domen + _x96; } set { _x96 = value.Split('?')[0]; } }
        public string x48 { get { return ShikiApiStatic.Domen + _x48; } set { _x48 = value.Split('?')[0]; } }

        public TitleImage() { }

        public TitleImage(int title_id, TitleType titleType) //titleType = "anime" | "manga"
        {
            original = "/system/" + titleType + "s/original/" + title_id + ".jpg";
            preview  = "/system/" + titleType + "s/preview/"  + title_id + ".jpg";
            x96      = "/system/" + titleType + "s/x96/"      + title_id + ".jpg";
            x48      = "/system/" + titleType + "s/x48/"      + title_id + ".jpg";
        }
    }

    public abstract class TitleShortInfo
    {
        private string _url;

        [JsonProperty(PropertyName = "id")]
        public int TitleId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "russian")]
        public string RusName { get; set; }

        [JsonProperty(PropertyName = "image")]
        public TitleImage Poster { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get { return ((!_url.Contains(ShikiApiStatic.Domen)) ? ShikiApiStatic.Domen : "") + _url; } set { _url = value; } }

        [JsonProperty(PropertyName = "kind")]
        public string Kind { get; set; } // tv/movie/ova/ona/special/..

        [JsonProperty(PropertyName = "status")]
        public string TitleStatus { get; set; } // released/ongoing/..

        [JsonProperty(PropertyName = "aired_on")]
        public string AiredOn { get; set; } //aired date

        [JsonProperty(PropertyName = "released_on")]
        public string ReleasedOn { get; set; } //released date
    }

    public abstract class TitleRate : TitleShortInfo
    {
        public int    UserRateId { get; set; }
        public string UserStatus { get; set; } // ptw/watching and etc.
        public int    Score { get; set; }

        public TitleRate() { }

        public TitleRate(TitleRate rate, _UserRate_v2 rate_upd)
        {
            UserRateId    = rate.UserRateId;
            TitleId       = rate.TitleId;
            Name          = rate.Name;
            RusName       = rate.RusName;
            Poster        = rate.Poster;
            Url           = rate.Url;
            Kind          = rate.Kind;
            TitleStatus   = rate.TitleStatus;
            AiredOn       = rate.AiredOn;
            ReleasedOn    = rate.ReleasedOn;
            UserStatus    = rate_upd.status;
            Score         = rate_upd.score;
        }
    }

    /*
    public class RatesScoresStat
    {
        public int name { get; set; }
        public int value { get; set; }
    }

    public class RatesStatusesStat
    {
        public string name { get; set; }
        public int value { get; set; }
    }
    */

    public abstract class TitleFullInfo : TitleShortInfo
    {
        [JsonProperty(PropertyName = "english")]
        public List<string> EnNames { get; set; } //other name

        [JsonProperty(PropertyName = "japanese")]
        public List<string> JpNames { get; set; } //other name

        [JsonProperty(PropertyName = "synonyms")]
        public List<string> Synonyms { get; set; } //other name

        [JsonProperty(PropertyName = "score")]
        public double TitleScore { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "description_html")]
        public string DescriptionHtml { get; set; }

        [JsonProperty(PropertyName = "favoured")]
        public bool Favoured { get; set; }

        [JsonProperty(PropertyName = "anons")]
        public bool Anons { get; set; }

        [JsonProperty(PropertyName = "ongoing")]
        public bool Ongoing { get; set; }

        [JsonProperty(PropertyName = "topic_id")]
        public int? TopicId { get; set; }

        [JsonProperty(PropertyName = "myanimelist_id")]
        public int MyAnimeListId { get; set; }

        //public List<RatesScoresStat> rates_scores_stats { get; set; }
        //public List<RatesStatusesStat> rates_statuses_stats { get; set; }

        [JsonProperty(PropertyName = "rates_scores_stats")]
        public IDictionary<int, int> RatesScoresStats { get; set; }

        [JsonProperty(PropertyName = "rates_statuses_stats")]
        public IDictionary<string, int> RatesStatusesStats { get; set; }

        [JsonProperty(PropertyName = "genres")]
        public List<Genre> Genres { get; set; }

        [JsonProperty(PropertyName = "screenshots")]
        public List<Screenshot> Screenshots { get; set; }

        [JsonProperty(PropertyName = "user_rate")]
        public _UserRate UserRate { get; set; }

        public int GetScoresStats(int score)
        {
            //return rates_scores_stats.FirstOrDefault(x => x.name == score).value;
            return RatesScoresStats.ContainsKey(score) ? RatesScoresStats[score] : 0;
        }

        public int GetStatusStats(string status)
        {
            //return rates_statuses_stats.FirstOrDefault(x => x.name == status).value;
            return RatesStatusesStats.ContainsKey(status) ? RatesStatusesStats[status] : 0;
        }
    }

    public class TitleSearch
    {
        public string SearchText { get; set; }
        public int Limit { get; set; }
        public bool Censored { get; set; }
        public int Page { get; set; }
        public int TitleScore { get; set; }
        public IDictionary<UserStatus, bool> MyList { get; set; }
        public Order Order { get; set; }
        public IDictionary<Rating, bool> Rating { get; set; }
        public IDictionary<TitleStatus, bool> TitleStatus { get; set; }
        public IDictionary<string, bool> Season { get; set; }
        public IDictionary<int, bool> Genre { get; set; }

        public TitleSearch()
        {
            SearchText = "";
            Limit = 999999;
            Censored = false;
            Page = 1;
            TitleScore = 0;
            MyList = new Dictionary<UserStatus, bool>();
            Order = Order.ranked;
            Rating = new Dictionary<Rating, bool>();
            TitleStatus = new Dictionary<TitleStatus, bool>();
            Season = new Dictionary<string, bool>();
            Genre = new Dictionary<int, bool>();
        }

        protected static string DictToStr<T>(IDictionary<T, bool> filter)
        {
            var str = "";
            foreach (var item in filter)
            {
                str += (item.Value) ? "" : "!";

                if (typeof(T) == typeof(UserStatus))
                {
                    str += Convert.ToInt32(item.Key) + ",";
                }
                else
                {
                    str += item.Key + ",";
                }
            }
            return str.Remove(str.Length - 1);
        }
    }
}