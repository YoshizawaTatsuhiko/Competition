using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening; 

public class LoadScene : MonoBehaviour
{
    [Header("Fade in/out ����܂ł̎���")]
    [Tooltip("Fade in/out ����܂ł̎���")] [SerializeField] float _fadeTime = 1f;
    [Header("Fade in/out ������Panel")]
    [Tooltip("Fade in/out ������Panel")]    [SerializeField] Image _image = default;

    void Start()
    {
        //Panel��������悤�ɂ��Ă���
        _image?.CrossFadeAlpha(1, 0, false);
        //Fade in ������APanel������
        _image.enabled = true;
        _image.DOFade(0f, _fadeTime).OnComplete(() => _image.enabled = false);
    }

    /// <summary>�V�[����J�ڂ�����</summary>
    /// <param name="sceneName">�V�[���̖��O</param>
    public void SceneToLoad(string sceneName)
    {
        //Panel�������Ȃ����Ă���
        _image.CrossFadeAlpha(1, 0, false);
        //Fade out ������A�V�[����J�ڂ���
        _image.enabled = true;
        _image.DOFade(1f, _fadeTime).OnComplete(() => SceneManager.LoadScene(sceneName));
    }
}
