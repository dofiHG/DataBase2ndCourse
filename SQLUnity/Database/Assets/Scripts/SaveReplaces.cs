using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityNpgsql;

public class SaveReplaces : MonoBehaviour
{
    private List<string> _names = new List<string>();
    private List<string> _values = new List<string>();
    private Connection _connection;

    public string _tablNa;
    public Transform _infoContent;
    public Transform _header;
    public GameObject _exptPanel;
    public TMP_Text _exeptionText;

    private void Start()
    {
        _connection = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Connection>();
    }

    public void Replace()
    {
        try
        {
            _values.Clear();
            _names.Clear();
            foreach (Transform child in _header.transform)
            {
                _names.Add(child.gameObject.GetComponent<TMP_Text>().text);
            }



            int i = -1;
            foreach (Transform child in _infoContent.transform)
            {
                TMP_InputField inputField = child.GetComponent<TMP_InputField>();
                i++;
                if (inputField.text.Length != 0)
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
                else
                {
                    try
                    {
                        int convInt = Convert.ToInt32(inputField.transform.Find("Text Area").Find("Placeholder").GetComponent<TMP_Text>().text);
                        _values.Add(inputField.transform.Find("Text Area").Find("Placeholder").GetComponent<TMP_Text>().text);
                    }
                    catch
                    {
                        _values.Add($"'{inputField.transform.Find("Text Area").Find("Placeholder").GetComponent<TMP_Text>().text}'");
                    }
                }
            }

            string pkName = "";
            List<string> pkVal = new List<string>();
            pkVal.Clear();

            var cmd = new NpgsqlCommand("SELECT a.attname AS column_name " +
                                        "FROM pg_index i  " +
                                        "JOIN pg_attribute a ON a.attrelid = i.indrelid AND a.attnum = ANY(i.indkey)" +
                                        $"WHERE i.indrelid = '{_tablNa}'::regclass AND i.indisprimary;", _connection.connection);
            var reader = cmd.ExecuteReader();
            gameObject.GetComponent<Image>().color = new Color32(140, 255, 129, 255);
            StartCoroutine(Delay());
            while (reader.Read()) { pkName = reader.GetString(0); }
            reader.Close();

            cmd = new NpgsqlCommand($"SELECT {pkName} FROM {_tablNa};", _connection.connection);
            reader = cmd.ExecuteReader();
            while (reader.Read()) { pkVal.Add(Convert.ToString(reader[0])); }
            reader.Close();

            for (int j = 0; j < pkVal.Count; j++)
            {
                for (int k = 0; k < _names.Count; k++)
                {
                    cmd = new NpgsqlCommand($"UPDATE {_tablNa} SET {_names[k]} = {_values[0]} WHERE {pkName} = {pkVal[j]}", _connection.connection);
                    cmd.ExecuteNonQuery();
                    _values.RemoveAt(0);
                }
            }
        }

        catch (Exception exc)
        {
            _exeptionText.text = exc.Message;
            _exptPanel.gameObject.SetActive(true);
        }

    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);

    }
}
