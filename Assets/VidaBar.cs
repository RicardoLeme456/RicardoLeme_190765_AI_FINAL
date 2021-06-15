using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VidaBar : MonoBehaviour
{
    public Drive Barra;
    public float vida;
   // public Tiro TT;
    public float Vida
    {
        get
        {
            return vida;
        }
        set
        {
            vida = Mathf.Clamp(value, 0, MaxLife);
        }
    }
    public float MaxLife = 100;

    public Image balaBar;


    private void Update()
    {
        UpdateBalaBar();
        //bala = TT.Ballas;
        vida = Barra.health;
    }

    private void UpdateBalaBar()
    {
        balaBar.fillAmount = vida / MaxLife;
    }

    public void AddBala()
    {
        Vida += 0.5f;
    }

    public void TiraBallass()
    {
        Vida -= 0.5f;
    }
}
