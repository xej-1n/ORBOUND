using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    [SerializeField] private Slider _hpSlider;

    public void SetHP(float currentHP, float maxHP)
    {
        _hpSlider.value = currentHP / maxHP; //적 체력바 설정
    }
}
