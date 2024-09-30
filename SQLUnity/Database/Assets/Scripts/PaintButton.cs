using UnityEngine;
using UnityEngine.UI;

public class PaintButton : MonoBehaviour
{
    public void PaintBtn()
    {
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("DBName");

        foreach (GameObject button in buttons) 
            button.GetComponent<Image>().color = new Color32(255, 255, 255, 255);

        gameObject.GetComponent<Image>().color = new Color32(255, 184, 239, 255);
    }
}
