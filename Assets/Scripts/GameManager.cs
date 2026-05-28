using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public List<Card_data> deck = new List<Card_data>();
    public List<Card_data> player_deck = new List<Card_data>();
    public List<Card_data> ai_deck = new List<Card_data>();
    public List<Card> player_hand = new List<Card>();
    public List<Card> ai_hand = new List<Card>();
    public List<Card_data> discard_pile = new List<Card_data>();
    public Canvas canvas;
    public Vector3 player_hand_pos;
    public Vector3 ai_hand_pos;
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
        player_hand_pos.x += 165;
        
        ai_hand_pos.x = player_hand_pos.x;
        
        ai_hand_pos.y = player_hand_pos.y + 150;
        
        Shuffle();
        
        Deal();

        Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void Deal()
    {
    Debug.Log("player is dealing cards.");
        for (int i = 0; i < 5; i++)
        {
            Card top_card = Instantiate(blank, player_hand_pos, Quaternion.identity, canvas.transform);

            player_hand_pos.x += 100;

            top_card.data = player_deck[0];

            player_hand.Add(top_card);

            player_deck.RemoveAt(0);
        }
        ai_deal();
    }

    void Shuffle()
    {
        Debug.Log("shuffling player deck.");
        // Fisher-Yates shuffle algorithm for player deck
        for (int i = player_deck.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            // Swap
            Card_data temp = player_deck[i];
           
            player_deck[i] = player_deck[randomIndex];
           
            player_deck[randomIndex] = temp;
        }
    }

    void AI_Turn()
    {
        ai_deal();
    } 

    void ai_deal()
    {
        Debug.Log("AI is dealing cards.");
        for (int i = 0; i < 5; i++)
        {
            Card ai_top_card = Instantiate(blank, ai_hand_pos, Quaternion.identity, canvas.transform);

            ai_hand_pos.x += 100;
           
            ai_top_card.data = ai_deck[0];

            ai_hand.Add(ai_top_card);
            
            ai_deck.RemoveAt(0);

            /*
            player_deck.RemoveAt(0);
            ai_hand.Add(ai_deck[0]);
            ai_deck.RemoveAt(0);
            */
        }
    }

    void card_to_player_hand()
    {
        int randomIndex = Random.Range(0, deck.Count);

        Card deck_top_card = Instantiate(blank, player_hand_pos, Quaternion.identity, canvas.transform);
       
        player_hand_pos.x += 100;

        deck_top_card.data = deck[randomIndex]; 

        deck.RemoveAt(randomIndex);
        
        player_hand.Add(deck_top_card);

        Deal();
    } 
     void Play()
    {
         Card player_card = player_hand[0];
         Card ai_card = ai_hand[0];
            // Compare the cards and determine the winner
            if (player_card.data.cost > ai_card.data.cost)
            {
                Debug.Log("Player wins the round!");
                //Player wins, add cards to player's discard pile
                discard_pile.Add(player_card.data);
                player_hand.RemoveAt(0);
                discard_pile.Add(ai_card.data);
               player_hand.RemoveAt(0);
            }
            else if (player_card.data.cost < ai_card.data.cost)
            {
                Debug.Log("AI wins the round!");
               //AI wins, add cards to AI's discard pile
                discard_pile.Add(player_card.data);
                player_hand.RemoveAt(0);
                discard_pile.Add(ai_card.data);
                player_hand.RemoveAt(0);
            }
            else
            {
                Debug.Log("It's a tie!");
                // In case of a tie, both players keep their cards
            }
    }     
}