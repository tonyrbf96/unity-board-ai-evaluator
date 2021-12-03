using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteligenceEngine;
public class Snake : Game
{
    public static new string description = "Move the player to the marked target";


    (int, int) head;

    List<(int, int)> tail = new List<(int, int)>();

    List<(int, int)> targets = new List<(int, int)>();

    int maxTargets = 3;



    public override int maxPoints => 5;

    public Snake() : base()
    {


        for (int i = 0; i < maxTargets; i++)
        {
            targets.Add(GameUtils.GetRandomPosition());
        }

        head = GameUtils.GetRandomPosition();

        points = Random.Range(0, 3);

    }


    public enum TileType
    {
        empty = 0, player = 1, target = 2 , tail = 3
    }


    public override GameBoardState GetBoard()
    {
        var board = new GameBoardState(height, width);

        board[head.Item1, head.Item2] = (int)TileType.player;

        

        foreach (var t in tail)
        {
            board[t.Item1, t.Item2] = (int)TileType.tail;
        }

        foreach (var target in targets)
        {
            board[target.Item1, target.Item2] = (int)TileType.target;
        }
        return board;
    }




    public override void Play(bool[] inputs)
    {
        tail.Add(head);

        head.Item2 += (inputs[0] ? 1 : 0) + (inputs[2] ? -1 : 0);
        head.Item1 += (inputs[1] ? 1 : 0) + (inputs[3] ? -1 : 0);


        head.Item1 = Mathf.Clamp(head.Item1, 0, width - 1);
        head.Item2 = Mathf.Clamp(head.Item2, 0, height - 1);

        

        if (tail.Count > points)
            tail.RemoveAt(0);

        foreach (var i in tail.ToArray())
        {
            if (i == head)
            {
                state = State.Lose;
               
            }
        }

        foreach (var target in targets.ToArray())
        {
            if (target == head)
            {
                points += 1;

                targets.Remove(target);

                targets.Add(GameUtils.GetRandomPosition());

                if (targets.Count == 0)
                    state = State.Win;

                points++;
            }
        }
    }


    public override GameBoardState GetBoardForDebug()
    {
       return GetBoard();
    }
}
