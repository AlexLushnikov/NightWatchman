namespace NightWatchman
{
    public interface ILevelService
    {
        Level CurrentLevel { get; }
        Environment CurrentEnvironment { get; }
        void SpawnLevel();
        void FinishLevel();
        void SetupAnomaly();
    }
}