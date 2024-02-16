using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PodsController : MonoBehaviour
{
    public bool isTrue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        if (isTrue)
        {
            Compare2Controller.Instance.TruePod();
            transform.DOPunchPosition(Vector3.up,0.5f,vibrato:5);
        }
        else
        {
            Compare2Controller.Instance.FalsePod();
            transform.DOShakeScale(1,strength:0.2f);

        }
    }
}
