using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";


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
        Debug.Log(moveDelta);

        //Swap sprite direction, either facing left or right
        if(moveDelta.x > 0) {
            transform.localScale = Vector3.one; // transform.localScale = new Vector3(1, 1, 1);
        } else if(moveDelta.x < 0){
            transform.localScale = new Vector3(-1, 1, 1);
        }

        transform.Translate(moveDelta * Time.deltaTime);

        //Debug.Log(x);
        //Debug.Log(y);
    }
}
