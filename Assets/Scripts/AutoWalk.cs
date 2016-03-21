using UnityEngine;
using System.Collections;

public class AutoWalk : MonoBehaviour 
{
	CardboardHead head;
	Cardboard cardboard;
	public ProgressBar[] progressBars;
	public float speed = 20;
	public float boostspeed = 80;
	private bool goFaster = false;

	public float totalBoostTime = 2.5f;
	private float timeUntilBoostDone = 0.0f;

	void Start () 
	{
		head = Camera.main.GetComponent<StereoController>().Head;
		cardboard = head.GetComponentInParent<Cardboard> ();
	}

	void Update () 
	{
		// boost time?
		if (cardboard.Triggered) {
			for (var i = 0; i < progressBars.Length; i++) {
				goFaster = progressBars[i].ActivateProgressBar();
			}
			if (goFaster) {
				timeUntilBoostDone = Time.fixedTime + totalBoostTime;
			}
		}
		if (timeUntilBoostDone > Time.fixedTime) {
			speed = boostspeed;
		} else {
			speed = speed;
		}

		// for aiming where head is aiming
		Vector3 direction = new Vector3(head.transform.forward.x, 0, head.transform.forward.z).normalized * speed * Time.deltaTime;
		Quaternion rotation = Quaternion.Euler(new Vector3(0, -transform.rotation.eulerAngles.y, 0));
		transform.Translate(rotation * direction);
		// move
		transform.position = new Vector3(transform.position.x, 1, transform.position.z);
		speed = 20;
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Wall") {
			// hit wall, go back to middle
			gameObject.transform.position = new Vector3 (0, 1, 0);
		}
	}

}