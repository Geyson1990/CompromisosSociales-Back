using Abp.Domain.Repositories;
using Contable.Application;
using Contable.Manager.Base;
using Contable.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Manager
{
    public class FunctionManager : FunctionManagerBase
    {
        public FunctionManager(IRepository<Parameter> parameterRepository, IProcedureRepository procedureRepository) : base(parameterRepository, procedureRepository)
        {

        }
    }
}
