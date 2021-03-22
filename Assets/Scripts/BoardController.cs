using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardController : MonoBehaviour
{
    private string ballType;

    public List<Ball> foundBalls;

    public List<Collider2D> allFoundColliders;

    private bool isStartSession;

    private bool isCreatingNewBord;

    void Update()
    {
        if (Input.GetMouseButton(0) && !isCreatingNewBord)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.layer == 8)
                {
                    var foundedBall = hit.collider.GetComponent<Ball>();

                    if (foundedBall.isHold) return;

                    foundedBall.isHold = true;

                    allFoundColliders.Add(hit.collider);

                    StartGameSession(foundedBall);
                }
            }
            else
            {
                return;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            EndGameSession();

        }
    }

    private void StartGameSession(Ball ball)
    {
        if (!isStartSession)
        {
            isStartSession = true;

            ballType = ball.GetComponent<Image>().sprite.name;

            foundBalls.Add(ball);

            ball.HoldAnimation();

            ball.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            var ballName = ball.GetComponent<Image>().sprite.name;

            if (ballType == ballName && Vector2.Distance(foundBalls[foundBalls.Count - 1].transform.position, ball.gameObject.transform.position) < 1)
            {
                ball.gameObject.GetComponent<BoxCollider2D>().enabled = false;

                foundBalls.Add(ball);

                ball.HoldAnimation();
            }
        }
    }

    private void EndGameSession()
    {
        isCreatingNewBord = true;

        foreach (var item in allFoundColliders)
        {
            item.enabled = true;

            if(item.gameObject.layer == 8)
                item.GetComponent<Ball>().isHold = false;
        }

        foreach (var item in foundBalls)
        {
            item.EndAnimation();
        }

        if (foundBalls.Count >= 2)
        {
            foreach (var item in foundBalls)
            {
                item.FoundUpBalls();

                item.ChageSprite();

            }

            GameManager.instance.FinishSession(foundBalls.Count);
        }

        foundBalls.Clear();

        allFoundColliders.Clear();

        isStartSession = false;

        ballType = null;

        isCreatingNewBord = false;
    }
}
