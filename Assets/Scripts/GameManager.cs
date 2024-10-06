using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Player player;
    
    [SerializeField] private float eventPeriod;
    private float _timer;

    [SerializeField] private TextMeshProUGUI timerText;
    private float _survivalTimer;

    [SerializeField] private Animation gameOver;
    
    [SerializeField]
    private Event[] _events;
    
    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    private void UpdateTimerText()
    {
        timerText.text = $"{Math.Floor(_survivalTimer / 60) :00}:{Math.Floor(_survivalTimer % 60):00}";
    }

    private void Update()
    {
        _survivalTimer += Time.deltaTime;
        UpdateTimerText();
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            _timer += eventPeriod;
            SpawnEvent();
        }
    }
    
    private void SpawnEvent()
    {
        var ev = Instantiate(_events[Random.Range(0, _events.Length)]);
        ev.Activate(player);
    }

    public void OnPlayerDeath()
    {
        enabled = false;
        gameOver.Play();
    }

    public void Reload()
    {
        SceneManager.LoadScene(1);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
