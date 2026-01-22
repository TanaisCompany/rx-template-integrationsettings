using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Tanais.IntegrationCore.SyncEntityType;

namespace Tanais.IntegrationCore.Server
{
  partial class SyncEntityTypeFunctions
  {
    /// <summary>
    /// Получить тип сущности.
    /// </summary>
    /// <param name="typeCode">Код типа сущности.</param>
    /// <returns>Тип сущности.</returns>
    /// <remarks>Если тип сущности не найден, будет создано исключение.</remarks>
    [Public, Remote]
    public static ISyncEntityType GetSyncEntityTypeByCode(string typeCode)
    {
      if (string.IsNullOrEmpty(typeCode))
        throw AppliedCodeException.Create(Tanais.IntegrationCore.SyncEntityTypes.Resources.EntityTypeIsNotSpecified);
      
      var entityType = Tanais.IntegrationCore.SyncEntityTypes
        .GetAll(s => s.Code.ToLower() == typeCode.ToLower())
        .FirstOrDefault();
      
      if (entityType == null)
        throw AppliedCodeException.Create(Tanais.IntegrationCore.SyncEntityTypes.Resources.EntityTypeNotFoundFormat(typeCode));
      
      return entityType;
    }
  }
}