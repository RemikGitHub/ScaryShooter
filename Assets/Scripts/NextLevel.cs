using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private GameObject interactableUI;
    private void OnTriggerExit(Collider other)
    {
        interactableUI.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "End")
        {
            interactableUI.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
