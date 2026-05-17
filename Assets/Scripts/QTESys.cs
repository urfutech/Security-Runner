using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTESys : MonoBehaviour
{
    public TextMeshProUGUI shownKey;
    public TextMeshProUGUI progressTab;
    public Canvas QteCanvas;

    private KeyCode keyToPress;
    private int progress;
    private int result;// сколько надо
    private float timer;
    private float KeyChangeTime = 3f;
    // 0 - ничего 1 - удачно -1 - неудачно
    private int clickStatus;
    // таймер который убывает прогресс
    private float fireTimer = 3f;

    public void Init(int result)
    {
        progress = 0;
        this.result = result;
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        fireTimer -= Time.deltaTime;
        if (timer > KeyChangeTime)
        {
            var rnd = Random.Range(1, 4);
            if (rnd == 1) keyToPress = KeyCode.J;
            else if (rnd == 2) keyToPress = KeyCode.K;
            else if (rnd == 3) keyToPress = KeyCode.L;
            timer = 0;
        }

        ShowKey();
        GetInput();
        CheckInput();
        UpdateProgress();

        if (fireTimer < 0)
        {
            progress--;
            fireTimer = 4;
        }
        if (progress < 0)
            Die();
    }

    private void ShowKey()
    {
        shownKey.text = keyToPress.ToString();
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (keyToPress == KeyCode.J)
                clickStatus = 1;
            else clickStatus = -1;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (keyToPress == KeyCode.K)
                clickStatus = 1;
            else clickStatus = -1;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (keyToPress == KeyCode.L)
                clickStatus = 1;
            else clickStatus = -1;
        }
    }

    private void CheckInput()
    {
        if (clickStatus != 0)
        {
            if (clickStatus == 1)
            {
                progress++;
                timer += KeyChangeTime;
            }
            else if (clickStatus == -1)
            {
                progress--;
                timer += KeyChangeTime;
            }
            clickStatus = 0;
        }
    }

    private void UpdateProgress()
    {
        progressTab.text = 
            progress.ToString() + " / " + result;
    }

    private void Die()
    {
        GameManager.Instance.PlayerMove.SpeedFailure();
    }
}
