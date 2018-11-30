using UnityEngine;
using System.Collections;

public class TimeTaskModel  {
    public int Id;
    public long time;
    public TimeManager.TaskEvent Event;

    public TimeTaskModel() { }
    public TimeTaskModel (int id,long tTime,TimeManager .TaskEvent eEvent)
    {
        this.Id = id;
        this.time = tTime;
        this.Event = eEvent;
    }

    public void Run()
    {
        Event();
    }
}
