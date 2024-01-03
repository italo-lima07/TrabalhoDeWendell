using UnityEngine;

public class AtaquePlayer : MonoBehaviour
{
    private bool isActive = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isActive = !isActive; // Inverte o estado atual
            gameObject.SetActive(isActive); // Define o estado do objeto de acordo com isActive
        }
    }
}