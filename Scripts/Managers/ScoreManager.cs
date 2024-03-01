using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int score;

    public void AddScore(int value)
    {
        score += value;
        // Debug.Log(score);
    }

    public int GetScore()
    {
        return score;
    }

    public void SetScore(int value)
    {
        score = value;
    }
}
