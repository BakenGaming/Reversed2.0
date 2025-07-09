using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour
{
    public static event Action OnLevelRestartSelected;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject creditsScreen;
    [SerializeField] private GameObject levelEndMenu;
    [SerializeField] private bool isMainMenu;
    private bool levelEndMove = false;
    private RectTransform levelEndRectTransform;
    private Vector3 levelEndPosition, levelEndFinalPosition;

    private void OnEnable()
    {
        if (isMainMenu) Initialize();
        GameManager.OnPlayerSpawned += Initialize;
    }
    private void OnDisable()
    {
        LevelEndHandler.OnLevelEndReached -= OpenLevelEndMenu;
        GameManager.OnPlayerSpawned -= Initialize;
    }
    private void Initialize()
    {
        //GetComponent<VolumeSettings>().Initialize();
        pauseMenu.SetActive(false);
        creditsScreen.SetActive(false);
        settingsMenu.SetActive(false);
        levelEndMenu.SetActive(false);

        levelEndRectTransform = levelEndMenu.GetComponent<RectTransform>();
        levelEndPosition = levelEndRectTransform.localPosition;
        levelEndFinalPosition = Vector3.zero;

        LevelEndHandler.OnLevelEndReached += OpenLevelEndMenu;
    }

    private void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
        GameManager.i.PauseGame();
    }

    public void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
        GameManager.i.UnPauseGame();
    }

    public void OpenSettingsMenu()
    {
        settingsMenu.SetActive(true);
        GetComponent<VolumeSettings>().SettingsMenuOpened();
    }

    public void CloseSettingsMenu()
    {
        settingsMenu.SetActive(false);
    }

    public void OpenCreditsScreen()
    {
        creditsScreen.SetActive(true);
    }

    public void CloseCreditsScreen()
    {
        creditsScreen.SetActive(false);
    }

    public void StartGame() { SceneController.StartGame(); }
    public void OpenLevelEndMenu() { levelEndMove = true; levelEndMenu.SetActive(true); }
    public void CloseLevelEndMenu() { levelEndMenu.SetActive(false); }

    public void RestartLevel()
    {
        OnLevelRestartSelected?.Invoke();
        CloseLevelEndMenu();
    }

    public void OpenLevelSelect()
    {
        Debug.Log("Level Select");
    }
    public void BackToMainMenu()
    {
        SceneController.LoadMainMenu();
    }

    public void ExitGame()
    {
        SceneController.ExitGame();
    }
    void Update()
    {
        if (levelEndMove)
        {
            levelEndPosition = Vector3.Lerp(levelEndRectTransform.localPosition, levelEndFinalPosition, .05f);
            if (Vector3.Distance(levelEndFinalPosition, levelEndRectTransform.localPosition) < .5f)
            {
                levelEndRectTransform.localPosition = levelEndFinalPosition;
                levelEndMove = false;
            }
            else levelEndRectTransform.localPosition = levelEndPosition;
        }
    }

}
