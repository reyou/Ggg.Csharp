using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using intro1.Interfaces;
using intro1.Services;
using Microsoft.AspNetCore.Mvc;

namespace intro1.Controllers
{
    public class OperationsController : Controller
    {
        private readonly OperationService _operationService;
        private readonly IOperationTransient _transientOperation;
        private readonly IOperationScoped _scopedOperation;
        private readonly IOperationSingleton _singletonOperation;
        private readonly IOperationSingletonInstance _singletonInstanceOperation;

        public OperationsController(OperationService operationService,
            IOperationTransient transientOperation,
            IOperationScoped scopedOperation,
            IOperationSingleton singletonOperation,
            IOperationSingletonInstance singletonInstanceOperation)
        {
            _operationService = operationService;
            _transientOperation = transientOperation;
            _scopedOperation = scopedOperation;
            _singletonOperation = singletonOperation;
            _singletonInstanceOperation = singletonInstanceOperation;
        }

        public IActionResult Index()
        {
            // viewbag contains controller-requested services
            ViewBag.TransientOperation = _transientOperation;
            ViewBag.ScopedOperation = _scopedOperation;
            ViewBag.SingletonOperation = _singletonOperation;
            ViewBag.SingletonInstanceOperation = _singletonInstanceOperation;

            // operation service has its own requested services
            ViewBag.OperationService = _operationService;
            return View();
        }
    }
}