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

    [SerializeField]
    public Animator anim;
    [SerializeField]
    private Sprite[] cardSprites;
    public Card[] myCardDeck;

    public int cardCount = 1;
    public bool show = true;
    private float lastAnimTime = 0f;

    public List<int> array = new List<int>();

    void Start()
    {
        anim.SetInteger("getCard", cardCount);
        anim.SetBool("nextAnim", false);
        for (int i = 0; i < 54; i++) // Assuming there are 54 cards
        {
            array.Add(i);
        }

        Shuffle(array);
    }

    void Shuffle(List<int> array)
    {
        System.Random random = new System.Random();
        for (int i = array.Count - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1); // 0���� i������ ���� �ε��� ����
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("getCard"))
                {
                    StartCoroutine(MyGetCard());
                }
            }
        }
        float animTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (animTime >= 1.0f && lastAnimTime < 1.0f)
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
        }
        else
        {
            lastAnimTime = animTime;
        }

    }

    IEnumerator MyGetCard()
    {
        anim.SetBool("nextAnim", true);
        yield return new WaitForSeconds(0.1f);
    }
}
