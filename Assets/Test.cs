using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Test : MonoBehaviour
{
    public Image image;
	public Sprite ButtonClick;
	public Sprite Button;
	private void Start()
	{
		StartCoroutine("sest");
	}

	private IEnumerator sest()
	{
		yield return new WaitForSecondsRealtime(3f);

		image.overrideSprite = ButtonClick;
		yield return new WaitForSecondsRealtime(3f);
		image.overrideSprite = Button;
		StartCoroutine(sest());
	}

}
