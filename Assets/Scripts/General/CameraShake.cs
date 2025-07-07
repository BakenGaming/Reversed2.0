using Unity.Cinemachine;
using UnityEngine;


public static class CameraShake
{
    private static float shakeTimer;
    private static float baseShakeTime;
    private static float baseIntensity;
    private static CinemachineBasicMultiChannelPerlin _perlin;
    public static void ShakeCamera(float _intensity, float _time)
    {
        _perlin = GameManager.i.GetCinemachineCamera().GetComponent<CinemachineBasicMultiChannelPerlin>();

        _perlin.AmplitudeGain = _intensity;
        shakeTimer = _time;
        baseShakeTime = _time;
        baseIntensity = _intensity;

    }

    private static void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            _perlin.AmplitudeGain = Mathf.Lerp(baseIntensity, 0f, shakeTimer / baseShakeTime);
        }
    }
}
