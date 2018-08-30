namespace TTTCore
{
    public class Player
    {
        public string Name { get; set; }
        public string Token { get; set; }
        public int NumWins { get; set; }

        public virtual void SetPlayerName() {}
        public virtual void SetPlayerName(string name) {}
        public virtual void SetPlayerToken(string token) {}
    }
}
