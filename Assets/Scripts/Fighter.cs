using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

public class Fighter : MonoBehaviour {
    public int HitPoint = 10;
    public int MaxHitPoint = 10;
    public float pushRecoverySpeed = 0.2f;

    //inmunity
    protected float ImmuneTime = 1.0f;
    protected float LastInmune;

    //push
    protected Vector3 PushDirection;

    //All fighters can receive damage/die
    protected virtual void ReceiveDamage(Damage damage) {
        if (!CanReceiveDamage()) {
            return;
        }

        GameManager.instance.ShowText(damage.DamageAmount.ToString(), 25, Color.red, transform.position, Vector3.zero, 0.5f);
        LastInmune = Time.time;

        HitPoint -= damage.DamageAmount;
        PushDirection = (transform.position - damage.Origin).normalized * damage.PushForce;

        if (HitPoint <= 0) {
            Death();
        }
    }

    protected virtual void Death() {
        HitPoint = 0;
    }

    private Boolean CanReceiveDamage() {
        return Time.time > LastInmune;
    }



}