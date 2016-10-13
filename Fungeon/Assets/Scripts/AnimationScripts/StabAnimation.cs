using UnityEngine;
using System.Collections;

namespace UnityStandardAssets._2D
{
    public class StabAnimation : Weapon
    {

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
            foreach (BoxCollider2D box in bcArr)
            {
                Destroy(box);
            }
        }

        public override Vector3 GetSpawnPosition(PlatformerCharacter2D character)
        {
            //calculating position to spawn sprite
            Vector3 position = character.transform.position;

            //chekcing which side to attack on based on mouse position
            if (Input.mousePosition.x > (character.cam.WorldToViewportPoint(character.gameObject.transform.position).x) * Screen.width)
            {
                position.x += character.gameObject.GetComponent<BoxCollider2D>().size.x - .05f;
            }
            else
            {
                position.x -= character.gameObject.GetComponent<BoxCollider2D>().size.x;
            }
            position.y = character.gameObject.transform.FindChild("Center").transform.position.y + .05f;
            return position;
        }
    }
}
