using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EBAC.Core.Singleton
{
   public class Singleton<T> : MonoBehaviour where T : MonoBehaviour // parametro T pode apenas ser um Monobehaivour
    {
        public static T Instance;

        private void Awake() //roda no momento que o jogo/código inicia
        {
            if (Instance == null)
                Instance = GetComponent<T>(); //a instancia é igual ao script criado
            else
                Destroy(gameObject); //se existe outra instancia (instance é diferente de nulo), destroi a nova instancia
        }

    }
}