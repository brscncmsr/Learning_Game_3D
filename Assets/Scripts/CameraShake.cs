using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public CinemachineVirtualCamera vcam;
    CinemachineBasicMultiChannelPerlin noise;

    void Start()
    {
        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Noise(float amplitudeGain, float frequencyGain)
    {
        noise.m_AmplitudeGain = amplitudeGain;
        noise.m_FrequencyGain = frequencyGain;
    }
    public IEnumerator NoiseStart(float v1,float v2)
    {
        Noise(v1, v2);
        yield return new WaitForSeconds(5);
        Noise(0, 0);
    }
}