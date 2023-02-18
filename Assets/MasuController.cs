using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;//ランダム変数用
using UnityEngine.UI;

public class MasuController : MonoBehaviour
{
    System.Random saikoro = new System.Random();

    int nameid1;//イベントマス関係
    int nameid2;
    int nameid3;
    int nameid4;
    bool moveName;
    int nameID;
    float currentTime;
    public TextMeshProUGUI name1;
    public TextMeshProUGUI name2;
    public TextMeshProUGUI name3;
    public TextMeshProUGUI name4;
    public TextMeshProUGUI sugakusyacomment;
    public Button SugakusyaCommentButton;
    string[] sugakusya = {"ニュートン", "アルキメデス", "ノイマン", "ピタゴラス", "チューリング"};
    List<List<string>> sugakusya_comment_list = new List<List<string>>() {
        new List<string> {"私はニュートン", "よろしく"},
        new List<string> {"私はアルキメデス", "よろしく"},
        new List<string> {"私はノイマン", "ですよ"},
        new List<string> {"私はピタゴラス"},
        new List<string> {"私はチューリング"},};
    int selected_sugakusya_id;
    int sugakusyacommentid;


    string[] ITEMS = new string[] {"アイテム1", "アイテム2", "アイテム3", "アイテム4", "アイテム5"};//アイテムます関係
    int selected_item;
    int item1, item2, item3;
    public Image nekoImage;
    public Sprite[] nekoImages;
    public TextMeshProUGUI nekoserihu;//ショップの猫のセリフ
    public TextMeshProUGUI syojikin;//右下のコインの枚数のテキスト
    public TextMeshProUGUI shopitem1;//ショップますで使うボタンのテキスト
    public TextMeshProUGUI shopitem2;
    public TextMeshProUGUI shopitem3;

    //関所ます関係
    string[] comment = new string[] {"こんにちは", "ここは関所です", "コイン50枚でメダルに交換できます。", "交換しますか?"};
    int commentid=0;
    public TextMeshProUGUI Dirichletcomment;
    public GameObject yes;
    public GameObject no;
    public GameObject SekisyoHaikeiButton;
    public TextMeshProUGUI DirichleSyojikin;



    // Start is called before the first frame update
    void Start()
    {
        //EventMasu();
        ShopMasu(0,1,2);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime>0.1f && moveName){
            nameID+=1;
            nameID%=4;//4人選択
            currentTime = 0f;
            name1.color = new Color(0, 0, 0, 1f);
            name2.color = new Color(0, 0, 0, 1f);
            name3.color = new Color(0, 0, 0, 1f);
            name4.color = new Color(0, 0, 0, 1f);
            if(nameID==0)name1.color = new Color(1f, 0.92f, 0.016f, 1f);
            if(nameID==1)name2.color = new Color(1f, 0.92f, 0.016f, 1f);
            if(nameID==2)name3.color = new Color(1f, 0.92f, 0.016f, 1f);
            if(nameID==3)name4.color = new Color(1f, 0.92f, 0.016f, 1f);
        }
    }




    private void CoinPlus(){
        GameController.players_coin[GameController.players_turn] += 1;
    }

    private void CoinMinus(){
        GameController.players_coin[GameController.players_turn] -= 1;
        GameController.players_coin[GameController.players_turn] = Math.Max(GameController.players_coin[GameController.players_turn], 0);
    }

    /*
    Syop画面の残りの作業
    ・アイテムの値段を決めて、購入後に所持金を減らす
    ・購入後の画面の切り替えとか色々
    */
    private void ShopMasu(int newitem1, int newitem2, int newitem3){//入荷アイテム3種類。今選択したアイテム。
        item1 = newitem1;
        item2 = newitem2;
        item3 = newitem3;
        selected_item = -1;//はじめは存在しないアイテムで初期化
        shopitem1.text = ITEMS[item1];
        shopitem2.text = ITEMS[item2];
        shopitem3.text = ITEMS[item3];
        syojikin.text = "×"+ GameController.players_coin[GameController.players_turn].ToString();
    }


    //ItemButton1とかはアイテムリストのボタンを押したときに反応
    public void ItemButton1(){//全部黒にしてから選んだものだけ黄色にする
        shopitem1.color = new Color(0, 0, 0, 1f);
        shopitem2.color = new Color(0, 0, 0, 1f);
        shopitem3.color = new Color(0, 0, 0, 1f);
        shopitem1.color = new Color(1f, 0.92f, 0.016f, 1f);
        if(selected_item==0){
            GameController.players_item[GameController.players_turn, item1] += 1;
            nekoserihu.text = "お買い上げありがとうございます！";
            nekoImage.sprite = nekoImages[5];
            syojikin.text = "×"+ GameController.players_coin[GameController.players_turn].ToString();
        }else{
            nekoserihu.text = ITEMS[item1] + "にしますか?";
            selected_item = item1;
            nekoImage.sprite = nekoImages[saikoro.Next(0,5)];
        }
    }


    public void ItemButton2(){
        shopitem1.color = new Color(0, 0, 0, 1f);
        shopitem2.color = new Color(0, 0, 0, 1f);
        shopitem3.color = new Color(0, 0, 0, 1f);
        shopitem2.color = new Color(1f, 0.92f, 0.016f, 1f);
        if(selected_item==1){
                GameController.players_item[GameController.players_turn, item2] += 1;
                nekoserihu.text = "お買い上げありがとうございます！";
                nekoImage.sprite = nekoImages[5];
                syojikin.text = "×"+ GameController.players_coin[GameController.players_turn].ToString();
        }else{
            nekoserihu.text = ITEMS[item2] + "にしますか?";
            selected_item = item2;
            nekoImage.sprite = nekoImages[saikoro.Next(0,5)];
        }
    }

    public void ItemButton3(){
        shopitem1.color = new Color(0, 0, 0, 1f);
        shopitem2.color = new Color(0, 0, 0, 1f);
        shopitem3.color = new Color(0, 0, 0, 1f);
        shopitem3.color = new Color(1f, 0.92f, 0.016f, 1f);
        if(selected_item==2){
            GameController.players_item[GameController.players_turn, item3] += 1;
            nekoserihu.text = "お買い上げありがとうございます！";
            nekoImage.sprite = nekoImages[5];
            syojikin.text = "×"+ GameController.players_coin[GameController.players_turn].ToString();
        }else{
            nekoserihu.text = ITEMS[item3] + "にしますか?";
            selected_item = item3;
            nekoImage.sprite = nekoImages[saikoro.Next(0,5)];
        }
    }

    
    public void SekisyoMasu(){//とりあえずクリックしたときに呼ばれる
        DirichleSyojikin.text = "×" + GameController.players_coin[GameController.players_turn].ToString();
        if (commentid < 3){
            commentid += 1;
            Dirichletcomment.text = comment[commentid];
        }else{
            yes.SetActive(true);
            no.SetActive(true);
        }
    }


    public void yesfunction(){
        if(GameController.players_coin[GameController.players_turn] < 50){
            Dirichletcomment.text = "お金が足りません";
        }else{
            GameController.players_coin[GameController.players_turn] -= 50;
            GameController.players_medal[GameController.players_turn] += 1;
            yes.SetActive(false);
            no.SetActive(false);
            Dirichletcomment.text = "まいどありがとうございます";
            DirichleSyojikin.text = "×" + GameController.players_coin[GameController.players_turn].ToString();
            StartCoroutine(ReturnFromSekisyo());
        }
    }


    public void nofunction(){
        Dirichletcomment.text = "さようなら";
        yes.SetActive(false);
        no.SetActive(false);
        StartCoroutine(ReturnFromSekisyo());
    }


    IEnumerator ReturnFromSekisyo(){
        yield return new WaitForSeconds(3f);
        SekisyoHaikeiButton.SetActive(false);
    }


    private void EventMasu(){
        SugakusyaCommentButton.interactable = false;
        while(nameid1==nameid2 || nameid2==nameid3 || nameid3==nameid4 || nameid4==nameid1){
            nameid1 = saikoro.Next(0, 5);
            nameid2 = saikoro.Next(0, 5);
            nameid3 = saikoro.Next(0, 5);
            nameid4 = saikoro.Next(0, 5);
        }
        name1.text = sugakusya[nameid1];
        name2.text = sugakusya[nameid2];
        name3.text = sugakusya[nameid3];
        name4.text = sugakusya[nameid4];
        moveName = true;
    }
    

    public void StopNameRoulette(){
        moveName = false;
        int [] selected_list = {nameid1, nameid2, nameid3, nameid4};
        Debug.Log(nameID);
        selected_sugakusya_id = selected_list[nameID];
        sugakusyacomment.text = sugakusya_comment_list[selected_sugakusya_id][sugakusyacommentid];
        SugakusyaCommentButton.interactable = true;
    }


    public void SugakusyaCommentFunc(){//ボタンを押すと反応
        sugakusyacommentid += 1;
        if(sugakusyacommentid < sugakusya_comment_list[selected_sugakusya_id].Count){
            sugakusyacomment.text = sugakusya_comment_list[selected_sugakusya_id][sugakusyacommentid];
        }else{
            SugakusyaCommentButton.interactable = false;
        }
    }
}