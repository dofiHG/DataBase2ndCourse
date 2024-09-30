using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityNpgsql;

public class Connection : MonoBehaviour
{
    private PanelBtns _panelBtns;

    public GameObject _exptPanel;
    public TMP_Text _exeptionText;

    private void Start() => _panelBtns = GameObject.FindGameObjectWithTag("StartPanel").GetComponent<PanelBtns>();

    public NpgsqlConnection connection;

    private string _hostText;
    private string _usernameText;
    private string _passwordText;
    private string _dbnameText;

    public void Connect()
    {
        try
        {
            _hostText = _panelBtns._host.text;
            _usernameText = _panelBtns._username.text;
            _passwordText = _panelBtns._password.text;
            _dbnameText = _panelBtns._dbName.text;

            string connectionString = $"Server={_hostText}; Database={_dbnameText}; User Id={_usernameText}; Password={_passwordText};";
            connection = new NpgsqlConnection(connectionString);
            connection.Open();
            _panelBtns._panelStart.SetActive(false);
        }
        catch (Exception exc)
        {
            _exeptionText.text = exc.Message;
            _exptPanel.gameObject.SetActive(true);
        }
    }
}
