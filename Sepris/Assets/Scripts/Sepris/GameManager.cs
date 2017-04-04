using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager manager;
    Tetromino tetro;
    public Vector3[] spawnPos;
    public GameObject[] tetros;

    public List<GameObject> spawnedTetros = new List<GameObject>();

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
        else {
            if (Input.GetButtonDown("Jump")) {
                Restart();
            }
        }
    }
    // spawns a new tetronimo and also decided what shape will be next
    public void SpawnNew() {
        tetro = (Tetromino)Instantiate(tetros[next], spawnPos[Random.Range(0, spawnPos.Length)], Quaternion.identity).transform.GetChild(0).GetComponent<Tetromino>();
        spawnedTetros.Add(tetro.gameObject);
        next = Random.Range(0, tetros.Length);
        UIManager.thisManager.UpdateNext(next);
    }
    // for all neccesery input
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
    void Restart() {
        UIManager.thisManager.Restart();
        Grid.thisGrid.Restart();
        for(int a = 0; a < spawnedTetros.Count; a++) {
            Destroy(spawnedTetros[a]);
        }
        gameOver = false;
        SpawnNew();

    }
}