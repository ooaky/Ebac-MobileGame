using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControl : MonoBehaviour
{
    public Vector2 pastPosition;
    public float velocity = 1f;

    //Input.GetTouch(0); // não é testavel no computador, busca direto no touch, mas é melhor no geral (acho)
    //Input.GetMouseButton(0); //funciona similar, mas para o mouse

    void Start()
    {
        
    }

    void Update()
    {

        if(Input.GetMouseButton(0)) //enquanto o mouse for segurado, o objeto se move em conjunto (no caso de touch o objeto acompanha o dedo relativo a onde foi tocado na tela)
        {
            //mousePosition ATUAL - mousePosition PASSADO
            Move(Input.mousePosition.x - pastPosition.x);
        }
        pastPosition = Input.mousePosition; // ao clicar no botao do mouse, pega a posição atual dele e transforma na nova posição
    }


    public void Move(float speed)
    {

        transform.position += Vector3.right * Time.deltaTime * speed *velocity;
    }


}
