using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BasketController : MonoBehaviour
{
    public bool isTrue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        if (isTrue)
        {
            Compare3Controller.Instance.TruePod();
            transform.DOPunchPosition(Vector3.up, 0.5f, vibrato: 5);
        }
        else
        {
            Compare3Controller.Instance.FalsePod();
            transform.DOShakeScale(1, strength: 0.2f);

        }
    }
}
