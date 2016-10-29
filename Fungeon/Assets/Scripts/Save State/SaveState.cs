using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UnityStandardAssets._2D
{
    public class SaveState : MonoBehaviour
    {
        private RectangleEnemy[] rects;
        private Vector3[] rectPositions;
        private bool[] aliveRects;
        private Transform[] parents;
        private FlockManager[] flockManagers;
        private Vector3 spawnPoint;
        private GameObject room;

        public RectangleEnemy[] Rects { get { return rects; } set { rects = value; } }
        public bool[] AliveRects { get { return aliveRects; } set { aliveRects = value; } }
        public Vector3[] RectPositions { get { return rectPositions; } set { rectPositions = value; } }
        public FlockManager[] FlockManagers { get { return flockManagers; } set { flockManagers = value; } }
        public Vector2 SpawnPoint { get { return spawnPoint; } set { spawnPoint = value; } }
        public GameObject Room { get { return room; } set { room = value; } }
        public Transform[] Parents { get { return parents; } set { parents = value; } }

        public SaveState(RectangleEnemy[] rects, Vector3[] rectPos, FlockManager[] fm, Transform[] parents, Vector2 spawnPoint, GameObject room)
        {
            rectPositions = rectPos;
            flockManagers = fm;
            this.spawnPoint = spawnPoint;
            this.room = room;
            aliveRects = new bool[rectPositions.Length];
            for(int i=0; i<rectPositions.Length; i++)
            {
                aliveRects[i] = true;
            }
            this.rects = rects;
            this.parents = parents;
        }

        public void Respawn()
        {
            //respawning rectangles
            //for(int i = 0; i < rects.Length; i++)
            //{
            //    if(rects[i] == null)
            //    {
            //        Instantiate(GameManager.Instance.RectangleEnemies, rectPositions[i], Quaternion.identity, parents[i]);
            //    }
            //}
            
            ////destroying triangles in spawn room
            //for(int i=0; i < flockManagers.Length; i++)
            //{
            //    if (flockManagers[i] != null)
            //    {
            //        if (flockManagers[i].EnemyFlock.Count > 0)
            //        {
            //            for (int j = 0; j < flockManagers[i].EnemyFlock.Count; j++)
            //            {
            //                Destroy(flockManagers[i].EnemyFlock[0]);
            //            }
            //            flockManagers[i].Spawn();
            //        }
            //    }
            //}

            //disabling past room's scripts
            GameObject lastRoom = GameObject.Find(GameManager.Instance.Player.CurrentRoom);
            Enemy[] e = lastRoom.GetComponentsInChildren<Enemy>();
            for(int i=0; i<e.Length; i++)
            {
                if(e[i].gameObject.name != "Collider")
                {
                    e[i].enabled = false;
                }
            }
            FlockManager[] oldFlocks = lastRoom.GetComponentsInChildren<FlockManager>();
            for (int i = 0; i < oldFlocks.Length; i++)
            {
                oldFlocks[i].enabled = false;
                for (int j = 0; j < oldFlocks[i].EnemyFlock.Count; j++)
                {
                    if (oldFlocks[i].EnemyFlock[j] != null)
                    {
                        oldFlocks[i].EnemyFlock[j].GetComponent<TriangleEnemy>().enabled = false;
                    }
                }
            }

            //enabling new room's scripts
            e = room.GetComponentsInChildren<Enemy>();
            for (int i = 0; i < e.Length; i++)
            {
                if (e[i].gameObject.name != "Collider")
                {
                    e[i].enabled = true;
                    RectangleEnemy er = e[i].GetComponent<RectangleEnemy>();
                    if(er != null)
                    {
                        er.enabled = true;
                    }
                }
            }
            FlockManager[] flocks = room.GetComponentsInChildren<FlockManager>();
            for (int i = 0; i < flocks.Length; i++)
            {
                flocks[i].enabled = true;
                for (int j = 0; j < flocks[i].EnemyFlock.Count; j++)
                {
                    if (flocks[i].EnemyFlock[j] != null)
                    {
                        flocks[i].EnemyFlock[j].GetComponent<TriangleEnemy>().enabled = true;
                    }
                }
            }
            GameManager.Instance.ChangeRoomColor(room);
        }
    }
}