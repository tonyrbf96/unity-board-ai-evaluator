using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using InteligenceEngine;

public sealed class GameController : MonoBehaviour
{
    public int id;

    IBoardDrawer drawer;
    public bool enableRendering;

    [GameTypeDrowdown]
    public string gameName;


    private void Awake()
    {
        drawer = Dependencies.Get<IBoardDrawer>();
    }

    public void RestartGame()
    {
        game = CreateGame();
        board = game.GetBoard();
    }

    public void DrawGame()
    {
        if (!enableRendering) return;
        drawer.DrawBoard(game.GetBoardForDebug(), id);
    }

    public int lastPoints;
    public void UpdateGame(bool[] actions)
    {
        Debug.Assert(state != Game.State.Idle, $"The game is already finished, please call {nameof(GameController)}.{nameof(RestartGame)} before continue.", this);
        Debug.Assert(actions.Length == 4, $"The array \"{nameof(actions)}\" size is distint of 4", this);


        game.Play(actions);

        lastPoints = game.points - lastPoints;

        board = game.GetBoard();
    }



    public GameBoardState board { get; private set; }
    public Game.State state => game.state;
    public int points => game.points;

    private Game game;

    private Game CreateGame()
    {
        // Create a instance of the game
        var gameType = AppDomain.CurrentDomain.GetAllDerivedTypes<Game>().First(t => t.Name == gameName);

        return Activator.CreateInstance(gameType) as Game;
    }


}
