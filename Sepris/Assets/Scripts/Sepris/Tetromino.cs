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

    void Awake() {
        fall = StartCoroutine(FallWait(fallTime));
        int a = 0;
        foreach (Transform childs in transform) {
            blocks.Add(transform.GetChild(a));
            if (Grid.grid[(int)blocks[a].position.x,(int)blocks[a].position.y] != null) {
                print("Game Over");
                GameManager.manager.gameOver = true;
                Destroy(gameObject);

            }
            a++;
        }
    }
    //moves tetronimo down
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
    //Wait time before checking for fall again
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
    //checks if falling is possible
    public bool CheckFall() {
        for(int b = 0; b < blocks.Count; b++) {
            if (blocks[b].position.y < Grid.h) {
                if (blocks[b].position.y == 0) {
                    
                    return false;
                }
                else if ( Grid.grid[(int)blocks[b].position.x, (int)blocks[b].position.y - 1] != null) {
                    print(blocks[b].position + "Blocked by " + Grid.grid[(int)blocks[b].position.x, (int)blocks[b].position.y - 1] + Grid.grid[(int)blocks[b].position.x, (int)blocks[b].position.y - 1].position);
                    return false;
                }
            }
        }
        return true;
    }
    //Moves the tetronimo left or right
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
    //Checks if you can move left or right
    public bool CheckMove(bool left) {
        for (int b = 0; b < blocks.Count; b++) {
            if (left) {
                if ((int)(blocks[b].position.x - 1) < 0) {
                    print("too far left" + blocks[b].position);
                    return false;
                }
                else if (Grid.grid[(int)(blocks[b].position.x - 1), (int)(blocks[b].position.y)] != null) {
                    print("blocked by blocks left");
                    return false;
                }

            }
            else {
                if (blocks[b].position.x + 1 > 9) {
                    print("too far right");
                    return false;
                }
                else if (Grid.grid[(int)(blocks[b].position.x + 1), (int)(blocks[b].position.y)] != null) {
                    print("blocked by blocks right");
                    return false;
                }
            } 
         }
        return true;
    }
    //A delay so movement feels more blocky
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
    // Checks if rotation is possible
    public void  CheckRotation() {
        for (int a = 0; a < blocks.Count; a++) {
            if (blocks[a].position.x > 9) {
                transform.parent.transform.position -= new Vector3(1, 0 , 0);

            }
            else if (blocks[a].position.x < 0) {
                transform.parent.transform.position += new Vector3(1, 0, 0);

            }
            else if (blocks[a].position.y < 0) {
                transform.parent.transform.position += new Vector3(0, 1, 0);
            }
            else if (blocks[a].position.y > 19) {
                transform.parent.transform.position += new Vector3(0, -1, 0);
            }
            else if (Grid.grid[(int)blocks[a].position.x, (int)blocks[a].position.y] != null) {
                transform.Rotate(new Vector3(0, 0, -90));
            }
        }
    }
    //Happens when falling is not possible. Will place the blocks in the grid and tell the GameManager to spawn the next block.
    public void BecomeBottom() {
        for (int a = 0; a < blocks.Count; a++) {

            Grid.grid[(int)blocks[a].position.x, (int)blocks[a].position.y] = blocks[a];
            print("Placed" + Grid.grid[(int)blocks[a].position.x, (int)blocks[a].position.y] + Grid.grid[(int)blocks[a].position.x, (int)blocks[a].position.y].position);
            
        }
        Grid.DeleteFullRows();

        if (!GameManager.manager.gameOver)
            GameManager.manager.SpawnNew();

    }
}
