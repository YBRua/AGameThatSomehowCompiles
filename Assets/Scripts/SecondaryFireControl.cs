using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryFireControl : FireControlBase
{
    void Update()
    {
        if(Input.GetMouseButton(0) && shotsFired < maxShots)
        {
            var proj = Instantiate(Projectile, transform.position, transform.rotation);
            proj.GetComponent<SpriteRenderer>().color = ProjectileColor;
            StartCoroutine(Cooldown(data.CoolDown));
        }
    }
}
