using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EBAC.Core.Singleton;

public class ColorManager : Singleton<ColorManager>
{
    public List<Material> materials;
    public List<ColorSettup> colourSettup;

    public void ChangeColourByType(ArtManager.ArtType artType)
    {
        var settup = colourSettup.Find(i => i.artType == artType);

        for (int i = 0; i < materials.Count; i++)
        {
            materials[i].SetColor("_Color", settup.colors[i]); //pega a cor do index 1 e coloca no material de index 1
        }
    }
}


[System.Serializable]
public class ColorSettup
{
    public ArtManager.ArtType artType;
    public List<Color> colors; //para cada elemento da lista de materiais, escolhe-se a cor que estes irão se transformar, na ordem em que foram indexados
                               //com o TYPE, relacionado a lista presente no ArtManager
}