using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RefManager : MonoBehaviour
{
    public static RefManager Instance;
    public Animator childAnimator;
    public Animator TrueAnswer;
    public Animator motherAnimator;
    public NavMeshAgent motherAgent;
    public NavMeshAgent childAgent;
    public GameObject hint1;
    // Start is called before the first frame update


    private void Awake()
    {
        Instance = this;
    }

}
