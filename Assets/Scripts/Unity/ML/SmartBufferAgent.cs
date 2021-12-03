using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class SmartBufferAgent : BaseAgent
{

    public override void CollectObservations(VectorSensor sensor)
    {
        base.CollectObservations(sensor);
    }


    public override void OnActionReceived(ActionBuffers actions)
    {
        base.OnActionReceived(actions);
    }

}
