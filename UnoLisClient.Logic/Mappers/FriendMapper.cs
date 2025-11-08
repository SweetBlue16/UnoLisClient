using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnoLisClient.Logic.Models;
using UnoLisClient.Logic.UnoLisServerReference.Friends;

namespace UnoLisClient.Logic.Mappers
{
    public static class FriendMapper
    {
        public static ObservableCollection<Friend> ToObservable(this IEnumerable<FriendData> source)
        {
            return new ObservableCollection<Friend>(
                source.Select(dto => new Friend(dto))
            );
        }
    }
}
