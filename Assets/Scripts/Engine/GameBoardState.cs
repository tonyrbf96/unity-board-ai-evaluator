using System;



namespace InteligenceEngine
{
    public struct GameBoardState
    {
        public int height, width;

        public int[] tiles;

        public GameBoardState(int height, int width)
        {
            this.height = height;
            this.width = width;
            tiles = new int[width* height];
        }


        public int this[int x, int y]
        {
            get => tiles[y * width + x];
            set => tiles[y * width + x] = value;
        }
    }


}