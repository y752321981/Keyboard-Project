using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class File : MonoBehaviour
{
    public Button Delete;
    public Button Load;
    public TextMeshProUGUI Text;
	public SaveInfo SaveInfo;

	private void Start()
	{
		Load.GetComponent<Button>().onClick.AddListener(LoadSave);
		Delete.GetComponent<Button>().onClick.AddListener(DeleteSave);
	}

	public void LoadSave()
	{
		UIManager.Instance.Load(SaveInfo);
	}

	public void DeleteSave()
	{
		SaveAndLoad.Instance.Delete(SaveInfo);
	}

	


}
