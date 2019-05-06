using System;


namespace ITI.Roomies.WebApp.Models.TaskModel
{
    public class TaskViewModel
    {
        public string TaskName { get; set; }

        public string TaskDes { get; set; }

        public DateTime TaskDate { get; set; }

        public int collocId { get; set; }

        public int[] roomiesId { get; set; }
    }
}
