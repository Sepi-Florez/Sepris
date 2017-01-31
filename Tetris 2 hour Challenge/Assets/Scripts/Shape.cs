using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour {
    public TetrisManager manager;
    public float fallTime;
    public List<Transform> blocks = new List<Transform>();
	void Start () {
        StartCoroutine(Fall(fallTime));
        int a = 0;
        foreach (Transform childs in transform) {
            blocks.Add(transform.GetChild(a));
            a++;
        }
	}
    void Update() {
        Debug.DrawRay(blocks[0].position, -transform.parent.transform.up, Color.red);
    }
	public void Down() {
        Vector3 downVector = new Vector3(0, 0.5f, 0);
        transform.position -= downVector;
    }
    IEnumerator Fall(float time) {
        yield return new WaitForSeconds(time);
        if (CheckFall())
            yield break;
        else {
            Down();
            StartCoroutine(Fall(fallTime));
        }

    }
    public bool CheckFall() {
        bool hit = false;
        RaycastHit rayHit;
        for(int a = 0; a < blocks.Count; a++) {
            if (Physics.Raycast(blocks[a].position, -transform.parent.transform.up,out rayHit, 0.6f)){
                if(rayHit.transform.tag == "bottom") {
                    hit = true;
                    

                }
            }
        }
        return hit;
    }
    public void Move(int side) {
        if (!CheckMove(side)) { 
            if (side == 1) {
                Vector3 downVector = new Vector3(0.5f, 0, 0);
                transform.parent.transform.position -= downVector;
            }
            else {
                Vector3 downVector = new Vector3(-0.5f, 0, 0);
                transform.parent.transform.position -= downVector;
            }
        }
    }
    public void Rotate() {
        transform.Rotate(new Vector3(0, 0, 90));
    }
    public bool CheckMove(int side) {
        bool hit = false;
        RaycastHit rayHit;
        Vector3 sideVector;
        if(side == 1) {
            sideVector = -transform.parent.transform.right;
        }
        else {
            sideVector = transform.parent.transform.right;
        }
        for (int a = 0; a < blocks.Count; a++) {
            if (Physics.Raycast(blocks[a].position, sideVector , out rayHit, 0.6f)) {
                if (rayHit.transform.tag == "Side") {
                    hit = true;
                    manager.SpawnNew();
                }
            }
        }
        return hit;
    }
}
