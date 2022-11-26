using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    [Header("UI�ե�")]
    public TextMeshProUGUI textLabel;
    public Image faceImage;


    [Header("�奻���")]
    public TextAsset textFile;
    public int index;
    public float textSpeed;

    [Header("��ø")]
    public Sprite headPlayer;
    public Sprite headNPC;

    bool textFinished;  //�奻�O�_�]��
    bool isTyping;  //�O�_�b�v�r���


    List<string> textList = new List<string>();

    void Awake()
    {
        GetTextFromFile(textFile);
    }

    void OnEnable()
    {
        index = 0;  //��ܮبC�������ܬ���ܴN���m���
        textFinished = true;    //��ܮبC�������ܬ���ܪ��A�ܬ��奻�w����
        StartCoroutine(setTextUI());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && index == textList.Count)
        {
            gameObject.SetActive(false);
            return;
        }

        //���UF��A��e��奻�����N�����{�A��e��奻�������N������ܷ�e��奻
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (textFinished)
            {
                StartCoroutine(setTextUI());
            }
            else if (!textFinished)
            {
                isTyping = false;
            }
        }

        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    textLabel.text = textList[index];
        //    index++;
        //}
    }


    //�q�奻��������
    void GetTextFromFile(TextAsset file)
    {
        textList.Clear();//�M�Ť奻���e

        //���Τ奻��󤺮e�M��@��@��[��list���X��
        var lineData = file.text.Split('\n');
        foreach (var line in lineData)
        {
            textList.Add(line);
        }
    }

    IEnumerator setTextUI()
    {
        textFinished = false;   //�i�J��r��ܪ��A
        textLabel.text = "";    //���m�奻���e

        //�P�_�奻���̪����e
        switch (textList[index].Trim())
        {
            case "A":
                faceImage.sprite = headPlayer;
                index++;
                break;
            case "B":
                faceImage.sprite = headNPC;
                index++;
                break;
        }

        //�C���@��F�伽��@���r
        int word = 0;
        while (isTyping && word < textList[index].Length - 1)
        {
            //�v�r���
            textLabel.text += textList[index][word];
            word++;
            yield return new WaitForSeconds(textSpeed);
        }
        //�ֳt��ܤ奻���e�����椺�e
        textLabel.text = textList[index];

        isTyping = true;
        textFinished = true;
        index++;
    }
}