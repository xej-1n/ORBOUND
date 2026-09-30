using UnityEngine;
using TMPro;
using DG.Tweening;

public class DamageText : MonoBehaviour
{
    public TextMeshProUGUI damageText;

    void Start()
    {
        Color color = damageText.color;
        color.a = 0f;
        damageText.color = color;

        damageText.DOFade(1.0f, 0.5f);
        damageText.transform.DOLocalMoveY(0, 0.5f);
        damageText.DOFade(0.0f, 0.5f).SetDelay(1.5f).OnComplete(() => Destroy(gameObject));
    }
}
