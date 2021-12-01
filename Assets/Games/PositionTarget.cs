
using UnityEngine;
using InteligenceEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class PositionTarget : Game
{
    public static new string description = "Move the player to the marked target";


    (int, int) player;

    List<(int, int)> playerMoves = new List<(int, int)>();

    (int, int) target;


    public override int maxpoints => 0;

    public PositionTarget() : base()
    {
        player = GameUtils.GetRandomPosition();
        target = GameUtils.GetRandomPosition();
    }


    public enum Colors
    {
        empty = 0, player = 1, target = 2, wall = 3,
    }


    public override GameBoardState GetBoard()
    {
        var board = new GameBoardState(height, width);

        board[player.Item1, player.Item2] = (int)Colors.player;
        board[target.Item1, target.Item2] = (int)Colors.target;

        return board;
    }


    

    public override int Play(bool[] inputs)
    {
        player.Item2 += (inputs[0] ? 1 : 0) + (inputs[2] ? -1 : 0);
        player.Item1 += (inputs[1] ? 1 : 0) + (inputs[3] ? -1 : 0);


        player.Item1 = Mathf.Clamp(player.Item1, 0, width - 1);
        player.Item2 = Mathf.Clamp(player.Item2, 0, height - 1);

        playerMoves.Add(player);

        state = player == target ? State.Win : State.Idle;

        return 0;
    }


    public override GameBoardState GetBoardForDebug()
    {
        var board = GetBoard();

        foreach (var move in playerMoves)
        {
            if (board[move.Item1, move.Item2] == 0)
                board[move.Item1, move.Item2] = 4;
        }

        return board;
    }
}
