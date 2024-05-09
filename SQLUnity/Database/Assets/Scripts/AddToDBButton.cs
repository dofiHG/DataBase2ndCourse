using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityNpgsql;

public class AddToDBButton : MonoBehaviour
{
    private Connection _connection;
    private Dictionary<string, string> _hashTable = new Dictionary<string, string>();
    private List<string> _names = new List<string>();
    private List<string> _values = new List<string>();

    public GameObject _inputFieldArea;
    public OpenTableLines _openTableLines;
    public GameObject _exptPanel;
    public TMP_Text _exeptionText;

    private void Start()
    {
        _connection = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Connection>();
        _openTableLines = GameObject.FindGameObjectWithTag("DBName").GetComponent<OpenTableLines>();
    }

    public void ClickToAdd()
    {
        try
        {
            int i = -1;
            foreach (Transform child in _inputFieldArea.transform)
            {
                TMP_InputField inputField = child.GetComponent<TMP_InputField>();
                i++;
                if (inputField != null)
                {
                    _names.Add(_openTableLines._namesCols[i]);

                    try
                    {
                        int convInt = Convert.ToInt32(inputField.text);
                        _values.Add(inputField.text);
                    }
                    catch
                    {
                        _values.Add($"'{inputField.text}'");
                    }

                }
            }

            string colums = string.Join(",", _names.ToArray());
            string values = string.Join(",", _values.ToArray());

            var cmd = new NpgsqlCommand($"INSERT INTO {_openTableLines._tableName} ({colums}) VALUES ({values})", _connection.connection);
            cmd.ExecuteNonQuery();
        }

        catch (Exception exc)
        {
            _exeptionText.text = exc.Message;
            _exptPanel.gameObject.SetActive(true);
        }
    }
}
