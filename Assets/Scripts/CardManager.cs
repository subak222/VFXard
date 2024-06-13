using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardManager : MonoBehaviour
{
    [Serializable]
    public struct Card
    {
        public int cardNumber;
        public GameObject mycard;
    }

    GameManager gameManager;
    AiManager aiManager;
    Card card;

    public GameObject showCard;

    public Animator anim;
    [SerializeField]
    public Sprite[] cardSprites;
    public Card[] myCardDeck;

    public int cardCount = 1;
    public bool show = true;
    private float lastAnimTime = 0f;

    public int showCardNum;

    public List<int> array = new List<int>();

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        aiManager = GameObject.Find("AiManager").GetComponent<AiManager>();

        anim.SetInteger("getCard", cardCount);
        anim.SetBool("nextAnim", false);
        for (int i = 0; i < 54; i++) // Assuming there are 54 cards
        {
            array.Add(i);
        }

        Shuffle(array);
        showCard.GetComponent<SpriteRenderer>().sprite = cardSprites[array[0]];
        showCardNum = array[0];
        array.RemoveAt(0);
    }

    void Shuffle(List<int> array)
    {
        System.Random random = new System.Random();
        for (int i = array.Count - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1); // 0부터 i까지의 랜덤 인덱스 선택
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }

    void Update()
    {
        anim.SetInteger("getCard", cardCount);
        if (gameManager.turn == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("getCard"))
                    {
                        if (gameManager.startSetting == false && gameManager.attack == false)
                        {
                            StartCoroutine(MyGetCard());
                        }
                        else
                        {
                            if (gameManager.attackGetCard == true)
                            {
                                StartCoroutine(AttackGetCard());
                            }
                            else
                            {
                                StartCoroutine(AttackGetCard());
                            }
                        }
                    }
                }
            }
        }

        float animTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (animTime >= 1.0f && lastAnimTime < 1.0f && !anim.GetCurrentAnimatorStateInfo(0).IsName("CardIdle"))
        {
            lastAnimTime = 1.0f;
            anim.SetBool("nextAnim", false);
            int deck = array[0];
            array.RemoveAt(0);
            myCardDeck[cardCount - 1].mycard.GetComponent<SpriteRenderer>().sprite = cardSprites[deck];
            myCardDeck[cardCount - 1].cardNumber = deck;
            myCardDeck[cardCount - 1].mycard.SetActive(true);
            cardCount++;
            anim.SetInteger("getCard", cardCount);
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

    public IEnumerator AttackGetCard()
    {
        GameObject.Find("getCard").GetComponent<BoxCollider2D>().enabled = false;
        while (cardCount != cardCount + gameManager.attackCount)
        {
            anim.SetBool("nextAnim", true);
            yield return new WaitForSeconds(1f);
        }
        gameManager.attackGetCard = false;
    }

    public IEnumerator MyGetCard()
    {
        GameObject.Find("getCard").GetComponent<BoxCollider2D>().enabled = false;
        anim.SetBool("nextAnim", true);
        yield return new WaitForSeconds(0.1f);
    }

    public void nextTurn()
    {
        GameObject.Find("getCard").GetComponent<BoxCollider2D>().enabled = true;
        gameManager.turn = false;
        aiManager.checkCard = true;
    }
}
