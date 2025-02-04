namespace NightWatchman
{
    public interface ISoundsService
    {
        void PlayCorrect();
        void PlayIncorrect();
        void SetEffectVolume(float volume);
        void SetMusicVolume(float volume);
    }
}