using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Diagnostics;
using UnityEngine;
using System.Collections.Generic;

public class ToolControlTaskBar
{
    [DllImport("user32.dll")]   //这里是引入 user32.dll 库， 这个库是windows系统自带的。
    public static extern int ShowWindow(int hwnd, int nCmdShow); //这是显示任务栏
    [DllImport("user32.dll")]
    public static extern int FindWindow(string lpClassName, string lpWindowName); //这是隐藏任务栏
    [DllImport("User32.dll", EntryPoint = "SetWindowLong")]
    private static extern int SetWindowLong(int hWnd, int nIndex, long dwNewLong);

    [DllImport("User32.dll", EntryPoint = "SetWindowLong")]
    private static extern int GetWindowLong(int hWnd, int nIndex);



    private const int SW_HIDE = 0;  //hied task bar
    private const int SW_RESTORE = 9;//show task bar
                                     // Use this for initialization

    const int GWL_STYLE = -16;
    private const int WS_CAPTION = 0xC00000;
    private const int WS_SIZEBOX = 0x040000;
    private const int WS_SYSMENU = 0x00080000;
    /// <summary>
    /// show TaskBar
    /// </summary>
    public static void ShowTaskBar()
    {
        ShowWindow(FindWindow("Shell_TrayWnd", null), SW_RESTORE);
    }
    /// <summary>
    /// Hide TaskBar
    /// </summary>
    public static void HideTaskBar()
    {
        ShowWindow(FindWindow("Shell_TrayWnd", null), SW_HIDE);
    }
    public static void ShowTitle()
    {
        int hwd = FindWindow(null, Application.productName);

        SetWindowLong(hwd, GWL_STYLE, 369164288 | WS_CAPTION | WS_SIZEBOX | WS_SYSMENU);
    }
    /// <summary>
    /// Hide TaskBar
    /// </summary>
    public static void HideTitle()
    {
        int hwd = FindWindow(null, Application.productName);

        SetWindowLong(hwd, GWL_STYLE, 369164288 );
    }

}



public class WindowMod : MonoBehaviour
{

    [DllImport("user32.dll")]
    static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hPos, int x, int y, int cx, int cy, uint nflags);

    [DllImport("User32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("User32.dll", EntryPoint = "SetWindowLong")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, long dwNewLong);

    [DllImport("User32.dll", EntryPoint = "GetWindowLong")]
    private static extern int GetWindowLong(IntPtr hWnd, int dwNewLong);

    [DllImport("User32.dll", EntryPoint = "MoveWindow")]
    private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool repaint);

    [DllImport("user32.dll", EntryPoint = "ShowWindow", CharSet = CharSet.Auto)]
    public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

    [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
    public static extern int SendMessage(IntPtr hwnd, int msg, IntPtr wP, IntPtr IP);

    [DllImport("user32.dll", EntryPoint = "SetParent", CharSet = CharSet.Auto)]
    public static extern IntPtr SetParent(IntPtr hChild, IntPtr hParent);

    [DllImport("user32.dll", EntryPoint = "GetParent", CharSet = CharSet.Auto)]
    public static extern IntPtr GetParent(IntPtr hChild);

    [DllImport("User32.dll", EntryPoint = "GetSystemMetrics")]
    public static extern IntPtr GetSystemMetrics(int nIndex);

    [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
    public static extern bool SetForegroundWindow(IntPtr hWnd);//设置此窗体为活动窗体
    [DllImport("user32.dll")] static extern uint GetActiveWindow();
    [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
    public static extern bool SetFocus(IntPtr hWnd);//设置此窗体为活动窗体

    public enum appStyle
    {
        FullScreen = 0,
        WindowedFullScreen = 1,
        Windowed = 2,
        WindowedWithoutBorder = 3,
    }
    public appStyle AppWindowStyle = appStyle.WindowedFullScreen;

    public enum zDepth
    {
        Normal = 0,
        Top = 1,
        TopMost = 2,
    }
    public zDepth ScreenDepth = zDepth.Normal;


    public int windowLeft = 0;
    public int windowTop = 0;

    private int windowWidth = 1248;
    private int windowHeight = 512;


    const uint SWP_SHOWWINDOW = 0x0040;
    const int GWL_STYLE = -16;
    const int WS_BORDER = 1;
    private Rect screenPosition;
    private const int GWL_EXSTYLE = (-20);
    private const int WS_CAPTION = 0xC00000;
    private const int WS_POPUP = 0x800000;
    IntPtr HWND_TOP = new IntPtr(0);
    IntPtr HWND_TOPMOST = new IntPtr(-1);
    IntPtr HWND_NORMAL = new IntPtr(-2);

    private const int SM_CXSCREEN = 0x00000000;
    private const int SM_CYSCREEN = 0x00000001;

    int Xscreen;
    int Yscreen;

    //add 2015.4.21
    public bool StartAuto = false;
    public enum ScreenDirection
    {
        defaultDirection,
        horizontal,
        vertical,
    }

    public ScreenDirection CurDirection = ScreenDirection.defaultDirection;
    public static WindowMod Instance;
    void Awake()
    {
        Instance = this;
        ToolControlTaskBar.HideTaskBar();
        Xscreen = (int)GetSystemMetrics(SM_CXSCREEN);
        Yscreen = (int)GetSystemMetrics(SM_CYSCREEN);

        if (!StartAuto)
        {
            if (Xscreen > Yscreen)
            {
                windowWidth = 1920;
                windowHeight = 1080;
                // Global.CurDictiion = Global.EnumDiction.Horizontal;
            }
            else
            {
                windowWidth = 1080;
                windowHeight = 1920;
                //Global.CurDictiion = Global.EnumDiction.Vertical;
            }
        }
        else
        {
            if (CurDirection == ScreenDirection.horizontal)
            {
                windowWidth = 1920;
                windowHeight = 1080;
                // Global.CurDictiion = Global.EnumDiction.Horizontal;
            }
            else if (CurDirection == ScreenDirection.vertical)
            {
                windowWidth = 1080;
                windowHeight = 1920;
                //Global.CurDictiion = Global.EnumDiction.Vertical;
            }
        }


        if ((int)AppWindowStyle == 0)
        {
            Screen.SetResolution(Xscreen, Yscreen, true);
        }
        if ((int)AppWindowStyle == 1)
        {
            //Screen.SetResolution(Xscreen - 1, Yscreen - 1, false);
            //screenPosition = new Rect(0, 0, Xscreen - 1, Yscreen - 1);

            Screen.SetResolution(windowWidth, windowHeight, false);
            screenPosition = new Rect(0, 0, windowWidth, windowHeight);
        }
        if ((int)AppWindowStyle == 2)
        {
            Screen.SetResolution(1000, 400, false);
        }
        if ((int)AppWindowStyle == 3)
        {
            Screen.SetResolution(windowWidth, windowWidth, false);
            screenPosition = new Rect(windowLeft, windowTop, windowWidth, windowWidth);
        }
        ToolControlTaskBar.HideTitle();
    }


   
    
    int i = 0;
    void Update()
    {
        IntPtr hWnd = FindWindow(null, Application.productName);//修改“test”为自己发部程序的窗口名

        SetForegroundWindow(hWnd);
        
        if (i < 30)
        {
            if ((int)AppWindowStyle == 1)
            {
                SetWindowLong(hWnd, -16, 369164288);
                if ((int)ScreenDepth == 0)
                    SetWindowPos(hWnd, HWND_NORMAL, (int)screenPosition.x, (int)screenPosition.y, (int)screenPosition.width, (int)screenPosition.height, SWP_SHOWWINDOW);
                if ((int)ScreenDepth == 1)
                    SetWindowPos(hWnd, HWND_TOP, (int)screenPosition.x, (int)screenPosition.y, (int)screenPosition.width, (int)screenPosition.height, SWP_SHOWWINDOW);
                if ((int)ScreenDepth == 2)
                    SetWindowPos(hWnd, HWND_TOPMOST, (int)screenPosition.x, (int)screenPosition.y, (int)screenPosition.width, (int)screenPosition.height, SWP_SHOWWINDOW);
                //ShowWindow(GetForegroundWindow(), 3);
            }

            if ((int)AppWindowStyle == 2)
            {
                if ((int)ScreenDepth == 0)
                {
                    SetWindowPos(hWnd, HWND_NORMAL, 0, 0, 0, 0, 0x0001 | 0x0002);
                    SetWindowPos(hWnd, HWND_NORMAL, 0, 0, 0, 0, 0x0001 | 0x0002 | 0x0020);
                }
                if ((int)ScreenDepth == 1)
                {
                    SetWindowPos(hWnd, HWND_TOP, 0, 0, 0, 0, 0x0001 | 0x0002);
                    SetWindowPos(hWnd, HWND_TOP, 0, 0, 0, 0, 0x0001 | 0x0002 | 0x0020);
                }
                if ((int)ScreenDepth == 2)
                {
                    SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, 0x0001 | 0x0002);
                    SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, 0x0001 | 0x0002 | 0x0020);
                }

            }

            if ((int)AppWindowStyle == 3)
            {
                SetWindowLong(hWnd, -16, 369164288);
                if ((int)ScreenDepth == 0)
                    SetWindowPos(hWnd, HWND_NORMAL, (int)screenPosition.x, (int)screenPosition.y, (int)screenPosition.width, (int)screenPosition.height, SWP_SHOWWINDOW);
                if ((int)ScreenDepth == 1)
                    SetWindowPos(hWnd, HWND_TOP, (int)screenPosition.x, (int)screenPosition.y, (int)screenPosition.width, (int)screenPosition.height, SWP_SHOWWINDOW);
                if ((int)ScreenDepth == 2)
                    SetWindowPos(hWnd, HWND_TOPMOST, (int)screenPosition.x, (int)screenPosition.y, (int)screenPosition.width, (int)screenPosition.height, SWP_SHOWWINDOW);
            }
        }
        i++;

    }
    




    private void OnDestroy()
    {
        ToolControlTaskBar.ShowTaskBar();
    }
}
