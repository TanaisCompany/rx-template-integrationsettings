using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using tanais.IntegrationCore.IntegratedSystem;

namespace tanais.IntegrationCore.Server
{
  partial class IntegratedSystemFunctions
  {
    
    /// <summary>
    /// Получить интегрированную систему.
    /// </summary>
    /// <param name="systemCode">Код интегрированной системы.</param>
    /// <returns>Интегрированная система.</returns>
    [Public, Remote]
    public tanais.IntegrationCore.IIntegratedSystem GetIntegratedSystemByCode(string systemCode)
    {
      if (string.IsNullOrEmpty(systemCode))
        throw new Exception(tanais.IntegrationCore.IntegratedSystems.Resources.SystemCodeIsNotFilled);
      
      var integratedSystem = tanais.IntegrationCore.IntegratedSystems
        .GetAll(s => s.Code.ToLower() == systemCode.ToLower())
        .FirstOrDefault();
      
      if (integratedSystem == null)
        throw new Exception(tanais.IntegrationCore.IntegratedSystems.Resources.IntegratedSystemNotFound);
      
      return integratedSystem;
    }
    
  }
}