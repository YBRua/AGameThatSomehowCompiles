using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControlBase : MonoBehaviour
{
    [SerializeField] protected GameObject Projectile;
    [SerializeField] protected GameObject Aircraft;
    protected WeaponryData data;
    public int maxShots;
    protected int shotsFired;
    protected Color ProjectileColor;

    protected virtual void Start()
    {
        data = Projectile.GetComponent<WeaponryData>();
        shotsFired = 0;
        ProjectileColor = Aircraft.GetComponent<SpriteRenderer>().color;
    }

    protected IEnumerator Cooldown(float cd)
    {
        shotsFired += 1;
        yield return new WaitForSeconds(cd);
        shotsFired -= 1;
    }
}
