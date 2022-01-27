using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private GameObject player, elevatorDoor1, elevatorDoor2, blueLight, redLight;
    private Animator anim, door1, door2;
    [SerializeField] private AudioClip elevatorMove;
    [SerializeField] private AudioSource elevatorSource;
    private void Start()
    {
        anim = GetComponent<Animator>();
        door1 = elevatorDoor1.GetComponent<Animator>();
        door2 = elevatorDoor2.GetComponent<Animator>();
        anim.SetBool("isUp", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            player.transform.parent = transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            player.transform.parent = null;
            door1.SetBool("doorOpen", false);
            door1.SetBool("doorClose", true);
            door2.SetBool("doorOpen", false);
            door2.SetBool("doorClose", true);
        }
    }
    public void AlertObservers(string message)
    {
        if (message.Equals("MoveEnd"))
        {
            door1.SetBool("doorOpen", true);
            door1.SetBool("doorClose", false);
            door2.SetBool("doorOpen", true);
            door2.SetBool("doorClose", false);
        }
        if (message.Equals("MoveStart"))
        {
            elevatorSource.clip = elevatorMove;
            elevatorSource.Play();
            door1.SetBool("doorOpen", false);
            door1.SetBool("doorClose", true);
            door2.SetBool("doorOpen", false);
            door2.SetBool("doorClose", true);
        }
        if (message.Equals("IsUp"))
        {
            redLight.SetActive(false);
            blueLight.SetActive(true);
            anim.SetBool("isUp", true);
            anim.SetBool("moveUp", false);
            anim.SetBool("moveDown", false);
        }
        if (message.Equals("IsDown"))
        {
            redLight.SetActive(true);
            blueLight.SetActive(false);
            anim.SetBool("isUp", false);
            anim.SetBool("moveUp", false);
            anim.SetBool("moveDown", false);
        }
    }
}
