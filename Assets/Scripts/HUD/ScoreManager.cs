using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreManager : MonoBehaviour
{
    public int score;
    public float accuracy;
    public int remaining;
    [SerializeField] private int basePoints = 100;

    public void IncreaseScore(int streak)
    {
        float multiplier = Mathf.Min(1f + (streak * 0.1f), 3f);
        int pointsEarnet = Mathf.RoundToInt(streak > 0 ? basePoints * multiplier : 0);
        score += pointsEarnet;
    }
    public void SetAccuracy(int correctCounter, int incorrectCounter)
    {
        accuracy = Mathf.Round((correctCounter * 1f / (correctCounter + incorrectCounter) * 100) * 10) / 10;
    }

    public void SetRemaining(int counter, int total)
    {
        Debug.Log("total"+ total);
        Debug.Log("counter"+counter);
        remaining = total - counter;
    }
}
