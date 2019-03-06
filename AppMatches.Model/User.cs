using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShikiApiLib;
using static ShikiApiLib.ShikiApiStatic;

namespace AppMatches.Model
{
	public class User
	{
		public static List<User> Users = new List<User>()
		{
			new User(227956),
			new User(345242),
			new User(204018),
			new User(504304),
			new User(404534)
		};

		public static List<Match> GetMatches(List<User> users)
		{
			return Match.GetMatch(users);
		}

		private readonly int userID;
		public UserInfo Info { get; private set; }

		public List<AnimeRate> Animes = new List<AnimeRate>();
		public List<AnimeRate> Planned => Animes.Where(x => x.UserStatus == "planned").ToList();
		public List<AnimeRate> Watching => Animes.Where(x => x.UserStatus == "watching").ToList();
		public List<AnimeRate> Rewatching => Animes.Where(x => x.UserStatus == "rewatching").ToList();
		public List<AnimeRate> Completed => Animes.Where(x => x.UserStatus == "completed").ToList();
		public List<AnimeRate> OnHold => Animes.Where(x => x.UserStatus == "on_hold").ToList();
		public List<AnimeRate> Dropped => Animes.Where(x => x.UserStatus == "dropped").ToList();

		public User(int userID)
		{
			this.userID = userID;
			GetInfo();
		}

		public void GetInfo()
		{
			Info = GetUserInfo(userID);
			Animes = GetAnimeRates(userID);
		}
	}
}
