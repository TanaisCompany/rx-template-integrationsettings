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
    /// <returns>Настройка интегрированной системы.</returns>
    [Public, Remote]
    public static tanais.IntegrationCore.IIntegrationSetting GetIntegrationSettingBySystem(tanais.IntegrationCore.IIntegratedSystem integratedSystem)
    {
      if (integratedSystem == null)
        throw new Exception(tanais.IntegrationCore.IntegrationSettings.Resources.IntegratedSystemIsNoteFilled);
      
      var integrationSetting = tanais.IntegrationCore.IntegrationSettings
        .GetAll(s => s.IntegratedSystem.Id == integratedSystem.Id)
        .FirstOrDefault();
      
      if (integrationSetting == null)
        throw new Exception(tanais.IntegrationCore.IntegrationSettings.Resources.IntegrationSettingNotFound);
      
      return integrationSetting;
    }
    
  }
}