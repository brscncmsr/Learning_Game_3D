using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Compare2Controller : MonoBehaviour
{
    public static Compare2Controller Instance;
    public AudioClip trysound;
    public Transform childTarget;
    public Transform motherTarget;
    public float destinationReachedTreshold;
    public GameObject pods;
    public PodsController podLeast;
    public PodsController podMid;
    public PodsController podHighest;
    public Hint hint1;
    public Hint hint2;
    public Hint hint3;
    public Hint hint4;
    public Game Compare1;
    public Game Compare2;
    private bool isStop=false;
    public Game game;



    private void Awake()
    {
        Instance = this;
    }
    [System.Serializable]
    public class Hint
    {
        public SoundManager.GameSounds h1;
        public SoundManager.GameSounds h2;
        public SoundManager.GameSounds h3;
        public GameObject r1;
        public GameObject r2;
        public GameObject HintCanvas;
        

        public IEnumerator OpenHints()
        {
  
            HintCanvas.SetActive(true);
            h1.PlaySound();
            yield return new WaitForSeconds(h1.clip.length);
            h2.PlaySound();
            yield return new WaitForSeconds(h2.clip.length);
            h3.PlaySound();
            r1.SetActive(true);
            r2.SetActive(true);
        }



    }
    [System.Serializable]
    public class Game
    {
        public int falsecount = 0;
        public bool isdone = false;
        public Hint hintObject1;
        public Hint hintObject2;
        public SoundManager.GameSounds storySound;
        public SoundManager.GameSounds gameSound;
        public bool isopened;
        public int whichObject;
        public bool whichOneTrue;
    }
    void Start()
    {
        SoundManager.Instance.waitSound += OnMove;
        SoundManager.Instance.PlaySound(SoundManager.Instance.compare2Story1);
        Compare1.storySound = SoundManager.Instance.compare2Story;
        Compare1.gameSound = SoundManager.Instance.compare2Game;
        Compare1.hintObject1 = hint1;
        Compare1.hintObject2 = hint2;
        Compare1.whichOneTrue= true;
        Compare1.whichObject = 0;
        game = Compare1;
        Compare2.storySound = SoundManager.Instance.compare2Story2;
        Compare2.gameSound = SoundManager.Instance.compare2Game2;
        Compare2.whichOneTrue = false;
        Compare2.whichObject = 1;
        Compare2.hintObject1 = hint3;
        Compare2.hintObject2 = hint4;
        RefManager.Instance.childAgent.destination = childTarget.position;
        RefManager.Instance.motherAgent.destination = motherTarget.position;
        RefManager.Instance.childAnimator.SetBool("iswalk", true);
        RefManager.Instance.motherAnimator.SetBool("iswalking", true);

    }
    private void Update()
    {
        if (!isStop)
        {
            CheckDestinationReached();
        }
     
      

    }
    public void OnMove()
    {
     
        SoundManager.Instance.waitSound -= OnMove;
        SoundManager.Instance.waitSound += OnGame;
        SoundManager.Instance.PlaySound(game.storySound);

    }
    void CheckDestinationReached()
    {
        float distanceToTarget = Vector3.Distance(RefManager.Instance.childAgent.transform.position, childTarget.position);
        if (distanceToTarget < destinationReachedTreshold)
        {
            OnStop();
            isStop = true;
        }
    }

    public void OnStop()
    {

        UIManager.Instance.FruitBar.SetActive(false);
        RefManager.Instance.childAnimator.SetBool("iswalk", false);
        RefManager.Instance.motherAnimator.SetBool("iswalking", false);
        pods.transform.DOMove(new Vector3(pods.transform.position.x,2, pods.transform.position.z), 1);


    }
    public void OnGame()
    {
        SoundManager.Instance.waitSound -= OnGame;
        game.gameSound.PlaySound();
        StartCoroutine(AllowClick());
        
    }
    private IEnumerator AllowClick()
    {
        yield return new WaitForSeconds(game.gameSound.clip.length);
        UIManager.Instance.star.SetActive(false);
        podHighest.gameObject.GetComponent<BoxCollider>().enabled = true;
        podMid.gameObject.GetComponent<BoxCollider>().enabled = true;
        podLeast.gameObject.GetComponent<BoxCollider>().enabled = true;
        podLeast.isTrue = game.whichOneTrue;
        podMid.isTrue = false;
        podHighest.isTrue = !game.whichOneTrue;
    }
    public void FalsePod()
    {
        if (game.isopened)
        {
            UIManager.Instance.FruitBar.SetActive(true);
            RefManager.Instance.childAnimator.SetBool("isdance", true);
            
            StartCoroutine(dance());
            UIManager.Instance.objectNo = game.whichObject;
            StartCoroutine(makefalseFunction());
            if (game == Compare2)
            {
                SoundManager.Instance.fail.PlaySound();
                StartCoroutine(FinishGame());
            }
        }
        game.falsecount++;
        if (game.falsecount == 2)
        {;
            if (game == Compare2)
            {

                StartCoroutine(game.hintObject1.OpenHints());
            }
            else
            {
                RefManager.Instance.hint1.SetActive(true);
            }

            podHighest.gameObject.GetComponent<BoxCollider>().enabled = false;
            podMid.gameObject.GetComponent<BoxCollider>().enabled = false;
            podLeast.gameObject.GetComponent<BoxCollider>().enabled = false;
            OpenHint();
      
        }
      
    }
    public void TruePod()
    {

        UIManager.Instance.FruitBar.SetActive(true);
        RefManager.Instance.childAnimator.SetBool("isdance", true);
        RefManager.Instance.TrueAnswer.gameObject.SetActive(true);
        UIManager.Instance.star.SetActive(true);
        UIManager.Instance.objectNo = game.whichObject;
        
        StartCoroutine(UIManager.Instance.makeTrue());
        StartCoroutine(makeTrueFunction());
        if (game ==Compare2)
        {
            StartCoroutine(FinishGame());
        }

    }
    public void NewCompareGame()
    {
        
        RefManager.Instance.childAnimator.SetBool("isstop", true);
        podHighest.gameObject.GetComponent<BoxCollider>().enabled = false;
        podMid.gameObject.GetComponent<BoxCollider>().enabled = false;
        podLeast.gameObject.GetComponent<BoxCollider>().enabled = false;
        OnMove();
        game = Compare2;
    }
    public void OpenHint()
    {
        game.isopened = true;
    }
    
    public void CloseHint()
    {
        game.hintObject1.HintCanvas.SetActive(false);
        game.hintObject2.HintCanvas.SetActive(false);
        podHighest.gameObject.GetComponent<BoxCollider>().enabled = true;
        podMid.gameObject.GetComponent<BoxCollider>().enabled = true;
        podLeast.gameObject.GetComponent<BoxCollider>().enabled = true;
        OnGame();
    }
    public void TrueHint()
    {
        game.hintObject1.HintCanvas.SetActive(false);
        game.hintObject2.HintCanvas.SetActive(false);
        OnGame();
    }
    public void FalseHint()
    {
        StartCoroutine(game.hintObject2.OpenHints());
    }
    public IEnumerator FinishGame()
    {
        yield return new WaitForSeconds(4);
        GameManager.Instance.NextLevel();
    }
    private IEnumerator dance()
    {
        yield return new WaitForSeconds(1);
        RefManager.Instance.childAnimator.SetBool("isdance",false);
    }
    public IEnumerator makeTrueFunction()
    {
        SoundManager.Instance.celebrate.PlaySound();
        yield return new WaitForSeconds(2);
        game = Compare2;
        NewCompareGame();

    }
    public IEnumerator makefalseFunction()
    {
        SoundManager.Instance.fail.PlaySound();
        yield return new WaitForSeconds(4);
        game = Compare2;
        NewCompareGame();

    }
}
   

