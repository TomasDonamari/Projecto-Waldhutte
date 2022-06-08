using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaPlayer : MonoBehaviour
{
    public float vida = 100;

    public Image barraDeVida;

    void Update()
    {
        vida = Mathf.Clamp(vida, 0, 100);

        barraDeVida.fillAmount = vida / 100;
    }
}
