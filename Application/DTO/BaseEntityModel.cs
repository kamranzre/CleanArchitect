using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class BaseEntityModel
    {
        public BaseEntityModel()
        {
            CreateDate = DateTime.Now;
        }

        [Key]
        public long Id { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime EditationDate { get; set; }

        public long UserCreate { get; set; }

        public long UserEditation { get; set; }
    }
}
