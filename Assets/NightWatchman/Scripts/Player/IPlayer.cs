using UnityEngine;

namespace NightWatchman
{
    public interface IPlayer
    {
        Camera Camera { get; }
        void Spawn(Vector3 spawnPoint);
    }
}