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
    public void OpenWarningPanel() => _imageExeption.gameObject.SetActive(true);

    /*private string connectionString = "Server=localhost; Port=5432; Database=mydb; User Id=postgres; Password=root;";
    private NpgsqlConnection connection;

    void Start()
    {
        connection = new NpgsqlConnection(connectionString);
        connection.Open();
        Debug.Log("Connected to PostgreSQL!");
    }

    void OnDestroy()
    {
        if (connection != null && connection.State == System.Data.ConnectionState.Open)
        {
            connection.Close();
            Debug.Log("Disconnected from PostgreSQL!");
        }
    }*/

    public void Connect()
    {
        _hostText = _host.text;
        _usernameText = _username.text;
        _passwordText = _password.text;
        _dbnameText = _dbName.text;

        try
        {
            string connectionString = $"Server={_hostText}; Database={_dbnameText}; User Id={_usernameText}; Password={_passwordText};";
            NpgsqlConnection connection;
            connection = new NpgsqlConnection(connectionString);
            connection.Open();
            Debug.Log("Connected to PostgreSQL!");
            _panelStart.SetActive(false);
        }
        catch (Exception exc) { _exeptionText.text = exc.Message; OpenWarningPanel(); Debug.Log(exc.Message); }

        

        /*using (var conn = new NpgsqlConnection(connString))
        {
            conn.Open();

            using (var cmd = new NpgsqlCommand("SELECT * FROM Тренеры", conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Debug.Log(reader[i]);
                        }
                    }

                }
            }
        }*/
    }
    public void Exit() => Application.Quit();



}
