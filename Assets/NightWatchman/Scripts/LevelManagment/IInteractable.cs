namespace NightWatchman
{
    public interface IInteractable
    {
        EInteractableIds ID { get; }
        InteractableState State { get; }
        Difficulty Difficulty { get; }
        bool IsAnomaly { get; }
        void Init();
        void SetData(Difficulty difficulty);
        void ChangeState(InteractableState state);
    }
}