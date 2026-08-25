using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _comboText;

    public void UpdateScore(int score)
    {
        _scoreText.text = $"SCORE {score}";
    }

    public void UpdateCombo(int combo)
    {
        _comboText.text = $"COMBO x{combo}";
    }

}
