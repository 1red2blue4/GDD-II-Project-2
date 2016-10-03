using UnityEngine;
using System.Collections;

public class SpriteCollider : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "weapon")
        {
            print("poop");
            SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
            if (this != null)
            {
                sr.color = Color.blue;
            }
        }
    }
}
