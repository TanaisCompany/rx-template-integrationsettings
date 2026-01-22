using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Tanais.IntegrationCore.IntegratedSystem;

namespace Tanais.IntegrationCore.Server
{
  partial class IntegratedSystemFunctions
  {
    
    /// <summary>
    /// Получить интегрированную систему.
    /// </summary>
    /// <param name="systemCode">Код интегрированной системы.</param>
    /// <returns>Интегрированная система.</returns>
    /// <remarks>Если система не найдена, будет создано исключение.</remarks>
    [Public, Remote]
    public static Tanais.IntegrationCore.IIntegratedSystem GetIntegratedSystemByCode(string systemCode)
    {
      if (string.IsNullOrEmpty(systemCode))
        throw AppliedCodeException.Create(Tanais.IntegrationCore.IntegratedSystems.Resources.SystemCodeIsNotSpecified);
      
      var integratedSystem = Tanais.IntegrationCore.IntegratedSystems
        .GetAll(s => s.Code.ToLower() == systemCode.ToLower())
        .FirstOrDefault();
      
      if (integratedSystem == null)
        throw AppliedCodeException.Create(Tanais.IntegrationCore.IntegratedSystems.Resources.IntegratedSystemNotFoundFormat(systemCode));
      
      return integratedSystem;
    }
    
  }
}