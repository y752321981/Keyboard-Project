using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    public static SaveAndLoad Instance;
	private void Awake()
	{
		Instance = this;
	}
	DirectoryInfo Saves;
	
	private string Path;
	private List<SaveInfo> saveInfos;
	private void Start()
	{
		
		Path = Application.persistentDataPath + "/Saves";
		Debug.Log(Path);
		Saves = new DirectoryInfo(Path);
		saveInfos = new List<SaveInfo>();
		if(!Saves.Exists)
		{
			Directory.CreateDirectory(Path);
			Saves = new DirectoryInfo(Path);
		}
		{
			Init();
		}
	}

	private void Init()
	{
		FindFiles();
		if (saveInfos.Count != 0)
			UIManager.Instance.Load(saveInfos[saveInfos.Count - 1]);
	}

	internal void Delete(SaveInfo saveInfo)
	{
		string path = Path + $"/{saveInfo.FileName}";
		FileInfo fileInfo = new FileInfo(path);
		if(fileInfo.Exists)
		{
			fileInfo.Delete();
			
		}
		Refresh();
	}

	public void Refresh()
	{
		FindFiles();
		UIManager.Instance.CreateSaves(saveInfos);
	}

	private void FindFiles()
	{
		if (saveInfos.Count != 0)
			saveInfos.Clear();
		FileInfo[] fileInfos = Saves.GetFiles();
		for (int i = 0; i < fileInfos.Length; i++)
		{
			FileStream fs = fileInfos[i].OpenRead();
			byte[] datas = new byte[fs.Length];

			fs.Read(datas, 0, datas.Length);
			saveInfos.Add(Codec.FromJSON<SaveInfo>(datas));
		}
	}

	
	public void CreatSaveFile(SaveInfo save)
	{
		
		
		string path = $"{Path}/{save.FileName}";
		FileInfo file = new FileInfo(path);
		if (file.Exists)
		{
			file.Delete();
		}
		try
		{
			using (FileStream fs = file.Create())
			{
				byte[] info = Codec.ToJSON(save);

				fs.Write(info, 0, info.Length);

			}
		}
		catch (Exception e)
		{
			Debug.LogError(e.Message);
			
			throw e;
		}
		
	}


}
