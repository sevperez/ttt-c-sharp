using System.Collections.Generic;

namespace ArtificialIntelligence
{
    public interface IBoard
    {
        int BoardSize { get; set; }

        List<IBoardUnit> Units { get; set; }

        int[] GetAvailableLocations();

        bool IsFull();

        string[] GetTokenArray();
    }
}
