using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compare1Controller : MonoBehaviour
{
    public Transform childTarget;
    public float destinationReachedTreshold;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startWalk());
        SoundManager.Instance.compare1Sound.PlaySound();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDestinationReached();
    }
    public void OnMove()
    {
        RefManager.Instance.childAnimator.SetBool("iswalk", true);
        RefManager.Instance.motherAnimator.SetBool("isidle", true);
        RefManager.Instance.childAgent.destination = childTarget.position;
    }
    IEnumerator startWalk()
    {
        yield return new WaitForSeconds(SoundManager.Instance.compare1Sound.clip.length);
        OnMove();
    }
    void CheckDestinationReached()
    {
        float distanceToTarget = Vector3.Distance(RefManager.Instance.childAgent.transform.position, childTarget.transform.position);
        if (distanceToTarget < destinationReachedTreshold)
        {
            OnStop();
        }
    }
    public void OnStop()
    {
        GameManager.Instance.NextLevel();
    
    }
}
