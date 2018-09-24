namespace ArtificialIntelligence
{
    public interface IAlgorithm
    {
        IScorer Scorer { get; set; }

        int CalculateScore(IBoard board, int depth, bool ownerNext, int alpha, int beta);
    }
}
