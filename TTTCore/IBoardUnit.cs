using System;

namespace ArtificialIntelligence
{
    public interface IBoardUnit
    {
        string CurrentToken { get; set; }

        void Fill(string token);
    }
}
