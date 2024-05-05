using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityNpgsql;

public class OpenTableLines : MonoBehaviour
{
    private Connection _connection;
    private Transform _panel;

    public List<string> _namesCols = new List<string>();
    public GameObject _prefab;
    public string _tableName;

    private void Start()
    {
        _connection = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Connection>();
        _panel = GameObject.FindGameObjectWithTag("InputFields").GetComponentInParent<Transform>();
    }

    public void Click()
    {
        foreach (Transform child in _panel) { Destroy(child.gameObject); }
        _tableName = gameObject.GetComponentInChildren<TMP_Text>().text;

        var cmd = new NpgsqlCommand($"SELECT column_name FROM information_schema.columns WHERE table_name = '{gameObject.GetComponentInChildren<TMP_Text>().text}'", _connection.connection);
        var reader = cmd.ExecuteReader();

        while (reader.Read()) 
        { 
            GameObject obj = Instantiate(_prefab, _panel);
            _namesCols.Add(reader.GetString(0));
            obj.gameObject.GetComponentInChildren<TMP_Text>().text = reader.GetString(0);
        }
        reader.Close();
    }
}
