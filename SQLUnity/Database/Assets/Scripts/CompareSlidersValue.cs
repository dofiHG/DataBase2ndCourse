using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CompareSlidersValue : MonoBehaviour
{
    public void CompareValues(Scrollbar dependSlider) => dependSlider.value = gameObject.GetComponent<Scrollbar>().value;
}
