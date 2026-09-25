using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class EndingManager : MonoBehaviour
{
    [Header("시간을 띄울 글자칸")]
    public TextMeshProUGUI timeText;

    void Start()
    {
        float totalTime = Time.realtimeSinceStartup;
        int min = Mathf.FloorToInt(totalTime / 60f);
        int sec = Mathf.FloorToInt(totalTime % 60f);

        timeText.text = "플레이 시간 : " + min + "분 " + sec + "초";
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}