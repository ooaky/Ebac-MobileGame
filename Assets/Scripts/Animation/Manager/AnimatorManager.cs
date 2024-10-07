using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
    public List<AnimationSettup> animationSettup; //cria uma lsita

    public enum AnimationType //itens da lista
    {
        IDLE,
        RUN,
        DEAD
    }

    public void Play(AnimationType type, float currentSpeedFactor = 1f)
    {
       
        //mesma coisa do de baixo por�m de "uma linha s�"
        /* animationSettup.ForEach(i => 
        { 
            if(i.type == type)
            {
                animator.SetTrigger(i.trigger);
            }
        });*/

        foreach(var animation in animationSettup)
        {
            if (animation.type == type)
            {
                animator.SetTrigger(animation.trigger);
                animator.speed = animation.animationSpeed * currentSpeedFactor; //permite controlar a velociadade base da anima��o via o manager
                break;
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Play(AnimationType.RUN);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Play(AnimationType.DEAD);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Play(AnimationType.IDLE);
        }

    }
}

[System.Serializable]
public class AnimationSettup
{
    public AnimatorManager.AnimationType type; //variaveis da lista, este primeiro s� puxa os valores do enum
    public string trigger;
    public float animationSpeed = 1f;
}