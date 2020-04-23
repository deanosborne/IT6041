using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Quest : MonoBehaviour
{
    public Quest Quest { get; set; }
    public List<Goal> Goals { get; set; } = new List<Goal>();
    public string QuestName { get; set; }
    public string Description { get; set; }
    public bool Completed { get; set; }

    public void CheckGoals()
    {
        if (Goals.All(g => g.Completed))
        {

        }
    }

    void Complete()
    {
        Completed = true;
    }
}
