using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vida : MonoBehaviour
{
    private int Vidas = 3;
    public Text PuntajeVida;

    public int GetVida()
    {
        return Vidas;
    }

    public void QuitarVida (int Vidas)
    {
        this.Vidas -= Vidas;
        PuntajeVida.text = "Vidas: " + GetVida();
    }
}
