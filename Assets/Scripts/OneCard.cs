using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneCard : MonoBehaviour
{
    public bool isOneCard = false;
    public bool isOneCardBattel = false;

    AiManager aiManager;
    CardManager cardManager;
    GameManager gameManager;

    void Start()
    {
        aiManager = GameObject.Find("AiManager").GetComponent<AiManager>();
        cardManager = GameObject.Find("CardManager").GetComponent <CardManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (isOneCard)
        {
            isOneCard = false;
            float random = Random.Range(3, 5f);
            Invoke("AiOneCard", random);
        }
    }

    public void PressOneCard()
    {
        isOneCard = false;
        aiManager.anim.SetBool("AiNextAnim", true);
        isOneCardBattel = false;
        gameManager.oneCardObj.SetActive(false);
    }

    public void AiOneCard()
    {
        cardManager.anim.SetBool("nextAnim", true);
        isOneCardBattel = false;
        gameManager.oneCardObj.SetActive(false);
    }
}
