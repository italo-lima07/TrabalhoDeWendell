using UnityEngine;

public class AtaquePlayer : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Verifica se o botão esquerdo do mouse foi pressionado
        {
            gameObject.SetActive(true); // Ativa o objeto
        }
        else if (Input.GetMouseButtonUp(0)) // Verifica se o botão esquerdo do mouse foi solto
        {
            gameObject.SetActive(false); // Desativa o objeto
        }
    }
}