using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextManager : MonoBehaviour
{
    private Character character;

    public GameObject floatingTextPrefab; // Assign a prefab with a TextMeshPro component in the Inspector
    public float fadeDuration = 2f;       // Duration for the text to fade out
    public float verticalOffset = 25f;    // Vertical offset for stacked texts

    private List<FloatingText> activeTexts = new List<FloatingText>();

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void OnEnable()
    {
        character.OnTakeDamage += OnTakeDamage;
    }
    
    private void OnDisable()
    {
        character.OnTakeDamage -= OnTakeDamage;
    }



    public void ShowText(string message, Vector3 position, Transform parent)
    {
        GameObject textObject = Instantiate(floatingTextPrefab, position, Quaternion.identity, parent);

        // Ensure text is properly initialized
        FloatingText floatingText = textObject.GetComponent<FloatingText>();
        if (floatingText == null)
        {
            Debug.LogError("FloatingText component is missing from prefab.");
            Destroy(textObject);
            return;
        }

        floatingText.Setup(message, fadeDuration);

        // Stack active texts and push them up
        foreach (var text in activeTexts)
        {
            text.PushUp(verticalOffset);
        }

        activeTexts.Add(floatingText);
        StartCoroutine(RemoveAfterFade(floatingText));
    }

    private IEnumerator RemoveAfterFade(FloatingText floatingText)
    {
        yield return new WaitForSeconds(fadeDuration);
        activeTexts.Remove(floatingText);
    }

    private void OnTakeDamage(int damage) => ShowText(damage.ToString(), new Vector3(), character.transform);
}