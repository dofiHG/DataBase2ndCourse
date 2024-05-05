using UnityEngine;
using UnityNpgsql;

public class Connection : MonoBehaviour
{
    private PanelBtns _panelBtns;

    private void Start() => _panelBtns = GameObject.FindGameObjectWithTag("StartPanel").GetComponent<PanelBtns>();

    public NpgsqlConnection connection;

    private string _hostText;
    private string _usernameText;
    private string _passwordText;
    private string _dbnameText;

    public void Connect()
    {
        _hostText = _panelBtns._host.text;
        _usernameText = _panelBtns._username.text;
        _passwordText = _panelBtns._password.text;
        _dbnameText = _panelBtns._dbName.text;

        string connectionString = $"Server=localhost; Database=mydb; User Id=postgres; Password=root;";
        connection = new NpgsqlConnection(connectionString);
        connection.Open();
        _panelBtns._panelStart.SetActive(false);
    }
}
