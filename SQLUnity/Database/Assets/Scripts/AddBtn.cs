using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityNpgsql;

public class AddBtn : MonoBehaviour
{
    [SerializeField] GameObject _panel;
    [SerializeField] GameObject _prefab;
    [SerializeField] Transform _content;

    private PanelBtns _panelBtns;
    private List<string> _dataBaseNames = new List<string>();

    private void Start() => _panelBtns = GameObject.FindGameObjectWithTag("StartPanel").GetComponent<PanelBtns>();

    public void Click() => _panel.SetActive(true);

    public void AddNamesToArray()
    {
        var cmd = new NpgsqlCommand("SELECT table_name FROM information_schema.tables WHERE table_schema = 'public';", _panelBtns.connection);
        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            GameObject obj = Instantiate(_prefab, _content);
            obj.gameObject.GetComponentInChildren<Button>().GetComponent<TMP_Text>().text = reader.GetString(0);
            _dataBaseNames.Add(reader.GetString(0));
        }
        reader.Close();

        for(int i = 0; i < _dataBaseNames.Count; i++) { Debug.Log(_dataBaseNames[i]); }
    }
}
