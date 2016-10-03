using UnityEngine;
using System.Collections;

public class SpriteCollider : MonoBehaviour {


    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "stab")
        {
            SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
            if(this != null)
            {
                sr.color = Color.blue;
            }
        }
    }
}
