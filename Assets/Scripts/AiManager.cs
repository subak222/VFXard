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
    public bool AiGetCards = false;

    void Start()
    {
        cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("AiGetCard", aiCardCount);

        if (gameManager.turn == false && gameManager.startSetting == false && checkCard == true)
        {
            checkCard = false;
            if (gameManager.attack == false)
            {
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
                    if (gameManager.attackGetCard == true) StartCoroutine(AiAttackGetCard());
                    else StartCoroutine(AiGetCard());
                }
            }
            else
            {
                Debug.Log("공격 받았다");
                for (int i = 0; i < aiCardDeck.Length; i++)
                {
                    if (cardManager.showCardNum == 52 && (aiCardDeck[i].cardNumber == 53 || (aiCardDeck[i].cardNumber / 13 == 0 || aiCardDeck[i].cardNumber / 13 == 3) && (aiCardDeck[i].cardNumber % 13 == 0 || aiCardDeck[i].cardNumber % 13 == 1)))
                    {
                        // 낸 카드가 흑조커 일 때, 덱 중 컬러조커 or 스페이드나 클로버 중 a or 2 있을 떄
                        Debug.Log("111");
                        showCardList.Add(new ShowCard(i, aiCardDeck[i].cardNumber));
                    }
                    else if (cardManager.showCardNum == 53 && (aiCardDeck[i].cardNumber == 52 || (aiCardDeck[i].cardNumber / 13 == 1 || aiCardDeck[i].cardNumber / 13 == 2) && (aiCardDeck[i].cardNumber % 13 == 0 || aiCardDeck[i].cardNumber % 13 == 1)))
                    {
                        // 낸 카드가 컬러조커 일 때, 댁 중 흑조커 or 다이아나 하트 중 a or 2 있을 떄
                        Debug.Log("222");
                        showCardList.Add(new ShowCard(i, aiCardDeck[i].cardNumber));
                    }
                    else if (cardManager.showCardNum % 13 == 0 && ((aiCardDeck[i].cardNumber % 13 == 0 && aiCardDeck[i].cardNumber != 52) || (((cardManager.showCardNum / 13 == 0 || cardManager.showCardNum / 13 == 3) && aiCardDeck[i].cardNumber == 52) || ((cardManager.showCardNum / 13 == 1 || cardManager.showCardNum / 13 == 2) && aiCardDeck[i].cardNumber == 53))))
                    {
                        // 낸 카드가 a 일 때, 덱 중 a가 있거나 같은 색 조커가 있을 떄
                        Debug.Log("333");
                        showCardList.Add(new ShowCard(i, aiCardDeck[i].cardNumber));
                    }
                    else if (cardManager.showCardNum % 13 == 1 && ((aiCardDeck[i].cardNumber % 13 == 1 && aiCardDeck[i].cardNumber != 53) || (((cardManager.showCardNum / 13 == 0 || cardManager.showCardNum / 13 == 3) && aiCardDeck[i].cardNumber == 52) || ((cardManager.showCardNum / 13 == 1 || cardManager.showCardNum / 13 == 2) && aiCardDeck[i].cardNumber == 53))))
                    {
                        // 낸 카드가 2 일 때, 덱 중 2가 있거나 같은 색 조커가 있을 때
                        Debug.Log("444");
                        showCardList.Add(new ShowCard(i, aiCardDeck[i].cardNumber));
                    }
                    else if (cardManager.showCardNum / 13 == aiCardDeck[i].cardNumber / 13)
                    {
                        if (cardManager.showCardNum % 13 == 0 && aiCardDeck[i].cardNumber % 13 == 1)
                        {
                            // 같은 모양일 때,  0일 때 1이 있는 경우
                            Debug.Log("555");
                            showCardList.Add(new ShowCard(i, aiCardDeck[i].cardNumber));
                        }
                        else if (cardManager.showCardNum % 13 == 1 && aiCardDeck[i].cardNumber % 13 == 0)
                        {
                            // 같은 모양일 때, 1일 때 0이 있는 경우
                            Debug.Log("666");
                            showCardList.Add(new ShowCard(i, aiCardDeck[i].cardNumber));
                        }
                    }
                    if (cardManager.showCardNum / 13 == aiCardDeck[i].cardNumber && aiCardDeck[i].cardNumber % 13 == 2)
                    {
                        Debug.Log("777");
                        showCardList.Add(new ShowCard(i, aiCardDeck[i].cardNumber));
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
                    if (gameManager.attackGetCard == true) StartCoroutine(AiAttackGetCard());
                    else StartCoroutine(AiGetCard());
                }
            }
        }

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
            if (gameManager.attackCount != 0 && gameManager.attack == true)
            {
                gameManager.attackCount--;
                if (gameManager.attackCount == 0)
                {
                    gameManager.attack = false;
                    gameManager.attackGetCard = false;
                    Invoke("nextTurn", 1f);
                }
            }
            if (gameManager.attack == false)
            {
                Invoke("nextTurn", 1f);
            }
        }
        else
        {
            lastAnimTime = animTime;
        }
    }

    public IEnumerator AiAttackGetCard()
    {
        while (aiCardCount != aiCardCount + gameManager.attackCount)
        {
            anim.SetBool("AiNextAnim", true);
            yield return new WaitForSeconds(1f);
        }
        gameManager.attackGetCard = false;
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
