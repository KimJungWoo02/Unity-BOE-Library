using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
/// <summary>
/// 파일 입출력을 관리하는 매니저
/// </summary>
public class FileMng : Singleton<FileMng>
{
    protected FileMng() { }
    string filepath;

    private void Awake()
    {
        filepath = Application.persistentDataPath + "/PlayerData.txt";
    }

    public void LoadFile<T>(ref Dictionary<string, T> a, string path) where T : Object
    {
        string log = path + "에서 불러온 파일 \n";
        a = new Dictionary<string, T>();
        T[] particleSystems = Resources.LoadAll<T>(path);
        foreach (var particle in particleSystems)
        {
            log += particle.name + "\n";
            a.Add(particle.name, particle);
        }
        Debug.Log(log);
    }

    public void LoadFile<T>(ref List<T> a, string path) where T : Object
    {
        string log = path + "에서 불러온 파일 \n";
        a = new List<T>();
        T[] particleSystems = Resources.LoadAll<T>(path);
        foreach (var particle in particleSystems)
        {
            log += particle.name + "\n";
            a.Add(particle);
        }
        Debug.Log(log);
    }

    public static List<string[]> Load(string path)
    {
        List<string[]> _dataList = new List<string[]>();

        TextAsset _asset = Resources.Load<TextAsset>(path);
        if (_asset == null)
        {
            Debug.Log("CSV Parse Fail : " + path);
            return null;
        }

        StringReader sr = new StringReader(_asset.text);

        string _inputData = sr.ReadLine();
        while (_inputData != null)
        {
            string[] _datas = _inputData.Split('\t');
            if (_datas.Length == 0) continue;
            _dataList.Add(_datas);
            _inputData = sr.ReadLine();
        }
        sr.Close();
        Debug.Log("CSV Parse Load : " + path);
        return _dataList;
    }

}
