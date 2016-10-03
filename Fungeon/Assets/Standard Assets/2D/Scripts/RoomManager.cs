using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour {

    public List<GameObject> roomSet = new List<GameObject>();
    static public int numRooms = 2;
    static public GameObject[] rooms = new GameObject[numRooms];
    static public float[] roomsWidth = new float[numRooms];
    static public float[] roomsHeight = new float[numRooms];

    static private Vector2 initialPosition;


	// Use this for initialization
	void Start () {
        initialPosition = new Vector2(20, 0);
        SpawnRooms();

    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void SpawnRooms()
    {
        //select all possible rooms
        rooms = GameObject.FindGameObjectsWithTag("Room");

        //add each room to a collection
        foreach (GameObject room in rooms)
        {            
            roomSet.Add(room);
        }

        //define the rooms
        for (int i = 0; i < rooms.Length; i++)
        {
            int randRoom = Random.Range(0, roomSet.Count);
            rooms[i] = roomSet[randRoom];
            roomsWidth[i] = rooms[i].GetComponent<BoxCollider2D>().size.x;
            roomsHeight[i] = rooms[i].GetComponent<BoxCollider2D>().size.y;
        }

        //Instantiate the roomss
        for (int i = 0; i < rooms.Length; i++)
        {
            if (i == 0)
            {
                MonoBehaviour.Instantiate(rooms[i], initialPosition, Quaternion.identity);
            }
            else
            {
                MonoBehaviour.Instantiate(rooms[i], new Vector2(initialPosition.x + roomsWidth[i-1] + (roomsWidth[i] - roomsWidth[i-1])/2, initialPosition.y), Quaternion.identity);
            }
        }
    }
}
