using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int currentScore = 0;

    [SerializeField]
    private GameMenu gameMenu;

    public void AddScore(int score)
    {
        currentScore += score;
        gameMenu.SetScoreCount(currentScore);
    }

    public void BreakScore()
    {
        currentScore = 0;
        gameMenu.SetScoreCount(currentScore);
    }
}
