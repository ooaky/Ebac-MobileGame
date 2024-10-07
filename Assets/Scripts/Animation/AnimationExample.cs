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
        animation.CrossFade(a.name); //permite transicionar entre 2 animações, necessario dar o nome da animação
        
        /* outra opção, mas que nao tem transição, só toca a animação:
         * animation.clip = a;
        animation.Cross();*/

    }

}
