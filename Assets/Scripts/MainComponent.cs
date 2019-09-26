﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainComponent : MonoBehaviour
{
    private Player    m_player;
    private Labyrinth m_labyrinth;
    private bool      m_gameOver = false;

    private void Start             ()
    {
        Debug.Log("[DEBUG] Start...");
        
        // CREAMOS AL JUGADOR
        Weapon weapon = new Weapon ("Stick",       10);
        Shield shield = new Shield ("Wood Shield", 10);
        m_player = new Player (20, 5, 100, 100, 20, weapon, shield);

        // CREAMOS EL LABERINTO QUE CONTIENE LAS HABITACIONES
        m_labyrinth = new Labyrinth(m_player);

        // SE AÑADEN LAS HABITACIONES
        Enemy enemy = new Enemy  (m_player, "Angry Ent", 5, 5, 120, 60, 10, 10, 1, 20);
        Room  room  = new Room(Room.RoomType.ENEMY, enemy);
        m_labyrinth.AddRoom(room);

        Item item = new Item("J+RB", -5, 10, 10, 10, 0, -5);
        room = new Room(Room.RoomType.ITEM, item);
        m_labyrinth.AddRoom(room);

        Trap trap = new Trap("Examen", -10, -10);
        room = new Room(Room.RoomType.TRAP, trap);
        m_labyrinth.AddRoom(room);

        enemy = new Enemy  (m_player, "30fps", 5, 5, 120, 60, 10, 10, 1, 20);
        room = new Room(Room.RoomType.ENEMY, enemy);
        m_labyrinth.AddRoom(room);

        // ARRANCA EL LABERINTO
        m_labyrinth.ChangeRoom();
    }

    private void Update            ()
    {
        m_gameOver = m_player.IsDead();

        if (!m_gameOver && !m_labyrinth.IsFinished())
            m_labyrinth.UpdateLabyrinth(Time.deltaTime);
    }

    public void PlayerAttack       ()
    {
        if (!m_gameOver)
        {
            if (m_labyrinth.GetCurrentRoom().roomType == Room.RoomType.ENEMY)
            { 
                m_player.Attack(m_labyrinth.GetCurrentRoom().enemy);

                if (m_labyrinth.GetCurrentRoom().enemy.IsDead())
                {
                    Debug.Log("No pudiste aguantar los meteoros caballero de hojalata");
                    m_labyrinth.ChangeRoom (); 
                }
            }
        }
    }

    public void PlayerStrongAttack ()
    {
        if (!m_gameOver)
        {
            if (m_labyrinth.GetCurrentRoom().roomType == Room.RoomType.ENEMY)
            {
                m_player.StrongAttack(m_labyrinth.GetCurrentRoom().enemy);

                if (m_labyrinth.GetCurrentRoom().enemy.IsDead())
                {
                    Debug.Log("Hola, ¿estas bien?");
                    m_labyrinth.ChangeRoom (); 
                }
            }
        }
    }
}
