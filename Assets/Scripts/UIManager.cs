using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;
using TMPro;
public class UIManager : MonoBehaviour
{
	public static UIManager Instance;
	
	private void Awake()
	{
		Instance = this;
		
	}
    public bool isConfig;
	private List<KeyboradButton> keyboradButtons = new List<KeyboradButton>();
	private Stack<KeyboradButton> keyboradButtonsDeleted = new Stack<KeyboradButton>();
	public GameObject ButtonPrefab;
	public Transform Background;

	public List<File> files;
	public GameObject FilePrefab;

	public Transform FilesPanel;
	public GameObject SaveMenu;

	public GameObject Return;
	public GameObject AddButton;
	public GameObject SaveButton;
	public Sprite Button;
	public Sprite ButtonClick;
	private bool IsFocus;
	
	private void Start()
	{
	
		IsFocus = true;
		isConfig = false;
		var gameobjs = GameObject.FindGameObjectsWithTag("Button");
		for (int i = 0; i < gameobjs.Length; i++)
		{
			keyboradButtons.Add(gameobjs[i].GetComponent<KeyboradButton>());
			gameobjs[i].GetComponent<KeyboradButton>().ToggleConfig(false);
		}
		files = new List<File>();





	}

	

	public void ToggleConfig()
	{
		if(isConfig)
		{
			for (int i = 0; i < keyboradButtons.Count; i++)
			{
				keyboradButtons[i].ToggleConfig(false);
			}

			Return.SetActive(false);
			AddButton.SetActive(false);
			SaveButton.SetActive(false);
			isConfig = false;
			ToolControlTaskBar.HideTitle();
		}
		else
		{
			for (int i = 0; i < keyboradButtons.Count; i++)
			{
				keyboradButtons[i].ToggleConfig(true);

			}
			SaveButton.SetActive(true);
			Return.SetActive(true);
			AddButton.SetActive(true);
			isConfig = true;
			ToolControlTaskBar.ShowTitle();
		}
	}

	
	public void Save()
	{

		for (int i = 0; i < keyboradButtons.Count; i++)
		{
			keyboradButtons[i].UpdateButtonInfo();
		}
		SaveInfo saveInfo = new SaveInfo(keyboradButtons);
		SaveAndLoad.Instance.CreatSaveFile(saveInfo);
		SaveAndLoad.Instance.Refresh();
	}

	internal void CreateSaves(List<SaveInfo> saveInfos)
	{
		for (int i = 0; i < files.Count; i++)
		{
			Destroy(files[i].gameObject);
		}
		files.Clear();
		for (int i = 0; i < saveInfos.Count; i++)
		{
			var save = Instantiate(FilePrefab);
			save.transform.SetParent(FilesPanel);
			save.transform.localPosition = Vector3.zero;
			save.transform.localScale = Vector3.one;
			File file = save.GetComponent<File>();
			files.Add(file);
			file.SaveInfo = saveInfos[i];
			file.Text.text = saveInfos[i].saveTime.ToString();
		}
	}

	public void Load(SaveInfo saveInfo)
	{
		Debug.Log(saveInfo.ToString());
		List<ButtonInfo> buttonInfos = saveInfo.buttonInfos;
		for (int i = 0; i < keyboradButtons.Count; i++)
		{
			Destroy(keyboradButtons[i].gameObject);
		}

		keyboradButtons.Clear();
		keyboradButtonsDeleted.Clear();
		for (int i = 0; i < buttonInfos.Count; i++)
		{
			var Gameobj = Instantiate(ButtonPrefab);
			Gameobj.transform.SetParent(Background);
			Destroy(Gameobj.GetComponent<KeyboradButton>());
			Gameobj.GetComponent<DragWindow>().ReLoad();
			var k = Gameobj.AddComponent<KeyboradButton>();
			StartCoroutine(k.Create(buttonInfos[i]));
			
			keyboradButtons.Add(k);
		}
		ToggleSaves();
	}

	public void ToggleSaves()
	{
		if(SaveMenu.activeInHierarchy)
		{
			SaveMenu.SetActive(false);
		}
		else
		{
			SaveMenu.SetActive(true);
			SaveAndLoad.Instance.Refresh();
		}
	}

	public void EnableAddButton()
	{
		Add.Instance.AddButton();
	}

	public void OnAdd(int value)
	{
		var Gameobj = Instantiate(ButtonPrefab);
		Gameobj.transform.SetParent(Background);
		Destroy(Gameobj.GetComponent<KeyboradButton>());
		Gameobj.GetComponent<DragWindow>().ReLoad();
		var k = Gameobj.AddComponent<KeyboradButton>();
		StartCoroutine(k.Create(value));
		keyboradButtons.Add(k);
	}

	public void Delete(KeyboradButton keyboradButton)
	{
		keyboradButtonsDeleted.Push(keyboradButton);
		keyboradButtons.Remove(keyboradButton);
		GameObject.Destroy(keyboradButton.gameObject);
		
	}

	public void ReturnButton()
	{
	
		if (keyboradButtonsDeleted.Count == 0) return;
		var keyboradButton = keyboradButtonsDeleted.Pop();
		var Gameobj = Instantiate(ButtonPrefab);
		Gameobj.transform.SetParent(Background);
		Destroy(Gameobj.GetComponent<KeyboradButton>());
		Gameobj.GetComponent<DragWindow>().ReLoad();
		var k = Gameobj.AddComponent<KeyboradButton>();
		StartCoroutine(k.Create(keyboradButton));
		keyboradButtons.Add(k);
	}

	void OnApplicationFocus(bool focus)
	{
		IsFocus = focus;
		Debug.Log(IsFocus);
	}

	public void OnClick(int vkCode)
	{
		vkCode = Transform(vkCode);
		foreach (var item in keyboradButtons)
		{
			if ((KeyCode)vkCode == item.ButtonInfo.KeyCode)
			{
				Debug.Log((KeyCode)vkCode);
				StartCoroutine(item.OnClick());
			}

		}
	}

	private int Transform(int v)
	{
		if (v >= 65 && v <= 90)
		{
			v += 32;
		}
		switch (v)
		{
			case 20:
				v = 301;
				break;
			case 160:
				v = 304;
				break;
			case 162:
				v = 306;
				break;
			case 91:
				v = 310;
				break;
			case 161:
				v = 303;
				break;
			case 163:
				v = 305;
				break;
			case 188:
				v = 44;
				break;
			case 190:
				v = 46;
				break;
			case 191:
				v = 47;
				break;
			case 186:
				v = 59;
				break;
			case 222:
				v = 39;
				break;
			case 219:
				v = 91;
				break;
			case 221:
				v = 93;
				break;
			case 220:
				v = 92;
				break;
			case 189:
				v = 45;
				break;
			case 187:
				v = 61;
				break;
			case 38:
				v = 273;
				break;
			case 40:
				v = 274;
				break;
			case 37:
				v = 276;
				break;
			case 39:
				v = 275;
				break;
			case 192:
				v = 96;
				break;
			default:
				break;
		}

		return v;
	}
	private void Update()
	{
		
		if (IsFocus)
		{
			foreach (var item in keyboradButtons)
			{
				if (Input.GetKeyDown(item.ButtonInfo.KeyCode))
				{
					StartCoroutine(item.OnClick());
				}
			}
		}
	}

	
	

	
}
