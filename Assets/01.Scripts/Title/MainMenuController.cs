using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("Stage 1");
    }

    public void OnExitButtonClicked()
    {
        Debug.Log("나가기 버튼 클릭됨 - 게임 종료");
        Application.Quit();
    }
}