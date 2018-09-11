namespace TTTCore
{
    public interface IGameInterface
    {
        void WelcomeMessage();
        
        string GetGameModeSelection();
        
        int GetRoundsToWinSelection();
        
        string GetPlayerNameSelection(int playerNumber, string invalidName);
        
        string GetPlayerTokenSelection(int playerNumber, string invalidToken);
        
        int GetFirstPlayerSelection(Player player1, Player player2);

        int GetPlayerMoveSelection(Player player, Board board);

        void RequestMoveMessage(Player player, int[] emptyIndices);

        void DrawMainScreen(
            Player player1, Player player2, int numRounds, 
            Board board, int nextPlayerNumber
        );

        void DrawRoundEnd(
            Player player1, Player player2, int numRounds, 
            Board board, string winnerName
        );

        void DrawGameEnd(
            Player player1, Player player2, int numRounds, 
            Board board, string winnerName
        );

        void DrawGameBoard(string[] tokens);
    }
}
