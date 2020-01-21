using System;
using UnityEngine;

public class Tilde : MonoBehaviour
{

    public static event Action<string> OnCommand;

    private bool _visible = false; 
    private const string CONSOLE_CONTROL_NAME = "console";

    private string _inputText = "";

    private void Update() {
        if (Input.GetKeyDown(KeyCode.BackQuote)) 
            _visible = !_visible;
    }

    private void OnGUI() {
        if(!_visible) return;

        GUI.FocusControl(CONSOLE_CONTROL_NAME);
        GUI.SetNextControlName(CONSOLE_CONTROL_NAME);
        _inputText = GUILayout.TextField(_inputText, GUILayout.Width(Screen.width));

        if (_inputText.Contains("`")) {
            _inputText = _inputText.Replace("`", "");
            _visible = false;
        }

        var e = Event.current;
        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Return)
            OnCommand?.Invoke(_inputText);

    }

    // public static void WriteLine(string log) {
    //     Debug.Log(log);
    //     throw new NotImplementedException("Show logs in UI.");
    // }
}
