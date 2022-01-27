using System.Collections;
using UnityEngine;

public class BloodSplashController : MonoBehaviour
{
    private void Start()
    {
		//Start the remove/destroy coroutine
		StartCoroutine(RemoveBlood());
	}
    private IEnumerator RemoveBlood()
	{
		//Destroy the casing after set amount of seconds
		yield return new WaitForSeconds(1);
		//Destroy object
		Destroy(gameObject);
	}
}
