using System.Collections.Generic;
using TestOverMobile.SaveSystem;

namespace TestOverMobile.Interface
{
    public interface ISaveble
    {
        List<PlayerCard> GetPlayerCards();
        string GetPlayerName();
        void SetResultCurrentPlayer(int score);
        void CreateNewPlayer(string name);
        void Save();
        void Load();
    }
}