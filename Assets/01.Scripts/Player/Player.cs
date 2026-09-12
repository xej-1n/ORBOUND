using UnityEngine;

public class Player : MonoBehaviour
{
    public int _maxHp;
    private int _hp;
    public int _attack; //임시, 무기나 핀볼 완료시 수정
    public int _defense;
    private void Start()
    {
        _hp = _maxHp;
    }

    public void Damage(int enemyAtk)
    {
        int temp;
        temp = enemyAtk - _defense;
        if (temp > 0)
            _hp = temp;
    }
}