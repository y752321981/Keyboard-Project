using System;
using System.Collections.Generic;
using UnityEngine;
public class SaveInfo 
{
	
	public List<ButtonInfo> buttonInfos;
	public DateTime saveTime;
	public string FileName;
	public SaveInfo() {
		buttonInfos = new List<ButtonInfo>();
	}

	public SaveInfo(List<KeyboradButton> list)
	{
		buttonInfos = new List<ButtonInfo>();
		foreach (var item in list)
		{
			buttonInfos.Add(item.ButtonInfo);
		}
		
		saveTime = DateTime.Now;
		FileName = $"{DateTime.Now.Hour}_{ DateTime.Now.Minute}";
	}

	public override string ToString()
	{
		string output = $"FileName:{FileName},SaveTime:{saveTime.ToString()},Button:{buttonInfos.ToString()}";

		return output;
	}
}
