using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class Add : MonoBehaviour
{
    public static Add Instance;
	private void Awake()
	{
		Instance = this;
	}
	public GameObject Dropdown;
    public GameObject Yes;
	private TMP_Dropdown _DropDown;
	private void Start()
	{
		_DropDown = Dropdown.GetComponent<TMP_Dropdown>();

		_DropDown.ClearOptions();
		List<string> keyCodes = new List<string>();

		keyCodes.AddRange(Enum.GetNames(typeof(KeyCode)));
		_DropDown.AddOptions(keyCodes);


		

		ToggleConfig(false);
	}

	public void ToggleConfig(bool enable)
	{
		if(enable)
		{
			Dropdown.SetActive(true);
			Yes.SetActive(true);
		}
		else
		{
			Dropdown.SetActive(false);
			Yes.SetActive(false);
		}
	}

	public void AddButton()
	{
		ToggleConfig(true);
		Yes.GetComponent<Button>().onClick.AddListener(OnAddButton);

	}

	public void OnAddButton()
	{
		if (_DropDown.value == 0) return;
		UIManager.Instance.OnAdd(_DropDown.value);
	}
}
