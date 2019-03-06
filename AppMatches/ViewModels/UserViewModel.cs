﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using AppMatches.Model;

namespace AppMatches.Client.ViewModels
{
	public class UserViewModel : INotifyPropertyChanged
	{
		public readonly User MatchUser;

		public UserViewModel(User user)
		{
			MatchUser = user;
		}

		public string Name => MatchUser.Info.nickname;

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}
