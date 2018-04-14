using System.Collections;
using Boo.Lang;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour {
    public GameObject TextContainer;
    public GameObject TextPrefab;

    private System.Collections.Generic.List<FloatingText> _floatingTexts = new System.Collections.Generic.List<FloatingText>();

    public void Show(string message, int fontSize, Color color, Vector3 position, Vector3 motion, float duration) {
        FloatingText floatingText = GetFloatingText();

        floatingText.Text.text = message;
        floatingText.Text.fontSize = fontSize;
        floatingText.Text.color = color;
        floatingText.Motion = motion;
        floatingText.Duration = duration;
        //Transform world coordinates into camera coordinate
        floatingText.GameObject.transform.position = Camera.main.WorldToScreenPoint(position);

        floatingText.Show();
    }

    private void Update () {
        foreach (FloatingText text in _floatingTexts) {
            text.UpdateFloatingText();
        }
    }

    private FloatingText GetFloatingText() {
        FloatingText text = _floatingTexts.Find(t => !t.Active);

        if (text == null) {
            text = CreateText();
        }

        return text;
    }

    private FloatingText CreateText() {
        FloatingText text = new FloatingText();
        text.GameObject = Instantiate(TextPrefab);
        text.GameObject.transform.SetParent(TextContainer.transform);
        text.Text = text.GameObject.GetComponent<Text>();
        _floatingTexts.Add(text);

        return text;
    }
}
