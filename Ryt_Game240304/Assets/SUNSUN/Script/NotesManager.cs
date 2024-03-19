using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class NotesManager : MonoBehaviour
{
    //Unity�ʺA���J resources
    //Resources.Load( ��m+�ɦW );

    // full combo �ƶq
    public int Total_NoteNum;
    private string songName;
    //�b���D����
    public List<int> TrackNum = new List<int>();
    //note����
    public List<int> NoteType = new List<int>();
    //�P�w�ɶ�
    public List<float> NotesTime = new List<float>();
    public List<GameObject> NotesObj = new List<GameObject>();

    [SerializeField] float NoteSpeed;
    [SerializeField] GameObject noteObj;

     void OnEnable()
     {
       Total_NoteNum = 0;
        //�W�r�n��Json�ɤ@��
       songName = "USAO - Wildfire";
       Load(songName);
     }
     void Load(string Song_Name)
     {
        //�ѪRJason
        string inputString = Resources.Load<TextAsset>(Song_Name).ToString();
        Data inputJson = JsonUtility.FromJson<Data>(inputString);

        //�Э�Json����note������
        Total_NoteNum = inputJson.notes.Length;

        for(int i =0; i < inputJson.notes.Length; i++)
        {
            float BeatInterval = 60 / (inputJson.BPM * (float)inputJson.notes[i].LPB);
            //�C�ӭ��ťe�Ϊ��ɶ�,�C�`���ũ��*�C��ɶ�����
            float beatSec = BeatInterval * (float)inputJson.notes[i].LPB;
            //���ťX�{���ɶ��I,
            float time = (beatSec * inputJson.notes[i].num/(float)inputJson.notes[i].LPB) + inputJson.offeset+0.01f;
            //��m��List�x�s
            NotesTime.Add(time);
            TrackNum.Add(inputJson.notes[i].block);
            NoteType.Add(inputJson.notes[i].type);

            //�C�ӭ��Ū��ɶ���T * �t��
            float z = NotesTime[i] * NoteSpeed;
            //�ͦ�OBJ����,Quaternion.identity�S������
            NotesObj.Add(Instantiate(noteObj, new Vector3(inputJson.notes[i].block - 1.5f , 0.55f, z), Quaternion.identity));
        }

     }
}
//�ϧǦC�ư���Jason
[Serializable]
public class Data
{
    public string name;
    public int  maxBlock;
    public int BPM;
    public int offeset;
    public Note[] notes;

}
[Serializable]
public class Note
{
    public int type;
    public int num;
    public int block;
    public int LPB;//Lines Per Beat�^ ���

}