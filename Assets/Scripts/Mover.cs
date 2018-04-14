using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter {
    //TODO make it stateless?
    protected const string Horizontal = "Horizontal";
    protected const string Vertical = "Vertical";

    protected const string PlayerMask = "Player";
    protected const string BlokingMask = "Blocking";

    protected RaycastHit2D Hit;
    protected BoxCollider2D BoxCollider;
    protected Vector3 MoveDelta;

    protected virtual void Start() {
        BoxCollider = GetComponent<BoxCollider2D>();
    }

    //TODO should I send speed or just send it directly to vector3
    protected virtual void UpdateMotor(Vector3 input, float XSpeed, float YSpeed) {
        MoveDelta = new Vector3(input.x * XSpeed, input.y * YSpeed, 0);
        SwapSpriteDirection();
        Move();
    }

    private void SwapSpriteDirection() {
        if (MoveDelta.x > 0) {
            transform.localScale = Vector3.one; // transform.localScale = new Vector3(1, 1, 1);
        } else if (MoveDelta.x < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void Move() {
        Hit = Collided(GetYDirection(), GetDistance(MoveDelta.y));
        MoveY(Hit);

        Hit = Collided(GetXDirection(), GetDistance(MoveDelta.x));
        MoveX(Hit);
    }

    private RaycastHit2D Collided(Vector2 direction, float distance) {
        //Make sure we can move in this direction, by calling a box cast first, if the box returns null, we're free to move
        return Physics2D.BoxCast(transform.position, BoxCollider.size, 0, direction, distance, GetColliderMask());
    }

    private LayerMask GetColliderMask() {
        return LayerMask.GetMask(PlayerMask, BlokingMask);
    }

    private float GetDistance(float distance) {
        return Mathf.Abs(distance * Time.deltaTime);
    }

    private Vector2 GetYDirection() {
        return new Vector2(0, MoveDelta.y);
    }

    private Vector2 GetXDirection() {
        return new Vector2(MoveDelta.x, 0);
    }


    private void MoveY(RaycastHit2D hit) {
        if (hit.collider != null) {
            return;
        }

        transform.Translate(0, MoveDelta.y * Time.deltaTime, 0);
    }

    private void MoveX(RaycastHit2D hit) {
        if (hit.collider != null) {
            return;
        }
        transform.Translate(MoveDelta.x * Time.deltaTime, 0, 0);
    }
}
