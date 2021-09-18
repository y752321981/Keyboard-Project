using UnityEngine;

public class ButtonInfo
{
    private string position;
    public string Position { get { return position; } set { position = value; } }

    private string anchorPosition;
    public string AnchorPosition { get { return anchorPosition; } set { anchorPosition = value; } }
    private string sizeDelta;
    public string SizeDelta { get { return sizeDelta; } set { sizeDelta = value; } }
    private KeyCode keyCode;
	public KeyCode KeyCode { get { return keyCode; } set { keyCode = value; } }
    
    
    public ButtonInfo() { }

    public ButtonInfo(Vector3 position, KeyCode keyCode)
    {
        this.position = $"{position.x},{position.y}";
        this.keyCode = keyCode;
    }

    public static Vector3 Str2V3(string position)
	{
         string[] s = position.Split(',');
         return new Vector3(float.Parse(s[0]), float.Parse(s[1]), 0);
	}
    
    public static string V32Str(Vector3 position)
	{
        string s = $"{position.x},{position.y}";
        return s;
	}

    public ButtonInfo(KeyboradButton keyboradButton)
	{
        position = keyboradButton.ButtonInfo.position;
        keyCode = keyboradButton.ButtonInfo.keyCode;
    }

	public ButtonInfo(int value)
	{
        keyCode = (KeyCode)value;
	}

    public ButtonInfo(ButtonInfo buttonInfo)
	{
        position = buttonInfo.position;
        keyCode = buttonInfo.KeyCode;
	}

	public override string ToString()
	{
        string p = Str2V3(position).ToString();
        string k = System.Enum.GetName(typeof(KeyCode), keyCode);
        string o = p + ',' + k;
        return o;
	}

}
