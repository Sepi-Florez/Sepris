using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour {
    public float moveTime;
    public float fallTime;
    public float boostTime;

    bool move = true;
    bool bottom = false;
    public bool square = false;

    public List<Transform> blocks = new List<Transform>();

    Coroutine fall;

	void Start () {
        fall = StartCoroutine(Fall(fallTime));
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
    public void Boost (bool b) {
        if (b) {
            StopCoroutine(fall);
            fall = StartCoroutine(Fall(boostTime));
        }
        else {
            StopCoroutine(fall);
            fall = StartCoroutine(Fall(fallTime));
        }

    }   
    public IEnumerator Fall(float time) {
        yield return new WaitForSeconds(time);
        if (CheckFall())
            yield break;
        else {
            Down();
            fall = StartCoroutine(Fall(time));
        }

    }
    public bool CheckFall() {
        bool hit = false;
        if (!bottom) {

            RaycastHit rayHit;
            for (int a = 0; a < blocks.Count; a++) {
                if (Physics.Raycast(blocks[a].position, -transform.parent.transform.up, out rayHit, 0.6f)) {
                    if (rayHit.transform.tag == "bottom") {
                        hit = true;
                        TetrisManager.manager.shape = null;
                        TetrisManager.manager.SpawnNew();
                        for (int b = 0; b < blocks.Count; b++) {
                            blocks[b].tag = "bottom";

                        }
                        bottom = true;
                        TetrisManager.manager.CheckField();
                        StopAllCoroutines();
                        return hit;
                        

                    }
                }
            }
            
        }
        return hit;
    }
    public void Move(bool left) {
        if (move) {
            move = false;
            StartCoroutine(MoveDelay(moveTime));
            if (!CheckMove(left)) {
                if (left) {
                    Vector3 downVector = new Vector3(0.5f, 0, 0);
                    transform.parent.transform.position -= downVector;
                }
                else {
                    Vector3 downVector = new Vector3(-0.5f, 0, 0);
                    transform.parent.transform.position -= downVector;
                }
            }
        }
    }
    IEnumerator MoveDelay(float time) {
        yield return new WaitForSeconds(time);
        move = true;
    }
    public void Rotate() {
        if(!square)
            transform.Rotate(new Vector3(0, 0, 90));
        
    }
    public bool CheckMove(bool side) {
        bool hit = false;
        RaycastHit rayHit;
        Vector3 sideVector;
        if (side) {
            sideVector = -transform.parent.transform.right;
        }
        else {
            sideVector = transform.parent.transform.right;
        }
        for (int a = 0; a < blocks.Count; a++) {
            if (Physics.Raycast(blocks[a].position, sideVector, out rayHit, 0.6f)) {
                if (rayHit.transform.tag == "Side" || rayHit.transform.tag == "bottom") {
                    hit = true;

                }
            }
        }
        return hit;
    
    }
}
