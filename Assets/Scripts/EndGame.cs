using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
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
            if (Input.GetKeyUp(KeyCode.E))
            {
                SceneManager.LoadScene("Menu");
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
