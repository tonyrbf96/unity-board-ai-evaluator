using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine;
using InteligenceEngine;
using System;
using System.Linq;
using Unity.MLAgents.Policies;

public abstract class BaseAgent : Agent
{
    private GameController _controller;
    protected GameController controller {
        get {
            if (!_controller)
                GetComponent<GameController>();
            return _controller;
        }
    }


    int wins, loses;


    public override void OnEpisodeBegin()
    {
        controller.RestartGame();
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var _actions = new bool[Game.actions];
        _actions[actions.DiscreteActions.Array[0]] = true;

        _controller.UpdateGame(_actions);

        AddRewards();

        switch (controller.state)
        {
            case Game.State.Idle:
                break;
            case Game.State.Win:
                EndEpisode();
                wins++;
                Debug.Log($"Win {wins}! Points: {controller.points}", this);
                break;
            case Game.State.Lose:
                EndEpisode();
                loses++;
                Debug.Log($"Lose {loses}! Points: {controller.points}", this);
                break;
        }

    }

    public abstract void AddRewards();

}
