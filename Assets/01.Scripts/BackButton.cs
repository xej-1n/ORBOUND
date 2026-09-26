using UnityEngine;

public class BackButton : MonoBehaviour
{
    public void Back()
    {
        SoundManager.Instance.PlayClickSFX();
        GameSaveManager.Instance.LoadPreviousScene();
    }
}
