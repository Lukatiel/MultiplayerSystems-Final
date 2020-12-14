using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatManager : MonoBehaviour
{
    public TMP_Text[] chatMessages;
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
        for (int i = chatMessages.Length - 1; i > 0; i--)
        {
            //Debug.Log(chatMessages[i].text);
            chatMessages[i].text = chatMessages[i - 1].text;
        }

        chatMessages[0].text = chatInput.text;

        chatInput.text = "";
    }
}
