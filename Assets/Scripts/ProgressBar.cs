using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour{
	Image image;
	public float timeToFill = 7.0f;
	private bool syncFillBar = false;
	private float syncTimeUntilFillAgain = 0.0f;

	void Start()
	{
		image = GetComponent<Image>();
		image.fillAmount = 1;
	}

	void Update()
	{
		if (syncFillBar)
		{
			image.fillAmount = Mathf.MoveTowards(image.fillAmount, 1f, Time.deltaTime / timeToFill);
		} else if (syncTimeUntilFillAgain < Time.deltaTime)
		{
			syncFillBar = false;
		}
	}

	public bool ActivateProgressBar()
	{
		if (syncTimeUntilFillAgain < Time.fixedTime)
		{
			syncTimeUntilFillAgain = Time.fixedTime + timeToFill;
			image.fillAmount = 0;
			syncFillBar = true;
			return true;
		}
		return false;
	}

}
