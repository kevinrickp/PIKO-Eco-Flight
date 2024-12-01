using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    int contoh;
    int contoh2;
    bool condition1;
    bool _iniSudahBenar; 
    bool iniSudahBenar;
/*    string iniString = "String";*/
    int[] arrayContoh = new int[10];

    private void Awake()
    {
        IniMethod("Awake");
    }

    private void OnEnable()
    {
        IniMethod("OnEnable");
    }

    // Start is called before the first frame update
    void Start()
    {
        IniMethod("Start"); // Memanggil Method

        
    }

    // Update is called once per frame
    void Update()
    {
        IniMethod("Update");
    }

    private void LateUpdate()
    {
        IniMethod("LateUpdate");
    }

    void IniMethod(string s)
    {
        
        Debug.Log(s);
    }

    void IniMethod(int i)
    {
        Debug.Log(i);
    }
}