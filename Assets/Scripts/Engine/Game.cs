using System;

namespace InteligenceEngine
{

    public abstract class Game
    {
        // This should be unique acroos all implemented games
        public static string name;
        public static string description;

        /// <summary>
        /// The current points of the game
        /// </summary>
        public int points;

        /// <summary>
        /// The maximun amount of points recheables on the game design. If -1, them infinite. If 0, then this game is not using point systems.
        /// </summary>
        public abstract int maxpoints { get; }

        // This are some constrains used for the trainig and testing engine. All games should use this fixed values.
        public const int height = 15;
        public const int width = 15;
        public const int actions = 4;


        public Game()
        {

        }


        /// <summary>
        /// Play one step of the game and return the gained points of this step
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public abstract int Play(bool[] inputs);
        /// <summary>
        /// Get the [GameBoardState] of the current game.
        /// </summary>
        /// <returns></returns>
        public abstract GameBoardState GetBoard();
        /// <summary>
        /// Get a version of the board interpretation of the game for debug purposes
        /// </summary>
        /// <returns></returns>
        public abstract GameBoardState GetBoardForDebug();

        /// <summary>
        /// Return the [State] of the game. Idle means that the game is still running.
        /// </summary>
        /// <returns></returns>
        public virtual State state { get; protected set; }

        public enum State
        {
            Idle, Win, Lose
        }

    }

    public static class GameUtils {

        public static (int, int) GetRandomPosition()
        {
            return (UnityEngine.Random.Range(0, Game.width), UnityEngine.Random.Range(0, Game.height));
        }
    }



}