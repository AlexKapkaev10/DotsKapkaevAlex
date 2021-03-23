using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Ball : MonoBehaviour
{
    public Image sprite;

    public bool isHold;

    public int row, column;

    private void Awake()
    {
        sprite = GetComponent<Image>();
    }

    public void ChageSprite()
    {
        sprite.sprite = GameManager.instance.tileSprites[Random.Range(0, GameManager.instance.tileSprites.Length)];


    }

    public void HoldAnimation()
    {
        transform.DOScale(0.56f, 0.1f).SetEase(Ease.InBack);

        sprite.DOColor(new Color(0.5f, 0.5f, 0.5f), 0.1f);
    }

    public void EndAnimation()
    {
        sprite.color = new Color(1, 1, 1);

        transform.DOScale(0.5f, 0.1f).SetEase(Ease.Linear);

        sprite.DOColor(new Color(1, 1, 1), 0.2f);
    }

    public void BlowBalls()
    {
        transform.DOScale(0, 0.1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            transform.DOScale(0.5f, 0.1f).SetEase(Ease.Linear);

            GameManager.instance.MoveRow(column, row);
        });

        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, Vector2.up);

        int a = 1;
        int b = 1;

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.gameObject == gameObject) continue;

            hit[a].transform.DOScale(0, 0.05f).SetEase(Ease.Linear).SetDelay(i * 0.02f).OnComplete(() =>
            {
                hit[b].transform.DOScale(0.5f, 0.06f).SetDelay(i * 0.02f).SetEase(Ease.Linear);

                b++;
            });

            a++;
        }
    }
}
