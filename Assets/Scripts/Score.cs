using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] Ninja ninja = null;
    [SerializeField] GameObject extraPointText = null;
    [SerializeField] float extraPointTime = 1f;

    TextMeshProUGUI text;

    public int currentScore { get; private set; } = 0;

    private void OnEnable()
    {
        Ninja.onLand += AddScore;
    }

    private void OnDisable()
    {
        Ninja.onLand -= AddScore;
    }

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void AddScore()
    {
        AddScore(ninja.GetColumnIndexDifference());
    }

    private void AddScore(int columnJumped)
    {
        int added = 1;
        if (columnJumped > 1)
        {
            for(int i = 1; i < columnJumped; i++)
            {
                added += 4 - i;
            }
            StartCoroutine(ExtraPoint(added));
        }
        currentScore += added;
        text.text = currentScore.ToString();
    }

    private IEnumerator ExtraPoint(int point)
    {
        extraPointText.SetActive(true);
        extraPointText.GetComponent<TextMeshProUGUI>().text = "+" + point.ToString();
        yield return new WaitForSeconds(extraPointTime);
        extraPointText.SetActive(false);
    }

}
