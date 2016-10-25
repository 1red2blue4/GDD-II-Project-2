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
        private FlockManager []flockManagers; //Flock managers
        private RectangleEnemy[] rectangles; //Rectangles

        public void Start()
        {
            flockManagers = FindObjectsOfType<FlockManager>(); //Instantiate the flock managers
            //rectangles = FindObjectsOfType<RectangleEnemy>(); //Instantiate the flock managers

            for (int i = 0; i < flockManagers.Length; i++) //For each flock manager
            {
                flockManagers[i].enabled = false; //Disable them
            }

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
                other.gameObject.GetComponent<PlatformerCharacter2D>().CurrentRoom = roomName;

                Debug.Log(target.transform.parent.name);

                FlockManager[] flocks = target.transform.parent.GetComponentsInChildren<FlockManager>();
                for(int i=0; i<flocks.Length; i++)
                {
                    flocks[i].enabled = true;
                }

                Enemy[] enemies = target.transform.parent.GetComponentsInChildren<Enemy>();
                for(int i=0; i<enemies.Length; i++)
                {
                    enemies[i].enabled = true;
                }

                //if (flock.name.Contains("FlockManagerGO"))
                //{
                //    flock.enabled = true;
                //    //Debug.Log(flock.GetComponentInChildren<FlockManager>().name);
                //    //flock.GetComponentInChildren<FlockManager>().enabled = true;
                //}

                //RectangleEnemy rect = target.transform.parent.GetComponentInChildren<RectangleEnemy>();

                //if (rect.name.Contains("RectangleEnemy"))
                //{
                //    rect.enabled = true;
                //    //Debug.Log(flock.GetComponentInChildren<FlockManager>().name);
                //    //flock.GetComponentInChildren<FlockManager>().enabled = true;
                //}

                ////set enemies to active
                //TriangleEnemy[] tris = target.transform.parent.GetComponentsInChildren<TriangleEnemy>();

                //for (int i = 0; i < tris.Length; i++)
                //{
                //    Debug.Log(tris[i].name + " tris" + i);
                //    tris[i].gameObject.SetActive(true);
                //}

                //RectangleEnemy[] rects = target.transform.parent.GetComponentsInChildren<RectangleEnemy>();
                //for (int i = 0; i < rects.Length; i++)
                //{
                //    Debug.Log(rects[i].name + " rect" + i);
                //    rects[i].gameObject.SetActive(true);
                //}
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