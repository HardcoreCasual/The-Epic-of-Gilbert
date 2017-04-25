using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
//using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour
{
    /// <summary>
    /// FISH
    /// </summary>
    public GameObject BubbleFollowsThis;

    /// <summary>
    /// Offset from what it is follow, unless you want it underneath
    /// </summary>
    public Vector3 PositionOffset;

    /// <summary>
    /// The actual Text
    /// </summary>
    public string TextInBubble;

    /// <summary>
    /// Set the text of the reference using TextInBubble
    /// </summary>
    public Text TextBubbleReference;

    /// <summary>
    /// How fast before the next character appears in the bubble
    /// </summary>
    public float SpeedOfText;

    /// <summary>
    /// How long until the entire bubble disappears or whatever
    /// </summary>
    public float Seconds;

    public GameObject fishfood, bird, tutorial;
    public AudioClip foodSound, textChime;
    public int nextLevel;
    AudioSource source;

    /// <summary>
    /// Change whats in the text box immediately
    /// </summary>
    /// <param name="text"></param>
    /// <param name="characterSpeed"></param>
    /// <param name="howLongTestStays"></param>
    public void ChangeTextBubble(string text, float characterSpeed, float howLongTestStays)
    {
        Seconds = howLongTestStays;
        SpeedOfText = characterSpeed;
        TextInBubble = text;
        TextBubbleReference.text = TextInBubble;
    }

    /// <summary>
    /// Launch this to make the bubble stay for a few seconds and say some text then disappear!
    /// </summary>
    /// <param name="seconds"></param>
    /// <param name="text"></param>
    /// <param name="speedOfText"></param>
    /// <returns></returns>
    IEnumerator SetTextFor(float seconds, string text, float speedOfText)
    {
        TextBubbleReference.text = "";
        Stopwatch timer = Stopwatch.StartNew();
        int characterCounter = 0;
        while (true)
        {
            if (characterCounter < text.Length - 1)
            {
                TextBubbleReference.text += text[characterCounter++];
                //UnityEngine.Debug.Log(text[characterCounter]);
            }
            yield return new WaitForSeconds(speedOfText);
            if (timer.Elapsed.TotalSeconds > seconds)
            {
                break;
            }
        }
        TextBubbleReference.text = "";
    }

    public void startSetText(float sec, string tex, float speed)
    {
        StartCoroutine(SetTextFor(sec, tex, speed));
    }


    void Start()
    {
        //StartCoroutine(SetTextFor(Seconds, TextInBubble, Math.Abs(SpeedOfText)));

        source = GetComponent<AudioSource>();

        if (nextLevel == 1)
            StartCoroutine(pauseStart());
        else if (nextLevel == 2)
            StartCoroutine(sceneTwo());
        else
            StartCoroutine(sceneThree());


    }

    IEnumerator pauseStart()
    {
        while (!(Input.GetKey(KeyCode.W)) && !(Input.GetKey(KeyCode.A)) && !(Input.GetKey(KeyCode.S)) && !(Input.GetKey(KeyCode.D)))
        {
            yield return null;
        }
        tutorial.SetActive(false);
        StartCoroutine(sceneOne());
    }
    IEnumerator sceneOne()
    {
        yield return new WaitForSeconds(3f);
        source.clip = textChime;
        source.Play();
        StartCoroutine(SetTextFor(5, "Gilbert is in\nhis bowl. ", Math.Abs(SpeedOfText)));
        yield return new WaitForSeconds(10f);
        source.Play();
        StartCoroutine(SetTextFor(5, "Gilbert's bowl\nis very small. ", Math.Abs(SpeedOfText)));
        yield return new WaitForSeconds(10f);
        source.Play();
        StartCoroutine(SetTextFor(7, "Gilbert ponders\nhis days\nin the ocean. ", Math.Abs(SpeedOfText)));
        yield return new WaitForSeconds(12f);
        source.Play();
        //start feeding
        StartCoroutine(feedFish());
    }

    IEnumerator sceneTwo()
    {
        yield return new WaitForSeconds(3);
        source.clip = textChime;
        source.Play();
        StartCoroutine(SetTextFor(7, "Gilbert wakes up\nin his bowl. ", Math.Abs(SpeedOfText)));
        yield return new WaitForSeconds(10f);
        source.Play();
        StartCoroutine(SetTextFor(5, "Gilbert wonders\nhow long he will\nbe in this bowl. ", Math.Abs(SpeedOfText)));
        yield return new WaitForSeconds(10f);
        source.Play();
        StartCoroutine(SetTextFor(5, "Gilbert notices a\nbird flying\noutside. ", Math.Abs(SpeedOfText)));
        yield return new WaitForSeconds(2f);
        bird.SetActive(true);
        yield return new WaitForSeconds(8f);
        source.Play();
        //start feeding
        StartCoroutine(feedFish());

    }

    IEnumerator sceneThree()
    {
        yield return new WaitForSeconds(3);
        source.clip = textChime;
        source.Play();
        StartCoroutine(SetTextFor(5, "Gilbert wakes up,\nstill in his\nbowl. ", Math.Abs(SpeedOfText)));
        yield return new WaitForSeconds(12f);
        source.Play();
        StartCoroutine(SetTextFor(7, "Gilbert ponders\nwhat might exist\nbeyond his bowl. ", Math.Abs(SpeedOfText)));
        yield return new WaitForSeconds(12f);
        source.Play();
        //start feeding
        StartCoroutine(feedFish());
    }

    IEnumerator feedFish()
    {
        source.clip = foodSound;
        source.Play();
        yield return new WaitForSeconds(1);
        Instantiate(fishfood, new Vector3(0, 6, 0), Quaternion.identity);
    }

    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.G))
        //{
        //	StartCoroutine(SetTextFor(Seconds, TextInBubble, Math.Abs(SpeedOfText)));
        //}
        //transform.position = PositionOffset + BubbleFollowsThis.transform.position;
    }
}
