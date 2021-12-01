using System;

[Serializable]
public struct PlayRecordModel
{
    public string gameName;
    public string gameDescription;
    public int totalPoints;
    public int finalGameState; // 0 idle, 1 win, 2 lose
    public FrameRecordModel[] data;
}



[Serializable]
public struct FrameRecordModel {
    public int[][] input; 
    public bool[] output;
    public int points;
}