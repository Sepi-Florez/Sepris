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
        Debug.DrawRay(checkers[0].position, checkers[0].right, Color.blue);
    }
    public void SpawnNew() {
        Shape newShape = (Shape)Instantiate(shapes[next], spawnPos[Random.Range(0, spawnPos.Length)], Quaternion.identity).transform.GetChild(0).GetComponent<Shape>();
        next = Random.Range(0, shapes.Length);
        shape = newShape;
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
    public void CheckRow() {
        print("Checking Row");
        RaycastHit hit;
        List<Transform> hits = new List<Transform>();
        for (int a = 0; a < checkers.Length; a++) {
            if (Physics.Raycast(checkers[a].position, checkers[a].right, out hit, 0.6f)) {
                print("hit");
                if (hit.transform.tag == "bottom") {

                    print("bottom hit");
                    hits.Add(hit.transform);
                }
                else {
                    return;
                }
            }
        }
        if (hits.Count == 10) {

            for (int a = 0; a < hits.Count; a++) {
                hits[a].GetComponent<Animator>().SetTrigger("Kill");
                CheckUp(hits[a]);

            }

        }
    }
    public void CheckUp(Transform block) {
        RaycastHit hit;
        List<Transform> hits = new List<Transform>();
        hits.Add(block);
        for (int a = 0; a < hits.Count; a++) {
            if (Physics.Raycast(hits[a].position, transform.up, out hit, 0.6f)) {
                if (hit.transform.tag == "bottom") {
                    hits.Add(hit.transform);
                }
            }
        }
        Vector3 downVector = new Vector3(0, 0.5f, 0);
        for (int b = 0; b < hits.Count; b++) {
            hits[b].position -= downVector;
        }
    }

}