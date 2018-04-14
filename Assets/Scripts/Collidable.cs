using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{

    public ContactFilter2D filter;
    private BoxCollider2D _boxCollider;
    private Collider2D[] _hits = new Collider2D[10];

	protected virtual void Start ()
	{
	    _boxCollider = GetComponent<BoxCollider2D>();
	}
	
	protected virtual void Update () {
		//Collision stuff
	    _boxCollider.OverlapCollider(filter, _hits);

	    for (int ii = 0; ii < _hits.Length; ii++) {
	        if (_hits[ii] == null) {
	            continue;
	        }

            OnCollide(_hits[ii]);

	        _hits[ii] = null;
	    }
	}

    protected virtual void OnCollide(Collider2D collide) {
        Debug.Log("Override was not implemented on"+this.name);
    }
}
