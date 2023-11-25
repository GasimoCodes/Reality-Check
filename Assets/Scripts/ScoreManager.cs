using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Cinemachine;
using System.Collections.Generic;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance;

    public int maximumStars = 0;
    public int currentStars = 0;

    public TextMeshProUGUI scoreText;
    public AudioSource pickupSound;
    public AudioClip scoreSound;

    public string[] messages;

    public List<GameObject> stars = new List<GameObject>();

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("ScoreManager instance is null.");
            }
            return instance;
        }
    }

    public void Start()
    {
        scoreText.text = currentStars + " / " + maximumStars;
    }

    public void registerStar(GameObject star)
    {
        stars.Add(star);
        maximumStars++;

        scoreText.text = currentStars + " / " + maximumStars;
    }

    public void collectStar(GameObject star)
    {

        stars.Remove(star);
        currentStars++;
        star.SetActive(false);
        pickupSound.PlayOneShot(scoreSound);

        scoreText.transform.parent.DOShakePosition(0.5f, 40, 100, 90, true);
        // scoreText.transform.parent.DOShakeScale(0.5f, 1, 100);
        scoreText.text = currentStars + " / " + maximumStars;

        if (currentStars != maximumStars)
            PlayerMessage.Instance.SetMessage(messages[UnityEngine.Random.Range(0, messages.Length)]);
        else
            PlayerMessage.Instance.SetMessage("Thats all of them!");


    }
}
