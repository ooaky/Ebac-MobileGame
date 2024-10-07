using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EBAC.Core.Singleton;

public class ArtManager : Singleton<ArtManager>
{
    public enum ArtType
    {
        TYPE_01,
        TYPE_02,
        TYPE_03,
        TYPE_04
    }

    public List<ArtSettup> artSettup;

    public ArtSettup GetSetupByType(ArtType artType)
    {
        return artSettup.Find(i => i.artType == artType); //O Find usa o método de parâmetro para achar um dado elemento e retorná-lo.
    }

}

[System.Serializable] //permite ser visto no UI da unity
public class ArtSettup
{
    public ArtManager.ArtType artType;
    public GameObject gameObject; //objeto que irá substituir o placeholder

}