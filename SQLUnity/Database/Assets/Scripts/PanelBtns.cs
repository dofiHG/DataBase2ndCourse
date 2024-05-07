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

    [SerializeField] private TMP_Text _exeptionText;
    private Connection _connection;

    [SerializeField] public GameObject _panelStart;

    [SerializeField] public TMP_InputField _host;
    [SerializeField] public TMP_InputField _username;
    [SerializeField] public TMP_InputField _password;
    [SerializeField] public TMP_InputField _dbName;

    public NpgsqlConnection connection;

    private void Start() => _connection = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Connection>();

    public void OpenWarningPanel() => _imageExeption.gameObject.SetActive(true);

    public void ConnectClick()
    {
        try { _connection.Connect(); }
        catch (Exception exc) { _exeptionText.text = exc.Message; OpenWarningPanel(); Debug.Log(exc.Message); }
    }
    public void Exit() => Application.Quit();
}
