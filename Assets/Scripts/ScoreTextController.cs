using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTextController : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        scoreText.text = "";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        OnDisable();
        PlayerController.OnPlayerScoreUpdated += HandleOnPlayerScoreUpdated;
    }

    void OnDisable()
    {
        PlayerController.OnPlayerScoreUpdated -= HandleOnPlayerScoreUpdated;
    }

    private void HandleOnPlayerScoreUpdated(object sender, PlayerScoreUpdated e)
    {
        scoreText.text = e.ScoreTo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
