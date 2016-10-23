using UnityEngine;
using System.Collections;

public class DoorScripty : MonoBehaviour
{
    public GameObject target;
    public int side;
    public static AudioSource openDoor;

    private void OnTriggerStay2D(Collider2D other)
    {
        openDoor = GetComponent<AudioSource>();
        if (side == 0 && other.tag == "Player" && Input.GetButtonDown("DoorActivation"))
        {
            openDoor.Play();
            other.gameObject.transform.position = new Vector2(target.transform.position.x+2, target.transform.position.y + 2);
        }
        if (side == 1 && other.tag == "Player" && Input.GetButtonDown("DoorActivation"))
        {
            openDoor.Play();
            other.gameObject.transform.position = new Vector2(target.transform.position.x-2, target.transform.position.y + 2);
        }
        if (side == 2 && other.tag == "Player" && Input.GetButtonDown("DoorActivation"))
        {
            openDoor.Play();
            other.gameObject.transform.position = new Vector2(target.transform.position.x, target.transform.position.y + 0.5f);
        }
        if (side == 3 && other.tag == "Player" && Input.GetButtonDown("DoorActivation"))
        {
            openDoor.Play();
            other.gameObject.transform.position = new Vector2(target.transform.position.x , target.transform.position.y + 2);
        }
    }

}