public interface IScore
{
    public void AddScore(int newScore);
}

public interface IKiller
{
}

public interface IPlayerDead
{
    public void OnPlayerDead();
}

public interface IGameManager : IScore, IPlayerDead
{
    public bool IsGameover { get; }
}
