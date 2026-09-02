using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Player UI")]
    [SerializeField] private Slider _playerHpBar;

    [Header("Score UI")]
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _multiplierText;
    [SerializeField] private TMP_Text _gradeText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void SetPlayerHP(float current, float max)
    {
        _playerHpBar.value = current / max;
    }

    public void SetScore(int score)
    {
        _scoreText.text = score.ToString();
    }

    public void SetMultiplier(float multiplier)
    {
        _multiplierText.text = $"x{multiplier:0.0}";
    }

    public void SetGrade(string grade)
    {
        _gradeText.text = grade;
    }
}