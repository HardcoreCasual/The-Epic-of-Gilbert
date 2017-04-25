using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class endScript : MonoBehaviour {

    public GameObject endTitle, endButton;
    public Text scoreText;

    Animator cutSceneAnimator;
    AudioSource cutSceneSource;
    DataSaving Saving;
    SaveHighScore save;


    bool startAnim = false, playSound = false;

	void Start ()
    {
        cutSceneAnimator = GetComponent<Animator>();
        cutSceneSource = GetComponent<AudioSource>();

        Saving = new DataSaving();
        save = Saving.Load();

        scoreText.text = "Total Score\n------------\n" + Math.Round(save.OceanScore + save.SkyScore + save.SpaceScore);
    }

    public void toCredits()
    {
        SceneManager.LoadScene(7);
    }

    IEnumerator endGame()
    {
        yield return new WaitForSeconds(8);
        endTitle.SetActive(true);
        yield return new WaitForSeconds(2);
        scoreText.enabled = true;
        Saving.DeleteScores(true);
        yield return new WaitForSeconds(1);
        endButton.SetActive(true);
    }
	void LateUpdate ()
    {
		if(!startAnim &! playSound)
        {
            cutSceneAnimator.SetFloat("start", 1);
            cutSceneSource.Play();
            StartCoroutine(endGame());
            startAnim = true;
            playSound = true;
        }
	}
}
