using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using EBAC.Core.Singleton;
using System.Linq; //para usar o OrderBy entre outras coisas

public class CoinsAnimatorManager : Singleton<CoinsAnimatorManager>
{
    public List<ItemCollectableCoin> coins;

    [Header("Animation")]
    public float scaleDuration = .2f;
    public float scaleTimeBetweenPieces = .1f;
    public Ease ease = Ease.OutBack;


    private void Start()
    {
        coins = new List<ItemCollectableCoin>();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.R)) StartAnimations();
    }

    public void RegisterCoin(ItemCollectableCoin i)
    {
        if(!coins.Contains(i))
        {
            coins.Add(i);
            i.transform.localScale = Vector3.zero;
        }
    }

    public void StartAnimations()
    {
        StartCoroutine(ScalePiecesByType());
    }


    IEnumerator ScalePiecesByType()
    {
        foreach (var p in coins)
        {
            p.transform.localScale = Vector3.zero; //define o tamanho de todas as peças como 0 no começo
        }

        Sort();

        yield return null;

        for (int i = 0; i < coins.Count; i++)
        {
            coins[i].transform.DOScale(1, scaleDuration).SetEase(ease); //aumenta o tamanho das peças para 1 durante .2 segundos

            yield return new WaitForSeconds(scaleTimeBetweenPieces);
        }
    }

    public void Sort()
    {
        coins = coins.OrderBy(
            x => Vector3.Distance(this.transform.position, x.transform.position)).ToList(); //ordena a lista baseada na distancia entre o transform desse objeto com relação as moedas e retorna para a lista
    }

    public void CleanSpawnedCoins()
    {
        for (int i = coins.Count - 1; i >= 0; i--) //deleta do maior para o menor para não conflitar com as numerações de index
        {
            Destroy(coins[i].gameObject);
        }

        coins.Clear(); //limpa o array -> permite que o swpan seja no lugar certo e que a lista nao acabe sendo deletada e trave o jogo
    }
}
