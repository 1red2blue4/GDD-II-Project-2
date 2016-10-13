using UnityEngine;
using System.Collections;
namespace UnityStandardAssets._2D
{
    public abstract class Weapon : MonoBehaviour
    {

        //private float damage;
        //private float knockback;
        //private float cooldown;
        //private GameObject weaponPrefab;

        //public Weapon(float damage, float knockback, float cooldown, GameObject weaponPrefab)
        //{
        //    this.damage = damage;
        //    this.knockback = knockback;
        //    this.cooldown = cooldown;
        //    this.weaponPrefab = weaponPrefab;
        //}

        public Weapon()
        {

        }

        public abstract Vector3 GetSpawnPosition(PlatformerCharacter2D character);
    }
}