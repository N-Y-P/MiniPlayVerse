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

    [Header("UI 넣기")]
    [SerializeField] Mapping[] mappings;

    private void Start()
    {
        // 씬 시작하자마자 모든 매핑된 UI를 숨기기
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
        //태그에 매핑된 UI가 있으면 끄기
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
