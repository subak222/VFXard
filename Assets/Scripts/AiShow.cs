using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiShow : MonoBehaviour
{
    AiManager aiManager;
    GameManager gameManager;
    CardManager cardManager;

    public Animator[] showanim;

    private float lastAnimTime = 0f;

    void Start()
    {
        aiManager = GameObject.Find("AiManager").GetComponent<AiManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float animTime = showanim[0].GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (animTime >= 1.0f && lastAnimTime < 1.0f)
        {
        }
        else
        {
            lastAnimTime = animTime;
        }

        if (gameManager.startSetting == false && gameManager.turn == false)
        {
            showanim[0].SetBool("AiShow", true);
        }
    }
}
