using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour {
    Color thisColor;

    public Color[] colors;
    public float stepSize;
    public float waitSize;

    int i = 0;
    Coroutine play;
    public bool rainbow = true;
     
    void Awake () {
        if(transform.GetComponent<Image>() != null)
            thisColor = transform.GetComponent<Image>().color;
        else {
            thisColor = gameObject.GetComponent<Renderer>().material.color;
        }
    }
	void Start () {
        play = StartCoroutine(Rainbow());
	}
    void ColorSet (Color newColor) {
        if (transform.GetComponent<Image>() != null)
            transform.GetComponent<Image>().color = newColor;
        else {
            transform.GetComponent<Material>().color = newColor;
        }
    }
    Color GetColor() {
        if (transform.GetComponent<Image>() != null)
            return transform.GetComponent<Image>().color;
        else {
            return transform.GetComponent<Material>().color;
        }
    }
    IEnumerator Rainbow() {
        while (rainbow) {
            Color newColor;
            newColor = GetColor(); 
            newColor.r = Mathf.MoveTowards(newColor.r, colors[i].r, stepSize);
            newColor.g = Mathf.MoveTowards(newColor.g, colors[i].g, stepSize);
            newColor.b = Mathf.MoveTowards(newColor.b, colors[i].b, stepSize);
            ColorSet(newColor);
            if (newColor.r == colors[i].r && newColor.g == colors[i].g && newColor.b == colors[i].b) {
                if (i + 1 != colors.Length)
                    i++;
                else {
                    i = 0;
                }
            }
            yield return new WaitForSeconds(waitSize);
        }
    }
}
