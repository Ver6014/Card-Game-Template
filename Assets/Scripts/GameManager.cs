using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public List<Card_data> deck = new List<Card_data>();
    public List<Card_data> player_deck = new List<Card_data>();
    public List<Card_data> ai_deck = new List<Card_data>();
    public List<Card_data> player_hand = new List<Card_data>();
    public List<Card_data> ai_hand = new List<Card_data>();
    public List<Card_data> discard_pile = new List<Card_data>();

    public Canvas canvas;
    public Vector3 player_hand_pos;
    public Card blank;

    private void Awake()
    {
        if (gm != null && gm != this)
        {
            Destroy(gameObject);
        }
        else
        {
            gm = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Deal()
    {
        for (int i = 0; i < 5; i++)
        {
            Card top_card = Instantiate(blank, player_hand_pos, Quaternion.identity, canvas.transform);

            player_hand_pos.x += 300;

            top_card.data = player_deck[0];

            player_hand.Add(top_card.data);

           player_deck.RemoveAt(0);

           /*
            player_deck.RemoveAt(0);
            ai_hand.Add(ai_deck[0]);
            ai_deck.RemoveAt(0);
            */
        }

    }

    void Shuffle()
    {

    }

    void AI_Turn()
    {

    }



    
}
