using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShikiApiLib
{
    //Вспомогательные классы перечисления

    /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="TitleType"]/*' />
    public enum TitleType
    {
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="TitleType.anime"]/*' />
        anime,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="TitleType.manga"]/*' />
        manga
    }
    /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="UserStatus"]/*' />
    public enum UserStatus
    {
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="UserStatus.planned"]/*' />
        planned,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="UserStatus.watching"]/*' />
        watching,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="UserStatus.completed"]/*' />
        completed,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="UserStatus.on_hold"]/*' />
        on_hold,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="UserStatus.dropped"]/*' />
        dropped,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="UserStatus.rewatching"]/*' />
        rewatching = 9
    }

    /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="Order"]/*' />
    public enum Order
    {
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="Order.ranked"]/*' />
        ranked,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="Order.popularity"]/*' />
        popularity,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="Order.name"]/*' />
        name,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="Order.aired_on"]/*' />
        aired_on,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="Order.id"]/*' />
        id,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="Order.random"]/*' />
        random,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="Order.status"]/*' />
        status,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="Order.type"]/*' />
        type,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="Order.episodes"]/*' />
        episodes,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="Order.volumes"]/*' />
        volumes,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="Order.chapters"]/*' />
        chapters
    }

    /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="Rating"]/*' />
    public enum Rating
    {
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="Rating.g"]/*' />
        g,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="Rating.pg"]/*' />
        pg,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="Rating.pg_13"]/*' />
        pg_13,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="Rating.r"]/*' />
        r,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="Rating.r_plus"]/*' />
        r_plus,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="Rating.rx"]/*' />
        rx
    }

    /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="TitleStatus"]/*' />
    public enum TitleStatus
    {
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="TitleStatus.released"]/*' />
        released,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="TitleStatus.latest"]/*' />
        latest,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="TitleStatus.ongoing"]/*' />
        ongoing,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="TitleStatus.anons"]/*' />
        anons
    }

    /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="AKind"]/*' />
    public enum AKind
    {
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="AKind.tv"]/*' />
        tv,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="AKind.tv_13"]/*' />
        tv_13,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="AKind.tv_24"]/*' />
        tv_24,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="AKind.tv_48"]/*' />
        tv_48,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="AKind.movie"]/*' />
        movie,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="AKind.ova"]/*' />
        ova,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="AKind.ona"]/*' />
        ona,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="AKind.special"]/*' />
        special,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="AKind.music"]/*' />
        music
    }

    /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="MKind"]/*' />
    public enum MKind
    {
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="MKind.manga"]/*' />
        manga,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="MKind.manhwa"]/*' />
        manhwa,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="MKind.manhua"]/*' />
        manhua,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="MKind.novel"]/*' />
        novel,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="MKind.one_shot"]/*' />
        one_shot,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="MKind.doujin"]/*' />
        doujin
    }

    /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="Duration"]/*' />
    public enum Duration
    {
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="Duration.S"]/*' />
        S,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="Duration.D"]/*' />
        D,
        /// <include file='Docs/ExternalSummary.xml' path='docs/Enums/enum[@name="Duration.F"]/*' />
        F
    }



    public static class UserRateStatus
    {
        public static string Planned   { get { return "planned"; } }
        public static string Watching  { get { return "watching"; } }
        public static string Completed { get { return "completed"; } }
        public static string OnHold    { get { return "on_hold"; } }
        public static string Dropped   { get { return "dropped"; } }
    }

    public static class AnimeKind
    {
        public static string tv         { get { return "tv"; } }
        public static string tv_short   { get { return "tv_13"; } }
        public static string tv_average { get { return "tv_24"; } }
        public static string tv_long    { get { return "tv_48"; } }
        public static string movie      { get { return "movie"; } }
        public static string ova        { get { return "ova"; } }
        public static string ona        { get { return "ona"; } }
        public static string special    { get { return "special"; } }
        public static string music      { get { return "music"; } }
    }

    public static class MangaKind
    {
        public static string manga    { get { return "manga"; } }
        public static string manhwa   { get { return "manhwa"; } }
        public static string manhua   { get { return "manhua"; } }
        public static string novel    { get { return "novel"; } }
        public static string one_shot { get { return "one_shot"; } }
        public static string doujin   { get { return "doujin"; } }
    }
}
