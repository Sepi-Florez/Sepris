using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisManager : MonoBehaviour {
    public static TetrisManager manager;
    public Vector3[] spawnPos;
    public GameObject[] shapes;

    public GameObject shapeObj;

    void Start() {
        SpawnNew();
        manager = transform.gameObject.GetComponent<TetrisManager>();

    }
    void Update() {
        Controls();
    }
    public void SpawnNew() {
        GameObject shape = (GameObject)Instantiate(shapes[Random.Range(0, shapes.Length)], spawnPos[Random.Range(0, spawnPos.Length)], Quaternion.identity);
        shapeObj = shape;
    }
    void Controls() {
        if (Input.GetAxis("Vertical") < 0) {
            if (!shapeObj.transform.GetChild(0).GetComponent<Shape>().CheckFall())
                shapeObj.transform.GetChild(0).GetComponent<Shape>().Down();
        }
        if (Input.GetButtonDown("Up")) {
            shapeObj.transform.GetChild(0).GetComponent<Shape>().Rotate();

        }
        if (Input.GetButtonDown("Left")) {
            shapeObj.transform.GetChild(0).GetComponent<Shape>().Move(1);


        }
        else if (Input.GetButtonDown("Right")) {
            shapeObj.transform.GetChild(0).GetComponent<Shape>().Move(-1);

        }
    }

}