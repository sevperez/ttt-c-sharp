namespace ArtificialIntelligence
{
    public interface IScorer
    {
        int GetTerminalScore();

        int GetHeuristicScore();
    }
}
