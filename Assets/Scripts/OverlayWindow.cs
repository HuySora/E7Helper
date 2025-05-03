using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class OverlayWindow : MonoBehaviour {
    private const int GWL_EXSTYLE = -20;
    private const uint WS_EX_LAYERED = 0x00080000;
    private const uint WS_EX_TRANSPARENT = 0x00000020;
    private static readonly IntPtr HWND_TOPMOST = new(-1);
    
    private IntPtr m_HWnd;
    
    private struct MARGINS {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }
    
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();
    
    [DllImport("dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);
    
    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
    
    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
    
    private void Start() {
        if (Application.isEditor) return;
        
        m_HWnd = GetActiveWindow();
        SetWindowPos(m_HWnd, HWND_TOPMOST, 0, 0, 0, 0, 0);
        SetWindowLong(m_HWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
        var margins = new MARGINS {
            cxLeftWidth = -1
        };
        DwmExtendFrameIntoClientArea(m_HWnd, ref margins);
    }
    
    private void Update() {
        bool isOverUi = EventSystem.current.IsPointerOverGameObject();
        SetClickThrough(!isOverUi);
    }
    
    private void SetClickThrough(bool isClickThrough) {
        if (Application.isEditor) return;
        
        SetWindowLong(m_HWnd, GWL_EXSTYLE, isClickThrough
            ? WS_EX_LAYERED | WS_EX_TRANSPARENT
            : WS_EX_LAYERED
        );
    }
}