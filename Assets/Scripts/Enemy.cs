using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover {
    //Experience
    public int XpValue = 1;

    //Logic
    public float TriggerLength = 0.3f;
    public float ChaseLength = 1.0f;
    private bool _chasing;
    private bool _collidingWithPlayer;
    private Transform _playerTransform;
    private Vector3 _startingPosition;

    //Hitbox TODO, DRY this is the same in collidable.cs
    private BoxCollider2D _boxCollider;
    private Collider2D[] _hits = new Collider2D[10];
    public ContactFilter2D filter;

    // Update is called once per frame
    protected override void Start() {
        base.Start();

        _playerTransform = GameManager.instance.Player.transform;
        _startingPosition = transform.position;
        //GetChild gets the component inside the current component
        _boxCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();

    }

    //TODO simplify
    private void FixedUpdate() {
        Vector3 input;

        //is the player in range?
        if (!IsPlayerOnRange()) {
            input = _startingPosition - transform.position;
            UpdateMotor(input, 1, 0.75f);
            _chasing = false;
        } else {
            if (ShouldTriggerChase()) {
                _chasing = true;
            }

            if (_chasing) {
                if (!_collidingWithPlayer) {
                    input = (_playerTransform.position - transform.position).normalized;
                    UpdateMotor(input, 0.75f, 0.5f);
                }
            } else {
                input = _startingPosition - transform.position;
                UpdateMotor(input, 1, 0.75f);
            }
        }

        CollideCheck();
    }

    private void CollideCheck() {
        _collidingWithPlayer = false;
        _boxCollider.OverlapCollider(filter, _hits);

        for (int ii = 0; ii < _hits.Length; ii++) {
            if (_hits[ii] == null) {
                continue;
            }

            if (_hits[ii].tag == "Fighter" && _hits[ii].name == "Player") {
                _collidingWithPlayer = true;
                Debug.Log(_collidingWithPlayer);
            }

            _hits[ii] = null;
        }
    }

    private Boolean IsPlayerOnRange() {
        return Vector3.Distance(_playerTransform.position, _startingPosition) < ChaseLength; //position of the player - position of the enemy
    }

    private Boolean ShouldTriggerChase() {
        return Vector3.Distance(_playerTransform.position, _startingPosition) < TriggerLength;
    }

    protected override void Death() {
        base.Death();
        Destroy(gameObject);
        GameManager.instance.Exp += XpValue;
        GameManager.instance.ShowText("+ " + XpValue + "Exp", 30, Color.magenta, transform.position, Vector3.up * 50, 1.0f);
    }

}
