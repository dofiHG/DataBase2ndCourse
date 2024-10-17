using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PaintButton : MonoBehaviour
{
    public void PaintBtn()
    {
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("DBName");

        foreach (GameObject button in buttons)
        {
            button.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            button.GetComponentInChildren<TMP_Text>().color = new Color32(0, 0, 0, 255);
        }

        gameObject.GetComponent<Image>().color = new Color32(17, 136, 245, 255);
        gameObject.GetComponentInChildren<TMP_Text>().color = new Color32(255, 255, 255, 255);
    }
}
