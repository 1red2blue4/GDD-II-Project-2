using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class RoomManager : MonoBehaviour {

    public List<GameObject> roomSet = new List<GameObject>();
    static public int numRooms = 49;
    static public GameObject[] saveRooms = new GameObject[50];
    static public GameObject[] rooms = new GameObject[numRooms];
    static public GameObject firstRoom;
    static public float[] roomsWidth = new float[numRooms];
    static public float[] roomsHeight = new float[numRooms];
    static public float[] roomsOffsetX = new float[numRooms];
    static public float[] roomsOffsetY = new float[numRooms];

    static private Vector2 initialPosition;


	// Use this for initialization
	void Start () {
        initialPosition = new Vector2(this.GetComponent<Transform>().position.x, this.GetComponent<Transform>().position.y);
        firstRoom = GameObject.FindGameObjectWithTag("FirstRoom");
        SpawnRooms();

    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void SpawnRooms()
    {
        //select all possible rooms
        saveRooms = GameObject.FindGameObjectsWithTag("Room");

        //add each room to a collection
        foreach (GameObject room in saveRooms)
        {            
            roomSet.Add(room);
        }

        //define the rooms
        for (int i = 0; i < numRooms; i++)
        {
            int randRoom = UnityEngine.Random.Range(0, roomSet.Count);
            rooms[i] = roomSet[randRoom];
            roomsWidth[i] = rooms[i].GetComponent<Room>().width;
            roomsHeight[i] = rooms[i].GetComponent<Room>().height;
            roomsOffsetX[i] = rooms[i].GetComponent<Room>().offsetX;
            roomsOffsetY[i] = rooms[i].GetComponent<Room>().offsetY;
        }

        //Instantiate the rooms
        for (int i = 0; i < (int)Math.Sqrt((double)numRooms); i++)
        {
            for (int t = 0; i < (int)Math.Sqrt((double)numRooms); t++)
            {
                if (i >= (int)Math.Sqrt((double)numRooms) || t >= (int)Math.Sqrt((double)numRooms))
                {
                    break;
                }
                if (i == 0 && t == 0)
                {
                    MonoBehaviour.Instantiate(firstRoom, initialPosition, Quaternion.identity);
                }
                else
                {
                    int sqrtRooms = (int)Math.Sqrt((double)numRooms);
                    int index = i * sqrtRooms + t;
                    Debug.Log((int)Math.Sqrt((double)numRooms));
                    float distanceSumX = 0;
                    float distanceSumY = 0;
                    for (int j = index; j > sqrtRooms - 1; j-=sqrtRooms)
                    {
                        distanceSumX += roomsWidth[j - sqrtRooms];
                    }
                    for (int j = index; j > index - t; j--)
                    {
                        distanceSumY += roomsHeight[j - 1];
                    }
                    float xVal = initialPosition.x + roomsOffsetX[index] + distanceSumX + (roomsWidth[index] - roomsWidth[index - 1]) / 2;
                    float yVal = initialPosition.y + roomsOffsetY[index] + distanceSumY + (roomsHeight[index] - roomsHeight[index - 1]) / 2;
                    MonoBehaviour.Instantiate(rooms[index], new Vector2(xVal, yVal), Quaternion.identity);
                }
            }
        }
    }
}
