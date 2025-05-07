using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using ServiceAbstraction;

namespace Service
{
    public class ServiceManager: IServiceManager
    {

        private readonly Lazy<IProductService> _LazyProductService;
        public ServiceManager(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _LazyProductService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
        }
        public IProductService ProductService => _LazyProductService.Value;

    }


}
