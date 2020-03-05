using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoseScreen : MonoBehaviour, ISaveable
{
    [SerializeField] Score score = null;
    [SerializeField] TextMeshProUGUI levelScore = null;
    [SerializeField] TextMeshProUGUI bestScore = null;

    int best = 0;

    public object CaptureState()
    {
        return best;
    }

    public void RestoreState(object state)
    {
        best = (int)state;
    }

    private void Awake()
    {
        levelScore.text = score.currentScore.ToString();
    }

    public void HighScore()
    {
        best = score.currentScore > best ? score.currentScore : best;
        bestScore.text = best.ToString();
    }
}
