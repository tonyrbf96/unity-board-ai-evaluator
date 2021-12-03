using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

using InteligenceEngine;
using System;
using System.Linq;
using Unity.MLAgents.Policies;

public class BaseAgent : Agent
{
    private GameController _controller;
    protected GameController controller {
        get {
            if (!_controller)
                GetComponent<GameController>();
            return _controller;
        }
    }


    public override void OnEpisodeBegin()
    {
        controller.RestartGame();
    }
}
