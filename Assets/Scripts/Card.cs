using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Card : MonoBehaviour
{
    Animator anim;
    public int decknum;

    private float lastAnimTime = 0;


    CardManager cardManager;
    GameManager gameManager;
    AiManager aiManager;
    ChangeShape changeShape;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        aiManager = GameObject.Find("AiManager").GetComponent<AiManager>();
        changeShape = GameObject.Find("ChangeShape").GetComponent<ChangeShape>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.turn == true)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        if (gameManager.attack == true && gameManager.turn == true && gameManager.defence == false)
        {
            for (int i = 0; i < cardManager.cardCount; i++)
            {
                if (!(cardManager.showCardNum == 52 && (cardManager.myCardDeck[i].cardNumber == 53 || (cardManager.myCardDeck[i].cardNumber / 13 == 0 || cardManager.myCardDeck[i].cardNumber / 13 == 3) && (cardManager.myCardDeck[i].cardNumber % 13 == 0 || cardManager.myCardDeck[i].cardNumber % 13 == 1))))
                {
                    // 낸 카드가 흑조커 일 때, 덱 중 컬러조커 or 스페이드나 클로버 중 a or 2 있을 떄
                    gameManager.attackGetCard = true;
                }
                else if (!(cardManager.showCardNum == 53 && (cardManager.myCardDeck[i].cardNumber == 52 || (cardManager.myCardDeck[i].cardNumber / 13 == 1 || cardManager.myCardDeck[i].cardNumber / 13 == 2) && (cardManager.myCardDeck[i].cardNumber % 13 == 0 || cardManager.myCardDeck[i].cardNumber % 13 == 1))))
                {
                    // 낸 카드가 컬러조커 일 때, 댁 중 흑조커 or 다이아나 하트 중 a or 2 있을 떄
                    gameManager.attackGetCard = true;
                }
                else if (!(cardManager.showCardNum % 13 == 0 && ((cardManager.myCardDeck[i].cardNumber % 13 == 0 && cardManager.myCardDeck[i].cardNumber != 52) || (((cardManager.showCardNum / 13 == 0 || cardManager.showCardNum / 13 == 3) && cardManager.myCardDeck[i].cardNumber == 52) || ((cardManager.showCardNum / 13 == 1 || cardManager.showCardNum / 13 == 2) && cardManager.myCardDeck[i].cardNumber == 53)))))
                {
                    // 낸 카드가 a 일 때, 덱 중 a가 있거나 같은 색 조커가 있을 떄
                    gameManager.attackGetCard = true;
                }
                else if (!(cardManager.showCardNum % 13 == 1 && ((cardManager.myCardDeck[i].cardNumber % 13 == 1 && cardManager.myCardDeck[i].cardNumber != 53) || (((cardManager.showCardNum / 13 == 0 || cardManager.showCardNum / 13 == 3) && cardManager.myCardDeck[i].cardNumber == 52) || ((cardManager.showCardNum / 13 == 1 || cardManager.showCardNum / 13 == 2) && cardManager.myCardDeck[i].cardNumber == 53)))))
                {
                    // 낸 카드가 2 일 때, 덱 중 2가 있거나 같은 색 조커가 있을 때
                    gameManager.attackGetCard = true;
                }
                else if (cardManager.showCardNum / 13 == cardManager.myCardDeck[i].cardNumber / 13)
                {
                    if (!(cardManager.showCardNum % 13 == 0 && cardManager.myCardDeck[i].cardNumber % 13 == 1))
                    {
                        // 같은 모양일 때,  0일 때  1이 있는 경우
                        gameManager.attackGetCard = true;
                    }
                    else if (!(cardManager.showCardNum % 13 == 1 && cardManager.myCardDeck[i].cardNumber % 13 == 0))
                    {
                        // 같은 모양일 때, 1일 때 0이 있는 경우
                        gameManager.attackGetCard = true;
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(0) && cardManager.show == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider != null)
            {
                if (gameManager.attack == true && gameManager.turn == true && hit.collider.gameObject.name == "card" + decknum.ToString() && gameManager.startSetting == false)
                {
                    if (cardManager.showCardNum == 52 && (cardManager.myCardDeck[decknum - 1].cardNumber == 53 || (cardManager.myCardDeck[decknum - 1].cardNumber / 13 == 0 || cardManager.myCardDeck[decknum - 1].cardNumber / 13 == 3) && (cardManager.myCardDeck[decknum - 1].cardNumber % 13 == 0 || cardManager.myCardDeck[decknum - 1].cardNumber % 13 == 1)))
                    {
                        show();
                        attackCount();
                    }
                    else if (cardManager.showCardNum == 53 && (cardManager.myCardDeck[decknum - 1].cardNumber == 52 || (cardManager.myCardDeck[decknum - 1].cardNumber / 13 == 1 || cardManager.myCardDeck[decknum - 1].cardNumber / 13 == 2) && (cardManager.myCardDeck[decknum - 1].cardNumber % 13 == 0 || cardManager.myCardDeck[decknum - 1].cardNumber % 13 == 1)))
                    {
                        show();
                        attackCount();
                    }
                    else if (cardManager.showCardNum % 13 == 0 && cardManager.myCardDeck[decknum - 1].cardNumber % 13 == 0)
                    {
                        show();
                        attackCount();
                    }
                    else if (cardManager.showCardNum % 13 == 1 && cardManager.myCardDeck[decknum - 1].cardNumber % 13 == 1)
                    {
                        show();
                        attackCount();
                    }
                    else if (cardManager.showCardNum / 13 == cardManager.myCardDeck[decknum - 1].cardNumber / 13)
                    {
                        if (cardManager.showCardNum % 13 == 0 && cardManager.myCardDeck[decknum - 1].cardNumber % 13 == 1)
                        {
                            show();
                            attackCount();
                        }
                        else if (cardManager.showCardNum % 13 == 1 && cardManager.myCardDeck[decknum - 1].cardNumber % 13 == 1)
                        {
                            show();
                            attackCount();
                        }
                        else if (cardManager.myCardDeck[decknum - 1].cardNumber % 13 == 2)
                        {
                            show();
                            gameManager.attack = false;
                            gameManager.attackCount = 0; 
                            gameManager.attackGetCard = false;
                        }
                    }
                }
                if (hit.collider.gameObject.name == "card" + decknum.ToString() && gameManager.startSetting == false && gameManager.turn == true && gameManager.attack == false)
                {
                    if (cardManager.myCardDeck[decknum - 1].cardNumber == 52 && (cardManager.showCardNum / 13 == 0 || cardManager.showCardNum / 13 == 3))
                    {
                        show();
                    }
                    else if (cardManager.myCardDeck[decknum - 1].cardNumber == 53 && (cardManager.showCardNum / 13 == 1 || cardManager.showCardNum / 13 == 2))
                    {
                        show();
                    }

                    else
                    {
                        if (cardManager.showCardNum == 52 && (cardManager.myCardDeck[decknum - 1].cardNumber / 13 == 0 || cardManager.myCardDeck[decknum - 1].cardNumber / 13 == 3))
                        {
                            show();
                        }
                        else if (cardManager.showCardNum == 53 && (cardManager.myCardDeck[decknum - 1].cardNumber / 13 == 1 || cardManager.myCardDeck[decknum - 1].cardNumber / 13 == 2))
                        {
                            show();
                        }
                        else if (cardManager.showCardNum / 13 == cardManager.myCardDeck[decknum - 1].cardNumber / 13 || cardManager.showCardNum % 13 == cardManager.myCardDeck[decknum - 1].cardNumber % 13)
                        {
                            show();
                        }
                    }

                    if (cardManager.myCardDeck[decknum - 1].cardNumber % 13 == 0 || cardManager.myCardDeck[decknum - 1].cardNumber % 13 == 1 || cardManager.myCardDeck[decknum - 1].cardNumber == 52 || cardManager.myCardDeck[decknum - 1].cardNumber % 13 == 53)
                    {
                        gameManager.attack = true;
                        attackCount();
                    }
                }
            }
        }

        float animTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (animTime >= 1.0f && lastAnimTime < 1.0f && anim.GetCurrentAnimatorStateInfo(0).IsName("PutCard"))
        {
            cardManager.showCard.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            lastAnimTime = 1.0f;
            anim.SetInteger("show", 0);
            cardManager.myCardDeck[cardManager.cardCount-1].mycard.SetActive(false);
            if (cardManager.myCardDeck[decknum - 1].cardNumber % 13 == 6)
            {
                gameManager.changeShape = true;
                changeShape.selectShape.SetActive(true);
            }
            else if (cardManager.myCardDeck[decknum - 1].cardNumber % 13 != 10 && cardManager.myCardDeck[decknum - 1].cardNumber % 13 != 11 && cardManager.myCardDeck[decknum - 1].cardNumber % 13 != 12)
            {
                Invoke("nextTurn", 1f);
            }
            else if (cardManager.myCardDeck[decknum - 1].cardNumber % 13 == 10 || cardManager.myCardDeck[decknum - 1].cardNumber % 13 == 11 || cardManager.myCardDeck[decknum - 1].cardNumber % 13 == 12)
            {
                GameObject.Find("getCard").GetComponent<BoxCollider2D>().enabled = true;
            }
            for (int i = decknum-1; i < 20; i++)
            {
                cardManager.myCardDeck[i].cardNumber = cardManager.myCardDeck[i + 1].cardNumber;
                cardManager.myCardDeck[i].mycard.GetComponent<SpriteRenderer>().sprite = cardManager.myCardDeck[i + 1].mycard.GetComponent<SpriteRenderer>().sprite;
            }
            cardManager.show = true;
            gameManager.defence = false;
        }
        else
        {
            lastAnimTime = animTime;
        }
    }

    public void show()
    {
        anim.SetInteger("show", decknum);
        cardManager.cardCount--;
        cardManager.anim.SetInteger("getCard", cardManager.cardCount);
        cardManager.show = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        GameObject.Find("getCard").GetComponent<BoxCollider2D>().enabled = false;
        cardManager.showCardNum = cardManager.myCardDeck[decknum - 1].cardNumber;
    }

    public void attackCount()
    {
        gameManager.defence = true;
        if (cardManager.myCardDeck[decknum - 1].cardNumber == 52)
        {
            gameManager.attackCount += 5;
        }
        else if (cardManager.myCardDeck[decknum - 1].cardNumber == 53)
        {
            gameManager.attackCount += 7;
        }
        else if (cardManager.myCardDeck[decknum - 1].cardNumber % 13 == 0)
        {
            if (cardManager.myCardDeck[decknum - 1].cardNumber / 13 == 3)
            {
                gameManager.attackCount += 5;
            }
            else
            {
                gameManager.attackCount += 3;
            }
        }
        else if (cardManager.myCardDeck[decknum - 1].cardNumber % 13 == 1)
        {
            gameManager.attackCount += 2;
        }
    }

    public void nextTurn()
    {
        GameObject.Find("getCard").GetComponent<BoxCollider2D>().enabled = true;
        gameManager.turn = false;
        aiManager.checkCard = true;
    }
}
