using UnityEngine;

namespace AIR.Tilde {
    internal class TildeCmd : MonoBehaviour {
        private const string CONSOLE_CONTROL_NAME = "console";
        private static string _inputText = "";

        internal void Draw() {
            
            GUILayout.BeginHorizontal();
            GUI.FocusControl(CONSOLE_CONTROL_NAME);
            GUI.SetNextControlName(CONSOLE_CONTROL_NAME);
            _inputText = GUILayout.TextField(_inputText, GUILayout.ExpandWidth(true));

            if (_inputText.Contains("`")) {
                _inputText = _inputText.Replace("`", "");
                Tilde.Visible = false;
            }

            var e = Event.current;
            var enterKeyPressed = e.type == EventType.KeyDown && e.keyCode == KeyCode.Return;

            var enterButtonPressed = GUILayout.Button("enter", GUILayout.ExpandWidth(false));
            
            if (enterKeyPressed || enterButtonPressed) {
                Tilde.RunCommand(_inputText);
                Tilde.Visible = false;
            }

            GUILayout.EndHorizontal();
        }
    }
}