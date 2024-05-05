using UnityEngine;

public class ClosePanel : MonoBehaviour
{
    public GameObject _panel;

    public void ClosePan() => _panel.SetActive(false);
}
