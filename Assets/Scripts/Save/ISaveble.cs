using System.Collections.Generic;
using TestOverMobile.SaveSystem;

namespace TestOverMobile.Interface
{
    public interface ISaveble
    {
        List<PlayerCard> GetPlayerCards();
        string GetPlayerName();
        void CreateNewPlayer(string name);
        void SetResultCurrentPlayer(int score);
        void Save();
    }
}