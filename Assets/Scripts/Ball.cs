using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Ball : MonoBehaviour
{
    public Image sprite;

    public bool isHold;

    private void Awake()
    {
        sprite = GetComponent<Image>();
    }

    public void ChageSprite()
    {
        DOTween.Kill(transform);

        transform.DOScale(0, 0.2f).SetEase(Ease.Linear).OnComplete(()=>
        {
            sprite.sprite = GameManager.instance.tileSprites[Random.Range(0, GameManager.instance.tileSprites.Length)];
            transform.DOScale(0.5f, 0.1f).SetEase(Ease.Linear);
        });
        
    }

    public void HoldAnimation()
    {
        transform.DOScale(0.55f, 0.1f).SetEase(Ease.InBack);
        sprite.DOColor(new Color(0.5f, 0.5f, 0.5f), 0.1f);
        //sprite.color = new Color(0.5f, 0.5f, 0.5f);
    }

    public void EndAnimation()
    {
        sprite.color = new Color(1, 1, 1);

        transform.DOScale(0.5f, 0.2f).SetEase(Ease.Linear);

        sprite.DOColor(new Color(1, 1, 1), 0.2f);
    }

    public void FoundUpBalls()
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, Vector2.up);

        int a = 0;

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.gameObject == gameObject) continue;

            hit[i].collider.GetComponent<Image>().sprite = GameManager.instance.tileSprites[Random.Range(0, GameManager.instance.tileSprites.Length)];

            //hit[a].transform.DOScale(0, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
            //{
                
            //    hit[a].transform.DOScale(0.5f, 0.1f).SetEase(Ease.Linear);
            //});
            a++;
            Debug.Log(hit[i].collider.name);
        }
        //a = 0;
    }
}
