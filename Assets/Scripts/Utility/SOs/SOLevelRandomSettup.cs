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

    public int nOfPieces = 5; //n� de peda�os de cen�rio a serem criados
    public int nOfPiecesStart = 2; //n� de peda�os de cen�rio a serem criados
    public int nOfPiecesEnd = 3; //n� de peda�os de cen�rio a serem criados

}
