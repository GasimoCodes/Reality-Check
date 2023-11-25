using Cinemachine;
using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public class ScreenFX : MonoBehaviour
{
    private static ScreenFX instance;
    public CinemachineVirtualCamera playerCam;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static ScreenFX Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("ScreenFX instance is null.");
            }
            return instance;
        }
    }

    public void ShakeCurrentCamera(float amplitudeGain = 1f, float duration = 0.5f)
    {
        CinemachineImpulseSource ShakeImpulseSource = this.GetComponent<CinemachineImpulseSource>();

        if (ShakeImpulseSource != null)
        {
            ShakeImpulseSource.m_ImpulseDefinition.m_AmplitudeGain = amplitudeGain;
            ShakeImpulseSource.m_ImpulseDefinition.m_TimeEnvelope.m_SustainTime = duration;
            ShakeImpulseSource.GenerateImpulse();
        }
    }


}
