﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShikiApiLib;
using static ShikiApiLib.ShikiApiStatic;

namespace AppMatches
{
	public class User
	{
		private readonly int userID;
		public UserInfo Info { get; private set; }

		public List<AnimeRate> Anime = new List<AnimeRate>();
		public List<AnimeRate> PlannedAnime => Anime.Where(x => x.UserStatus == "planned").ToList();
		public List<AnimeRate> WatchingAnime => Anime.Where(x => x.UserStatus == "watching").ToList();
		public List<AnimeRate> RewatchingAnime => Anime.Where(x => x.UserStatus == "rewatching").ToList();
		public List<AnimeRate> CompletedAnime => Anime.Where(x => x.UserStatus == "completed").ToList();
		public List<AnimeRate> OnHoldAnime => Anime.Where(x => x.UserStatus == "on_hold").ToList();
		public List<AnimeRate> DroppedAnime => Anime.Where(x => x.UserStatus == "dropped").ToList();

		public User(int userID)
		{
			this.userID = userID;
			GetInfo();
		}

		public void GetInfo()
		{
			Info = GetUserInfo(userID);
			Anime = GetAnimeRates(userID);
		}
	}
}
