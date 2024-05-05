using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityNpgsql;

public class AddToDBButton : MonoBehaviour
{
    private Connection _connection;
    private Dictionary<string, string> _hashTable = new Dictionary<string, string>();
    public GameObject _inputFieldArea;

    public OpenTableLines _openTableLines;

    private void Start()
    {
        _connection = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Connection>();
        _openTableLines = GameObject.FindGameObjectWithTag("DBName").GetComponent<OpenTableLines>();
    }

    public void ClickToAdd()
    {
        int i = -1;
        foreach (Transform child in _inputFieldArea.transform)
        {
            TMP_InputField inputField = child.GetComponent<TMP_InputField>();
            i++;
            if (inputField != null)
            {
                string value = inputField.text;
                _hashTable.Add(_openTableLines._namesCols[i], value);
            }
        }

        foreach (string key in _hashTable.Keys)
        {
            try
            {
                int value = Convert.ToInt32(_hashTable[key]);
                var cmd = new NpgsqlCommand($"INSERT INTO {_openTableLines._tableName} ({key}) VALUES ({value})", _connection.connection);
                Debug.Log(_openTableLines._tableName);
                Debug.Log($"{key}   {value}");
            }
            catch 
            {
                string value = _hashTable[key];
                var cmd = new NpgsqlCommand($"INSERT INTO {_openTableLines._tableName} ({key}) VALUES ({value})", _connection.connection);
                Debug.Log(_openTableLines._tableName);
                Debug.Log($"{key}   {value}");
            }
            
        }
    }
}
