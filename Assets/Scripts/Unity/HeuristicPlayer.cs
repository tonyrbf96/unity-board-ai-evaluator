using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteligenceEngine;
using System;

public class HeuristicPlayer : MonoBehaviour
{
    [Range(0.001f,0.3f)]
    public float deltaTime;
    readonly Dictionary<KeyCode, int> keys = new Dictionary<KeyCode, int>() {
        { KeyCode.LeftArrow, 3 },
        { KeyCode.RightArrow, 1 },
        { KeyCode.UpArrow, 0 },
        { KeyCode.DownArrow, 2 },
    };


    public void Play(Action<bool[]> callback)
    {
        StartCoroutine(InputEnumerator(callback));
    }

    IEnumerator InputEnumerator(Action<bool[]> callback)
    {
        bool[] inputs = new bool[keys.Count];

        var delta = 0f;
        while (delta < deltaTime)
        {
            yield return null;
            foreach (var key in keys)
            {
                if (Input.GetKeyDown(key.Key))
                    inputs[key.Value] = Input.GetKeyDown(key.Key);
            }

            delta += Time.deltaTime;
        }


        callback?.Invoke(inputs);


    }
}
