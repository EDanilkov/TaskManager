using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedServicesModule.ResponseModel
{
    public class UpdateTaskModel
    {
        public Task Task { get; set; }

        public string TaskName { get; set; }

        public string TaskDescription { get; set; }

        public int UserId { get; set; }

        public DateTime TaskFinishDate { get; set; }

    }
}
