using System.Collections;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    private TextMeshProUGUI tmpText;       // For TextMeshPro
    private CanvasGroup canvasGroup;      // Used for fading out
    private float fadeDuration;

    private Vector3 originalPosition;

    public void Setup(string message, float fadeDuration)
    {
        this.fadeDuration = fadeDuration;

        tmpText = GetComponentInChildren<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (tmpText == null || canvasGroup == null)
        {
            Debug.LogError("Required components (TextMeshProUGUI, CanvasGroup) are missing.");
            return;
        }

        tmpText.text = message;
        originalPosition = transform.position;
        StartCoroutine(FadeOut());
    }

    public void PushUp(float offset)
    {
        transform.position += new Vector3(0, offset, 0);
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f;
        Destroy(gameObject);
    }
}
