using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        SoundManager.Instance.PlayClickSFX();
        SceneManager.LoadScene("Stage 1");
    }
    public void OnContinueButtonClicked()
    {
        SoundManager.Instance.PlayClickSFX();
        if (!GameSaveManager.Instance.HasSave())
        {
            Debug.Log("저장된 게임이 없습니다.");
            return;
        }

        GameSaveManager.Instance.Load();
        SceneManager.LoadScene("Stage 1");
    }
    public void OnExitButtonClicked()
    {
        SoundManager.Instance.PlayClickSFX();
        Debug.Log("나가기 버튼 클릭됨 - 게임 종료");
        Application.Quit();
    }
}