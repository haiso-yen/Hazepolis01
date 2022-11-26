using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public AudioMixer audioMixer;    // �i�汱�Mixer�ܶq

    public void SetMasterVolume(float volume)    // ����D���q�����
    {
        audioMixer.SetFloat("MasterVolume", volume);
        // MasterVolume���ڭ̼��S�X�Ӫ�Master���Ѽ�
    }

    public void SetMusicVolume(float volume)    // ����I�����֭��q�����
    {
        audioMixer.SetFloat("MusicVolume", volume);
        // MusicVolume���ڭ̼��S�X�Ӫ�Music���Ѽ�
    }

    public void SetSoundEffectVolume(float volume)    // ����ĭ��q�����
    {
        audioMixer.SetFloat("SoundEffectVolume", volume);
        // SoundEffectVolume���ڭ̼��S�X�Ӫ�SoundEffect���Ѽ�
    }
}