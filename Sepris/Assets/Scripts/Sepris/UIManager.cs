using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour {
    public static UIManager thisManager;

    public GameObject gameOverScreen;
    public Text gameOverScore;

    public Sprite[] sprites;

    public Text scoreText;
    public Image next;

    int i;


    int totalScore = 0;
    public float scoreIncreaseRate;
    public float scoreIncreaseTime;

    void Awake() {
        thisManager = this;
    }

    public void CallUpdateScore(int score,int i) {
        StartCoroutine(UpdateScore(score, i));
    }
    public void UpdateNext(int spriteIndex) {
        next.sprite = sprites[spriteIndex];
    }

    IEnumerator UpdateScore(int score, int i) {
        int ts = totalScore;
        while(totalScore < ts + score) {
            i++;
            totalScore = (int)(Mathf.MoveTowards(totalScore, totalScore + score, i * scoreIncreaseRate));
            if (totalScore > ts + score)
                totalScore = ts + score;
            scoreText.text = totalScore.ToString();
            yield return new WaitForSeconds(scoreIncreaseTime);
        }
    }
    public void GameOver() {
        gameOverScreen.SetActive(true);
        gameOverScore.text += totalScore;
    }
    public void Restart() {
        gameOverScreen.SetActive(false);
        gameOverScore.text = "Score : " ;
        totalScore = 0;
        scoreText.text = totalScore.ToString();
        while(!GameManager.manager.gameOver){

        }
    }
}
