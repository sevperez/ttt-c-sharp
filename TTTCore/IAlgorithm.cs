using TTTCore;

namespace ArtificialIntelligence
{
    public interface IAlgorithm
    {
        int CalculateScore(Board board, int depth, bool ownerNext, int alpha, int beta);
    }
}
