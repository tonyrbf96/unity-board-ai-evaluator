using System.Linq;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using InteligenceEngine;

[RequireComponent(typeof(Tilemap))]
public class TilemapRender : MonoBehaviour, IBoardDrawer
{
    public int gridSize = 3;


    public Tile tile;
    public Grid grid;
    Tilemap tilemap;

    [Serializable]
    public struct TileColorPair
    {
        public Color color;
        public int id;
    }

    public TileColorPair[] colorsPerTile;


    
    private void Register() {


    }

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        grid = GetComponentInParent<Grid>();
    }

    public void ResetTilemap()
    {



    }

    public void DrawBoard(GameBoardState board, int index)
    {

        for (int h = 0; h < board.height; h++)
        {
            for (int w = 0; w < board.width; w++)
            {
                var color = colorsPerTile.FirstOrDefault((p) => p.id == board[w, h]).color;
                var pos = new Vector3Int(w + index % gridSize * (board.width + 2), h + index / gridSize * (board.height + 2) , 0);
                tilemap.SetTile(pos, tile);
                tilemap.SetColor(pos, color);
                tilemap.RefreshTile(pos);
            }
        }

        //var size =  Mathf.Max(board.width, board.height);
        //tilemap.transform.localPosition = new Vector3(-board.width / 2 , -board.height / 2 );
    }

    public void DrawInputs(bool[] inputs) { }
}


public interface IBoardDrawer
{
    void DrawBoard(GameBoardState board, int index);
}

