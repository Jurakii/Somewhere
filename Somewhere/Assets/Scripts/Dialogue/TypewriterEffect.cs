using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float typewriterSpeed = 50f;
    [SerializeField] private float letterSoundDelay = 0.05f; // Adjust this value to control the delay between each letter sound

    public bool IsRunning { get; private set; }

    private readonly List<Punctuation> punctuations = new List<Punctuation>()
    {
        new Punctuation(new HashSet<char>() {'.', '!', '?'}, 0.6f),
        new Punctuation(new HashSet<char>() {',', ';', ':'}, 0.3f)
    };

    private Coroutine typingCoroutine;
    private TMP_Text textLabel;

    private string textToType;

    // Reference to the SFX audio source from DialogueUI
    private AudioSource SFX;

    public void Run(string textToType, TMP_Text textLabel, AudioSource sfx)
    {
        this.textToType = textToType;
        this.textLabel = textLabel;
        this.SFX = sfx; // Assign the SFX audio source

        typingCoroutine = StartCoroutine(TypeText());
    }

    public void Stop()
    {
        if (!IsRunning) return;

        StopCoroutine(typingCoroutine);
        OnTypingCompleted();
    }

    private IEnumerator TypeText()
    {
        IsRunning = true;

        textLabel.maxVisibleCharacters = 0;
        textLabel.text = textToType;

        float t = 0;
        int charIndex = 0;

        while (charIndex < textToType.Length)
        {
            int lastCharIndex = charIndex;

            t += Time.deltaTime * typewriterSpeed;

            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            for (int i = lastCharIndex; i < charIndex; i++)
            {
                bool isLast = i >= textToType.Length - 1;

                textLabel.maxVisibleCharacters = i + 1;

                if (IsPunctuation(textToType[i], out float waitTime) && !isLast && !IsPunctuation(textToType[i + 1], out _))
                {
                    yield return new WaitForSeconds(waitTime);
                }

                // Play SFX sound for each typed letter (if SFX is not null)
                if (SFX != null)
                {
                    SFX.PlayOneShot(SFX.clip);
                    yield return new WaitForSeconds(letterSoundDelay);
                }
            }

            yield return null;
        }

        OnTypingCompleted();
    }

    private void OnTypingCompleted()
    {
        IsRunning = false;
        textLabel.maxVisibleCharacters = textToType.Length;
    }

    private bool IsPunctuation(char character, out float waitTime)
    {
        foreach (Punctuation punctuationCategory in punctuations)
        {
            if (punctuationCategory.Punctuations.Contains(character))
            {
                waitTime = punctuationCategory.WaitTime;
                return true;
            }
        }

        waitTime = default;
        return false;
    }

    private readonly struct Punctuation
    {
        public readonly HashSet<char> Punctuations;
        public readonly float WaitTime;

        public Punctuation(HashSet<char> punctuations, float waitTime)
        {
            Punctuations = punctuations;
            WaitTime = waitTime;
        }
    }
}
