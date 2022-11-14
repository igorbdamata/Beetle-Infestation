using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShakeController : MonoBehaviour
{
    public void SetShake(AnimationCurve gain, AnimationCurve freuquency, float shakeTime)
    {
        StartCoroutine(SetShakeIEnumerator(gain, freuquency, shakeTime));
    }

    private IEnumerator SetShakeIEnumerator(AnimationCurve gain, AnimationCurve frequency, float shakeTime)
    {
        CinemachineBasicMultiChannelPerlin perlin = GetComponent<CinemachineVirtualCamera>().
                                                    GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        float timer = 0;
        while (timer < shakeTime)
        {

            perlin.m_AmplitudeGain = gain.Evaluate(timer);
            perlin.m_FrequencyGain = frequency.Evaluate(timer);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        perlin.m_AmplitudeGain = 0;
        perlin.m_FrequencyGain = 0;
    }
}
