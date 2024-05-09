using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityNpgsql;

public class CreateSQL : MonoBehaviour
{
    private Connection _connection;

    public TMP_InputField _anySQL;
    public GameObject _showPanel;
    public GameObject _exptPanel;
    public TMP_Text _exeptionText;

    private void Start() =>  _connection = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Connection>();

    public void TryYourRequest()
    {
        try
        {
            var cmd = new NpgsqlCommand(_anySQL.text, _connection.connection);
            cmd.ExecuteNonQuery();
        }
        catch (Exception exc)
        {
            _exeptionText.text = exc.Message;
            _exptPanel.gameObject.SetActive(true);
        }
    }
}
