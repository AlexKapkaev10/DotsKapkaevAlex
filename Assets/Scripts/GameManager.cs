using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public AudioSource audioSource;

    public Transform GameBoard;

    public Ball[,] allBalls;

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

        Ball[] allImages = GameBoard.GetComponentsInChildren<Ball>();

        int imageIndex = 0;

        allBalls = new Ball[6, 6];

        for (int row = 0;row < 6;row++)
        {
            for (int column = 0; column < 6;column++)
            {
                allBalls[column, row] = allImages[imageIndex];

                allBalls[column, row].column = column;

                allBalls[column, row].row = row;

                imageIndex++;
                
            }
        }

        foreach (var item in allBalls)
        {
            item.sprite.sprite = tileSprites[Random.Range(0, tileSprites.Length)];
        }
    }

    public void MoveRow(int ballColumn,int ballRaw)
    {
        List<Sprite> ballSprites = new List<Sprite>();

        int spriteIndex = 0;

        for (int row = 0; row < ballRaw; row++)
        {
            ballSprites.Add(allBalls[ballColumn, row].sprite.sprite);

            spriteIndex++;
        }

        spriteIndex = 0;

        for (int row = 0; row < ballRaw; row++)
        {
            allBalls[ballColumn, row + 1].sprite.sprite = ballSprites[spriteIndex];

            spriteIndex++;
        }

        allBalls[ballColumn, 0].ChageSprite();
    }

    public void FinishSession(int addScore)
    {
        audioSource.Play();

        score += addScore;

        scoreTxt.text = "Счет " + score.ToString();

        PlayerPrefs.SetInt("score", score);
    }

    public void OnDisable()
    {
        DOTween.KillAll();
    }

}
