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
        public Sprite sprite;
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
    public bool AiSetCards = false;

    private bool attackGetCard;

    void Start()
    {
        cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

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
            aiCardDeck[aiCardCount - 1].sprite = cardManager.cardSprites[deck];
            // aiCardDeck[aiCardCount - 1].mycard.GetComponent<SpriteRenderer>().sprite = cardManager.cardSprites[deck];
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
            else
            {
                Invoke("nextTurn", 1f);
            }

        }
        else
        {
            lastAnimTime = animTime;
        }
    }

    public void nextTurn()
    {
        gameManager.turn = true;
        gameManager.defence = false;
    }

    public void DrowRandom()
    {
        if (AiSetCards == true)
        {
            AiSetCards = false; // AiSetCards �÷��׸� false�� �����Ͽ� �ߺ� ȣ�� ����

            // ���� showCardList�� ��� ������ ó������ �ʰ� �޼��带 ����
            if (showCardList.Count == 0)
            {
                Debug.Log("DrowRandom(): showCardList is empty, returning early.");
                return;
            }

            // showCardList���� ������ ī�� ����
            int randomIndex = UnityEngine.Random.Range(0, showCardList.Count);
            ShowCard randomShowCard = showCardList[randomIndex];

            // ���õ� ī�� ���� ��� (������)
            Debug.Log($"DrowRandom(): Selected randomShowCard - deckNum: {randomShowCard.deckNum}, spriteNum: {randomShowCard.spriteNum}");

            // showCardList�� Ŭ�����Ͽ� ���õ� ī�尡 �ߺ� ���õ��� �ʵ��� ��
            showCardList.Clear();

            // ���õ� ī�忡 ���� ���� ���� ������Ʈ
            UpdateGameState(randomShowCard);

            aishows[randomShowCard.deckNum].drowCard(randomShowCard.deckNum, randomShowCard.spriteNum);
        }
    }

    private void UpdateGameState(ShowCard selectedCard)
    {
        // ���õ� ī�忡 ���� ���� ���� ������Ʈ ������ �߰��մϴ�.
        // ���⿡�� gameManager�� �ٸ� �Ŵ������� Ȱ���Ͽ� ������ ���¸� �����մϴ�.
        // ����:
        if (selectedCard.spriteNum == 52)
        {
            gameManager.attackCount += 5;
            gameManager.defence = true;
            gameManager.attack = true;
        }
        else if (selectedCard.spriteNum == 53)
        {
            gameManager.attackCount += 7;
            gameManager.defence = true;
            gameManager.attack = true;
        }
        else if (selectedCard.spriteNum % 13 == 0)
        {
            if (selectedCard.spriteNum / 13 == 3)
            {
                gameManager.attackCount += 5;
                gameManager.defence = true;
                gameManager.attack = true;
            }
            else
            {
                gameManager.attackCount += 3;
                gameManager.defence = true;
                gameManager.attack = true;
            }
        }
        else if (selectedCard.spriteNum % 13 == 1)
        {
            gameManager.attackCount += 2;
            gameManager.defence = true;
            gameManager.attack = true;
        }
        else if (selectedCard.spriteNum % 13 == 2)
        {
            gameManager.attackCount = 0;
            gameManager.attack = false;
        }
    }

}
