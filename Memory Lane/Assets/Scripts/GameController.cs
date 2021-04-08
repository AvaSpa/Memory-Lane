﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Mover Mover;
    public TileGenerator Platform;
    public GameObject Panel;
    public GameObject ResetButton;
    public Text LevelText;
    public CountDown CountDown;
    public int CurrentLevel;
    public Transform Camera;
    public int CameraTranslationSpeed;
    public int CameraRotationSpeed;

    private const string CurrentLevelKey = "CurrentLevel";
    private const string MaxLevelKey = "MaxLevel";

    private void Awake()
    {
        CurrentLevel = PlayerPrefs.GetInt(CurrentLevelKey, 1);
        LevelText.text = $"Level: {CurrentLevel}";
    }

    private void Start()
    {
        StartCoroutine(MoveCameraToPlayPosition(CameraTranslationSpeed));
        StartCoroutine(RotateCameraToPlayPosition(CameraRotationSpeed));
    }

    private IEnumerator MoveCameraToPlayPosition(float speed = 1)
    {
        var destination = new Vector3(20, 40, -30);
        while (Camera.position != destination)
        {
            Camera.position = Vector3.MoveTowards(Camera.position, destination, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator RotateCameraToPlayPosition(float speed = 1)
    {
        var destination = new Vector3(75, 0, -20);
        while (Camera.rotation.eulerAngles != destination)
        {
            Camera.rotation = Quaternion.RotateTowards(Camera.rotation, Quaternion.Euler(destination), speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    public void EndGame(bool fail)
    {
        if (fail)
            GameOver();
        else
            LevelComplete();
    }

    private void LevelComplete()
    {
        CurrentLevel++;
        PlayerPrefs.SetInt(CurrentLevelKey, CurrentLevel);

        var currentMaxLevel = PlayerPrefs.GetInt(MaxLevelKey, 1);
        if (currentMaxLevel <= CurrentLevel)
            PlayerPrefs.SetInt(MaxLevelKey, CurrentLevel);

        PlayerPrefs.Save();

        var buttonScript = ResetButton.GetComponent<ResetButtonHandler>();
        buttonScript.Next.SetActive(true);
        buttonScript.Restart.SetActive(false);

        Panel.SetActive(false);
        ResetButton.SetActive(true);
    }

    private void GameOver()
    {
        Mover.Kill();
        Platform.Collapse();
        Panel.SetActive(false);
        ResetButton.SetActive(true);
    }
}
