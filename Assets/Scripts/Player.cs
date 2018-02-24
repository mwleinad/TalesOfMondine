using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private const string PLAYER_MASK = "Player";
    private const string BLOCKING_MASK = "Blocking";

    private RaycastHit2D hit;

    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;

	private void Start () {
        boxCollider = GetComponent<BoxCollider2D>();		
	}
	
	// Update is called once per frame
	private void FixedUpdate () {
        
        //returns -1 when holding left, 0 if you are not pressing anything and 1 if you're pressing right
        float x = Input.GetAxisRaw(HORIZONTAL);
        float y = Input.GetAxisRaw(VERTICAL);
        //reset move delta
        moveDelta = new Vector3(x, y, 0);

        swapSpriteDirection();

        move();
    }

    private void swapSpriteDirection() {
        if (moveDelta.x > 0) {
            transform.localScale = Vector3.one; // transform.localScale = new Vector3(1, 1, 1);
        } else if (moveDelta.x < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void move() {
        hit = collided(getYDirection(), getDistance(moveDelta.y));
        moveY(hit);

        hit = collided(getXDirection(), getDistance(moveDelta.x));
        moveX(hit);
    }

    private RaycastHit2D collided(Vector2 direction, float distance) {
        //Make sure we can move in this direction, by calling a box cast first, if the box returns null, we're free to move
        return Physics2D.BoxCast(transform.position, boxCollider.size, 0, direction, distance, getColliderMask());
    }

    private LayerMask getColliderMask() {
        return LayerMask.GetMask(PLAYER_MASK, BLOCKING_MASK);
    }

    private float getDistance(float distance) {
        return Mathf.Abs(distance * Time.deltaTime);
    }

    private Vector2 getYDirection() {
        return new Vector2(0, moveDelta.y);
    }

    private Vector2 getXDirection() {
        return new Vector2(moveDelta.x, 0);
    }

    private void moveY() {
        transform.Translate(0, moveDelta.y* Time.deltaTime, 0);
    }

    private void moveY(RaycastHit2D hit) {
        if (hit.collider != null) {
            return;
        }

        transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
    }

    private void moveX(RaycastHit2D hit) {
        if (hit.collider != null) {
            return;
        }
        transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
    }

}
