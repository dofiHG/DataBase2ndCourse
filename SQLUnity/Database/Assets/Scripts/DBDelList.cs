using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityNpgsql;

public class DBDelList : MonoBehaviour
{
    private Connection _connection;

    public TMP_Text _warningText;
    public TMP_Text _whatDelText;
    public TMP_InputField _inputField;

    private void Start()
    {
        _connection = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Connection>();
        _warningText = GameObject.Find("WarningText").GetComponent<TMP_Text>();
        _whatDelText = GameObject.Find("WhatDeleteText").GetComponent<TMP_Text>();
        _inputField = GameObject.Find("DelInputField").GetComponent<TMP_InputField>();
    }

    public void ChoseTable()
    {
        _warningText.enabled = true;
        _whatDelText.enabled = true;
        _inputField.image.enabled = true;
        string tableName = gameObject.GetComponentInChildren<TMP_Text>().text;

        var cmd = new NpgsqlCommand(@$"
                SELECT
                    pg_constraint.conname AS constraint_name,
                    pg_attribute.attname AS column_name
                FROM
                    pg_constraint
                INNER JOIN pg_attribute 
                    ON pg_attribute.attrelid = pg_constraint.conrelid
                    AND pg_attribute.attnum = ANY(pg_constraint.conkey)
                WHERE
                    pg_constraint.contype = 'p'
                    AND pg_attribute.attnum > 0
                    AND pg_constraint.conrelid = '{tableName}'::regclass;", _connection.connection);

        var reader = cmd.ExecuteReader();
        while (reader.Read()) { Debug.Log(reader.GetString(1)); _whatDelText.text = $"¬ведите {reader.GetString(1)}"; }
    }
}
