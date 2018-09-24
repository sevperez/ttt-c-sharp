namespace ArtificialIntelligence
{
    public interface IScorer
    {
        IBoard Board { get; set; }
        
        int GetTerminalScore();

        int GetHeuristicScore();
    }
}
