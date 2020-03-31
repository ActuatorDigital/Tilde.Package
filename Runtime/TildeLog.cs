using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AIR.Tilde {
    public class TildeLog : MonoBehaviour {

        public void Log(string log) => Log(log, LogType.Log);
        public void LogError(string logError) => Log(logError, LogType.Error);
        public void LogWarning(string logWarning) => Log(logWarning, LogType.Warning);

        private Queue<TildeLoggedMsg> Logs = new Queue<TildeLoggedMsg>();
        private Vector2 _scrollPos;
        private GUIStyle _style;
        
        private void Log(string log, LogType logType) {
            var logTypeStr =
                logType == LogType.Log ? "Log"
                : logType == LogType.Error ? "Err"
                : logType == LogType.Warning ? "Wrn"
                : String.Empty;
            Logs.Enqueue( new TildeLoggedMsg {
                Message = logTypeStr + ":" + log + "\n",
                LogType = logType
            });
        }

        private void Awake() {
            Application.logMessageReceived += LogHandler;
            _style = new GUIStyle{ fontSize = 10 };
        }

        private void LogHandler(string message, string stacktrace, LogType type) {
            Log(message + "\n" + stacktrace, type);
            _scrollPos = new Vector2(_scrollPos.x, float.MaxValue);
        }

        public void Draw() {
            var consoleHeight = Screen.height * (2f / 3f);
            _scrollPos = GUILayout.BeginScrollView(_scrollPos, 
                alwaysShowHorizontal: false,
                alwaysShowVertical: true,
                GUILayout.Width(Screen.width), 
                GUILayout.Height(consoleHeight));

            foreach (var logMsg in Logs) {
                
                switch (logMsg.LogType) {
                    case LogType.Exception:
                    case LogType.Error:
                        _style.normal.textColor = Color.red;
                        break;
                    case LogType.Assert:
                    case LogType.Warning:
                        _style.normal.textColor = Color.yellow;
                        break;
                    case LogType.Log:
                        _style.normal.textColor = Color.white;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                GUILayout.Label(logMsg.Message, _style,
                    GUILayout.ExpandWidth(true), 
                    GUILayout.ExpandHeight(false));
            }
            
            GUILayout.EndScrollView();

        }

        internal string LogsToString() {

            var sb = new StringBuilder();
            foreach (var log in Logs)
                sb.Append(log.Message + "\n");

            return sb.ToString();
        }

        internal struct TildeLoggedMsg {
            public LogType LogType;
            public string Message;
        }
    }
}