using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Tanais.IntegrationCore.IntegrationSetting;

namespace Tanais.IntegrationCore.Server
{
  partial class IntegrationSettingFunctions
  {
    
    /// <summary>
    /// Получить настройку интегрированной системы.
    /// </summary>
    /// <param name="integratedSystem">Интегрированная система.</param>
    /// <param name="postfix">Постфикс.</param>
    /// <returns>Настройка интегрированной системы.</returns>
    /// <remarks>Если настройка не найдена, будет создано исключение.</remarks>
    [Public, Remote]
    public static Tanais.IntegrationCore.IIntegrationSetting GetIntegrationSettingBySystem(Tanais.IntegrationCore.IIntegratedSystem integratedSystem)
    {
      if (integratedSystem == null)
        throw new Exception(Tanais.IntegrationCore.IntegrationSettings.Resources.IntegratedSystemIsNotSpecified);
      
      var integrationSetting = Tanais.IntegrationCore.IntegrationSettings
        .GetAll()
        .Where(s => s.IntegratedSystem.Id == integratedSystem.Id)
        .FirstOrDefault();
      
      if (integrationSetting == null)
        throw new Exception(Tanais.IntegrationCore.IntegrationSettings.Resources.IntegrationSettingNotFoundFormat(integratedSystem.Name));
      
      return integrationSetting;
    }
    
    /// <summary>
    /// Получить настройку интегрированной системы.
    /// </summary>
    /// <param name="integratedSystem">Интегрированная система.</param>
    /// <param name="postfix">Постфикс.</param>
    /// <returns>Настройка интегрированной системы.</returns>
    /// <remarks>Если настройка не найдена, будет создано исключение.</remarks>
    [Public, Remote]
    public static Tanais.IntegrationCore.IIntegrationSetting GetIntegrationSettingBySystem(Tanais.IntegrationCore.IIntegratedSystem integratedSystem, string postfix)
    {
      if (integratedSystem == null)
        throw new Exception(Tanais.IntegrationCore.IntegrationSettings.Resources.IntegratedSystemIsNotSpecified);
      
      var integrationSetting = Tanais.IntegrationCore.IntegrationSettings
        .GetAll()
        .Where(s => s.IntegratedSystem.Id == integratedSystem.Id)
        .Where(s => string.IsNullOrEmpty(postfix) || s.Postfix == postfix)
        .FirstOrDefault();
      
      if (integrationSetting == null)
        throw new Exception(Tanais.IntegrationCore.IntegrationSettings.Resources.IntegrationSettingNotFoundFormat(integratedSystem.Name));
      
      return integrationSetting;
    }
    
  }
}