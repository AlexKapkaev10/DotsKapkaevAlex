using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public AudioSource audioSource;

    public List<Image> allBalls;

    public Sprite[] tileSprites;

    public Text scoreTxt;

    private int score;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InitSprites();
    }

    private void InitSprites()
    {
        score = PlayerPrefs.GetInt("score");

        scoreTxt.text = "cчет " + score.ToString();

        foreach (var item in allBalls)
        {
            item.sprite = tileSprites[Random.Range(0, tileSprites.Length)];
        }
    }

    public void FinishSession(int addScore)
    {
        audioSource.Play();

        score += addScore;

        scoreTxt.text = "Счет " + score.ToString();

        PlayerPrefs.SetInt("score", score);
    }

}
