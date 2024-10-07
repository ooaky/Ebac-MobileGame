using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EBAC.Core.Singleton;
using TMPro;

public class ItemManager : Singleton<ItemManager>
{
    public SOInt coins;

    public TextMeshProUGUI textCoins;


    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        coins.value = 0;
        textCoins.text = "x 0";
        UIupdate();
    }

    public void AddCoin(int ammout = 1) //permite que em ocasioes especiais o ammout seja alterado quando a função é chamada Ex.: Addcoin(5) -> moeda agora vale 5
    {
        coins.value += ammout; //puxa o valor stored no SOint - o scriptable object
        UIupdate();
    }

    private void UIupdate()
    {
        //textCoins.text = "x " + coins.ToString();
        //UIinGameManager.Instance.UpdateTextCoins(coins.ToString());
        //UIinGameManager.UpdateTextCoins(coins.value.ToString());
    }
}
