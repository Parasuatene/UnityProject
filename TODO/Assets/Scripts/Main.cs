using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

// 定義したクラスをJSONデータに変換できるようにする
[Serializable]
public class TodoData
{
    public string content;
    public string datetime;

    public TodoData(string content, string datetime)
    {
        this.content = content;
        this.datetime = datetime;
    }
}


public class Main : MonoBehaviour
{
    // テスト用のTextコンポーネント
    public Text inputTxt;

    private string jsonPath;

    private void Awake()
    {
        //初めに保存先を計算する　Application.dataPathで今開いているUnityプロジェクトのAssetsフォルダ直下を指定して、後ろに保存名を書く
        jsonPath = Application.dataPath + "/TestJson.json";
    }

    // Start is called before the first frame update
    void Start()
    {
        TodoData td = LoadJsonData();
        Debug.Log("content: " + td.content + " / datetime: " + td.datetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
     * TODO追加ボタン
     */
    public void onClickBtn()
    {
        // 入力文字列の取得
        string content = inputTxt.text;

        // 現在の日付を取得し，文字列として保存
        string datetime = DateTime.Now.ToString("yyyy/MM/dd");

        TodoData todoData = new TodoData(content, datetime);
        //string jsondata = JsonUtility.ToJson(todoData);

        SaveDataToJson(todoData);  // JSON形式で保存する
    }

    /*
     * Jsonファイルへの保存処理
     */
    public void SaveDataToJson(TodoData todoData)
    {
        string jsonstr = JsonUtility.ToJson(todoData);  // 受け取ったTodoDataをJSONに変換
        StreamWriter writer = new StreamWriter(jsonPath, false);  // 初めに指定したデータの保存先を開く
        writer.WriteLine(jsonstr);  // JSONデータを書き込み
        writer.Flush();  // バッファをクリアする
        writer.Close();  // ファイルをクローズする
    }

    // Jsonファイルを読み込み，返却する
    public TodoData LoadJsonData()
    {
        StreamReader reader = new StreamReader(jsonPath);  // 受け取ったパスのファイルを読み込む
        string dataStr = reader.ReadToEnd();  // ファイルの中身をすべて読み込む
        reader.Close();  // ファイルを閉じる

        return JsonUtility.FromJson<TodoData>(dataStr);  // 読み込んだJSONファイルをPlayerData型に変換して返す
    }
}
