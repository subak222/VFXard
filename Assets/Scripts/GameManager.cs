using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    CardManager cardManager;
    AiManager aiManager;

    public bool turn;
    public bool startSetting = true;

    [SerializeField]
    private GameObject arrow;

    void Start()
    {
        aiManager = GameObject.Find("AiManager").GetComponent<AiManager>();
        cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();
        arrow.SetActive(false);
        turn = (Random.value > 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (startSetting == true && (cardManager.cardCount < 8 && aiManager.aiCardCount < 8))
        {
            StartCoroutine(start());
        }
        if (cardManager.cardCount > 7 && aiManager.aiCardCount > 7)
        {
            startSetting = false;
            arrow.SetActive(true);
        }

        if (turn == true)
        {
            arrow.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    IEnumerator start()
    {
        aiManager.anim.SetBool("AiNextAnim", true);
        cardManager.anim.SetBool("nextAnim", true);
        yield return new WaitForSeconds(0.1f);
    }
}
