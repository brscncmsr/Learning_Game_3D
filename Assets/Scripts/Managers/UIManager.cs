using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject FruitBar;
    public int objectNo=0;
    public GameObject star;
    public GameObject finishCanvas;
    public FruitBar bar;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            SetBarObjects();
        }
           

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator makeTrue()
    {
        yield return new WaitForSeconds(1);
        FruitBar.transform.GetChild(objectNo).GetComponent<Image>().color= Color.Lerp(Color.black, Color.white, Mathf.PingPong(Time.time, 10)); ;
        RefManager.Instance.TrueAnswer.gameObject.SetActive(false);
        SetTrueBar();
        RefManager.Instance.childAnimator.SetBool("isdance", false);

   
    }
    public void SetTrueBar()
    {
        if (objectNo==0)
        {
            bar.first = true;
        }
        if (objectNo == 1)
        {
            bar.second = true;
        }
        if (objectNo == 2 )
        {
            bar.third = true;
        }
        if (objectNo == 3 )
        {
            bar.fourth = true;
        }
        if (objectNo == 4 )
        {
            bar.fifth = true;
        }
    }
    public void SetBarObjects()
    {
        if (bar.first)
        {
            FruitBar.transform.GetChild(0).GetComponent<Image>().color = Color.white;
        }
        if (bar.second)
        {
            FruitBar.transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if ( bar.third)
        {
            FruitBar.transform.GetChild(2).GetComponent<Image>().color = Color.white;
        }
        if (bar.fourth)
        {
            FruitBar.transform.GetChild(3).GetComponent<Image>().color = Color.white;

        }
        if ( bar.fifth)
        {
            FruitBar.transform.GetChild(4).GetComponent<Image>().color = Color.white;
        }
    }
    public void TrueHintButton()
    {
        if (SceneManager.GetActiveScene().name == "Compare2")
        {
            Compare2Controller.Instance.TrueHint();
            RefManager.Instance.hint1.SetActive(false);
        }
        else
        {
            Compare3Controller.Instance.TrueHint();
        }
       
    }
    public void FalseHintButton()
    {

        if (SceneManager.GetActiveScene().name == "Compare2")
        {
            Compare2Controller.Instance.FalseHint();
            RefManager.Instance.hint1.SetActive(false);
        }
        else
        {
            Compare3Controller.Instance.FalseHint();
        }
            
    }
    public void CloseHintButton()
    {

        if (SceneManager.GetActiveScene().name == "Compare2")
        {
            Compare2Controller.Instance.CloseHint();
            RefManager.Instance.hint1.SetActive(false);
        }
        else
        {
            Compare3Controller.Instance.CloseHint();
        }

    }
    public void GoToMenüButton()
    {
        GameManager.Instance.GoToMenü();
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void StartButton()
    {
        bar.first = false;
        bar.second = false;
        bar.third= false;
        bar.fourth = false;
        bar.fifth = false;
        GameManager.Instance.NextLevel();
    }
    public void CheckCanvas()
    {
        Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        if (EventSystem.current.currentSelectedGameObject.transform.parent.name == Compare3Controller.Instance.istrue.name)
        {
            Compare3Controller.Instance.TruePod();
        }
        else
        {
            Compare3Controller.Instance.FalsePod();
        }

    }
}

