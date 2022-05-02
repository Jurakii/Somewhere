using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float typewriterSpeed = 50f;

    public Coroutine Run(string textToType, TMP_Text textLabel)
    {
        return StartCoroutine(routine: TypeText(textToType, textLabel));
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        textLabel.text = string.Empty;

        float t = 0;
        int charindex = 0;

        while (charindex < textToType.Length)
        {
            t += Time.deltaTime * typewriterSpeed;
            charindex = Mathf.FloorToInt(t);
            charindex = Mathf.Clamp(value: charindex, min: 0, max: textToType.Length);

            textLabel.text = textToType.Substring(startIndex:0, length:charindex);

            yield return null;

            textLabel.text = textToType;
        }
    }
}
