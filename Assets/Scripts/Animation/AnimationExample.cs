using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationExample : MonoBehaviour
{
    public Animation animation;

    public AnimationClip run;
    public AnimationClip idle;


    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayAnimation(run);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            PlayAnimation(idle);
        }
    }

    private void PlayAnimation(AnimationClip a)
    {
        animation.CrossFade(a.name); //permite transicionar entre 2 anima��es, necessario dar o nome da anima��o
        
        /* outra op��o, mas que nao tem transi��o, s� toca a anima��o:
         * animation.clip = a;
        animation.Cross();*/

    }

}
