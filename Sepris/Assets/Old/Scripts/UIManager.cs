using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public static UIManager UI;
    public GameObject[] shapes;
    GameObject currentNext;
    public Transform posNext; 
	void Start () {
        UI = this;
	}
    public void NextUpdate(int next) {
        Destroy(currentNext);

        currentNext = (GameObject)Instantiate(shapes[next], posNext.position, Quaternion.identity);
        currentNext.transform.position -= new Vector3(0, 0, 3);
        

    }
}
