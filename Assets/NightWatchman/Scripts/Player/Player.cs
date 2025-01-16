using UnityEngine;

namespace NightWatchman
{
    public class Player : MonoBehaviour, IPlayer
    {
        [SerializeField] private Camera _camera;
        public Camera Camera => _camera;
   
        public void Spawn(Vector3 spawnPoint)
        {
            transform.position = new Vector3(spawnPoint.x, transform.position.y, spawnPoint.z);
        }
    }
}