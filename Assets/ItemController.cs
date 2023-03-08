using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;//ランダム変数用
using TMPro;
using UnityEngine.UI;
using System.Linq;//Containsメソッド用

public class ItemController : MonoBehaviour
{
    public TextMeshProUGUI item1;
    public TextMeshProUGUI item2;
    public TextMeshProUGUI item3;
    public TextMeshProUGUI item4;
    public TextMeshProUGUI item5;
    public static int reverse = 1;//リバースフラグ。-1の時逆順になる
    public static int[] skip = new int[] {0, 0, 0, 0};//それぞれの人のスキップフラグ

    string[] itemlist = new string[] {"SKIPカード", "リバースカード", "保険証", "更地カード", "指定マスカード"};
    static int[] used = new int[] {1, 1, 1, 1, 1};//使ったアイテムは???から実際の名前にする

    public GameController gamecontroller;
    public TextMeshProUGUI message;//エンディング、アイテム使用時などのメッセージ
  

    // Update is called once per frame

    void Update()
    {   
        int players_turn = GameController.players_turn;
        if(used[0]!=0)item1.text = itemlist[0] + "×" + GameController.players_item[players_turn, 0].ToString();
        if(used[1]!=0)item2.text = itemlist[1] + "×" + GameController.players_item[players_turn, 1].ToString();
        if(used[2]!=0)item3.text = itemlist[2] + "×" + GameController.players_item[players_turn, 2].ToString();
        if(used[3]!=0)item4.text = itemlist[3] + "×" + GameController.players_item[players_turn, 3].ToString();
        if(used[4]!=0)item5.text = itemlist[4] + "×" + GameController.players_item[players_turn, 4].ToString();
    }


    public static int[] skip_flg = new int[] {0, 0, 0, 0};
    public void Skip(){//Skipカード
        if(GameController.players_item[GameController.players_turn, 0] >= 1){
            GameController.players_item[GameController.players_turn, 0] -= 1;
            int next_player = GameController.players_turn;
            next_player += reverse;
            next_player %= GameController.PLAYERS_NUM;
            skip_flg[next_player] = 1;//スキップフラグを1に
            StartCoroutine(Message("スキップカード！"));
        }
    }

    public void Reverse(){//リバースカード
        reverse *= -1;
        if(GameController.players_item[GameController.players_turn, 1] >= 1){
            GameController.players_item[GameController.players_turn, 1] -= 1;
             reverse *= -1;
            StartCoroutine(Message("リバースカード！"));
        }
    }

   

    public void HokenSyo(){

    }

    public void Sarachi(){//更地カード

    }


    public void ShiteiMasu(){//指定マスカード
        if(GameController.players_item[GameController.players_turn, 4] >= 0){
            GameController.players_item[GameController.players_turn,4] -= 1;
            StartCoroutine(Message("指定マスカード！", true));
        }
    }

    

    public TMP_InputField Masume;
    public GameObject ShiteiMasuPanel;
    public void InputMasume(){//ますを指定してインプットしたらコールバックされる
        string[] atai = new string[] {"1", "2", "3", "4", "5", "6"};
        if(atai.Contains(Masume.text))gamecontroller.Walk(int.Parse(Masume.text),0,0);
         ShiteiMasuPanel.SetActive(false);
    }
    

    public Button ItemPanelButton;
    public GameObject ItemPanel;
    IEnumerator Message(string newmessage, bool isShiteiMasuCard=false){//メッセージ, 指定マスパネルのときはパネルを出すアクティブにする
        ItemPanelButton.interactable = false;
        ItemPanel.SetActive(false);
        message.text = newmessage;
        yield return new WaitForSeconds(1f);
        message.text = "";
        if (isShiteiMasuCard){
            ShiteiMasuPanel.SetActive(true);
        }
    }
    public void ItemPanelActive(){//アイテムボタンを押すとアイテムパネルが表示される
        if(ItemPanel.activeSelf) ItemPanel.SetActive(false);
        else ItemPanel.SetActive(true);
    }
}
