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

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        aiManager = GameObject.Find("AiManager").GetComponent<AiManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && cardManager.show == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.name == "card"+decknum.ToString() && gameManager.startSetting == false && gameManager.turn ==  true)
                {
                    if (cardManager.myCardDeck[decknum-1].cardNumber == 52 && (cardManager.showCardNum / 13 == 0 || cardManager.showCardNum / 13 == 3))
                    {
                        show();
                    }
                    else if(cardManager.myCardDeck[decknum - 1].cardNumber == 53 && (cardManager.showCardNum / 13 == 1 || cardManager.showCardNum / 13 == 2))
                    {
                        show();
                    }
                    else
                    {
                        if (cardManager.showCardNum == 52 && (cardManager.myCardDeck[decknum - 1].cardNumber/13==0 || cardManager.myCardDeck[decknum - 1].cardNumber / 13 == 3))
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
            if (cardManager.myCardDeck[decknum - 1].cardNumber % 13 != 10 && cardManager.myCardDeck[decknum - 1].cardNumber % 13 != 11 && cardManager.myCardDeck[decknum - 1].cardNumber % 13 != 12)
            {
                Invoke("nextTurn", 1f);
            }
            for (int i = decknum-1; i < 20; i++)
            {
                cardManager.myCardDeck[i].cardNumber = cardManager.myCardDeck[i + 1].cardNumber;
                cardManager.myCardDeck[i].mycard.GetComponent<SpriteRenderer>().sprite = cardManager.myCardDeck[i + 1].mycard.GetComponent<SpriteRenderer>().sprite;
            }
            cardManager.show = true;
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
        cardManager.showCardNum = cardManager.myCardDeck[decknum - 1].cardNumber;
    }

    public void nextTurn()
    {
        gameManager.turn = false;
        aiManager.checkCard = true;
    }
}
