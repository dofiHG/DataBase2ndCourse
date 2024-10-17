using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityNpgsql;

public class DeleteColBtn : MonoBehaviour
{
    private Connection _connection;
    private DBDelList _delList;

    public TMP_InputField _inputField;
    public GameObject _exptPanel;
    public TMP_Text _exeptionText;

    private void Start()
    {
        _connection = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Connection>();
        _delList = GameObject.FindGameObjectWithTag("DBName").GetComponent<DBDelList>();
    }

    public void DeleteCol()
    {
        try
        {
            var cmd = new NpgsqlCommand($"DELETE FROM {_delList.tableName} WHERE {_delList._whatDelText.text.Substring(8)} = @Value", _connection.connection);
            cmd.Parameters.AddWithValue("@Value ", _inputField.text);
            cmd.ExecuteNonQuery();
            gameObject.GetComponent<Image>().color = new Color32(140, 255, 129, 255);
            StartCoroutine(Delay());

        }
        catch (Exception exc)
        {
            _exeptionText.text = exc.Message;
            _exptPanel.gameObject.SetActive(true);
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<Image>().color = new Color32(17, 136, 245, 255);
    }
}
