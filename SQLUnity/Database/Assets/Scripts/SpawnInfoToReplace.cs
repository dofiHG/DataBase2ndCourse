using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityNpgsql;

public class SpawnInfoToReplace : MonoBehaviour
{
    private Connection _connection;
    private Transform _panel;
    private SaveReplaces _saveRep;

    public GameObject _prefab;
    public GameObject _prefabText;
    public string _tableName;
    public Transform _infoContent;
    public Transform _infoPanel;
    public GridLayoutGroup _glg;

    private void Start()
    {
        _connection = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Connection>();
        _panel = GameObject.Find("Header").GetComponent<Transform>();
        _infoContent = GameObject.FindGameObjectWithTag("InfoContent").GetComponent<Transform>();
        _infoPanel = GameObject.Find("Information").GetComponent<Transform>();
        _glg = GameObject.FindGameObjectWithTag("InfoContent").GetComponent<GridLayoutGroup>();
        _saveRep = GameObject.Find("Replace").GetComponent<SaveReplaces>();
    }

    public void ShowHeaders()
    {
        foreach (Transform child in _panel.Find("Viewport").Find("Content")) { Destroy(child.gameObject); }
        _tableName = gameObject.GetComponentInChildren<TMP_Text>().text;

        _saveRep._tablNa = _tableName;

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
            GameObject obj = Instantiate(_prefab, _panel.Find("Viewport").Find("Content"));
            obj.gameObject.GetComponent<TMP_Text>().text = reader.GetString(0);
            obj.GetComponent<RectTransform>().sizeDelta = new Vector3(130, 20);
        }
        reader.Close();
    }

    public void ShowInformation()
    {
        foreach (Transform child in _infoContent) { Destroy(child.gameObject); }

        var cmd = new NpgsqlCommand($"SELECT COUNT(*) FROM information_schema.columns WHERE table_name = '{_tableName}'", _connection.connection);
        int columnCount = Convert.ToInt32(cmd.ExecuteScalar());

        cmd = new NpgsqlCommand($"SELECT * FROM {_tableName}", _connection.connection);
        var reader = cmd.ExecuteReader();

        _glg.constraintCount = columnCount;

        while (reader.Read())
        {
            for (int i = 0; i < columnCount; i++)
            {
                GameObject obj1 = Instantiate(_prefabText, _infoContent);
                obj1.gameObject.GetComponentInChildren<TMP_Text>().text = Convert.ToString(reader[i]);
            }
        }
    }
}
