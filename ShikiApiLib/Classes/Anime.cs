using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShikiApiLib
{
    public class AnimeShortInfo : TitleShortInfo
    {
        [JsonProperty(PropertyName = "episodes")]
        public int TotalEpisodes { get; set; } //Total count (For ongoin = 0)
        [JsonProperty(PropertyName = "episodes_aired")]
        public int AiredEpisodes { get; set; } //For ongoing
    }

    public class AnimeRate : TitleRate
    {
        public int TotalEpisodes { get; set; }
        public int AiredEpisodes { get; set; }
        public int CompletedEpisodes { get; set; } // user completed episodes

        #region Constructors

        public AnimeRate() : base() { }

        public AnimeRate(_UserRate rate)
        {
            UserRateId  = rate.id;
            UserStatus  = rate.status;
            Score       = rate.score;
            TitleId     = rate.anime.TitleId;
            Name        = rate.anime.Name;
            RusName     = rate.anime.RusName;
            Poster      = rate.anime.Poster;
            Url         = rate.anime.Url;
            Kind        = rate.anime.Kind;
            TitleStatus = rate.anime.TitleStatus;
            AiredOn     = rate.anime.AiredOn;
            ReleasedOn  = rate.anime.ReleasedOn;

            TotalEpisodes     = rate.anime.TotalEpisodes;
            AiredEpisodes     = rate.anime.AiredEpisodes;
            CompletedEpisodes = (rate.episodes != null) ? (int)rate.episodes : 0;
        }

        public AnimeRate(AnimeRate rate, _UserRate_v2 rate_upd) : base(rate, rate_upd)
        {
            TotalEpisodes     = rate.TotalEpisodes;
            AiredEpisodes     = rate.AiredEpisodes;
            CompletedEpisodes = rate_upd.episodes;
        }

        public AnimeRate(_UserRate_v2 rate_upd)
        {
            CompletedEpisodes = rate_upd.episodes;
            UserStatus        = rate_upd.status;
            Score             = rate_upd.score;
            UserRateId        = rate_upd.id;
            TitleId           = rate_upd.target_id;

            var title = ShikiApiStatic.GetAnimeFullInfo(TitleId);
            Name          = title.Name;
            RusName       = title.RusName;
            Poster        = title.Poster;
            Url           = title.Url;
            Kind          = title.Kind;
            TitleStatus   = title.TitleStatus;
            AiredOn       = title.AiredOn;
            ReleasedOn    = title.ReleasedOn;
            TotalEpisodes = title.TotalEpisodes;
            AiredEpisodes = title.AiredEpisodes;
        }

        #endregion

        #region Static Methods

        public static AnimeRate GetRateByUserRateId(List<AnimeRate> titles, int user_rate_id)
        {
            return titles.FirstOrDefault(x => x.UserRateId == user_rate_id);
        }

        public static AnimeRate GetRateByTitleId(List<AnimeRate> titles, int title_id)
        {
            return titles.FirstOrDefault(x => x.TitleId == title_id);
        }

        public static AnimeRate GetRateByTitleId(int title_id)
        {
            try
            {
                return new AnimeRate(ShikiApiStatic.GetAnimeFullInfo(title_id).UserRate);
            }
            catch (Exception)
            {
                return null; // not found title
            }
        }

        #endregion
    }

    public class AnimeFullInfo : TitleFullInfo
    {
        [JsonProperty(PropertyName = "rating")]
        public string Rating { get; set; } // age rate - "rg-13" and etc.

        [JsonProperty(PropertyName = "episodes")]
        public int TotalEpisodes { get; set; } //total episodes

        [JsonProperty(PropertyName = "episodes_aired")]
        public int AiredEpisodes { get; set; }

        [JsonProperty(PropertyName = "duration")]
        public int Duration { get; set; } // duration of one episodes in minutes

        [JsonProperty(PropertyName = "world_art_id")]
        public int WorldArtId { get; set; }

        [JsonProperty(PropertyName = "ani_db_id")]
        public int AniDBId { get; set; }

        [JsonProperty(PropertyName = "studios")]
        public List<Studio> Studios { get; set; }

        [JsonProperty(PropertyName = "videos")]
        public List<Video> Videos { get; set; } // OP/ED links
    }

    public class AnimeSearch : TitleSearch
    {
        public IDictionary<AKind, bool> Kind { get; set; }
        public IDictionary<Duration, bool> Duration { get; set; }
        public IDictionary<int, bool> Studio { get; set; }

        public AnimeSearch() : base()
        {
            Kind = new Dictionary<AKind, bool>();
            Duration = new Dictionary<Duration, bool>();
            Studio = new Dictionary<int, bool>();
        }

        public List<AnimeShortInfo> GetSearch(ShikiApi user = null)
        {
            var url = ShikiApiStatic.DomenApi + "animes?";

            url += "order=" + Order + "&";
            if (SearchText != "") { url += "search=" + SearchText + "&"; }
            if (Limit > 1) { url += "limit=" + Limit + "&"; } // тут всё верно. в запросе по умолчанию limit=1, я же хочу, чтобы (если не указано) выдавало страницу полностью (обычно до 50 строк)
            if (Censored) { url += "censored=" + "true" + "&"; }
            if (Page > 1) { url += "page=" + Page + "&"; }
            if (TitleScore > 0) { url += "score=" + TitleScore + "&"; }
            if (MyList.Count > 0) { url += "mylist=" + DictToStr(MyList) + "&"; }
            if (Kind.Count > 0) { url += "type=" + DictToStr(Kind) + "&"; }
            if (Duration.Count > 0) { url += "duration=" + DictToStr(Duration) + "&"; }
            if (Rating.Count > 0) { url += "rating=" + DictToStr(Rating) + "&"; }
            if (TitleStatus.Count > 0) { url += "status=" + DictToStr(TitleStatus) + "&"; }
            if (Season.Count > 0) { url += "season=" + DictToStr(Season) + "&"; }
            if (Genre.Count > 0) { url += "genre=" + DictToStr(Genre) + "&"; }
            if (Studio.Count > 0) { url += "studio=" + DictToStr(Studio) + "&"; }

            return Query.GET<List<AnimeShortInfo>>(url, user);
        }

        #region For Debug

        private string GetSearchString() //для отладки (!) для использования сменить модификатор с private на public и пересобрать решение.
        {
            var url = ShikiApiStatic.DomenApi + "animes?";

            url += "order=" + Order + "&";
            if (SearchText != "") { url += "search=" + SearchText + "&"; }
            if (Limit > 1) { url += "limit=" + Limit + "&"; } // тут всё верно. в запросе по умолчанию limit=1, я же хочу, чтобы (если не указано) выдавало страницу полностью (обычно до 50 строк)
            if (Censored) { url += "censored=" + "true" + "&"; }
            if (Page > 1) { url += "page=" + Page + "&"; }
            if (TitleScore > 0) { url += "score=" + TitleScore + "&"; }
            if (MyList.Count > 0) { url += "mylist=" + DictToStr(MyList) + "&"; }
            if (Kind.Count > 0) { url += "type=" + DictToStr(Kind) + "&"; }
            if (Duration.Count > 0) { url += "duration=" + DictToStr(Duration) + "&"; }
            if (Rating.Count > 0) { url += "rating=" + DictToStr(Rating) + "&"; }
            if (Season.Count > 0) { url += "season=" + DictToStr(Season) + "&"; }
            if (Genre.Count > 0) { url += "genre=" + DictToStr(Genre) + "&"; }
            if (Studio.Count > 0) { url += "studio=" + DictToStr(Studio) + "&"; }

            return url.Remove(url.Length - 1);
        }

        #endregion
    }
}
