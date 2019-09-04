using System;

namespace ServerAPI.Models
{
    public class UpdateTaskModel
    {
        public ServerAPI.Data.Task Task { get; set; }

        public string TaskName { get; set; }

        public string TaskDescription { get; set; }

        public int UserId { get; set; }

        public DateTime TaskFinishDate { get; set; }

    }
}
