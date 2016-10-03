using UnityEngine;
using System.Collections;

public class StabAnimation : MonoBehaviour {

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    public void SetCollider()
    {
        BoxCollider2D bc = this.gameObject.AddComponent<BoxCollider2D>();
        bc.size = new Vector2(.64f, .32f);
        bc.isTrigger = true;
    }

    public void RemoveCollider()
    {
        BoxCollider2D[] bcArr = this.gameObject.GetComponents<BoxCollider2D>();
        foreach(BoxCollider2D box in bcArr)
        {
            Destroy(box);
        }
    }
}
