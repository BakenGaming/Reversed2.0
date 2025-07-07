using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class StaticVariables : MonoBehaviour
{
    public static StaticVariables i;
    [SerializeField] private LayerMask whatIsNonInteractable, whatIsGround, whatIsHazard, whatIsSecret,
        whatIsPlayer, whatIsPlayerProjectile, whatIsEnemy, whatIsEnemyProjectile, whatIsCollectable, whatIsUI;
    [SerializeField] private AudioMixerGroup masterMixer, sfxMixer, musicMixer;

    private void Awake() 
    {
        i = this;
    }

    public LayerMask GetNonInteractableLayer() { return whatIsNonInteractable; }
    public LayerMask GetGroundLayer() { return whatIsGround; }
    public LayerMask GetHazardLayer() { return whatIsHazard; }
    public LayerMask GetSecretLayer() { return whatIsSecret; }
    public LayerMask GetPlayerLayer() { return whatIsPlayer; }
    public LayerMask GetPlayerProjectileLayer() { return whatIsPlayerProjectile; }
    public LayerMask GetEnemyLayer() { return whatIsEnemy; }
    public LayerMask GetEnemyProjectileLayer() { return whatIsEnemyProjectile; }
    public LayerMask GetCollectableLayer() { return whatIsCollectable; }
    public LayerMask GetUILayer(){ return whatIsUI; }
    public AudioMixerGroup GetMasterMixer(){ return masterMixer; }
    public AudioMixerGroup GetSFXMixer(){ return sfxMixer; }
    public AudioMixerGroup GetMusicMixer(){ return musicMixer; }

}
