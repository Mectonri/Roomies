using System;

namespace ITI.Roomies.DAL
{
    public class TasksData
    {
        public int TaskId {get; set;}

        public string TaskName { get; set; }

        public DateTime TaskDate { get; set; }

        public bool State { get; set; }

        public int CollocId { get; set; }
    }
}
