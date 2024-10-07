using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EBAC.Core.Singleton;
using TMPro;
using DG.Tweening;


public class PlayerController : Singleton<PlayerController>
{
    #region Variables

    #region Public
    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 1f;

    [Header("Movement")]
    public float speed = 1f;
    public float limit = 5;
   
    [Header("Text mesh")]
    public TextMeshPro uiTextPowerUp;

    [Header("End Game Tags")]
    public string enemyTag = "Enemy";
    public string endTag = "EndLine";
    public bool invincible = false;

    [Header("End Game Stuff")]
    public GameObject endScreen;

    [Header("Coin Setup")]
    public GameObject coinCollector;

    [Header("Animation")]
    public AnimatorManager animatorManager;
    public GameObject player;
    public float scaleDuration = .2f;
    public Ease ease = Ease.OutBack;

    [Header("VFX")]
    public ParticleSystem vfxDeath;






    #endregion

    #region Private Vars
    private Vector3 _pos;
    private bool _canRun;
    private float _currentSpeed;
    private Vector3 _startPosition;
    private float _baseAnimationSpeed = 10f;

    [SerializeField] private BounceHelper _bounceHelper;


    #endregion

    #endregion


    private void Start()
    {
        _startPosition = transform.position;
        StartCoroutine(ScalePlayerByTime());
        ResetSpeed();
    }



    void Update()
    {
        if (!_canRun) return;

        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        //limita a posição do personagem em X para nao sair do "Chão"
        if (_pos.x < -limit) _pos.x = -limit;
        else if (_pos.x > limit) _pos.x = limit;

        /*
        outra opção é a utilização de um Vector2, que pode ser mais flexivel em níveis assimetricos
        public Vector2 limitVector = new Vector2(limite 1, limite 2)
        if (_pos.x < limitVector.x) _pos.x = limitVector.x;
        else if (_pos.x > limitVector.y) _pos.x = limitVector.y;         
         */

        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        

        transform.Translate(transform.forward*_currentSpeed*Time.deltaTime); //adiciona movimento para frente no transform do objeto, multiplicado pela velocidade definida, dentro do relógio padronizado (apara nao ser afetado por framerate)
    }


    public void Bounce()
    {
        if(_bounceHelper != null)
           _bounceHelper.Bounce();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == enemyTag)
        {
            if (!invincible)
            {
                MoveBack();
                EndGame(AnimatorManager.AnimationType.DEAD);//passa a animação de dead neste caso de colisão
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == endTag)
        {
            if (!invincible) EndGame(); //se invincible = false, permite finalizar o jogo
        }
    }

    private void MoveBack() //move o personagem um pouco para tras para nao clippar na parede enemy
    {
        transform.DOMoveZ(-1f, 1f).SetRelative(); //-1 é relativo a posição atual dele
        /*outro jeito:
         * transform.position.z - 1f;
         */
    }

    private void EndGame(AnimatorManager.AnimationType animationType = AnimatorManager.AnimationType.IDLE) //para a variavel animationType (que só é um dos tipos presentes no animation manager, standard eh sempre IDLE, mas pode receber outras variaveis)
    {
        _canRun = false;
        animatorManager.Play(animationType);
        endScreen.SetActive(true);
        if(vfxDeath != null) vfxDeath.Play();
    }

    public void StartRunning()
    {
        _canRun = true;
        animatorManager.Play(AnimatorManager.AnimationType.RUN, _currentSpeed/_baseAnimationSpeed); //leva para o animator manager e altera a velocidade da animação declarada
    }

    public void SetPowerUpText(string s)
    {
        uiTextPowerUp.text = s;
    }

    #region Power Up: Speed

    public void PowerUpSpeedUp(float f)
    {
        _currentSpeed = f; //vira a nova velocidade
    }

    public void ResetSpeed()
    {
        _currentSpeed = speed;
    }
    #endregion

    #region Power Up: Invincible

    public void SetInvincible(bool b = true)
    {
        invincible = b;
    }

    #endregion

    #region Power Up: Height

    public void ChangeHeight(float amount, float duration, float animationDuration, Ease ease)
    {
        //Módo básico, sem animação
        /*var p = transform.position; //salva a posição atual
        p.y = _startPosition.y + amount; //altera a posição Y para a posição inicial + a definição de altura de voo definido 
        transform.position = p;*/

        transform.DOMoveY(_startPosition.y + amount,animationDuration).SetEase(ease);//.OnComplete(ResetHeight);a
        Invoke(nameof(ResetHeight), duration);
    }

    public void ResetHeight(float animationDuration)
    {
        /*var p = transform.position;
        p.y = _startPosition.y;
        transform.position = p;*/

        transform.DOMoveY(_startPosition.y, animationDuration);
    }

    #endregion

    #region Power Up: Collect coins

    public void ChangeCoinCollectorSize(float amount)
    {
        coinCollector.transform.localScale = Vector3.one * amount;
    }

    #endregion

    #region Animation

    IEnumerator ScalePlayerByTime() 
    {
        gameObject.transform.localScale = Vector3.zero;

        yield return null;

        gameObject.transform.DOScale(1, scaleDuration).SetEase(ease);
    }



    #endregion
}
