using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    private float damage;
    private float knockback;
    private float cooldown;
    private GameObject weaponPrefab;

    public Weapon(float damage, float knockback, float cooldown, GameObject weaponPrefab)
    {
        this.damage = damage;
        this.knockback = knockback;
        this.cooldown = cooldown;
        this.weaponPrefab = weaponPrefab;
    }
}
