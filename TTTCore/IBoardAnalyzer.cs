using System.Collections.Generic;
using TTTCore;

namespace ArtificialIntelligence
{
    public interface IBoardAnalyzer
    {
        List<MoveOption> GetTopMoveOptions(List<MoveOption> allMoveOptions);

        List<MoveOption> GetAllMoveOptions(Board board, bool ownerNext);

        MoveOption GenerateMoveOption(Board board, int moveIndex, bool ownerNext);
    }
}
