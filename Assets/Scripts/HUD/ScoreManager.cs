using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score;
    [SerializeField] private int basePoints = 100;

    public void IncreaseScore(int streak)
    {
        float multiplier = Mathf.Min(1f + (streak * 0.1f), 3f);
        int pointsEarnet = Mathf.RoundToInt(basePoints * multiplier);
        score += pointsEarnet;
    }
}
