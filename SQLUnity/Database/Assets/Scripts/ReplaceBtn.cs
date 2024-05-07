using TMPro;
using UnityEngine;
using UnityNpgsql;

public class ReplaceBtn : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _content;

    private Connection _connection;

    private void Start() => _connection = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Connection>();

    public void Click() => _panel.SetActive(true);

    public void AddNamesToArray()
    {
        foreach (Transform child in _content) { Destroy(child.gameObject); }
        var cmd = new NpgsqlCommand("SELECT table_name FROM information_schema.tables WHERE table_schema = 'public';", _connection.connection);
        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            GameObject obj = Instantiate(_prefab, _content);
            obj.GetComponentInChildren<TMP_Text>().text = reader.GetString(0);
        }
        reader.Close();
    }
}
