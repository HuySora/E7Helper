using System;
using SoraTehk.E7Helper.Interop;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SoraTehk.E7Helper {
    public class TransparentWindow : MonoBehaviour {
        private IntPtr m_HWnd;
        
        private void Start() {
            if (Application.isEditor) return;
            
            m_HWnd = User32Interop.GetActiveWindow();
            User32Interop.SetWindowPos(m_HWnd, Win32Constants.HWND_TOPMOST, 0, 0, 0, 0, 0);
            User32Interop.SetWindowLong(m_HWnd, Win32Constants.GWL_EXSTYLE, Win32Constants.WS_EX_LAYERED | Win32Constants.WS_EX_TRANSPARENT);
            var margins = new MARGINS {
                cxLeftWidth = -1
            };
            DwmapiInterop.DwmExtendFrameIntoClientArea(m_HWnd, ref margins);
        }
        
        private void Update() {
            if (Application.isEditor) return;
            
            bool isOverUi = EventSystem.current.IsPointerOverGameObject();
            SetClickThrough(!isOverUi);
        }
        
        private void SetClickThrough(bool isClickThrough) {
            if (Application.isEditor) return;
            
            User32Interop.SetWindowLong(m_HWnd, Win32Constants.GWL_EXSTYLE, isClickThrough
                ? Win32Constants.WS_EX_LAYERED | Win32Constants.WS_EX_TRANSPARENT
                : Win32Constants.WS_EX_LAYERED
            );
        }
    }
}