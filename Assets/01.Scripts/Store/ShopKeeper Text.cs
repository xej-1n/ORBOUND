using System.Collections;
using TMPro;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    [SerializeField] private GameObject speechBubble;
    [SerializeField] private TMP_Text speechText;
    private Coroutine messageCoroutine;

    private void Start()
    {
        HideMessage();
    }

    public void ShowMessage(string message)
    {
        speechText.text = message;
        speechBubble.SetActive(true);

        if (messageCoroutine != null)
            StopCoroutine(messageCoroutine);

        messageCoroutine = StartCoroutine(HideMessageAfterDelay());
    }

    private IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        HideMessage();
        messageCoroutine = null;
    }

    public void HideMessage()
    {
        speechBubble.SetActive(false);
    }
}