using AutoMapper;
using Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IMapper Mapper;
        protected BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.UnitOfWork = unitOfWork;
            this.Mapper = mapper;
        }
    }
}
