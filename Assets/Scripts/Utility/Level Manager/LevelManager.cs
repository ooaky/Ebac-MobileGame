using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    [Header("General Level Spawning")]
    public Transform container;
    public List<GameObject> levels;

    //private
    [SerializeField] private int _index; //serialize field permite aparecer o espectro na engine, similar a se fosse publico, index come�a do 0 sempre
    private GameObject _currentLevel;

    public List<SOLevelRandomSettup> levelRandomSettup;

    [Header("Animation")]
    public float scaleDuration = .2f;
    public float scaleTimeBetweenPieces = .1f;
    public Ease ease = Ease.OutBack;



    private List<LevelPieceBase> _spawnedPieces = new List<LevelPieceBase>();
    private SOLevelRandomSettup _currSettup;
    public float timeBetweenPieces = 1f; //coroutine settup, delay between pieces spawning

    private void Start() //usar start para garantir que o level � gerado ap�s o singleton dos managers
    {
        //SpawnNextLevel(); //para spawn de um n�vel completo
        //CreateLevel(); //cria um level aleatorio na ordem do Index
        CreateRandomLevel(); //cria um level aleat�rio do index toda vez que inicia o jogo

    }

    #region Controlled Level Spawning

    private void SpawnNextLevel()
    {
        if (_currentLevel != null) 
        {
            Destroy(_currentLevel);
            _index++;
            if(_index >= levels.Count)
            {
                ResetLevelIndex();
            }
        }
        _currentLevel = Instantiate(levels[_index], container); //instancia/cria um level (prefab), com o n� indicado pelo index presente no container (empty de cena com as coodenadas para definir o 0)
        _currentLevel.transform.localPosition = Vector3.zero; //garante que o level vai spawnar nas coordenadas 0,0,0
    }

    private void ResetLevelIndex()
    {
        _index = 0;
    }

    #endregion

    #region Random Level generator

    //vers�o sem ser corrotina

    private void CreateLevel()
    {
        CleanSpawnedPieces();

        if (_currSettup != null)
        {
            _index++;
            if (_index >= levelRandomSettup.Count) ResetLevelIndex();
        }

        _currSettup = levelRandomSettup[_index];

        for (int i = 0; i < _currSettup.nOfPiecesStart; i++) //cria um n� de peda�os de cenario da lista especificada usando a fun��o, repetendo para cada int dentro da variavel
        {
            CreateLevelPieces(_currSettup.levelPiecesStart);
        }

        for (int i = 0; i < _currSettup.nOfPieces; i++) //cria um n� de peda�os de cenario da lista especificada usando a fun��o, repetendo para cada int dentro da variavel
        {
            CreateLevelPieces(_currSettup.levelPieces);
        }

        for (int i = 0; i < _currSettup.nOfPiecesEnd; i++) //cria um n� de peda�os de cenario da lista especificada usando a fun��o, repetendo para cada int dentro da variavel
        {
            CreateLevelPieces(_currSettup.levelPiecesEnd);
        }

        ColorManager.Instance.ChangeColourByType(_currSettup.artType);

        StartCoroutine(ScalePiecesByTime());
    }

    private void CreateRandomLevel()
    {
        CleanSpawnedPieces();

        var levelRange = Random.Range(0, levelRandomSettup.Count);

        if (_currSettup != null)
        {

            _index = levelRange;
            if (_index >= levelRandomSettup.Count) ResetLevelIndex();
        }

        _currSettup = levelRandomSettup[levelRange];

        for (int i = 0; i < _currSettup.nOfPiecesStart; i++) //cria um n� de peda�os de cenario da lista especificada usando a fun��o, repetendo para cada int dentro da variavel
        {
            CreateLevelPieces(_currSettup.levelPiecesStart);
        }

        for (int i = 0; i < _currSettup.nOfPieces; i++) //cria um n� de peda�os de cenario da lista especificada usando a fun��o, repetendo para cada int dentro da variavel
        {
            CreateLevelPieces(_currSettup.levelPieces);
        }

        for (int i = 0; i < _currSettup.nOfPiecesEnd; i++) //cria um n� de peda�os de cenario da lista especificada usando a fun��o, repetendo para cada int dentro da variavel
        {
            CreateLevelPieces(_currSettup.levelPiecesEnd);
        }

        ColorManager.Instance.ChangeColourByType(_currSettup.artType);

        StartCoroutine(ScalePiecesByTime());
    }



    private void CreateLevelPieces(List<LevelPieceBase> list)
    {
        var piece = list[Random.Range(0, list.Count)]; //escolhe uma pe�a aleatoria desde a indicada como 0 na lsita at� o numero total de pe�as listadas
        var spawnedPiece = Instantiate(piece, container);

        if(_spawnedPieces.Count > 0)
        {
            var lastpiece = _spawnedPieces[_spawnedPieces.Count - 1]; //identifica a ultima pe�a pela posi��o na lista

            spawnedPiece.transform.position = lastpiece.endPiece.position; //pega a posi��o inicial e insere ela em cima da posi��o final da ultima pe�a
        }
        else
        {
            spawnedPiece.transform.localPosition = Vector3.zero;
        }

        foreach(var p in spawnedPiece.GetComponentsInChildren<ArtPiece>()) //para cada pe�a no ArtPiece ->
        {
            p.ChangePiece(ArtManager.Instance.GetSetupByType(_currSettup.artType).gameObject);
        }

        _spawnedPieces.Add(spawnedPiece); //adiciona a pe�a spawnada na lista
    }


    private void CleanSpawnedPieces()
    {
        for (int i = _spawnedPieces.Count - 1; i >= 0; i--) //deleta do maior para o menor para n�o conflitar com as numera��es de index
        {
            Destroy(_spawnedPieces[i].gameObject);
        }

        _spawnedPieces.Clear(); //limpa o array -> permite que o swpan seja no lugar certo e que a lista nao acabe sendo deletada e trave o jogo

        CoinsAnimatorManager.Instance.CleanSpawnedCoins();
        PowerUpAnimatorManager.Instance.CleanSpawnedPowerUps();
    }


    IEnumerator ScalePiecesByTime() //pode ser usado tamb�m em objetos menores como moedas e outras coisas para crescerem e ficar mais bonitinho
    {


        foreach (var p in _spawnedPieces)
        {
            p.transform.localScale = Vector3.zero; //define o tamanho de todas as pe�as como 0 no come�o
        }

        yield return null;

        for (int i = 0; i < _spawnedPieces.Count; i++)
        {
            _spawnedPieces[i].transform.DOScale(1, scaleDuration).SetEase(ease); //aumenta o tamanho das pe�as para 1 durante .2 segundos

            yield return new WaitForSeconds(scaleTimeBetweenPieces);
        }

        CoinsAnimatorManager.Instance.StartAnimations();
        PowerUpAnimatorManager.Instance.StartAnimations();

    }


    //vers�o corrotina
    /*private void CreateLevel()
    {
        StartCoroutine(CreateLevelPieceCouroutine());
    }*/

    //n�o est� em uso
    IEnumerator CreateLevelPieceCouroutine()
    {
        _spawnedPieces = new List<LevelPieceBase>();

        for (int i = 0; i <= _currSettup.nOfPieces; i++) //cria um n� de peda�os de cenario usando a fun��o, repetendo para cada int dentro da variavel
        {
            CreateLevelPieces(_currSettup.levelPieces);
            yield return new WaitForSeconds(timeBetweenPieces);
        }
    }

    #endregion


    #region Test triggers

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) SpawnNextLevel();
        if (Input.GetKeyDown(KeyCode.D)) CreateLevel();
    }

    #endregion
}
