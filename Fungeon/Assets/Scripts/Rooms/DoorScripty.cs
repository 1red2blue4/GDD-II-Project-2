using UnityEngine;
using System.Collections;
using System.Collections.Generic; //List

namespace UnityStandardAssets._2D
{
    public class DoorScripty : MonoBehaviour
    {
        public GameObject target;
        public int side;
        public Collider2D otherCollision;
        public AudioSource openDoor;
        private string roomName;
        private FlockManager[] oldFlocks;
        private Enemy[] oldEnemies;

        public void Start()
        {
            oldFlocks = new FlockManager[0];
            oldEnemies = new Enemy[0];
            //flockManagers = FindObjectsOfType<FlockManager>(); //Instantiate the flock managers
            ////rectangles = FindObjectsOfType<RectangleEnemy>(); //Instantiate the flock managers

            //for (int i = 0; i < flockManagers.Length; i++) //For each flock manager
            //{
            //    flockManagers[i].enabled = false; //Disable them
            //}

            //for (int i = 0; i < rectangles.Length; i++) //For rectangle
            //{
            //    rectangles[i].enabled = false; //Disable them
            //}
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            otherCollision = other;
            if (other.tag == "Player" && Input.GetButtonDown("DoorActivation") && !GameManager.Instance.MovingBetweenRooms)
            {
                GameManager.Instance.MovingBetweenRooms = true;
                openDoor = GetComponent<AudioSource>();
                openDoor.Play();
                FadeBetween();
                string roomName = target.transform.parent.name;

                //Disabling enemies in past room
                oldFlocks = this.transform.parent.transform.parent.GetComponentsInChildren<FlockManager>();
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
                oldEnemies = this.transform.parent.transform.parent.GetComponentsInChildren<Enemy>();
                for (int i = 0; i < oldEnemies.Length; i++)
                {
                    oldEnemies[i].enabled = false;
                }

                //Setting new enemies to being enabled
                other.gameObject.GetComponent<PlatformerCharacter2D>().CurrentRoom = roomName;

                FlockManager[] flocks = target.transform.parent.GetComponentsInChildren<FlockManager>();
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
                oldFlocks = flocks;

                Enemy[] enemies = target.transform.parent.GetComponentsInChildren<Enemy>();
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].enabled = true;
                }
                oldEnemies = enemies;

                print(GameManager.Instance.Player.CurrentRoom);
            }
        }
        public void FadeBetween()
        {
            Vector3 pos = GameManager.Instance.MainCamera.gameObject.transform.position;
            pos.x -= 27f;
            pos.y -= 15f;
            pos.z = -1f;
            GameObject screen = (GameObject)Instantiate(GameManager.Instance.MainCamera.GetComponent<Camera2DFollow>().fadeScreen, pos, GameManager.Instance.MainCamera.gameObject.transform.rotation);
            screen.transform.parent = GameManager.Instance.MainCamera.transform;
            screen.GetComponent<FadeScreen>().door = this;
        }
    }
}