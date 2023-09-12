using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    private int score;

    // Update is called once per frame
    public void IncrementScore()
    {
        score = score + 1;
        scoreText.text = score.ToString();
    }

    public void Reset()
    {
        score = 0;
        scoreText.text = score.ToString();
    }
}
