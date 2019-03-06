using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShikiApiLib
{
    /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApiStatic/class[@name="ShikiApiStatic"]/*' />
    public static class ShikiApiStatic
    {
        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApiStatic/property[@name="Domen"]/*' />
        public static string Domen { get { return "https://shikimori.org/"; } }
        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApiStatic/property[@name="DomenApi"]/*' />
        public static string DomenApi { get { return "https://shikimori.org/api/"; } }
        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApiStatic/property[@name="ClientName"]/*' />
        public static string ClientName { get; set; } = "Shiki Desktop Anime Matching App";

        private const int rate_list_limit = 99999;

        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApiStatic/method[@name="GetUserInfo"]/*' />
        public static UserInfo GetUserInfo(int user_id)
        {
            string url = DomenApi + "users/" + user_id;
            return new UserInfo(Query.GET<_UserFullInfo>(url));
        }

        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApiStatic/method[@name="GetAnimeRates"]/*' />
        public static List<AnimeRate> GetAnimeRates(int user_id, int limit = rate_list_limit)
        {
            string url = DomenApi + "users/" + user_id + "/anime_rates?limit=" + limit;
            return Query.GET<List<_UserRate>>(url).Select(x => new AnimeRate(x)).ToList();
        }

        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApiStatic/method[@name="GetMangaRates"]/*' />
        public static List<MangaRate> GetMangaRates(int user_id, int limit = rate_list_limit)
        {
            string url = DomenApi + "users/" + user_id + "/manga_rates?limit=" + limit;
            return Query.GET<List<_UserRate>>(url).Select(x => new MangaRate(x)).ToList();
        }

        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApiStatic/method[@name="GetTitleType1"]/*' />
        public static TitleType GetTitleType(_UserRate rate)
        {
            if (rate.anime != null) { return TitleType.anime; }
            if (rate.manga != null) { return TitleType.manga; }
            return (TitleType)(-1);
        }

        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApiStatic/method[@name="GetTitleType2"]/*' />
        public static TitleType GetTitleType(_UserRate_v2 rate)
        {
            if (rate.target_type == "Anime") { return TitleType.anime; }
            if (rate.target_type == "Manga") { return TitleType.manga; }
            return (TitleType)(-1);
        }

        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApiStatic/method[@name="GetAnimeFullInfo"]/*' />
        public static AnimeFullInfo GetAnimeFullInfo(int title_id)
        {
            string url = DomenApi + "animes/" + title_id;
            return Query.GET<AnimeFullInfo>(url);
        }

        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApiStatic/method[@name="GetMangaFullInfo"]/*' />
        public static MangaFullInfo GetMangaFullInfo(int title_id)
        {
            string url = DomenApi + "mangas/" + title_id;
            return Query.GET<MangaFullInfo>(url);
        }

        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApiStatic/method[@name="GetStudios"]/*' />
        public static List<Studio> GetStudios()
        {
            string url = DomenApi + "studios";
            return Query.GET<List<Studio>>(url);
        }

        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApiStatic/method[@name="GetStudio"]/*' />
        public static Studio GetStudio(int id)
        {
            return GetStudios().FirstOrDefault(x => x.id == id);
        }

        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApiStatic/method[@name="GetPublishers"]/*' />
        public static List<Publisher> GetPublishers()
        {
            string url = DomenApi + "publishers";
            return Query.GET<List<Publisher>>(url);
        }

        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApiStatic/method[@name="GetPublisher"]/*' />
        public static Publisher GetPublisher(int id)
        {
            return GetPublishers().FirstOrDefault(x => x.id == id);
        }

        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApiStatic/method[@name="GetGenres"]/*' />
        public static List<Genre> GetGenres()
        {
            string url = DomenApi + "genres";
            return Query.GET<List<Genre>>(url);
        }

        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApiStatic/method[@name="GetGenre"]/*' />
        public static Genre GetGenre(int id)
        {
            return GetGenres().FirstOrDefault(x => x.id == id);
        }

        /// <include file='Docs/ExternalSummary.xml' path='docs/ShikiApiStatic/method[@name="GetVideos"]/*' />
        public static List<Video> GetVideos(int title_id) //Video of animes (OP or ED)
        {
            string url = DomenApi + "animes/" + title_id + "/videos";
            return Query.GET<List<Video>>(url);
        }
    }
}