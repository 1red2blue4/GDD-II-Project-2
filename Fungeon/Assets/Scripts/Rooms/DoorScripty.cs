using UnityEngine;
using System.Collections;

namespace UnityStandardAssets._2D
{
    public class DoorScripty : MonoBehaviour
    {
        public GameObject target;
        public int side;
        public Collider2D otherCollision;
        public AudioSource openDoor;
        private string roomName;

        public void Start()
        {

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