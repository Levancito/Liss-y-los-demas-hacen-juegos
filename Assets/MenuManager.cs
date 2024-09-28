using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI coment;


    private void Update()
    {
        UpdateComentary(coment.text);
    }
    public void UpdateComentary(string newcoment)
    {
        coment.text = newcoment;
    }
}
