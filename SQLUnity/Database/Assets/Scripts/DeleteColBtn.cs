using TMPro;
using UnityEngine;
using UnityNpgsql;

public class DeleteColBtn : MonoBehaviour
{
    private Connection _connection;
    private DBDelList _delList;

    public TMP_InputField _inputField;

    private void Start()
    {
        _connection = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Connection>();
        _delList = GameObject.FindGameObjectWithTag("DBName").GetComponent<DBDelList>();
    }

    public void DeleteCol()
    {
        var cmd = new NpgsqlCommand($"DELETE FROM {_delList.tableName} WHERE {_delList._whatDelText.text.Substring(8)} = @Value", _connection.connection);
        cmd.Parameters.AddWithValue("@Value ", _inputField.text);
        cmd.ExecuteNonQuery();
    }
}
