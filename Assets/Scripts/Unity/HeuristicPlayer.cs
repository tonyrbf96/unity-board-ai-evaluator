using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteligenceEngine;
using System;

public class HeuristicPlayer : MonoBehaviour
{
    GameController controller;

    readonly Dictionary<KeyCode, int> keys = new Dictionary<KeyCode, int>() {
        { KeyCode.LeftArrow, 3 },
        { KeyCode.RightArrow, 1 },
        { KeyCode.UpArrow, 0 },
        { KeyCode.DownArrow, 2 },
    };

    int wins, loses;

    void Awake()
    {
        controller = GetComponent<GameController>();
    }

    void Start()
    {
        controller.RestartGame();
        controller.DrawGame();
    }

    void Update()
    {
        var inputs = new bool[Game.actions];

        foreach (var key in keys.Keys)
        {
            if (Input.GetKeyDown(key))
            {
                inputs[keys[key]] = true;

                controller.UpdateGame(inputs);
                controller.DrawGame();
                break;

            }
        }

        switch (controller.state)
        {
            case Game.State.Idle:
                break;
            case Game.State.Win:
                Start();
                wins++;
                Debug.Log($"Win {wins}! Points: {controller.points}", this);
                break;
            case Game.State.Lose:
                Start();
                loses++;
                Debug.Log($"Lose {loses}! Points: {controller.points}", this);
                break;
        }

    }



}
