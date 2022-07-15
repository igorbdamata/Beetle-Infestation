using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class UI : MonoBehaviour
{
    public static UI ui;
    private void Awake()
    {
        if (ui == null) { ui = this; return; }
        Destroy(gameObject);
    }
}