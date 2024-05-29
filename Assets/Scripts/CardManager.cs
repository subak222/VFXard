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
        if (gameManager.turn == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("getCard") && gameManager.startSetting == false)
                    {
                        StartCoroutine(MyGetCard());
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
            Invoke("nextTurn", 1f);
        }
        else
        {
            lastAnimTime = animTime;
        }

    }

    public IEnumerator MyGetCard()
    {
        anim.SetBool("nextAnim", true);
        yield return new WaitForSeconds(0.1f);
    }

    public void nextTurn()
    {
        gameManager.turn = false;
        aiManager.checkCard = true;
    }
}
