using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int _currentScore = 0;
    public TextMeshProUGUI _scoreText;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
       
    }
    public void AddScore(int amount)
    {
        _currentScore += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if(_scoreText != null)
        {
            _scoreText.text = "Score: " + _currentScore;
        }
    }

}
