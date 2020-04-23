using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialQuest1 : Quest
{
    // Start is called before the first frame update
    void Start()
    {
        QuestName = "Your first quest";
        Description = "123";

        Goals = new List<Goal>
        {
            new CollectionGoal(this, "test", "Find a test", false, 0, 1)
        };

        Goals.ForEach(g => g.Init());
    }

}
