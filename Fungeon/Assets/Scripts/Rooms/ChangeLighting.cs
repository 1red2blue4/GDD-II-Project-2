using UnityEngine;
using System.Collections;

public class ChangeLighting : MonoBehaviour {

    public Light[] lights;
    public FlockManager[] enemies;

	// Use this for initialization
	void Start () {
        lights = this.GetComponentsInChildren<Light>();
        enemies = this.GetComponentsInChildren<FlockManager>();
        
        
	}
	
	// Update is called once per frame
	void Update () {
	    if (!enemies[0])
        {
            foreach (Light light in lights)
            {
                light.color = Color.gray;
            }
        }
	}
}
