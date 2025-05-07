using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interacts : MonoBehaviour
{
    [Serializable]
    struct Mapping
    {
        public string tag;
        public GameObject ui;
    }

    [Header("UI �ֱ�")]
    [SerializeField] Mapping[] mappings;

    private void Start()
    {
        // �� �������ڸ��� ��� ���ε� UI�� �����
        foreach (var m in mappings)
        {
            if (m.ui != null)
                m.ui.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        foreach (var m in mappings)
        {
            if (other.CompareTag(m.tag))
            {
                m.ui.SetActive(true);
            }   
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        //�±׿� ���ε� UI�� ������ ����
        foreach (var m in mappings)
        {
            if (other.CompareTag(m.tag))
            {
                if (m.ui != null)
                    m.ui.SetActive(false);
            }
        }           
    }

    public void GoMinigame_1()
    {
        SceneManager.LoadScene("FlappyPlane");
    }
}
