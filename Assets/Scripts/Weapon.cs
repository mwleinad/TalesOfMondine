using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class Weapon : Collidable {
    //Damage struct
    public int DamagePoint = 1;
    public float PushForce = 2.0f;

    //Upgrade
    public int WeaponLevel = 1;
    private SpriteRenderer SpriteRenderer;

    //Swing
    private Animator _animator;
    private float _coolDown = 0.5f;
    private float _lastSwing;
    protected override void Start() {
        base.Start();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    protected override void Update() {
        base.Update();

        if (!CanSwing()) {
            return;
        }

        _lastSwing = Time.time;
        Swing();
    }

    protected override void OnCollide(Collider2D collide) {
        if (!IsEnemy(collide)) {
            return;
        }

        Damage damage = new Damage();
        damage.DamageAmount = DamagePoint;
        damage.PushForce = PushForce;
        damage.Origin = transform.position;

        collide.SendMessage("ReceiveDamage", damage);

        Debug.Log(collide.name);
    }

    private Boolean CanSwing() {
        return Input.GetKeyDown(KeyCode.Space) && Time.time - _lastSwing > _coolDown;
    }
    
    private void Swing() {
        _animator.SetTrigger("Swing");
        Debug.Log("Swing");
    }

    private Boolean IsEnemy(Collider2D collide) {
        if (collide.tag != "Fighter" || collide.name == "Player") {
            return false;
        }

        return true;
    }

}
