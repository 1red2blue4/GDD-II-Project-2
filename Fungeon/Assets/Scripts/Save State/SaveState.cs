using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UnityStandardAssets._2D
{
    public class SaveState : MonoBehaviour
    {
        private List<Vector3> rectPositions;
        private FlockManager[] flockManagers;
        private Vector3 spawnPoint;
        private GameObject room;

        public List<Vector3> RectPositions { get { return rectPositions; } set { rectPositions = value; } }
        public FlockManager[] FlockManagers { get { return flockManagers; } set { flockManagers = value; } }
        public Vector2 SpawnPoint { get { return spawnPoint; } set { spawnPoint = value; } }
        public GameObject Room { get { return room; } set { room = value; } }

        public SaveState(List<Vector3> rectPos, FlockManager[] fm, Vector2 spawnPoint, GameObject room)
        {
            rectPositions = rectPos;
            flockManagers = fm;
            this.spawnPoint = spawnPoint;
            this.room = room;
        }
    }
}