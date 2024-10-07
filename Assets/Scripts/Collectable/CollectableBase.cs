using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBase : MonoBehaviour
{
    public string compareTag = "Player";
    public ParticleSystem particleSystem;
    public float timeToHide = 2f;
    public GameObject graphicItem;

    [Header("Sound")]
    public AudioSource audioSource;

    private void Awake()
    {
        //if (particleSystem != null) particleSystem.transform.SetParent(null); //define uma nova hierarquia -> null deixa ele largado na hierarquia geral do projeto
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }


    protected virtual void Collect()
    {
        if(graphicItem!=null) graphicItem.SetActive(false); 
        Invoke("HideObject", timeToHide);
        OnCollect();

    }

    private void HideObject()
    {
        gameObject.SetActive(false); // só desliga, pode ser religado em outras funções ie. timer pra coletar todas?
    }

    protected virtual void OnCollect()
    {
        if (particleSystem != null)
        {
            particleSystem.transform.SetParent(null); //tira o sistema de dentro do objeto para que nao seja deletado imediatamente
            particleSystem.Play();
        }
        if (audioSource != null) audioSource.Play();
    }

}
