using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;


public class GameSystem : MonoBehaviour
{
    private static GameSystem ins;
    private int DieCount;
    private GameObject Tutorial;
    private PolygonCollider2D coll;
    private GameObject Guild;
    private GameObject Guide;
    private  CinemachineConfiner Cm;
    private void Awake()
    {
        if (ins == null)
        {
            ins = gameObject.GetComponent<GameSystem>();
            PlayerPrefs.SetInt("ESkill", 0);
            PlayerPrefs.SetInt("QSkill", 0);
            PlayerPrefs.SetInt("Coin", 0);
            PlayerPrefs.SetInt("Level", 1);
            DieCount = 0;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        
    }
    public int GetDie()
    {
        return DieCount;
    }
    public void AddDie()
    {
        DieCount++;
    }
    public static GameSystem instans()
    {
        if (ins != null)
            return ins;
        else
            return null;
    }
    void Start()
    {
        Debug.Log("a");
        PlayerPrefs.SetInt("isFirst", 1);
        SetTutorial();
    }
    public void SetTutorial()
    {
        if (PlayerPrefs.GetInt("isFirst")==1)
        {
            QuestSystem.GameLeval = 1;
            Tutorial = GameObject.Find("Map").transform.GetChild(0).gameObject;
            Tutorial.SetActive(true);
            coll = GameObject.Find("TutorialStage1").transform.GetChild(0).gameObject.GetComponent<PolygonCollider2D>();
            Cm = GameObject.Find("CM vcam1").GetComponent<CinemachineConfiner>();
            Cm.m_BoundingShape2D = coll;
            Guild = GameObject.Find("Map").transform.GetChild(1).gameObject;
            Guild.SetActive(true);
            Guide = GameObject.Find("Guild").transform.GetChild(0).gameObject;
            Guide.SetActive(true);
            Guild.SetActive(false);

        }
        else
        {
            Guild = GameObject.Find("Map").transform.GetChild(1).gameObject;
            Guild.SetActive(true);
            Guide = GameObject.Find("Guild").transform.GetChild(0).gameObject;
            Guide.SetActive(false);
        }
    }
    public void StartSet()
    {
        StartCoroutine(Set());
    }
    IEnumerator Set()
    {
        while(true)
        {
            if(SceneManager.GetActiveScene().name=="InGame")
            {
                SetTutorial();
                yield break;
            }
            yield return null;
        }
    }
    public void Guidefalse()
    {
        Guild = GameObject.Find("Map").transform.GetChild(1).gameObject;
        Guild.SetActive(true);
        Guide = GameObject.Find("Guild").transform.GetChild(0).gameObject;
        Guide.SetActive(false);
    }    
    //public void SetisFirst(bool val)
    //{
    //    isFirst = val;
    //}
}
