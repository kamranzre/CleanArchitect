using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class OperationViewModel
    {
        public long Num1 { get; set; }

        public long Num2 { get; set; }

        public string? operationStr { get; set; }

        public string? Result { get; set; }

        public OperationType? OperationType
        {
            get
            {
                int? convert = operationStr != null ? int.Parse(operationStr) : null;
                if (convert != null)
                {
                return convert != null ? (OperationType)convert : null;
                }
                return null;
            }
        }
    }

}
