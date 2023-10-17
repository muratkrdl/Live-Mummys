using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI multiplierText;
    [SerializeField] FloatScoreText floatingTextPrefab;
    [SerializeField] Canvas floatingScoreCanvas;

    int score = 0;
    int highScore = 0;

    float scoreMultiplierExpiration;
    int killMultiplier;

    void Start() 
    {
        Mummy.Died += Mummy_Died;
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "HIGH SCORE: " + highScore;
    }

    void OnDestroy() 
    {
        Mummy.Died -= Mummy_Died; 
    }

    void Mummy_Died(Mummy mummy)
    {
        UpdateKillMultiplier();

        score += killMultiplier;

        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = "HIGH SCORE: " + highScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        scoreText.text = score.ToString();

        var floatingText = Instantiate(
            floatingTextPrefab,
            mummy.transform.position,
            floatingScoreCanvas.transform.rotation,
            floatingScoreCanvas.transform);

        floatingText.SetScoreValue(killMultiplier);
    }

    private void UpdateKillMultiplier()
    {
        if (Time.time <= scoreMultiplierExpiration)
        {
            killMultiplier++;
        }
        else
        {
            killMultiplier = 1;
        }
        scoreMultiplierExpiration = Time.time + 1f;

        multiplierText.text = "X" + killMultiplier.ToString();
        if (killMultiplier < 3)
            multiplierText.color = Color.white;
        else if(killMultiplier < 7)
            multiplierText.color = Color.green;
        else if(killMultiplier < 11)
            multiplierText.color = Color.yellow;
        else if(killMultiplier < 17)
            multiplierText.color = new Color(255f, 155f, 0f);
        else if(killMultiplier < 30)
            multiplierText.color = Color.red;
    }
}
