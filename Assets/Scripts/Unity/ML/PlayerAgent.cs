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

public class PlayerAgent : Agent
{

    EngineController engine;
    BehaviorParameters behaviour;
    Game game => engine.game;

    private void Awake()
    {
        engine = GetComponent<EngineController>();
        behaviour = GetComponent<BehaviorParameters>();

        behaviour.BrainParameters.VectorObservationSize = Game.width * Game.height * 3;
    }
    Coroutine punishmentCoroutine;


    public override void OnEpisodeBegin()
    {
        engine.RestartGame();

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //var player = engine.board.tiles.ToList().IndexOf(1);
        //var goal = engine.board.tiles.ToList().IndexOf(2);

        //sensor.AddObservation(player % engine.board.width);
        //sensor.AddObservation(player / engine.board.height);

        //sensor.AddObservation(goal % engine.board.width);
        //sensor.AddObservation(goal / engine.board.height);

        for (int x = 0; x < engine.board.width; x++)
        {
            for (int y = 0; y < engine.board.height; y++)
            {
                sensor.AddObservation(new Vector3Int(x, y, engine.board[x, y]));
            }
        }

        engine.DrawGame();


    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        //Debug.Log("OnActionReceived");
        var _actions = new bool[Game.actions];
        //Debug.Log(actions.DiscreteActions.Array[0]);
        _actions[actions.DiscreteActions.Array[0]] = true;

        var points = engine.UpdateGame(_actions);

        AddReward(-1);

        var gameState = game.state;

        if (points > 0)
        {
            AddReward(  10 * points);
        }

        if (gameState == Game.State.Win)
        {
            AddReward(40);
            EndEpisode();

            Debug.Log($"Win { ++winCounter}! {engine.game.points} points ");
        }

        if (gameState == Game.State.Lose)
        {
            AddReward(-20);
            EndEpisode();
            Debug.Log("Lose!");
        }




    }


    public static int winCounter = 0;

    public void Play(Action<bool[]> callback)
    {

    }
}
