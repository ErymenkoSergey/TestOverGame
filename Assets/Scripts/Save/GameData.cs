using System;
using System.Collections.Generic;

namespace TestOverMobile.SaveSystem
{
    [Serializable]
    public class GameData
    {
        public List<PlayerCard> PlayerCards = new List<PlayerCard>();
    }
}