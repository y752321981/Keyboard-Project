using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class KeyboradButton : MonoBehaviour
{
	public DragWindow DragWindow;
	public TextMeshProUGUI Text;
	public ButtonInfo ButtonInfo;
	public GameObject Move;
	public GameObject Delet;
	

	private void Start()
	{
		//FindComponent();
		ToggleConfig(false);
	}
	

	

	public void ToggleConfig(bool enable)
	{
		if(enable)
		{
			Move.SetActive(true);
			Delet.SetActive(true);
			DragWindow.enabled = true;
		}
		else
		{
			Move.SetActive(false);
			Delet.SetActive(false);
			DragWindow.enabled = false;
		}
	}

	public void ToggleDragWindow(bool toggle)
	{
		DragWindow.enabled = toggle;
	}

	public void Delete()
	{
		ButtonInfo.Position = ButtonInfo.V32Str(this.gameObject.transform.localPosition);
		UIManager.Instance.Delete(this);
	
	}

	internal IEnumerator Create(ButtonInfo buttonInfo)
	{
		FindComponent();

		ButtonInfo = new ButtonInfo(buttonInfo);
		this.gameObject.GetComponent<Image>().SetNativeSize();
		//this.gameObject.GetComponent<RectTransform>().anchoredPosition = ButtonInfo.Str2V3(buttonInfo.AnchorPosition);
		//this.gameObject.GetComponent<RectTransform>().sizeDelta = ButtonInfo.Str2V3(buttonInfo.SizeDelta);
		this.gameObject.GetComponent<RectTransform>().localPosition = ButtonInfo.Str2V3(buttonInfo.Position);
		this.gameObject.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);

		Text.text = NameTransf(buttonInfo.KeyCode);
		yield return 0;
		ToggleConfig(false);
		if (UIManager.Instance.isConfig)
			UIManager.Instance.ToggleConfig();
	}

	public IEnumerator Create (KeyboradButton keyboradButton)
	{
		FindComponent();
		ButtonInfo = new ButtonInfo(keyboradButton);
		this.gameObject.GetComponent<Image>().SetNativeSize();
		this.gameObject.transform.localPosition = ButtonInfo.Str2V3(keyboradButton.ButtonInfo.Position);
		this.gameObject.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
		Text.text = NameTransf(ButtonInfo.KeyCode);
		yield return 0;
		ToggleConfig(true);
		
	}

	internal void UpdateButtonInfo()
	{
		ButtonInfo.Position = ButtonInfo.V32Str(gameObject.transform.localPosition);
		ButtonInfo.AnchorPosition = ButtonInfo.V32Str(gameObject.GetComponent<RectTransform>().anchoredPosition);
		ButtonInfo.SizeDelta = ButtonInfo.V32Str(gameObject.GetComponent<RectTransform>().sizeDelta);
	}

	internal IEnumerator Create(int value)
	{
		FindComponent();
		value = ChangeKeyCode(value);
		ButtonInfo = new ButtonInfo(value);

		Text.text = NameTransf(ButtonInfo.KeyCode);
		this.gameObject.GetComponent<RectTransform>().localPosition	 = new Vector2(408, 225);
		this.gameObject.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
		this.gameObject.GetComponent<Image>().SetNativeSize();
		yield return 0;
		ToggleConfig(true);
	}


	private void FindComponent()
	{
		DragWindow = GetComponent<DragWindow>();
		Text = GetComponentInChildren<TextMeshProUGUI>();
		Move = GetComponentInChildren<BoxCollider>().gameObject;
		Delet = GetComponentInChildren<Button>().gameObject;
		Delet.GetComponent<Button>().onClick.RemoveAllListeners();
		Delet.GetComponent<Button>().onClick.AddListener(Delete);
		
	}

	private int ChangeKeyCode(int value)
	{
		if (value <= 2 && value >= 1) value += 7;
		else if (value <= 4 && value >= 3) value += 9;
		else if (value == 5) value = 19;
		else if (value == 6) value = 27;
		else if (value >= 7 && value <= 39) value += 25;
		else if (value >= 40 && value <= 76) value += 51;
		else if (value >= 77 && value <= 117) value += 179;
		else if (value >= 118 && value <= 127) value += 182;
		else if (value == 127) value = 309;
		else if (value == 129) value = 310;
		else if (value >= 131 && value <= 133) value += 180;
		else if (value >= 134 && value <= 138) value += 181;
		else if (value >= 139) value += 184;
		return value;
	}

	public IEnumerator OnClick()
	{
		Debug.Log("111111");
		var i = GetComponent<Image>();
		i.overrideSprite =UIManager.Instance. ButtonClick;
		
		yield return new WaitForSeconds(0.3f);
		i.overrideSprite = UIManager.Instance.Button;
	}

	//public void OnClick()
	//{



	//	var a = gameObject.GetComponent<Animation>();
	//	if(a.isPlaying)
	//	{
	//		a.Stop();
	//	}
	//	a.Play();
	//	Debug.Log($"点击{Enum.GetName(typeof(KeyCode), ButtonInfo.KeyCode)}");

	//}


	private string NameTransf(KeyCode keyCode)
	{
		string o;
		switch (keyCode)
		{

			case KeyCode.UpArrow:
				o = "↑";
				break;
			case KeyCode.DownArrow:
				o = "↓";
				break;
			case KeyCode.RightArrow:
				o = "→";
				break;
			case KeyCode.LeftArrow:
				o = "←";
				break;
			case KeyCode.Alpha0:
				o = "0";
				break;
			case KeyCode.Alpha1:
				o = "1";
				break;
			case KeyCode.Alpha2:
				o = "2";
				break;
			case KeyCode.Alpha3:
				o = "3";
				break;
			case KeyCode.Alpha4:
				o = "4";
				break;
			case KeyCode.Alpha5:
				o = "5";
				break;
			case KeyCode.Alpha6:
				o = "6";
				break;
			case KeyCode.Alpha7:
				o = "7";
				break;
			case KeyCode.Alpha8:
				o = "8";
				break;
			case KeyCode.Alpha9:
				o = "9";
				break;
		
			case KeyCode.Quote:
				o = "'";
				break;


			case KeyCode.Comma:
				o = ",";
				break;
			case KeyCode.Minus:
				o = "-";
				break;
			case KeyCode.Period:
				o = ".";
				break;
			case KeyCode.Slash:
				o = "/";
				break;
	
			case KeyCode.Semicolon:
				o = ";"; 
				break;

			case KeyCode.Equals:
				o = "=";
				break;

			case KeyCode.LeftBracket:
				o = "[";
				break;
			case KeyCode.Backslash:
				o = "\\";
				break;
			case KeyCode.RightBracket:
				o = "]";
				break;
	
			case KeyCode.BackQuote:
				o = "`";
				break;
		
		
			case KeyCode.RightShift:
				
			case KeyCode.LeftShift:
				o = "shift";
				break;
			case KeyCode.RightControl:
			case KeyCode.LeftControl:
				o = "ctrl";
				break;
			case KeyCode.RightAlt:
			case KeyCode.LeftAlt:
				o = "alt";
				break;
			case KeyCode.LeftCommand:
				o = "win";
				break;
	
		
			default:
				o = Enum.GetName(typeof(KeyCode), keyCode);
				break;
		}
		return o;
	}

}
