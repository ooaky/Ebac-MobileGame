using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOLevelRandomSettup : ScriptableObject
{
    [Header("Art Settup Swap")]
    public ArtManager.ArtType artType;

    [Header("Randomization Pieces")]
    public List<LevelPieceBase> levelPieces;
    public List<LevelPieceBase> levelPiecesStart;
    public List<LevelPieceBase> levelPiecesEnd;

    public int nOfPieces = 5; //n° de pedaços de cenário a serem criados
    public int nOfPiecesStart = 2; //n° de pedaços de cenário a serem criados
    public int nOfPiecesEnd = 3; //n° de pedaços de cenário a serem criados

}
