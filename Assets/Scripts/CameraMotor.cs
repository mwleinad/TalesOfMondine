using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour {

    public Transform lookAt;
    public float boundX = 0.3f;
    public float boundY = 0.15f;

    //The reason to use lateUpdate is because is triggered after both update and fixUpdate, otherwise we can experience some desync
    public void LateUpdate() {
        Vector3 delta = Vector3.zero;

        Vector3 boundResult;
        boundResult.x = checkBounds(transform.position.x, lookAt.position.x, delta.x, boundX);
        boundResult.y = checkBounds(transform.position.y, lookAt.position.y, delta.y, boundY);

        transform.position += new Vector3(boundResult.x, boundResult.y, 0);
    }

    private float checkBounds(float cameraPosition, float lookAtAxis, float deltaAxisResult, float bound) {
       
        float deltaAxis = lookAtAxis - cameraPosition;

        if (deltaAxis > bound || deltaAxis < -bound) {
            if (cameraPosition < lookAtAxis) {
                deltaAxisResult = deltaAxis - bound;
            } else {
                deltaAxisResult = deltaAxis + bound;
            }
        }

        return deltaAxisResult;
    }
}
