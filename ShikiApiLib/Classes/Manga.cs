using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShikiApiLib
{
    public class MangaShortInfo : TitleShortInfo
    {
        [JsonProperty(PropertyName = "volumes")]
        public int TotalVolumes { get; set; }
        [JsonProperty(PropertyName = "chapters")]
        public int TotalChapters { get; set; }
    }

    public class MangaRate : TitleRate
    {
        public int TotalVolumes { get; set; }
        public int TotalChapters { get; set; }
        public int CompletedVolumes { get; set; }
        public int CompletedChapters { get; set; }

        #region Constructors

        public MangaRate() : base() { }

        public MangaRate(_UserRate rate)
        {
            UserRateId    = rate.id;
            UserStatus    = rate.status;
            Score         = rate.score;
            TitleId       = rate.manga.TitleId;
            Name          = rate.manga.Name;
            RusName       = rate.manga.RusName;
            Poster        = rate.manga.Poster;
            Url           = rate.manga.Url;
            Kind          = rate.manga.Kind;
            TitleStatus   = rate.manga.TitleStatus;
            AiredOn       = rate.manga.AiredOn;
            ReleasedOn    = rate.manga.ReleasedOn;

            TotalVolumes      = rate.manga.TotalVolumes;
            TotalChapters     = rate.manga.TotalChapters;
            CompletedVolumes  = (rate.volumes  != null) ? (int)rate.volumes  : 0;
            CompletedChapters = (rate.chapters != null) ? (int)rate.chapters : 0;
        }

        public MangaRate(MangaRate rate, _UserRate_v2 rate_upd) : base(rate, rate_upd)
        {
            TotalVolumes      = rate.TotalVolumes;
            TotalChapters     = rate.TotalChapters;
            CompletedVolumes  = rate_upd.volumes;
            CompletedChapters = rate_upd.chapters;
        }

        public MangaRate(_UserRate_v2 rate_upd)
        {
            CompletedVolumes  = rate_upd.volumes;
            CompletedChapters = rate_upd.chapters;
            UserStatus        = rate_upd.status;
            Score             = rate_upd.score;
            UserRateId        = rate_upd.id;
            TitleId           = rate_upd.target_id;

            var title = ShikiApiStatic.GetMangaFullInfo(TitleId);
            Name          = title.Name;
            RusName       = title.RusName;
            Poster        = title.Poster;
            Url           = title.Url;
            Kind          = title.Kind;
            TitleStatus   = title.TitleStatus;
            AiredOn       = title.AiredOn;
            ReleasedOn    = title.ReleasedOn;
            TotalVolumes  = title.TotalVolumes;
            TotalChapters = title.TotalChapters;
        }

        #endregion

        #region Static Methods

        public static MangaRate GetMangaRateByUserRateId(List<MangaRate> titles, int user_rate_id)
        {
            return titles.FirstOrDefault(x => x.UserRateId == user_rate_id);
        }

        public static MangaRate GetMangaRateByTitleId(List<MangaRate> titles, int title_id)
        {
            return titles.FirstOrDefault(x => x.TitleId == title_id);
        }

        public static MangaRate GetMangaRateByTitleId(int title_id)
        {
            try
            {
                return new MangaRate(ShikiApiStatic.GetMangaFullInfo(title_id).UserRate);
            }
            catch (Exception)
            {

                return null; // not found title
            }
        }

        #endregion
    }

    public class MangaFullInfo : TitleFullInfo
    {
        [JsonProperty(PropertyName = "volumes")]
        public int TotalVolumes { get; set; }

        [JsonProperty(PropertyName = "chapters")]
        public int TotalChapters { get; set; }

        [JsonProperty(PropertyName = "read_manga_id")]
        public string ReadMangaId { get; set; }

        [JsonProperty(PropertyName = "publishers")]
        public List<Publisher> Publishers { get; set; }
    }

    public class MangaSearch : TitleSearch
    {
        public IDictionary<MKind, bool> Kind { get; set; }
        public IDictionary<int, bool> Publisher { get; set; }

        public MangaSearch() : base()
        {
            Kind = new Dictionary<MKind, bool>();
            Publisher = new Dictionary<int, bool>();
        }

        public List<MangaShortInfo> GetSearch(ShikiApi user = null)
        {
            var url = ShikiApiStatic.DomenApi + "mangas?";

            url += "order=" + Order + "&";
            if (SearchText != "") { url += "search=" + SearchText + "&"; }
            if (Limit > 1) { url += "limit=" + Limit + "&"; } // тут всё верно. в запросе по умолчанию limit=1, я же хочу, чтобы (если не указано) выдавало страницу полностью (обычно до 50 строк)
            if (Censored) { url += "censored=" + "true" + "&"; }
            if (Page > 1) { url += "page=" + Page + "&"; }
            if (TitleScore > 0) { url += "score=" + TitleScore + "&"; }
            if (MyList.Count > 0) { url += "mylist=" + DictToStr(MyList) + "&"; }
            if (Kind.Count > 0) { url += "type=" + DictToStr(Kind) + "&"; }
            if (Rating.Count > 0) { url += "rating=" + DictToStr(Rating) + "&"; }
            if (TitleStatus.Count > 0) { url += "status=" + DictToStr(TitleStatus) + "&"; }
            if (Season.Count > 0) { url += "season=" + DictToStr(Season) + "&"; }
            if (Genre.Count > 0) { url += "genre=" + DictToStr(Genre) + "&"; }
            if (Publisher.Count > 0) { url += "studio=" + DictToStr(Publisher) + "&"; }

            return Query.GET<List<MangaShortInfo>>(url, user);
        }

        #region For Debug

        private string GetSearchString() //для отладки (!) для использования сменить модификатор с private на public и пересобрать решение.
        {
            var url = ShikiApiStatic.DomenApi + "mangas?";

            url += "order=" + Order + "&";
            if (SearchText != "") { url += "search=" + SearchText + "&"; }
            if (Limit > 1) { url += "limit=" + Limit + "&"; } // тут всё верно. в запросе по умолчанию limit=1, я же хочу, чтобы (если не указано) выдавало страницу полностью (обычно до 50 строк)
            if (Censored) { url += "censored=" + "true" + "&"; }
            if (Page > 1) { url += "page=" + Page + "&"; }
            if (TitleScore > 0) { url += "score=" + TitleScore + "&"; }
            if (MyList.Count > 0) { url += "mylist=" + DictToStr(MyList) + "&"; }
            if (Kind.Count > 0) { url += "type=" + DictToStr(Kind) + "&"; }
            if (Rating.Count > 0) { url += "rating=" + DictToStr(Rating) + "&"; }
            if (Season.Count > 0) { url += "season=" + DictToStr(Season) + "&"; }
            if (Genre.Count > 0) { url += "genre=" + DictToStr(Genre) + "&"; }
            if (Publisher.Count > 0) { url += "studio=" + DictToStr(Publisher) + "&"; }

            return url.Remove(url.Length - 1);
        }

        #endregion
    }
}
