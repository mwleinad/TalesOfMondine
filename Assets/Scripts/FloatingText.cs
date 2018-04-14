using UnityEngine;
using UnityEngine.UI;

public class FloatingText {
    public bool Active;
    public GameObject GameObject;

    public Text Text;
    public Vector3 Motion;
    public float Duration;
    public float LastShown;

    public void Show() {
        Active = true;
        LastShown = Time.time;
        GameObject.SetActive(Active);
    }

    public void Hide() {
        Active = false;
        GameObject.SetActive(Active);
    }

    public void UpdateFloatingText() {
        if (!Active) {
            return;
        }

        if (ShouldHide()) {
            Hide();
        }

        GameObject.transform.position += GetPosition();
    }

    private bool ShouldHide() {
        return Time.time - LastShown > Duration;
    }

    private Vector3 GetPosition() {
        return Motion * Time.deltaTime;
    }
}
