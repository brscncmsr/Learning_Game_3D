using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Compare3Controller : MonoBehaviour
{
    public static Compare3Controller Instance;
    public Game Compare1;
    public Game Compare2;
    public Game Compare3;
    public Hint hint1;
    public Hint hint2;
    public Hint hint3;
    public Hint hint21;
    public Hint hint22;
    public Hint hint23;
    public Hint hint31;
    public Hint hint32;
    public Hint hint33;
    public Game game;
    public CompareCanvasController Compare2C;
    public CompareCanvasController Compare3C;
    public GameObject baskets;
    public GameObject Compare2Canvas;
    public GameObject Compare3Canvas;
    public BasketController basketSmallest;
    public BasketController basketMid;
    public BasketController basketBiggest;
    public GameObject istrue;

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
        public GameObject r3;
        public GameObject HintCanvas;
        public bool isend;

        public IEnumerator OpenHints()
        {

            HintCanvas.SetActive(true);
            h1.PlaySound();
            yield return new WaitForSeconds(h1.clip.length);
            h2.PlaySound();
            yield return new WaitForSeconds(h2.clip.length);
            h3.PlaySound();
            isend = true;
            r1.SetActive(true);
            r2.SetActive(true);
            r3.SetActive(true);
        }



    }
    [System.Serializable]
    public class Game
    {
        public int falsecount = 0;
        public bool isdone = false;
        public Hint hintObject1;
        public Hint hintObject2;
        public Hint hintObject3;
        public SoundManager.GameSounds storySound;
        public SoundManager.GameSounds gameSound;
        public bool isopened;
        public int whichObject;
        public bool whichOneTrue;
    }
    // Start is called before the first frame update
    void Start()
    {
        Compare1.gameSound = SoundManager.Instance.compare3Game;
        Compare1.storySound = SoundManager.Instance.compare3Story;
        Compare1.hintObject1 = hint1;
        Compare1.hintObject2 = hint2;
        Compare1.hintObject3 = hint3;
        Compare1.whichOneTrue = true;
        Compare1.whichObject = 2;
        game = Compare1;
        Compare2.gameSound = SoundManager.Instance.compare3Game2;
        Compare2.storySound = SoundManager.Instance.compare3Story2;
        Compare2.hintObject1 = hint21;
        Compare2.hintObject2 = hint22;
        Compare2.hintObject3 = hint23;
        Compare2.whichOneTrue = true;
        Compare2.whichObject = 3;
        Compare3.gameSound = SoundManager.Instance.compare3Game3;
        Compare3.storySound = SoundManager.Instance.compare3Story3;
        Compare3.hintObject1 = hint31;
        Compare3.hintObject2 = hint32;
        Compare3.hintObject3 = hint33;
        Compare3.whichOneTrue = true;
        Compare3.whichObject = 4;
        SoundManager.Instance.PlaySound(game.storySound);
        SoundManager.Instance.waitSound += OnGame;
    }
    private void Update()
    {
        if (game.hintObject1.isend)
        {
            StartCoroutine(game.hintObject2.OpenHints());
            game.hintObject1.isend = false;
        }
    }
    public void OnMove()
    {

        SoundManager.Instance.waitSound -= OnMove;
        SoundManager.Instance.waitSound += OnGame;
        SoundManager.Instance.PlaySound(game.storySound);

    }

    public void OnGame()
    {
        SoundManager.Instance.waitSound -= OnGame;
        RefManager.Instance.childAnimator.SetBool("isstop", true);
        game.gameSound.PlaySound();
        UIManager.Instance.FruitBar.SetActive(false);
        baskets.transform.DOMove(new Vector3(baskets.transform.position.x, 2f, baskets.transform.position.z), 1);
        StartCoroutine(AllowClick());

       
    }
    private IEnumerator AllowClick()
    {
        yield return new WaitForSeconds(game.gameSound.clip.length);
        if (game == Compare1)
        {
            UIManager.Instance.star.SetActive(false);
            basketBiggest.gameObject.GetComponent<BoxCollider>().enabled = true;
            basketMid.gameObject.GetComponent<BoxCollider>().enabled = true;
            basketSmallest.gameObject.GetComponent<BoxCollider>().enabled = true;
            basketBiggest.isTrue = game.whichOneTrue;
            basketMid.isTrue = false;
            basketSmallest.isTrue = !game.whichOneTrue;
        }
        if (game == Compare2)
        {
            UIManager.Instance.star.SetActive(false);
            Compare2C.trueOne.gameObject.SetActive(true);
            Compare2C.falseOne.gameObject.SetActive(true);
            Compare2C.midOne.gameObject.SetActive(true);


        }

        if (game == Compare3)
        {
            UIManager.Instance.star.SetActive(false);
            Compare3C.trueOne.SetActive(true);
            Compare3C.falseOne.SetActive(true);
            Compare3C.falseOne1.SetActive(true);
            Compare3C.midOne.SetActive(true);
        }
    }

    public void CloseHint()
    {
        game.hintObject1.HintCanvas.SetActive(false);
        game.hintObject2.HintCanvas.SetActive(false);
        game.hintObject3.HintCanvas.SetActive(false);
        OnGame();
    }
    public void TrueHint()
    {
        game.hintObject1.HintCanvas.SetActive(false);
        game.hintObject2.HintCanvas.SetActive(false);
        game.hintObject3.HintCanvas.SetActive(false);
        OnGame();
    }
    public void FalseHint()
    {
        StartCoroutine(game.hintObject3.OpenHints());
  
    }




    public void TruePod()
    {

        UIManager.Instance.FruitBar.SetActive(true);
        RefManager.Instance.childAnimator.SetBool("isdance", true);
        RefManager.Instance.TrueAnswer.gameObject.SetActive(true);
        UIManager.Instance.star.SetActive(true);
        UIManager.Instance.objectNo = game.whichObject;
        StartCoroutine(UIManager.Instance.makeTrue());
        if (game == Compare1)
        {
            StartCoroutine(makeTrueFunction());
        }

        if (game == Compare2)
        {
            Compare2Canvas.SetActive(false);
            StartCoroutine(makeTrueFunction());
        }
        else if (game == Compare3)
        {
            Compare3Canvas.SetActive(false);
            StartCoroutine(FinishGame());
        }
    }

    public void FalsePod()
    {
        if (game.isopened)
        {
            UIManager.Instance.FruitBar.SetActive(true);
            RefManager.Instance.childAnimator.SetBool("isdance", true);
            baskets.transform.DOMove(new Vector3(baskets.transform.position.x, 0.7f, baskets.transform.position.z), 1);
            StartCoroutine(dance());
            UIManager.Instance.objectNo = game.whichObject;
            StartCoroutine(makefalseFunction());
            if (game == Compare3)
            {
                SoundManager.Instance.fail.PlaySound();
                StartCoroutine(FinishGame());
            }
        }
        game.falsecount++;
        if (game.falsecount == 2)
        {
            basketSmallest.gameObject.GetComponent<BoxCollider>().enabled = false;
            basketMid.gameObject.GetComponent<BoxCollider>().enabled = false;
            basketBiggest.gameObject.GetComponent<BoxCollider>().enabled = false;
            game.isopened = true;
            StartCoroutine(game.hintObject1.OpenHints());
        }

    }
    public void NewCompareGame()
    {

        RefManager.Instance.childAnimator.SetBool("isstop", true);
        basketBiggest.gameObject.GetComponent<BoxCollider>().enabled = false;
        basketMid.gameObject.GetComponent<BoxCollider>().enabled = false;
        basketSmallest.gameObject.GetComponent<BoxCollider>().enabled = false;

        if (game == Compare1)
        {
            game = Compare2;
            Compare2Canvas.SetActive(true);
            istrue = Compare2C.trueOne;
            OnMove();
        }
        else if (game == Compare2)
        {
            game = Compare3;
            Compare2Canvas.SetActive(false);
            Compare3Canvas.SetActive(true);
            istrue = Compare3C.trueOne;
            OnMove();

        }

    }
    public IEnumerator makeTrueFunction()
    {
        SoundManager.Instance.celebrate.PlaySound();
        yield return new WaitForSeconds(3);
        NewCompareGame();

    }
    public IEnumerator makefalseFunction()
    {
        SoundManager.Instance.fail.PlaySound();
        yield return new WaitForSeconds(4);
        NewCompareGame();

    }
    public IEnumerator FinishGame()
    {
       
        yield return new WaitForSeconds(3);
        SoundManager.Instance.compare3Succes.PlaySound();
        yield return new WaitForSeconds(2);
        UIManager.Instance.finishCanvas.SetActive(true);
        Time.timeScale = 0.1f;
    }
    private IEnumerator dance()
    {
        yield return new WaitForSeconds(1);
        RefManager.Instance.childAnimator.SetBool("isdance", false);
    }
}