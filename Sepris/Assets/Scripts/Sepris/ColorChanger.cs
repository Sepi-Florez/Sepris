using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour {
    Color thisColor;
    Image thisImage;
    public Material thisMaterial;

    public Color[] colors;
    public float stepSize;
    public float waitSize;

    int i = 0;
    Coroutine play;
    public bool rainbow = true;
     
    void Awake () {
        if (transform.GetComponent<Image>() != null) {
            thisImage = transform.GetComponent<Image>();
            thisColor = thisImage.color;
        }
        else {
            thisMaterial = gameObject.GetComponent<Renderer>().material;
            thisColor = thisMaterial.color;
        }
    }
	void Start () {
        play = StartCoroutine(Rainbow());
	}
    void ColorSet (Color newColor) {
        if (thisImage != null)
            thisImage.color = newColor;
        else {
            thisMaterial.color = newColor;
        } 
    }
    Color GetColor() {
        if (transform.GetComponent<Image>() != null)
            return transform.GetComponent<Image>().color;
        else {
            return transform.GetComponent<Renderer>().material.color;
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
