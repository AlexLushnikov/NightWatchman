using UnityEngine;

namespace NightWatchman
{
    public interface IPlayerController
    {
        Camera Camera { get; }
        void Spawn(Vector3 spawnPoint);
    }
}