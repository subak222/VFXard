using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    CardManager cardManager;
    AiManager aiManager;
    OneCard oneCard;

    public Animator cameraAnim;

    [SerializeField]
    private GameObject threeD;
    [SerializeField]
    private GameObject twoD;

    public GameObject transaction;

    public Animator tranAim;

    public bool turn;
    public bool startSetting = true;
    public bool attack = false;
    public int attackCount = 0;
    public bool defence = false;
    public bool attackGetCard = false;
    public bool changeShape;
    public bool aiShow;

    public Camera threeDCamera;

    GameObject meteorClone;
    GameObject blizzarClone;
    GameObject beamClone;
    GameObject slashGoClone;

    public bool isMeteor = false;
    public bool isBlizzard = false;
    public bool isBeam = false;
    public bool isSlashGo = false;

    [Header("Effect")]
    public GameObject meteor;
    public GameObject blizzard;
    public GameObject beam;
    public GameObject slashGo;


    [SerializeField]
    private GameObject arrow;

    public GameObject oneCardObj;

    void Start()
    {
        aiManager = GameObject.Find("AiManager").GetComponent<AiManager>();
        cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();
        oneCard = GetComponent<OneCard>();

        arrow.SetActive(false);
        turn = (Random.value > 0.5f);
    }

    void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();

        if (cardManager.cardCount - 1 == 0 && startSetting == false)
        {
            SceneManager.LoadScene("Win");
        }
        if (aiManager.aiCardCount - 1 == 0 && startSetting == false)
        {
            SceneManager.LoadScene("Lose");
        }

        if (isMeteor == true)
        {
            isMeteor = false;
            threeD.SetActive(true);
            twoD.SetActive(false);

            meteorClone = Instantiate(meteor);

            cameraAnim.SetBool("isMeteor", true);
            cameraAnim.enabled = true;

            Invoke("Meteor", 3.5f);
            Invoke("Next", 6f);
        }
        else if (isBlizzard == true)
        {
            isBlizzard = false;
            cameraAnim.enabled = true;
            threeD.SetActive(true);
            twoD.SetActive(false);

            blizzarClone = Instantiate(blizzard);

            cameraAnim.SetBool("isBilizard", true);

            Invoke("Blizzard", 0.8f);
            Invoke("Next", 4.5f);
        }
        else if (isBeam == true)
        {
            threeDCamera.GetComponent<Transform>().position = new Vector3(-5f, 3f, 2.5f);
            threeDCamera.GetComponent<Transform>().rotation = Quaternion.Euler(30, 140, 0);
            isBeam = false;
            cameraAnim.enabled = false;
            threeD.SetActive(true);
            twoD.SetActive(false);

            beamClone = Instantiate(beam);

            Invoke("Beam", 0.5f);
            Invoke("Next", 4f);
        }
        else if (isSlashGo == true)
        {
            isSlashGo = false;
            cameraAnim.enabled = true;
            threeD.SetActive(true);
            twoD.SetActive(false);

            slashGoClone = Instantiate(slashGo);

            cameraAnim.SetBool("isSlashGo", true);

            Invoke("SlashGo", 2f);
            Invoke("Next", 3.5f);

        }

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

    public void Meteor()
    {
        cameraAnim.enabled = false;
        transaction.SetActive(false);
        threeDCamera.GetComponent<Transform>().position = new Vector3(4.429579f, 8.764963f, 15.1102f);
        threeDCamera.GetComponent<Transform>().rotation = Quaternion.Euler(37.22f, 220, 0);
        cameraAnim.SetBool("isMeteor", false);
    }
    
    public void Blizzard()
    {
        cameraAnim.enabled = false;
        transaction.SetActive(false);
        threeDCamera.GetComponent<Transform>().position = new Vector3(0, 8f, 11f);
        threeDCamera.GetComponent<Transform>().rotation = Quaternion.Euler(40, 180, 0);
        cameraAnim.SetBool("isBilizard", false);
    }

    public void Beam()
    {
        transaction.SetActive(false);
    }

    public void SlashGo()
    {
        cameraAnim.enabled = false;
        transaction.SetActive(false);
        threeDCamera.GetComponent<Transform>().position = new Vector3(7, 5.5f, 9);
        threeDCamera.GetComponent<Transform>().rotation = Quaternion.Euler(32, 260, 0);
        cameraAnim.SetBool("isSlashGo", false);
    }

    public void Next()
    {
        threeD.SetActive(false);
        twoD.SetActive(true);

        turn = false;
        aiManager.checkCard = true;

        Destroy(meteorClone);
        Destroy(blizzarClone);
        Destroy(beamClone);
        Destroy(slashGoClone);
    }
}
