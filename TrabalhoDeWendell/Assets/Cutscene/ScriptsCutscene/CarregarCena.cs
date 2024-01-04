using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarregarCena : MonoBehaviour
{
    public string cenaParaCarregar;

    private void Start()
    {
        SceneManager.LoadScene(cenaParaCarregar);
    }
}