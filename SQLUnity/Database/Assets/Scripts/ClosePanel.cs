using UnityEngine;

public class ClosePanel : MonoBehaviour
{
    public GameObject _panel;
    public Transform _closePanel2;

    public void ClosePan()
    {
        _panel.SetActive(false);
        foreach (Transform child in _closePanel2)
            child.gameObject.SetActive(true);
    }
}
