using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableCoin : CollectableBase
{
    public Collider collider;
    public bool collect = false;
    public float lerp = 5f;
    public float minDistance = 1f;


    private void Start()
    {
        CoinsAnimatorManager.Instance.RegisterCoin(this);
    }

    protected override void OnCollect()
    {
        base.OnCollect();
        collider.enabled = false;
        collect = true;
        PlayerController.Instance.Bounce();
    }

    protected virtual void HideItens()
    {
        if (graphicItem != null) graphicItem.SetActive(false);
        Invoke("HideObject", timeToHide);
    }

    protected override void Collect()
    {
        HideItens();
        OnCollect();
    }

    private void Update()
    {
        if (collect)
        {
           transform.position = Vector3.Lerp(transform.position,PlayerController.Instance.transform.position, lerp * Time.deltaTime); //lerp da posição do player para a posição da coin -- arrsata moedas longe e coleta moedas perto

           if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < minDistance)
           {
                HideItens();
                Destroy(gameObject);
           }
        }
    }



}