using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

using InteligenceEngine;
using System;
using System.Linq;
using Unity.MLAgents.Policies;

public class DummyAgent : BaseAgent
{

    BehaviorParameters behaviour;

    protected void Awake()
    {
        behaviour = GetComponent<BehaviorParameters>();
        behaviour.BrainParameters.VectorObservationSize = Game.width * Game.height * 3;
    }


    public override void CollectObservations(VectorSensor sensor)
    {
        for (int x = 0; x < controller.board.width; x++)
        {
            for (int y = 0; y < controller.board.height; y++)
            {
                sensor.AddObservation(new Vector3Int(x, y, controller.board[x, y]));
            }
        }

        controller.DrawGame();
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var _actions = new bool[Game.actions];
        _actions[actions.DiscreteActions.Array[0]] = true;

        var points = controller.UpdateGame(_actions);

        AddReward(-1);

        var gameState = controller.state;

        if (points > 0)
        {
            AddReward(  10 * points);
        }

        if (gameState == Game.State.Win)
        {
            AddReward(40);
            EndEpisode();

            Debug.Log($"Win { ++winCounter}! {controller.points} points ");
        }

        if (gameState == Game.State.Lose)
        {
            AddReward(-20);
            EndEpisode();
            Debug.Log("Lose!");
        }
    }


    public static int winCounter = 0;

}
