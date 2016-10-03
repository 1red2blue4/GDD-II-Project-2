using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {

    public GameObject room;
    Vector2 position;
    int roomNumber;
    float scaleX;
    float scaleY;

	// Use this for initialization
	void Start () {
        scaleX = room.transform.localScale.x;
        scaleY = room.transform.localScale.y;
	}

    public void SetRoomNumber(int n)
    {
        roomNumber = n;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    
}
