using UnityEngine;
using System.Collections;
namespace UnityStandardAssets._2D
{
    public class HammerAnimation : Weapon
    {

        public PolygonCollider2D box1;
        public PolygonCollider2D box2;

        public void DestroySelf()
        {
            Destroy(this.gameObject);
        }

        public void SetBox1Enabled()
        {
            box1.enabled = true;
        }

        public void SetBox2Enabled()
        {
            box2.enabled = true;
        }

        public void SetBox1Disabled()
        {
            box1.enabled = false;
        }

        public void SetBox2Disabled()
        {
            box2.enabled = false;
        }

        public override Vector3 GetSpawnPosition(PlatformerCharacter2D character)
        {
            Vector3 position = character.transform.FindChild("HammerSpawn").transform.position;
            if (Input.mousePosition.x > (character.cam.WorldToViewportPoint(character.gameObject.transform.position).x) * Screen.width)
            {
            }
            else
            {

                position.x -= (character.transform.FindChild("HammerSpawn").transform.position.x - character.transform.FindChild("Center").transform.position.x) * 2;
            }
            return position;
        }
    }
}
