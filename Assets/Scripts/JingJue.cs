using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Debug = UnityEngine.Debug;

public class JingJue : MonoBehaviour
{
    
	//建立钩子
	[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
	private static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, uint threadId);

	//移除钩子
	[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
	private static extern bool UnhookWindowsHookEx(int idHook);

	//把消息传递到下一个监听
	[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
	private static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);

	[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	private static extern IntPtr GetModuleHandle(string lpModuleName);

    private const int WH_KEYBOARD_LL = 13;
    private delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);
    private const int WM_KEYDOWN = 0x0100;
	private int idHook;
    public static JingJue Instance;
	private void Awake()
	{
        Instance = this;
	}

   
	private void Start()
	{
        
         StartHook();
        Application.runInBackground = true;
    }
	private void OnDisable()
	{
        StopHook();
	}
	//安装钩子
	private void StartHook()
    {
        HookProc lpfn = new HookProc(Hook);

        idHook = SetWindowsHookEx(WH_KEYBOARD_LL, lpfn, GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName), 0);

        if (idHook > 0)
        {
			UnityEngine.Debug.Log("钩子[" + idHook + "]安装成功");
        }
        else
        {
            Debug.Log("钩子安装失败");
            UnhookWindowsHookEx(idHook);
        }
    }
    //卸载钩子
    private void StopHook()
    {
        UnhookWindowsHookEx(idHook);
    }
    private int Hook(int nCode, IntPtr wParam, IntPtr lParam)
    {
        try
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                Debug.Log("Keydown:" + vkCode);
                UIManager.Instance.OnClick(vkCode);
            }
            return CallNextHookEx(idHook, nCode, wParam, lParam);
        }
        catch (Exception ex)
        {
            //Debug.Log(ex.Message);
            return 0;
        }
    }
    
}