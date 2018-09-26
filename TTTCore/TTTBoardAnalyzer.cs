using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ArtificialIntelligence;

namespace TTTCore
{
    public class TTTBoardAnalyzer : IBoardAnalyzer
    {
        public bool IsEndState(IBoard board)
        {
            var round = new Round(board);
            return round.IsOver();
        }

        public IBoard SimulateMove(IBoard inputBoard, int moveIndex, string moveToken)
        {
            var simulatedBoard = new Board(inputBoard.BoardSize);
            for (var i = 0; i < inputBoard.Units.Count; i += 1)
            {
                var fillToken = inputBoard.Units[i].CurrentToken;
                if (fillToken != "")
                {
                simulatedBoard.Units[i].Fill(fillToken);
                }
            }
            simulatedBoard.Units[moveIndex].Fill(moveToken);

            return simulatedBoard;
        }
    }   
}
