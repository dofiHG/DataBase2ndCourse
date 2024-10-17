using TMPro;
using UnityEngine;
using UnityNpgsql;

public class ReplaceBtn : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _content;
    [SerializeField] private Transform _closePanel2;

    private Connection _connection;

    private void Start() => _connection = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Connection>();

    public void Click()
    {
        _panel.SetActive(true);
        foreach (Transform header in _panel.transform.Find("Scroll View").transform.Find("Header").Find("Viewport").Find("Content"))
            Destroy(header.gameObject);

        foreach (Transform inputField in _panel.transform.Find("Scroll View").transform.Find("Information").transform.Find("Viewport").transform.Find("Content"))
            Destroy(inputField.gameObject);
    }

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
        foreach (Transform child in _closePanel2)
            child.gameObject.SetActive(false);
    }
}
