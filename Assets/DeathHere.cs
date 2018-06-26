using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHere : MonoBehaviour
{
    //    Стандартна    функція    ,    якавикличеться,
    //колипоточнийоб’єктзіштовхнетьсяізіншим
    void OnTriggerEnter2D(Collider2D collider)
    {
        //        Намагаємося        отриматикомпоненткролика
        Rabbit rabbit = collider.GetComponent<Rabbit>();
        //        Впасти        мігнетількикролик
        if (rabbit != null)
        {
            //            Повідомляємо            рівень            ,            просмертькролика
            LevelController.current.onRabbitDeath(rabbit);
        }
    }
}