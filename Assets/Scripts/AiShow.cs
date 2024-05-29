using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiShow : MonoBehaviour
{
    AiManager aiManager;
    GameManager gameManager;
    CardManager cardManager;

    Animator showanim;
    public int aiDeckNum;
    public int deckNum;

    private float lastAnimTime = 0f;

    void Start()
    {
        showanim = GetComponent<Animator>();
        aiManager = GameObject.Find("AiManager").GetComponent<AiManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float animTime = showanim.GetCurrentAnimatorStateInfo(0).normalizedTime;

        if (animTime >= 1.0f && lastAnimTime < 1.0f && showanim.GetCurrentAnimatorStateInfo(0).IsName("PutCard"))
        {
            showanim.SetInteger("AiShow", 0);
            lastAnimTime = 1.0f;
            Invoke("nextTurn", 1f);
            aiManager.aiCardDeck[aiManager.aiCardCount - 1].mycard.SetActive(false);
            cardManager.showCard.GetComponent<SpriteRenderer>().sprite = aiManager.aiCardDeck[deckNum].mycard.GetComponent<SpriteRenderer>().sprite;
            for (int i = deckNum; i < 20; i++)
            {
                aiManager.aiCardDeck[i].cardNumber = aiManager.aiCardDeck[i + 1].cardNumber;
                aiManager.aiCardDeck[i].mycard.GetComponent<SpriteRenderer>().sprite = aiManager.aiCardDeck[i + 1].mycard.GetComponent<SpriteRenderer>().sprite;
            }
            aiManager.aiCardCount--;
        }
        else
        {
            lastAnimTime = animTime;
        }
    }

    public void drowCard(int deckNum, int spriteNum)
    {
        this.deckNum = deckNum;
        showanim.SetInteger("AiShow", deckNum + 1);
        cardManager.showCardNum = aiManager.aiCardDeck[deckNum].cardNumber;
    }

    public void nextTurn()
    {
        gameManager.turn = true;
    }
}
