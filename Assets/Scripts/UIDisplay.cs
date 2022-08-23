using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;


public class UIDisplay : MonoBehaviour
{
    [Header("Health")] 
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Health _playerHealth;

    [Header("Score")] 
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private ScoreKeeper _scoreKeeper;

    void Awake()
    {
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    
    void Start()
    {
        _healthSlider.maxValue = _playerHealth.GetHealth();
    }

    void Update()
    {
        _healthSlider.value = _playerHealth.GetHealth();
        _scoreText.text = _scoreKeeper.GetScore().ToString("  000000000");
    }
}
