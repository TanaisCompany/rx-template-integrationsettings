using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using tanais.IntegrationCore.IntegrationSetting;

namespace tanais.IntegrationCore.Server
{
  partial class IntegrationSettingFunctions
  {
    
    /// <summary>
    /// Получить настройку интегрированной системы.
    /// </summary>
    /// <param name="integratedSystem">Интегрированная система.</param>
    /// <param name="postfix">Постфикс.</param>
    /// <returns>Настройка интегрированной системы.</returns>
    [Public, Remote]
    public static tanais.IntegrationCore.IIntegrationSetting GetIntegrationSettingBySystem(tanais.IntegrationCore.IIntegratedSystem integratedSystem, string postfix)
    {
      if (integratedSystem == null)
        throw new Exception(tanais.IntegrationCore.IntegrationSettings.Resources.IntegratedSystemIsNotSpecified);
      
      var integrationSetting = tanais.IntegrationCore.IntegrationSettings
        .GetAll()
        .Where(s => s.IntegratedSystem.Id == integratedSystem.Id)
        .Where(s => string.IsNullOrEmpty(postfix) || s.Postfix == postfix)
        .FirstOrDefault();
      
      if (integrationSetting == null)
        throw new Exception(tanais.IntegrationCore.IntegrationSettings.Resources.IntegrationSettingNotFoundFormat(integratedSystem.Name));
      
      return integrationSetting;
    }
    
  }
}