
#if PLATFORM_STANDALONE_WIN

using System;
using UnityEngine;
using UniRx;
using Modules.StandAloneWindows;

namespace Example
{
    public static class WindowManager
    {
        //----- params -----

        //----- field -----

        //----- property -----

        //----- method -----

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        public static void OnAfterAssembliesLoaded()
        {
            // AspectRatio.
            
            var aspectRatioController = AspectRatioHandler.Instance;

            aspectRatioController.Initialize();
            aspectRatioController.SetAllowFullscreen(false);
            aspectRatioController.SetMaxSize(1800, 1013);
            aspectRatioController.SetMinSize(1136, 640);
            aspectRatioController.SetAspectRatio(16f, 9f);
            aspectRatioController.Apply();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        public static void OnBeforeSplashScreen()
        {
            Application.targetFrameRate = 30;

            // Window Size.

            var aspectRatioController = AspectRatioHandler.Instance;

            var displayWidth = Screen.currentResolution.width;
                
            var initialWindowWidth = Mathf.Min(Screen.width * 0.85f, Screen.currentResolution.width);

            if (initialWindowWidth != displayWidth)
            {
                var initialWindowHeight = initialWindowWidth * aspectRatioController.AspectRatio;

                Action setInitialScreenSize = () =>
                {
                    Screen.SetResolution((int)initialWindowWidth, (int)initialWindowHeight, false);
                };

                Observable.NextFrame().Subscribe(_ => setInitialScreenSize.Invoke());
            }
            
            // Window Style.

            var windowStyleHandler = WindowStyleHandler.Instance;

            windowStyleHandler.Initialize();

            // 最大化ボタンを持たないスタイルに変更.
            windowStyleHandler.WindowStyle = (int)(
                WindowStyles.WS_CAPTION |
                WindowStyles.WS_BORDER |
                WindowStyles.WS_SYSMENU |
                WindowStyles.WS_MINIMIZEBOX |
                WindowStyles.WS_SIZEBOX);

            windowStyleHandler.Apply();
        }
    }
}

#endif