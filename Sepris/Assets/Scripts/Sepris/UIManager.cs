using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour {
    public static UIManager thisManager;

    public Text scoreText;

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
}
