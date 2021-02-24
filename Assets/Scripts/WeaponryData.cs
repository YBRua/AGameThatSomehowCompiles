using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponryData : MonoBehaviour
{
    public float filghtSpeed;
    public float maxRange;
    public int Damage;
    public int DamageRange;
    public float CoolDown;
    public bool isActive;
    public int GetDamage()
    {
        return Damage + Random.Range(-DamageRange, DamageRange);
    }
}
