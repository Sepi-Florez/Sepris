using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisManager : MonoBehaviour {
    public static TetrisManager manager;
    public Shape shape;
    public Vector3[] spawnPos;
    public GameObject[] shapes;
    public Transform[] checkers;

    public int next;

    void Start() {
        SpawnNew();
        next = Random.Range(0, shapes.Length);
        manager = this;

    }
    void Update() {
        Controls();
    }
    public void SpawnNew() {
        Shape newShape = (Shape)Instantiate(shapes[next], spawnPos[Random.Range(0, spawnPos.Length)], Quaternion.identity).transform.GetChild(0).GetComponent<Shape>();
        next = Random.Range(0, shapes.Length);
        shape = newShape;
        //UIManager.UI.NextUpdate(next);
    }
    void Controls() {
        if (Input.GetButtonDown("Down")){
            shape.Boost(true);

        }
        if (Input.GetButtonUp("Down")) {
            shape.Boost(false);
        }
        if (Input.GetButtonDown("Up")) {
            shape.Rotate();
        }
        if (Input.GetButton("Left")) {
            shape.Move(true);
        }
        else if (Input.GetButton("Right")) {
            shape.Move(false);
        }
    }
    public void CheckField() {

    }
}