using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


[RequireComponent(typeof(MeshRenderer))] //faz com que o script só seja utilizavel se o objeto tem um Mesh Renderer na hierarquia

public class CenarioColorChange : MonoBehaviour
{
    public float duration = 1f;
    public MeshRenderer meshRenderer;
    public Color startColor = Color.white;

    private Color _correctColor;

    private void OnValidate() //puxa automaticamente no sistema
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        _correctColor = meshRenderer.materials[0].GetColor("_Color");
        LerpColor();
    }

    private void LerpColor()
    {
        meshRenderer.materials[0].SetColor("_Color", startColor);
        meshRenderer.materials[0].DOColor(_correctColor, duration).SetDelay(.5f);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) LerpColor();
    }

}
