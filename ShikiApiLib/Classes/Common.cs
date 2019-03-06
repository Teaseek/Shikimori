using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShikiApiLib
{
    public class _AccessTokenJson
    {
        public string api_access_token { get; set; }
    }

    public class _WhoAmI
    {
        public int id { get; set; }
        public string nickname { get; set; }
        public _ImageUser image { get; set; }
        public string last_online_at { get; set; }
        public string name { get; set; }
        public string sex { get; set; }
        public string website { get; set; }
        public string birth_on { get; set; }
    }

    public class _ImageUser
    {
        private string _x160, _x148, _x80, _x64, _x48, _x32, _x16;

        public string x160 { get { return _x160; } set { _x160 = value.Split('?')[0]; } }
        public string x148 { get { return _x148; } set { _x148 = value.Split('?')[0]; } }
        public string x80 { get { return _x80; } set { _x80 = value.Split('?')[0]; } }
        public string x64 { get { return _x64; } set { _x64 = value.Split('?')[0]; } }
        public string x48 { get { return _x48; } set { _x48 = value.Split('?')[0]; } }
        public string x32 { get { return _x32; } set { _x32 = value.Split('?')[0]; } }
        public string x16 { get { return _x16; } set { _x16 = value.Split('?')[0]; } }
    }
    //BEGIN--UserStats-- Count title in progress list (planned-0, watching-1, completed-2, on_hold-3, dropped-4)
    public class _UserFullInfo
    {
        public int id { get; set; }
        public string nickname { get; set; }
        public _ImageUser image { get; set; }
        public string last_online_at { get; set; }
        public string name { get; set; }
        public string sex { get; set; }
        public __Stats stats { get; set; }
    }

    public class __Stats
    {
        public __Statuses full_statuses { get; set; }
    }

    public class __Statuses
    {
        public List<__UserStats> anime { get; set; }
        public List<__UserStats> manga { get; set; }
    }

    public class __UserStats
    {
        public int id { get; set; } // 0/1/2/3/4/9
        public string name { get; set; }  // planned/watching/completed/on_hold/dropped/rewatching
        public int size { get; set; } //Title count
        public string type { get; set; } // Anime/Manga
    }
    //END--UserStats--

    //BEGIN--UserRate-- Object content info about progress watching and score of title
    public class _UserRate
    {
        public int id { get; set; } //user_rate id
        public int score { get; set; }
        public string status { get; set; } // planned/watching/completed/on_hold/dropped
        public int? episodes { get; set; } //Progress count
        public int? chapters { get; set; }
        public int? volumes { get; set; }
        public AnimeShortInfo anime { get; set; }
        public MangaShortInfo manga { get; set; }
    }
    //END--UserRate--

    public class _UserRate_v2
    {
        public int id { get; set; } // user_rate id
        public int user_id { get; set; }
        public int target_id { get; set; } // title id
        public string target_type { get; set; } //"Anime"/"Manga"
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public int episodes { get; set; }
        public int chapters { get; set; }
        public int volumes { get; set; }
        public int score { get; set; }
        public string status { get; set; } //planned/watching/completed/on_hold/dropped
    }

    public class Genre
    {
        public int id { get; set; }
        public string name { get; set; }
        public string russian { get; set; }
        public string kind { get; set; } //anime or manga
    }

    public class Studio
    {
        private string _image;

        public int id { get; set; }
        public string name { get; set; }
        public string filtered_name { get; set; }
        public bool real { get; set; }
        public string image { get { return (_image != null) ? ShikiApiStatic.Domen + _image : null; } set { _image = (value != null) ? value.Split('?')[0] : null ; } }
    }

    public class Video
    {
        public int id { get; set; }
        public string url { get; set; }
        public string image_url { get; set; }
        public string player_url { get; set; }
        public string name { get; set; }
        public string kind { get; set; } //OP or ED
        public string hosting { get; set; }
    }

    public class Screenshot
    {
        private string _original;
        private string _preview;

        public string original { get { return _original; } set { _original = value.Split('?')[0]; } }
        public string preview { get { return _preview; } set { _preview = value.Split('?')[0]; } }
    }

    public class Publisher
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public struct Credentials // autorization data
    {
        public int id;
        public string nickname;
        public string access_token;
    }

    public class UserInfo
    {
        public int id { get; set; }
        public string nickname { get; set; }
        public string name { get; set; }
        public string sex { get; set; }
        public _ImageUser avatar { get; set; }
        public __Statuses progress { get; set; }

        public UserInfo(_UserFullInfo obj)
        {
            id = obj.id;
            nickname = obj.nickname;
            name = obj.name;
            sex = obj.sex;
            avatar = obj.image;
            progress = new __Statuses { anime = new List<__UserStats>(), manga = new List<__UserStats>() }; ;
            progress.anime.AddRange(obj.stats.full_statuses.anime);
            progress.manga.AddRange(obj.stats.full_statuses.manga);
        }

        public int CountAnimeInAllList()
        {
            return progress.anime.Sum(x => x.size);
        }

        public int CountMangaInAllList()
        {
            return progress.manga.Sum(x => x.size);
        }
    }
}