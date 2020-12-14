using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatManager : MonoBehaviour
{
    public TMP_Text[] chatMessages = { };
    public TMP_InputField chatInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMessage()
    {
        Debug.Log("Hello");
        int count = 0;

        //for (int i = 0; i < chatMessages.Length; i++)
        //{
        //    Debug.Log(chatMessages[1].text);
        //    //chatMessages[i + 1].text = chatMessages[i].text;
        //}
        foreach (TMP_Text text in chatMessages)
        {
            chatMessages[count + 1].text = chatMessages[count].text;
            count++;
        }
        chatMessages[0].text = chatInput.text;

        chatInput.text = "";
    }
}
