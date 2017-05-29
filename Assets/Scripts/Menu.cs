using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public GameObject btnJugar, btnCreditos, btnSalir, creditos;

    void Start()
    {
        creditos.SetActive(false);    
    }

    public void Empezar()
    {
        SceneManager.LoadScene("1");
    }

    public void Creditos()
    {
        creditos.SetActive(true);
    }

    public void Salir()
    {
        Application.Quit();
    }
}
