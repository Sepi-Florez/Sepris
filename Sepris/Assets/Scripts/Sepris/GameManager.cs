using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager manager;
    public Tetromino tetro;
    public Vector3[] spawnPos;
    public GameObject[] tetros;

    public float moveTime;
    public float fallRate;
    public float fallTotal;
    public float boostRate;
    public float actionMargin;

    [SerializeField]
    private int next;

    public bool gameOver = false;

    void Awake() {
        manager = this;
    }
    void Start() {
        next = Random.Range(0, tetros.Length);
        SpawnNew();
    }
    void Update() {
        if(!gameOver)
            Controls();
    }
    public void SpawnNew() {
        
        tetro = (Tetromino)Instantiate(tetros[next], spawnPos[Random.Range(0, spawnPos.Length)], Quaternion.identity).transform.GetChild(0).GetComponent<Tetromino>();
        next = Random.Range(0, tetros.Length);
        //UIManager.UI.NextUpdate(next);
    }
    void Controls() {
        if (Input.GetButtonDown("Down")) {
            tetro.Boost(true);

        }
        if (Input.GetButtonUp("Down")) {
            tetro.Boost(false);
        }
        if (Input.GetButtonDown("Up")) {
            tetro.Rotate(true);
        }
        if (Input.GetButton("Left")) {
            tetro.Move(true);
        }
        else if (Input.GetButton("Right")) {
            tetro.Move(false);
        }
    }
}