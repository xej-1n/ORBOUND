using UnityEngine;

public class BackButton : MonoBehaviour
{
    public void Back()
    {
        GameSaveManager.Instance.LoadPreviousScene();
    }
}
