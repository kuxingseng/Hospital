namespace Blaze.Editor.Diagnostics
{
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// 远程控制台窗口。
    /// </summary>
    public class RemoteConsoleWindow : EditorWindow
    {
        /// <summary>
        /// 打开远程控制台窗口。
        /// </summary>
        [MenuItem("Blaze/Window/RemoteConsole")]
        public static void Open()
        {
            GetWindow<RemoteConsoleWindow>("RemoteConsole");
        }

        protected void OnApplicationQuit()
        {
            mConsole.Stop();
        }

        protected void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            if (mConsole.IsRunning)
            {
                GUILayout.Label("Port:");
                GUILayout.Label(mPort);
                GUI.color = Color.red;
                if (GUILayout.Button("Stop"))
                    mConsole.Stop();
            }
            else
            {
                GUILayout.Label("Port:");
                mPort = GUILayout.TextField(mPort);
                GUI.color = Color.green;
                if (GUILayout.Button("Start"))
                {
                    int port;
                    if (int.TryParse(mPort, out port))
                        mConsole.Start(port);
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        private readonly RemoteConsole mConsole = new RemoteConsole();
        private string mPort = "8921";
    }
}