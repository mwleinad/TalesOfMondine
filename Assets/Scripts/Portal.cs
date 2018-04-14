using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable {

    public string[] SceneNames;

    protected override void OnCollide(Collider2D collide) {
        if (collide.name == "Player") {
            Debug.Log("teleport");
            Debug.Log(SceneNames.Length);
            string sceneName = SceneNames[0];

            GameManager.instance.SaveState();
            SceneManager.LoadScene(sceneName);
        }
    }
}
