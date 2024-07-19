using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AiManager;

public class AiGetCard : MonoBehaviour
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
        if (gameManager.turn == false && gameManager.startSetting == false && aiManager.checkCard == true && gameManager.attack == false)
        {
            Debug.Log("일반 내기");
            aiManager.checkCard = false;
            for (int i = 0; i < aiManager.aiCardDeck.Length; i++)
            {
                if (aiManager.aiCardDeck[i].cardNumber != 0)
                {
                    if (aiManager.aiCardDeck[i].cardNumber == 52 && (cardManager.showCardNum / 13 == 0 || cardManager.showCardNum / 13 == 3))
                    {
                        aiManager.showCardList.Add(new ShowCard(i, aiManager.aiCardDeck[i].cardNumber));
                    }
                    else if (aiManager.aiCardDeck[i].cardNumber == 53 && (cardManager.showCardNum / 13 == 1 || cardManager.showCardNum / 13 == 2))
                    {
                        aiManager.showCardList.Add(new ShowCard(i, aiManager.aiCardDeck[i].cardNumber));
                    }
                    else
                    {
                        if (cardManager.showCardNum == 52 && (aiManager.aiCardDeck[i].cardNumber / 13 == 0 || aiManager.aiCardDeck[i].cardNumber / 13 == 3))
                        {
                            aiManager.showCardList.Add(new ShowCard(i, aiManager.aiCardDeck[i].cardNumber));
                        }
                        else if (cardManager.showCardNum == 53 && (aiManager.aiCardDeck[i].cardNumber / 13 == 1 || aiManager.aiCardDeck[i].cardNumber / 13 == 2))
                        {
                            aiManager.showCardList.Add(new ShowCard(i, aiManager.aiCardDeck[i].cardNumber));
                        }
                        else if (cardManager.showCardNum / 13 == aiManager.aiCardDeck[i].cardNumber / 13 || cardManager.showCardNum % 13 == aiManager.aiCardDeck[i].cardNumber % 13)
                        {
                            aiManager.showCardList.Add(new ShowCard(i, aiManager.aiCardDeck[i].cardNumber));
                        }
                    }
                }
            }
            if (aiManager.showCardList.Count != 0)
            {
                aiManager.AiSetCards = true;
                aiManager.DrowRandom();
            }
            else
            {
                StartCoroutine(AiGetCards());
            }
        }
    }
    public IEnumerator AiGetCards()
    {
        if (gameManager.defence != true)
        {
            aiManager.anim.SetBool("AiNextAnim", true);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
