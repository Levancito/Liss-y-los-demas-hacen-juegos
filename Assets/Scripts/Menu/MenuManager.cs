using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI coment;
    public TextMeshProUGUI updatetext;


    private void Update()
    {
        UpdateComentary(coment.text, updatetext.text);
    }
    public void UpdateComentary(string newcoment, string newupdate)
    {
        coment.text = newcoment;
        updatetext.text = newupdate;
    }
}
