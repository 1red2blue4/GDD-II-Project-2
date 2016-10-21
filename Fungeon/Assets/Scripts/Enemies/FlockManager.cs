using UnityEngine;
using System.Collections;

using System.Collections.Generic;


///------------------------------------------------------------ <summary>
/// This script will be responsible for controling a flock of "bat" enemies.
/// Wherever you want a flock of these enemies to spawn all you need to do is drag
/// the FlockManagerGO prefab and it will take care of the rest!
///------------------------------------------------------------- </summary>


public class FlockManager : MonoBehaviour {


    //-----------------------------VARIABLE DECLARATION-----------------------------------
    
    // these variables control the amount of random distance in initially spawning enemies
    // each of these represents the units to the left, right, down, and up of the centroid
    // enemies will be able to spawn into
    public float minXSpawn;
    public float maxXSpawn;
    public float minYSpawn;
    public float maxYSpawn;

    // the FlockManagerGO's position should be the same as the same as the centroid
    private Vector3 centroid;

    // the direction should steer the flock towards the player
    private Vector3 direction;

    // adjustable enemy flock size
    public int numEnemies;

    // this contains all enemies in the flock
    private List<GameObject> enemyFlock;
    private List<GameObject> obstacles;

    // for holding prefabs and target
    public GameObject enemyPrefab;
    public GameObject target;

    // to pass to each enemy flocker for determining at what distance they should attack
    //public float attackRad;

    //-------------------------------------------------------------------------------------

    // Accessor Statements, mostly to ensure that enemies know where and which direction to flock to
    public Vector3 Centroid
    {
        get { return centroid; }
    }

    public Vector3 Direction
    {
        get { return direction; }
    }

    public List<GameObject> EnemyFlock
    {
        get { return enemyFlock; }
    }

    public List<GameObject> Obstacles
    {
        get { return obstacles; }
    }

	// Use this for initialization
	void Start () {
	    
        // initialize any variables
        enemyFlock = new List<GameObject>();

        // populate the flock of enemies
        for (int i = 0; i < numEnemies; i++)
        {
            // calculate a random position around the centroid, assign each enemy a random pos
            Vector3 randPos = new Vector3(Random.Range(this.transform.position.x - minXSpawn, this.transform.position.x + maxXSpawn), 
                                          Random.Range(this.transform.position.y - minYSpawn, this.transform.position.y + maxYSpawn),
                                          0);

            // add an enemy to the flock
            enemyFlock.Add((GameObject)Instantiate(enemyPrefab, randPos, Quaternion.identity));
            //enemyFlock[i].GetComponent<TriangleEnemy>().target = target;
            //enemyFlock[i].GetComponent<TriangleEnemy>().attackRadius = attackRad;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (this == null || this.transform == null)
        {
            return;
        }

        // check to see if there are any enemies left in the flock
        // if not, delete it
        if (enemyFlock.Count <= 0)
        {
            Destroy(this.gameObject);
            return;
        }

        if (this != null)
        {
            // for each enemy in the flock check if they are equal to null
            // if so then remove it from the flock
            // this way, enemyFlock.Count should always reflect the current # of enemies in the flock
            for (int i = 0; i < enemyFlock.Count; i++)
            {
                if (enemyFlock[i] == null)
                {
                    enemyFlock.RemoveAt(i);
                }
            }

            // updates the flock's centroid
            CalcCentroid();
        }
	}

    /// <summary>
    /// This method will calculate the centroid that the enemies will seek to maintain cohesion
    /// the centroid should be where the FlockManager game object is located
    /// Use the Gizmo script attatched to it if you want to check this
    /// </summary>
    void CalcCentroid()
    {
        // initialize variables for centroid position
        float totalX = 0;
        float totalY = 0;

        // add up both x and y positions for each enemy
        for (int i = 0; i < enemyFlock.Count; i++)
        {
            totalX = enemyFlock[i].transform.position.x + totalX;

            totalY = enemyFlock[i].transform.position.y + totalY;
        }

        centroid.x = totalX / enemyFlock.Count;
        centroid.y = totalY / enemyFlock.Count;
        centroid.z = 0;

        this.transform.position = centroid;
    }
}
