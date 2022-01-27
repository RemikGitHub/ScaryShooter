using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithElevator : MonoBehaviour
{
	public GameObject interactableUI;
	public GameObject elevatorDoor1, elevatorDoor2, elevator;
	public Camera gunCamera;
	public AudioClip buttonPress;
	public AudioSource interactSource;
	Animator door1, door2, elevatorAnim;
	private bool isUp;
	// Start is called before the first frame update
	void Start()
    {
		door1 = elevatorDoor1.GetComponent<Animator>();
		door2 = elevatorDoor2.GetComponent<Animator>();
		elevatorAnim = elevator.GetComponent<Animator>();
		isUp = false;
	}

    // Update is called once per frame
    void Update()
    {
		RaycastHit hit;
		Ray ray = gunCamera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit, 1.3f) && hit.collider.tag == "ElevatorButton")
		{
			interactableUI.SetActive(true);
		}
		else
		{
			interactableUI.SetActive(false);
		}
		if (Input.GetKeyUp("e"))
		{
			if (Physics.Raycast(ray, out hit, 1.3f))
			{

				if (hit.collider.name == "ElevatorCallButton1" && !isUp)
				{
					door1.SetBool("doorOpen", true);
					door2.SetBool("doorOpen", true);
				}
				else if (hit.collider.name == "ElevatorCallButton1" && isUp)
				{
					elevatorAnim.SetBool("moveDown", true);
					isUp = false;
				}
				else if (hit.collider.name == "ElevatorCallButton2" && !isUp)
				{
					elevatorAnim.SetBool("moveUp", true);
					isUp = true;
				}
				else if (hit.collider.name == "ElevatorFloor1" && isUp)
				{
					elevatorAnim.SetBool("moveDown", true);
					isUp = false;
				}
				else if (hit.collider.name == "ElevatorFloor2" && !isUp)
				{
					elevatorAnim.SetBool("moveUp", true);
					isUp = true;
				}
				if(hit.collider.tag == "ElevatorButton")
                {
					interactSource.clip = buttonPress;
					interactSource.Play();
				}

			}

		}
	}
}
