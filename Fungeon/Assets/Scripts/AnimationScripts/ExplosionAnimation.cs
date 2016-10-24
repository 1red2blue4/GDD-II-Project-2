using UnityEngine;
using System.Collections;

namespace UnityStandardAssets._2D
{
    public class ExplosionAnimation : Weapon
    {
        public PolygonCollider2D box1;
        private PolygonCollider2D localCol;

        public ExplosionAnimation(float cooldown) : base(cooldown)
        {

        }

        public void Awake()
        {
            localCol = gameObject.AddComponent<PolygonCollider2D>();
            localCol.isTrigger = true;
            localCol.pathCount = 0;
        }

        public void DestroySelf()
        {
            Destroy(this.gameObject);
        }

        public void SetCollider()
        {
            localCol.SetPath(0, box1.GetPath(0));
        }

        public void RemoveCollider()
        {
            localCol.pathCount = 0;
        }

        public override Vector3 GetSpawnPosition(PlatformerCharacter2D character)
        {
            //calculating position to spawn sprite
            Vector3 position = character.transform.position;

            position.y = character.gameObject.transform.FindChild("Center").transform.position.y + 1f;
            return position;
        }

        public override Vector3 ControllerGetSpawnPosition(PlatformerCharacter2D character) //Weapon spawning for the controller
        {
            //calculating position to spawn sprite
            Vector3 position = character.transform.position;

            position.y = character.gameObject.transform.FindChild("Center").transform.position.y + 1f;

            return position;
        }
    }
}