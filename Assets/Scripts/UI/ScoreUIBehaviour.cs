using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreUIBehaviour : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        ObjectiveManager.OnScoreChange += UpdateScore;
    }

    private void OnDisable()
    {
        ObjectiveManager.OnScoreChange -= UpdateScore;
    }

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
