using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChangeShape : MonoBehaviour
{
    [Serializable]
    public struct ChageShape
    {
        public Sprite sprite;
        public int shapeNum;
    }

    public ChageShape[] changes;

    public GameObject selectShape;

    private GameManager gameManager;
    private CardManager cardManager;
    private AiManager aiManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();
        aiManager = GameObject.Find("AiManager").GetComponent<AiManager>();
        selectShape.SetActive(false);
    }

    public void AiSelectShape()
    {
        int random = UnityEngine.Random.Range(0, 4);
        cardManager.showCardNum = changes[random].shapeNum * 13 + 6;
        cardManager.showCard.GetComponent<SpriteRenderer>().sprite = changes[random].sprite;
        Invoke("AiNextTurn", 0.5f);
    }

    public void SelectClover()
    {
        cardManager.showCardNum = changes[0].shapeNum * 13 + 6;
        cardManager.showCard.GetComponent<SpriteRenderer>().sprite = changes[0].sprite;
        Invoke("nextTurn", 0.5f);
        selectShape.SetActive(false);
    }

    public void SelectDia()
    {
        cardManager.showCardNum = changes[1].shapeNum * 13 + 6;
        cardManager.showCard.GetComponent<SpriteRenderer>().sprite = changes[1].sprite;
        Invoke("nextTurn", 0.5f);
        selectShape.SetActive(false);
    }

    public void SelectHeart()
    {
        cardManager.showCardNum = changes[2].shapeNum * 13 + 6;
        cardManager.showCard.GetComponent<SpriteRenderer>().sprite = changes[2].sprite;
        Invoke("nextTurn", 0.5f);
        selectShape.SetActive(false);
    }

    public void SelectSpade()
    {
        cardManager.showCardNum = changes[3].shapeNum * 13 + 6;
        cardManager.showCard.GetComponent<SpriteRenderer>().sprite = changes[3].sprite;
        Invoke("nextTurn", 0.5f);
        selectShape.SetActive(false);
    }

    public void nextTurn()
    {
        gameManager.turn = false;
        aiManager.checkCard = true;
    }

    public void AiNextTurn()
    {
        gameManager.turn = true;
    }
}
