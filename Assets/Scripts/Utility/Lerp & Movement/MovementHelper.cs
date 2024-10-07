using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHelper : MonoBehaviour
{
    public List<Transform> positions;

    public float duration = 1f;

    private int _index = 0;


    private void Start()
    {
        transform.position = positions[0].transform.position; //define a posição inicial como igual a do objeto no 0 do index
        NextIndex();
        StartCoroutine(StartMovement());
    }

    private void NextIndex()
    {
        _index++;

        if (_index >= positions.Count) _index = 0; //reseta o index quando passa do n° de valores no array/lista

    }



    //função excelente para fazer inimigos/oibjetos/obstaculos que se movem entre pontos fixos especificos
    IEnumerator StartMovement()
    {
        float time = 0f;
        
        while (true)
        {
            var currentPosition = transform.position;

            var nextPosition = Random.Range(0, positions.Count); //necessário para randomizar

            while(time < duration)
            {


                //transform.position = Vector3.Lerp(currentPosition, positions[nextPosition].transform.position, (time / duration));

                /* essa versão vai aleatoriamente entre os varios pontos do index
                 * por ser true random, pode acabar ficando parado em um mesmo ponto por um tempo
                 * para não ser true random, necessario adicionar outra função para que os pontos do index nao se repitam até que o index seja resetado
                */

                //versão não random, vai entre os pontos do index em ordem
                transform.position = Vector3.Lerp(currentPosition, positions[_index].transform.position, (time / duration));
                /*                                                 onde está[index]                tempo entre as duas posições/quanto já pasou
                 *                                                 se move em ordem entre cada item no index
                 *                                                                                 quando o duration terminar o lerp deve terminar 
                */

                time += Time.deltaTime;
                yield return null;
            }

            NextIndex();
            time = 0; //reseta o tempo para permitir um proximo movimento


            yield return null; //necessário adicionar em while(true) para garantir que há uma pausa no processamento e o resto do código possa ser lido (se não, da pau e fica preso só nessa parte da função)
        }


    }

}
