using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SortButtons : MonoBehaviour
{
    private Connection _connection;

    private void Start() => _connection = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Connection>();

    public void ClickSortBtn() => gameObject.transform.Find("SortMethod").gameObject.SetActive(true);

    public void MinToMaxSort()
    {
        gameObject.transform.Find("SortMethod").gameObject.SetActive(false);
        string colName = gameObject.GetComponentInChildren<TMP_Text>().text;
        GameObject current = null;
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("DBName");
        foreach (GameObject button in buttons)
        if (button.GetComponent<Image>().color == new Color32(136, 255, 165, 255))
            current = button;

        current.GetComponent<SpawnHeaders>().ShowInformation(colName, "MinToMax");
    }

    public void MaxToMinSort()
    {
        gameObject.transform.Find("SortMethod").gameObject.SetActive(false);
        gameObject.transform.Find("SortMethod").gameObject.SetActive(false);
        string colName = gameObject.GetComponentInChildren<TMP_Text>().text;
        GameObject current = null;
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("DBName");
        foreach (GameObject button in buttons)
        if (button.GetComponent<Image>().color == new Color32(136, 255, 165, 255))
            current = button;

        current.GetComponent<SpawnHeaders>().ShowInformation(colName, "MaxToMin");
    }
}
