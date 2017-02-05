using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour {
    public float moveTime;
    public float fallTime;
    public float boostTime;

    bool move = true;
    bool bottom = false;
    public bool square = false;

    public List<Transform> blocks = new List<Transform>();

    Coroutine fall;

    void Start() {
        fall = StartCoroutine(FallWait(fallTime));
        int a = 0;
        foreach (Transform childs in transform) {
            blocks.Add(transform.GetChild(a));
            a++;
        }
    }
    public void Fall() {
        transform.position += new Vector3(0, -1, 0);
    }
    public void Boost(bool b) {
        if (b) {
            StopCoroutine(fall);
            fall = StartCoroutine(FallWait(boostTime));
        }
        else {
            StopCoroutine(fall);
            fall = StartCoroutine(FallWait(fallTime));
        }

    }
    public IEnumerator FallWait(float time) {
        yield return new WaitForSeconds(time);
        if (!CheckFall()) {
            BecomeBottom();
            yield break;
        }
        else {
            Fall();
            fall = StartCoroutine(FallWait(time));
        }

    }
    public bool CheckFall() {
        for(int b = 1; b < blocks.Count; b++) {
            if (blocks[b].position.y < Grid.h) {
                if (Grid.grid[(int)blocks[b].position.x, (int)blocks[b - 1].position.y] != null || blocks[b].position.y == 0) {
                    return false;
                }
            }
        }
        return true;
    }
    public void Move(bool left) {
        if (move) {
            move = false;
            StartCoroutine(MoveDelay(moveTime));
            if (CheckMove(left)) {
                if (left) {
                    transform.parent.transform.position -= new Vector3(1, 0, 0);
                }
                else {
                    transform.parent.transform.position -= new Vector3(-1, 0, 0);
                }
            }
        }
    }
    public bool CheckMove(bool left) {
        for (int b = 1; b < blocks.Count; b++) {
            if (Grid.InsideBorder(blocks[b].position)) {
                if (left) {
                    if (blocks[b].position.x < 5 || Grid.grid[(int)blocks[b].position.x + 1,(int)blocks[b].position.y] != null) {
                        return false;
                    }

                }
                else {
                    if (blocks[b].position.x > -5 || Grid.grid[(int)blocks[b].position.x - 1, (int)blocks[b].position.y] != null) {
                        return false;
                    }
                } 
             }
         }
        return true;
    }
    IEnumerator MoveDelay(float time) {
        yield return new WaitForSeconds(time);
        move = true;
    }
    public void Rotate(bool turn) {
        if (!square) {
            if (turn) {
                transform.Rotate(new Vector3(0, 0, 90));
                CheckRotation();
                
            }
        }
    }
    public void  CheckRotation() {
        for (int a = 0; a < blocks.Count; a++) {
            if (blocks[a].position.x > 10) {
                transform.parent.transform.position += new Vector3(1, 0, 0);

            }
            else if (blocks[a].position.x < 1) {
                transform.parent.transform.position -= new Vector3(1, 0, 0);

            }
            else if (blocks[a].position.y < 0) {
                transform.parent.transform.position += new Vector3(0, 1, 0);
            }
            else if (Grid.grid[(int)blocks[a].position.x, (int)blocks[a].position.y] != null) {
                transform.Rotate(new Vector3(0, 0, -90));
            }
        }
    }
    public void BecomeBottom() {
        for (int a = 0; a < blocks.Count; a++) {
            Grid.grid[(int)blocks[a].position.x, (int)blocks[a].position.y] = blocks[a];
        }
    }
}
