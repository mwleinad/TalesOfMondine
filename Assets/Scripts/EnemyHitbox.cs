using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable {

    //Damage
    public int DamagePoint;
    public float PushForce;

    protected override void OnCollide(Collider2D collide) {
        if (collide.tag != "Fighter" || collide.name != "Player") {
            return;
        }

        Damage damage = new Damage();
        damage.DamageAmount = DamagePoint;
        damage.PushForce = PushForce;
        damage.Origin = transform.position;

        collide.SendMessage("ReceiveDamage", damage);
    }
}
