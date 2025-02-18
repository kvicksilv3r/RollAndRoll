using UnityEditor.SceneManagement;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ClearPlayerPrefs : EditorWindow
{
    [MenuItem("Hg80/Clear player prefs")]

    static void Init()
    {
        EditorWindow window = EditorWindow.CreateInstance<ClearPlayerPrefs>();
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Clear player prefs?");

        if (GUILayout.Button("Yes"))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Lol it's all gone");
        }
    }
}