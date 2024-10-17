using System;
using TMPro;
using UnityEngine;
using UnityNpgsql;

public class Connection : MonoBehaviour
{
    public GameObject _exptPanel;
    public TMP_Text _exeptionText;
    public GameObject panelToClose;
    public TMP_InputField username;
    public TMP_InputField host;
    public TMP_InputField password;
    public TMP_InputField DBName;

    public NpgsqlConnection connection;

    private string _hostText;
    private string _usernameText;
    private string _passwordText;
    private string _dbnameText;

    public void Connect()
    {
        try
        {
            _hostText = host.text;
            _usernameText = username.text;
            _passwordText = password.text;
            _dbnameText = DBName.text;
            /*_hostText = "localhost";
            _usernameText = "postgres";
            _passwordText = "root";
            _dbnameText = "mydb";*/

            string connectionString = $"Server={_hostText}; Database={_dbnameText}; User Id={_usernameText}; Password={_passwordText};";
            connection = new NpgsqlConnection(connectionString);
            connection.Open();
            panelToClose.SetActive(false);

        }
        catch (Exception exc)
        {
            _exeptionText.text = exc.Message;
            _exptPanel.gameObject.SetActive(true);
        }
    }
}
