using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Tanais.IntegrationCore.DocumentSyncSetting;

namespace Tanais.IntegrationCore.Server
{
  partial class DocumentSyncSettingFunctions
  {
    
    /// <summary>
    /// Получить настройку синхронизации для типа документа.
    /// </summary>
    /// <param name="integratedSystem">Интегрированная система.</param>
    /// <param name="classTypeGuid">Guid типа документа.</param>
    /// <returns>Настройка синхронизации.</returns>
    /// <remarks>Если настройка не найдена, будет создано исключение.</remarks>
    [Public, Remote]
    public static Tanais.IntegrationCore.IDocumentSyncSetting GetDocumentSyncSetting(Tanais.IntegrationCore.IIntegratedSystem integratedSystem, Guid classTypeGuid)
    {
      if (integratedSystem == null)
        throw new Exception(Tanais.IntegrationCore.DocumentSyncSettings.Resources.IntegratedSystemIsNotSpecified);
      
      var setting = Tanais.IntegrationCore.DocumentSyncSettings
        .GetAll(s =>
                Equals(s.IntegratedSystem, integratedSystem) &&
                (s.DocumentTypeGuid == classTypeGuid.ToString() || s.DocumentType != null && s.DocumentType.DocumentTypeGuid == classTypeGuid.ToString()))
        .FirstOrDefault();
      
      if (setting == null)
        throw new Exception(Tanais.IntegrationCore.DocumentSyncSettings.Resources.DocumentSyncSettingNotFoundFormat(classTypeGuid.ToString()));
      
      return setting;
    }
    
    /// <summary>
    /// Получить настройку синхронизации для внешнего типа документа.
    /// </summary>
    /// <param name="integratedSystem">Интегрированная система.</param>
    /// <param name="extClassTypeGuid">Guid внешнего типа документа.</param>
    /// <returns>Настройка синхронизации.</returns>
    /// <remarks>Если настройка не найдена, будет создано исключение.</remarks>
    [Public, Remote]
    public static Tanais.IntegrationCore.IDocumentSyncSetting GetDocumentSyncSetting(Tanais.IntegrationCore.IIntegratedSystem integratedSystem, string extClassTypeGuid)
    {
      if (integratedSystem == null)
        throw new Exception(Tanais.IntegrationCore.DocumentSyncSettings.Resources.IntegratedSystemIsNotSpecified);
      
      var setting = Tanais.IntegrationCore.DocumentSyncSettings
        .GetAll(s =>
                Equals(s.IntegratedSystem, integratedSystem) &&
                s.DocumentType != null &&
                s.ExtDocumentTypeGuid == extClassTypeGuid)
        .FirstOrDefault();
      
      if (setting == null)
        throw new Exception(Tanais.IntegrationCore.DocumentSyncSettings.Resources.DocumentSyncSettingForExtTypeNotFoundFormat(extClassTypeGuid));
      
      return setting;
    }

  }
}