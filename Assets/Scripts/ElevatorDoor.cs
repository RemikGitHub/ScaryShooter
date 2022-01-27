using UnityEngine;

public class ElevatorDoor : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private AudioClip doorMove;
    [SerializeField] private AudioSource doorSource;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isOpen", false);
    }

    // Update is called once per frame
    public void AlertObservers(string message)
    {
        if (message.Equals("DoorOpened"))
        {
            anim.SetBool("isOpen", true);
        }
        if (message.Equals("DoorClosed"))
        {
            anim.SetBool("isOpen", false);
        }
        if (message.Equals("MoveEnd"))
        {
            anim.SetBool("doorOpen", false);
            anim.SetBool("doorClose", false);
        }
        if (message.Equals("MoveStart"))
        {
            doorSource.clip = doorMove;
            doorSource.Play();
        }
    }
}
