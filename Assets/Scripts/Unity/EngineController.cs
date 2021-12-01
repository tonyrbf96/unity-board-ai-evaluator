using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using InteligenceEngine;

public class EngineController : MonoBehaviour
{
    public int id;
    public int width, height = 20;
    IBoardDrawer drawer;
    public bool enableRendering;

    [GameTypeDrowdown]
    public string gameName;

    private Type gameType;

    private void Awake()
    {
        drawer = FindObjectOfType<TilemapRender>();

        // Get the correct gameType from all posibles games

    }

    public void RestartGame()
    {
        game = GetGame();
        board = game.GetBoard();
    }

    public void DrawGame()
    {
        if (!enableRendering) return;
        drawer.DrawBoard(game.GetBoardForDebug(), id);
    }

    public void UpdateGame(bool[] actions)
    {
        game.Play(actions);
        board = game.GetBoard();
    }

    public GameBoardState board { get; private set; }


    public Game game { get; private set; }


    public Game GetGame()
    {
        // Create a instance of the game
        if (gameType == null)
        {

            Debug.Log(gameName);

            gameType = AppDomain.CurrentDomain.GetAllDerivedTypes<Game>().First(t => t.Name == gameName);

        }
        return Activator.CreateInstance(gameType) as Game;
    }


}
