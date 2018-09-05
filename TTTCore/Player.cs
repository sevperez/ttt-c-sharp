namespace TTTCore
{
    public class Player
    {
        public string Name { get; set; }
        public string Token { get; set; }
        public int NumWins { get; set; }

        public AI ai { get; set; }

        public Player()
        {
            this.NumWins = 0;
        }

        public virtual void SetPlayerName() {}
        public virtual void SetPlayerName(string name) {}
        public virtual void SetPlayerToken(string token) {}
    }
}
