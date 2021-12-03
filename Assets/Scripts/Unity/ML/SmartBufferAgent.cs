using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using InteligenceEngine;
public class SmartBufferAgent : BaseAgent
{

    BufferSensorComponent bufferSensor;



    public override void CollectObservations(VectorSensor sensor)
    {
        var board = controller.board;

        for (int w = 0; w < Game.width; w++)
        {
            for (int h = 0; h < Game.height; h++)
            {
                if (board[w, h] != 0)
                {
                    bufferSensor.AppendObservation( new[] { w / (float)Game.width, h / (float)Game.height , board[w,h]});
                }
            }
        }
    }


    int points;
    public override void AddRewards()
    {
        AddReward( 10 * (controller.points - points));

        if (controller.state == Game.State.Win)
        {
            AddReward(100);

        }

        else if (controller.state == Game.State.Win)
        {
            AddReward(-50);
        }

    }
}
