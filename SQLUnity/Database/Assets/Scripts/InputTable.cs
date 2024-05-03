using TMPro;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.UI;
using UnityNpgsql;

public class InputTable : MonoBehaviour
{
    private PanelBtns _panelBtns;

    private void Start() => _panelBtns = GameObject.FindGameObjectWithTag("StartPanel").GetComponent<PanelBtns>();

    public void Click()
    {
        var cmd = new NpgsqlCommand("SELECT table_name FROM information_schema.tables WHERE table_schema = 'public';", _panelBtns.connection);
        var reader = cmd.ExecuteReader();
        while (reader.Read()) 
        { 
            Debug.Log($"ID:{reader.GetString(0)}"); 
        }
        reader.Close();
    }
}
