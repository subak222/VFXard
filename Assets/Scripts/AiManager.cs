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

    CardManager cardManager;
    GameManager gameManager;

    public Card[] aiCardDeck;

    public Animator anim;

    public int aiCardCount = 1;
    private float lastAnimTime = 0f;

    void Start()
    {
        cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
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
        }
        else
        {
            lastAnimTime = animTime;
        }
    }

    public IEnumerator AiGetCard()
    {
        anim.SetBool("AiNextAnim", true);
        yield return new WaitForSeconds(0.1f);
        gameManager.turn = true;
    }
}
