using System;
using System.IO;
using UnityEngine;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("AIR.Tilde.Tests")]

namespace AIR.Tilde {
    [RequireComponent(typeof(TildeLog))]
    [RequireComponent(typeof(TildeCmd))]
    public class Tilde : MonoBehaviour {
        public static event Action<string> OnCommand;
        internal static bool Visible = false;

        // private static Tilde Singleton;
        private static TildeLog _tildeLog;
        private static TildeCmd _tildeCmd;

        internal static void RunCommand(string command) => OnCommand?.Invoke(command);
        public static void Log(string logMsg) => _tildeLog.Log(logMsg);
        public static void LogError(string logErrorMsg) => _tildeLog.LogError(logErrorMsg);
        public static void LogWarning(string logWarningMsg) => _tildeLog.LogWarning(logWarningMsg);

        public void Start() {
            _tildeLog = GetComponent<TildeLog>();
            _tildeCmd = GetComponent<TildeCmd>();

            OnCommand += CheckSaveLog;
        }

        private void CheckSaveLog(string cmd) {
            if(cmd.ToLower() == "savelog")
                File.WriteAllText("./log.txt",_tildeLog.LogsToString());
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.BackQuote))
                Visible = !Visible;
        }

        private void OnGUI() {
            if(!Visible) return;
            _tildeLog.Draw();
            _tildeCmd.Draw();
        }
    }
}