using NestAlbania.Repositories;
using System.ComponentModel.DataAnnotations;

namespace NestAlbania.Data
{
    public class JobApply : BaseEntity
    { 
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Message { get; set; }

    }
}
