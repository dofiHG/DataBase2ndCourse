using System;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityNpgsql;

public class SpawnHeaders : MonoBehaviour
{
    private Connection _connection;
    private Transform _panel;
    private List<string> _names = new List<string>();
    private List<string> _values = new List<string>();

    public GameObject _prefab;
    public GameObject _prefabText;
    public string _tableName;
    public Transform _infoContent;
    public Transform _infoPanel;
    public GridLayoutGroup _glg;

    private void Start()
    {
        _connection = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Connection>();
        _panel = GameObject.Find("Header").GetComponentInParent<Transform>().Find("Viewport").Find("Content");
        _infoContent = GameObject.FindGameObjectWithTag("InfoContent").GetComponent<Transform>();
        _infoPanel = GameObject.Find("Information").GetComponent<Transform>();
        _glg = GameObject.FindGameObjectWithTag("InfoContent").GetComponent<GridLayoutGroup>();
    }

    public void ShowHeaders()
    {
        foreach (Transform child in _panel) { Destroy(child.gameObject); }
        _tableName = gameObject.GetComponentInChildren<TMP_Text>().text;

        var cmd = new NpgsqlCommand($"SELECT" +
            $"        a.attname as \"Column\"" +
            $"    FROM" +
            $"        pg_catalog.pg_attribute a" +
            $"    WHERE" +
            $"       a.attnum > 0" +
            $"       AND NOT a.attisdropped" +
            $"       AND a.attrelid = (" +
            $"           SELECT c.oid" +
            $"           FROM pg_catalog.pg_class c" +
            $"               LEFT JOIN pg_catalog.pg_namespace n ON n.oid = c.relnamespace" +
            $"            WHERE c.relname ~ '^({_tableName})$'" +
            $"               AND pg_catalog.pg_table_is_visible(c.oid)" +
            $"        );", _connection.connection);
        var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            GameObject obj = Instantiate(_prefab, _panel);
            obj.gameObject.GetComponent<TMP_Text>().text = reader.GetString(0);
            obj.GetComponent<RectTransform>().sizeDelta = new Vector3(130, 15);
            //obj.transform.Find("SortBtn").SetActive(true);
        }
        reader.Close();
    }

    public void ShowInfo() => ShowInformation();

    public void ShowInformation(string colName = "", string method = "")
    {
        foreach (Transform child in _infoContent) { Destroy(child.gameObject); }

        var cmd = new NpgsqlCommand($"SELECT COUNT(*) FROM information_schema.columns WHERE table_name = '{_tableName}'", _connection.connection);
        int columnCount = Convert.ToInt32(cmd.ExecuteScalar());

        if (method == "")
            cmd = new NpgsqlCommand($"SELECT * FROM {_tableName}", _connection.connection);
        else if (method == "MinToMax")
            cmd = new NpgsqlCommand($"SELECT * FROM {_tableName} ORDER BY {colName} ASC", _connection.connection);
        else if (method == "MaxToMin")
            cmd = new NpgsqlCommand($"SELECT * FROM {_tableName} ORDER BY {colName} DESC", _connection.connection);

        var reader = cmd.ExecuteReader();
        
        _glg.constraintCount = columnCount;

        while (reader.Read()) 
        {
            for (int i = 0; i < columnCount; i++)
            {
                GameObject obj1 = Instantiate(_prefabText, _infoContent);
                obj1.gameObject.GetComponent<TMP_Text>().text = Convert.ToString(reader[i]);
            }
        }
    }

    public void Replace()
    {
        _values.Clear();
        _names.Clear();

        int i = -1;
        foreach (Transform child in _infoContent.transform)
        {
            TMP_InputField inputField = child.GetComponent<TMP_InputField>();
            i++;
            if (inputField != null)
            {

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

        Debug.Log(values);
    }
}
