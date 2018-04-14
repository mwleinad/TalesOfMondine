using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable {
    public Sprite EmptyChest;
    public int PesosAmount = 10;

    protected override void OnCollect() {
        //base.OnCollect();
        if (Collected) {
            return;
        }

        base.OnCollect();

        GetComponent<SpriteRenderer>().sprite = EmptyChest;
        GameManager.instance.ShowText("+"+ PesosAmount +" Pesos!", 25, Color.yellow, transform.position, Vector3.up * 30, 1.5f);
    }
}
