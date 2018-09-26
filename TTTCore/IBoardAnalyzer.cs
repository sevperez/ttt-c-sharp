namespace ArtificialIntelligence
{
    public interface IBoardAnalyzer
    {
        bool IsEndState(IBoard board);

        IBoard SimulateMove(IBoard inputBoard, int moveIndex, string moveToken);
    }
}
