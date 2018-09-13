using System.Collections.Generic;
using System.Linq;

namespace TTTCore
{
    public class WinningLineGenerator
    {
        public int BoardSize { get; set; }

        public WinningLineGenerator(int boardSize)
        {
            this.BoardSize = boardSize;
        }

        public int[,] GetWinningLines()
        {
            var winningLineLists = new List<int[]>();

            this.AddHorizontalWinningLines(winningLineLists);
            this.AddVerticalWinningLines(winningLineLists);
            this.AddDiagonalWinningLines(winningLineLists);

            return this.ConvertWinningLineListsToMDArray(winningLineLists);
        }

        public int[,] ConvertWinningLineListsToMDArray(List<int[]> list)
        {
            int[,] mdArray = new int[this.BoardSize * 2 + 2, this.BoardSize];

            for (var i = 0; i < list.Count; i += 1)
            {
                for (var j = 0; j < list[0].Length; j += 1)
                {
                    mdArray[i, j] = list[i][j];
                }
            }

            return mdArray;
        }

        public void AddHorizontalWinningLines(List<int[]> lines)
        {
            for (var i = 0; i < this.BoardSize; i += 1)
            {
                var currentLine = new List<int>();
                for (var j = 0; j < this.BoardSize; j += 1)
                {
                    currentLine.Add(j + i * this.BoardSize);
                }

                lines.Add((int[])currentLine.ToArray());
            }
        }

        public void AddVerticalWinningLines(List<int[]> lines)
        {
            for (var i = 0; i < this.BoardSize; i += 1)
            {
                var currentLine = new List<int>();
                for (var j = 0; j < this.BoardSize; j += 1)
                {
                    currentLine.Add(i + j * this.BoardSize);
                }

                lines.Add((int[])currentLine.ToArray());
            }
        }

        public void AddDiagonalWinningLines(List<int[]> lines)
        {
            var diagonalLeft = new List<int>();
            var diagonalRight = new List<int>();

            for (var i = 0; i < this.BoardSize; i += 1)
            {
                for (var j = 0; j < this.BoardSize; j += 1)
                {
                    if (i == j)
                    {
                        diagonalLeft.Add(j + i * this.BoardSize);
                    }

                    if (i + j == this.BoardSize - 1)
                    {
                        diagonalRight.Add(j + i * this.BoardSize);
                    }
                }
            }

            lines.Add((int[])diagonalLeft.ToArray());
            lines.Add((int[])diagonalRight.ToArray());
        }
    }
}
