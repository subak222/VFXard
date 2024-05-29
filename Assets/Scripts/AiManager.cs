using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AiManager : MonoBehaviour
{
    [Serializable]
    public struct Card
    {
        public int cardNumber;
        public GameObject mycard;
    }

    [Serializable]
    public struct ShowCard
    {
        public int deckNum;
        public int spriteNum;

        public ShowCard(int deckNum, int spriteNum)
        {
            this.deckNum = deckNum;
            this.spriteNum = spriteNum;
        }
    }

    CardManager cardManager;
    GameManager gameManager;
    public AiShow[] aishows;

    public Card[] aiCardDeck;

    public List<ShowCard> showCardList = new List<ShowCard>();

    public Animator anim;

    public int aiCardCount = 1;
    public int showCount = 0;
    private float lastAnimTime = 0f;
    public bool checkCard = true;

    void Start()
    {
        cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("AiGetCard", aiCardCount);
        float animTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (animTime >= 1.0f && lastAnimTime < 1.0f && !anim.GetCurrentAnimatorStateInfo(0).IsName("AiCardIdle"))
        {
            lastAnimTime = 1.0f;
            anim.SetBool("AiNextAnim", false);
            int deck = cardManager.array[0];
            cardManager.array.RemoveAt(0);
            aiCardDeck[aiCardCount - 1].mycard.GetComponent<SpriteRenderer>().sprite = cardManager.cardSprites[deck];
            aiCardDeck[aiCardCount - 1].cardNumber = deck;
            aiCardDeck[aiCardCount - 1].mycard.SetActive(true);
            aiCardCount++;
            anim.SetInteger("AiGetCard", aiCardCount);
            Invoke("nextTurn", 1f);
        }
        else
        {
            lastAnimTime = animTime;
        }

        if (gameManager.turn == false && gameManager.startSetting == false && checkCard == true)
        {
            checkCard = false;
            for (int i = 0; i < aiCardDeck.Length; i++)
            {
                if (aiCardDeck[i].cardNumber != 0)
                {
                    if (aiCardDeck[i].cardNumber == 52 && (cardManager.showCardNum / 13 == 0 || cardManager.showCardNum / 13 == 3))
                    {
                        showCardList.Add(new ShowCard(i, aiCardDeck[i].cardNumber));
                    }
                    else if (aiCardDeck[i].cardNumber == 53 && (cardManager.showCardNum / 13 == 1 || cardManager.showCardNum / 13 == 2))
                    {
                        showCardList.Add(new ShowCard(i, aiCardDeck[i].cardNumber));
                    }
                    else
                    {
                        if (cardManager.showCardNum == 52 && (aiCardDeck[i].cardNumber / 13 == 0 || aiCardDeck[i].cardNumber / 13 == 3))
                        {
                            showCardList.Add(new ShowCard(i, aiCardDeck[i].cardNumber));
                        }
                        else if (cardManager.showCardNum == 53 && (aiCardDeck[i].cardNumber / 13 == 1 || aiCardDeck[i].cardNumber / 13 == 2))
                        {
                            showCardList.Add(new ShowCard(i, aiCardDeck[i].cardNumber));
                        }
                        else if (cardManager.showCardNum / 13 == aiCardDeck[i].cardNumber / 13 || cardManager.showCardNum % 13 == aiCardDeck[i].cardNumber % 13)
                        {
                            showCardList.Add(new ShowCard(i, aiCardDeck[i].cardNumber));
                        }
                    }
                }
            }
            if (showCardList.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, showCardList.Count);
                ShowCard randomShowCard = showCardList[randomIndex];
                aishows[randomShowCard.deckNum].drowCard(randomShowCard.deckNum, randomShowCard.spriteNum);
            }
            else
            {
                StartCoroutine(AiGetCard());
            }
        }
    }

    public IEnumerator AiGetCard()
    {
        anim.SetBool("AiNextAnim", true);
        yield return new WaitForSeconds(0.1f);
    }

    public void nextTurn()
    {
        gameManager.turn = true;
    }
}
