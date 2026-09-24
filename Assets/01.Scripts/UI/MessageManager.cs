using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;

public class MessageManager : MonoBehaviour
{
    public static MessageManager instance { get; private set; }

    public TextMeshProUGUI messageText;
    public CanvasGroup canvasGroup;

    void Awake()
    {
        instance = this;
    }
    
    public void Open(string message, float duration = 1f)
    {
        StopAllCoroutines();
        StartCoroutine(ShowMessage(message, duration));
    }

    IEnumerator ShowMessage(string message, float duration)
    {
        messageText.text = message;
        canvasGroup.alpha = 0;
        canvasGroup.DOKill();
        canvasGroup.DOFade(1, 0.5f);

        yield return new WaitForSeconds(duration);

        Close();
    }

    public void Close()
    {
        canvasGroup.DOKill();
        canvasGroup.DOFade(0, 0.5f);
    }
}
