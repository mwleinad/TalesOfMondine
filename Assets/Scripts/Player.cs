using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover {

    protected void FixedUpdate() {
        float x = Input.GetAxisRaw(Mover.Horizontal);
        float y = Input.GetAxisRaw(Mover.Vertical);

        UpdateMotor(new Vector3(x, y, 0), 1, 0.75f);
    }


}
