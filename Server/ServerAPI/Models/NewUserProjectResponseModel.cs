using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Models
{
    public class NewUserProjectResponseModel
    {
        public string Message { get; set; }

        public int CreatedUserId { get; set; }

        public int CreatedRoleId { get; set; }

        public int CreatedProjectId { get; set; }
    }
}
