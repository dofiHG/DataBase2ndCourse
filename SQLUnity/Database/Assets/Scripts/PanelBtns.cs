using System;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityNpgsql;

public class PanelBtns : MonoBehaviour
{
    [SerializeField] private Button _btnConnect;
    [SerializeField] private Button _btnExit;
    [SerializeField] private Button _btnOK;

    [SerializeField] private Image _imageExeption;

    [SerializeField] private GameObject _panelStart;

    [SerializeField] private TMP_InputField _host;
    [SerializeField] private TMP_InputField _username;
    [SerializeField] private TMP_InputField _password;
    [SerializeField] private TMP_InputField _dbName;

    [SerializeField] private TMP_Text _exeptionText;

    private string _hostText; 
    private string _usernameText;
    private string _passwordText;
    private string _dbnameText;

    public NpgsqlConnection connection;
    public void OpenWarningPanel() => _imageExeption.gameObject.SetActive(true);

    public void Connect()
    {
        _hostText = _host.text;
        _usernameText = _username.text;
        _passwordText = _password.text;
        _dbnameText = _dbName.text;

        try
        {
            string connectionString = $"Server=localhost; Database=mydb; User Id=postgres; Password=root;";
            connection = new NpgsqlConnection(connectionString);
            connection.Open();
            _panelStart.SetActive(false);

            var cmd = new NpgsqlCommand("SELECT table_name FROM information_schema.tables WHERE table_schema = 'public';", connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read()) { //Debug.Log($"ID:{reader.GetString(0)}"); 
                reader.Close(); }
        }
        catch (Exception exc) { _exeptionText.text = exc.Message; OpenWarningPanel(); Debug.Log(exc.Message); }
    }
    public void Exit() => Application.Quit();



}
