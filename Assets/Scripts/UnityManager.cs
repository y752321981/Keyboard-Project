using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityManager : MonoBehaviour
{

	public void ToggleConfig()
	{
		UIManager.Instance.ToggleConfig();
	}

	public void ToggleSaves()
	{
		UIManager.Instance.ToggleSaves();
	}

	public void Return()
	{
		UIManager.Instance.ReturnButton();
	}

	public void EnableAdd()
	{
		UIManager.Instance.EnableAddButton();
	}

	public void Save()
	{
		UIManager.Instance.Save();
	}

	public void Refresh()
	{
		SaveAndLoad.Instance.Refresh();
	}

	

}
