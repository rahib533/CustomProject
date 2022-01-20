using Business.Abstract;
using Business.BusinessAspects.AutoFac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDAL _productDAL;
        ICategoryService _categoryService;
        public ProductManager(IProductDAL productDAL, ICategoryService categoryService)
        {
            _productDAL = productDAL;
            _categoryService = categoryService;
        }

        //[SecuredOperation("admin,add")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        [TransactionScopeAspect]
        public IResult Add(Product product)
        {
            var result = BusinessRules.Run(CheckCategoryCorrectCount(15));
            if (result != null)
            {
                return new ErrorResult(Messages.OutOfCategoryCount);
            }
            _productDAL.Add(product);
            return new SuccessResult(Messages.SuccessOperation);
        }

        [SecuredOperation("add")]
        //[CacheAspect]
        [PerformanceAspect(5)]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<List<Product>> GetAll()
        {
            //if (DateTime.Now.Hour < 18)
            //{
            //    return new ErrorDataResult<List<Product>>(Messages.MaintanceTime);
            //}
            //Thread.Sleep(5000);
            return new SuccessDataResult<List<Product>>(_productDAL.GetAll(),Messages.SuccessOperation);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDAL.GetAll(x => x.CategoryId == id), Messages.SuccessOperation);
        }

        //[CacheAspect]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<Product> GetById(int id)
        {
            return new SuccessDataResult<Product>(_productDAL.Get(p => p.ProductId == id), Messages.SuccessOperation);
        }

        public IDataResult<List<ProductDetailDTO>> GetProdactDetails()
        {
            return new SuccessDataResult<List<ProductDetailDTO>>(_productDAL.GetProdactDetails(), Messages.SuccessOperation);
        }

        private IResult CheckCategoryCorrectCount(int count)
        {
            if(_categoryService.GetAll().Count <= count)
            {
                return new SuccessResult();
            }
            return new ErrorResult(Messages.OutOfCategoryCount);
        }
    }
}
