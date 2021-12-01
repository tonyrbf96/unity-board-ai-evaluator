using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

using InteligenceEngine;
using System;
using System.Linq;
public class PlayerAgent : Agent
{

    EngineController engine;
    Game game => engine.game;

    private void Awake()
    {
        engine = GetComponent<EngineController>();


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

        foreach (var tile in engine.board.tiles)
        {
            sensor.AddObservation(tile);
        }
        engine.DrawGame();


    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        //Debug.Log("OnActionReceived");
        var _actions = new bool[6];
        //Debug.Log(actions.DiscreteActions.Array[0]);
        _actions[actions.DiscreteActions.Array[0]] = true;

        engine.UpdateGame(_actions);

        AddReward(-1);

        var gameState = game.state;

        if (gameState == Game.State.Win)
        {
            AddReward(100);
            EndEpisode();

            Debug.Log($"Win! { ++winCounter}");
        }

        if (gameState == Game.State.Lose)
        {
            AddReward(-10);
            EndEpisode();
            Debug.Log("Lose!");
        }
    }

    public static int winCounter = 0;

    public void Play(Action<bool[]> callback)
    {

    }
}
