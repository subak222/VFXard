using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAttackGetCard : MonoBehaviour
{
    CardManager cardManager;
    GameManager gameManager;
    AiManager aiManager;

    void Start()
    {
        cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        aiManager = GameObject.Find("AiManager").GetComponent<AiManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.turn == false && gameManager.startSetting == false && gameManager.attack == true & gameManager.defence == false && aiManager.checkCard == true)
        {
            Debug.Log("공격 내기");
            aiManager.checkCard = false;
            for (int i = 0; i < aiManager.aiCardCount - 1; i++)
            {
                int cardNumber = aiManager.aiCardDeck[i].cardNumber;
                if (cardManager.showCardNum == 52)
                {
                    if (cardNumber == 53)
                    {
                        aiManager.showCardList.Add(new AiManager.ShowCard(i, cardNumber));
                        gameManager.attackGetCard = true;
                    }
                    else if ((cardNumber / 13 == 0 || cardNumber / 13 == 3) && (cardNumber % 13 == 0 || cardNumber % 13 == 1))
                    {
                        aiManager.showCardList.Add(new AiManager.ShowCard(i, cardNumber));
                        gameManager.attackGetCard = true;
                    }
                }
                else if (cardManager.showCardNum == 53)
                {
                    if (cardNumber == 52)
                    {
                        aiManager.showCardList.Add(new AiManager.ShowCard(i, cardNumber));
                        gameManager.attackGetCard = true;
                    }
                    else if ((cardNumber / 13 == 1 || cardNumber / 13 == 2) && (cardNumber % 13 == 0 || cardNumber % 13 == 1))
                    {
                        aiManager.showCardList.Add(new AiManager.ShowCard(i, cardNumber));
                        gameManager.attackGetCard = true;
                    }
                }
                else if (cardManager.showCardNum % 13 == 0)
                {
                    if ((cardManager.showCardNum / 13 == 0 || cardManager.showCardNum / 13 == 3) && cardNumber == 52)
                    {
                        aiManager.showCardList.Add(new AiManager.ShowCard(i, cardNumber));
                        gameManager.attackGetCard = true;
                    }
                    else if ((cardManager.showCardNum / 13 == 1 || cardManager.showCardNum / 13 == 2) && cardNumber == 53)
                    {
                        aiManager.showCardList.Add(new AiManager.ShowCard(i, cardNumber));
                        gameManager.attackGetCard = true;
                    }
                    else if ((cardManager.showCardNum / 13 == cardNumber / 13) && cardNumber % 13 == 1)
                    {
                        aiManager.showCardList.Add(new AiManager.ShowCard(i, cardNumber));
                        gameManager.attackGetCard = true;
                    }
                    else if (cardNumber % 13 == 0)
                    {
                        aiManager.showCardList.Add(new AiManager.ShowCard(i, cardNumber));
                        gameManager.attackGetCard = true;
                    }
                    else if (cardManager.showCardNum / 13 == cardNumber / 13 && cardNumber % 13 == 2)
                    {
                        aiManager.showCardList.Add(new AiManager.ShowCard(i, cardNumber));
                        gameManager.attackGetCard = true;
                    }
                }
                else if (cardManager.showCardNum % 13 == 1)
                {
                    if ((cardManager.showCardNum / 13 == 0 || cardManager.showCardNum / 13 == 3) && cardNumber == 52)
                    {
                        aiManager.showCardList.Add(new AiManager.ShowCard(i, cardNumber));
                        gameManager.attackGetCard = true;
                    }
                    else if ((cardManager.showCardNum / 13 == 1 || cardManager.showCardNum / 13 == 2) && cardNumber == 53)
                    {
                        aiManager.showCardList.Add(new AiManager.ShowCard(i, cardNumber));
                        gameManager.attackGetCard = true;
                    }
                    else if ((cardManager.showCardNum / 13 == cardNumber / 13) && cardNumber % 13 == 0)
                    {
                        aiManager.showCardList.Add(new AiManager.ShowCard(i, cardNumber));
                        gameManager.attackGetCard = true;
                    }
                    else if (cardNumber != 53 && cardNumber % 13 == 1)
                    {
                        aiManager.showCardList.Add(new AiManager.ShowCard(i, cardNumber));
                        gameManager.attackGetCard = true;
                    }
                    else if (cardManager.showCardNum / 13 == cardNumber / 13 && cardNumber % 13 == 2)
                    {
                        aiManager.showCardList.Add(new AiManager.ShowCard(i, cardNumber));
                        gameManager.attackGetCard = true;
                    }
                }
            }
            if (aiManager.showCardList.Count != 0)
            {
                aiManager.AiSetCards = true;
                aiManager.DrowRandom();
            }
            if (aiManager.showCardList.Count == 0)
            {
                StartCoroutine(AiAttackGetCards());
            }
        }
    }

    public IEnumerator AiAttackGetCards()
    {
        if (gameManager.defence != true)
        {
            while (aiManager.aiCardCount != aiManager.aiCardCount + gameManager.attackCount)
            {
                aiManager.anim.SetBool("AiNextAnim", true);
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
