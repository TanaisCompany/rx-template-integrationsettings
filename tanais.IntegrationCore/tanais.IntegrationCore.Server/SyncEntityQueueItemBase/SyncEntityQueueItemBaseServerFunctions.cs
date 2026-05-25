using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Tanais.IntegrationCore.SyncEntityQueueItemBase;

namespace Tanais.IntegrationCore.Server
{
  partial class SyncEntityQueueItemBaseFunctions
  {
    
    /// <summary>
    /// Создание элемента очереди синхронизации.
    /// </summary>
    /// <param name="json">Json с данными в виде строки.</param>
    /// <param name="systemCode">Код интегрированной системы.</param>
    /// <param name="messageGuid">GUID обмена.</param>
    /// <param name="entityTypeCode">Код типа сущности.</param>
    /// <returns>Элемент очереди синхронизации.</returns>
    [Public]
    public static ISyncEntityQueueItemBase CreateSyncEntityQueueItem(string json, string systemCode, string messageGuid, string entityTypeCode)
    {
      var integratedSystem = Tanais.IntegrationCore.PublicFunctions.IntegratedSystem.Remote.GetIntegratedSystemByCode(systemCode);
      var entityType = Tanais.IntegrationCore.PublicFunctions.SyncEntityType.Remote.GetSyncEntityTypeByCode(entityTypeCode);
      
      var syncEntityQueueItem = SyncEntityQueueItemBases.Create();
      syncEntityQueueItem.Json = json;
      syncEntityQueueItem.ExternalId = messageGuid;
      syncEntityQueueItem.IntegratedSystem = integratedSystem;
      syncEntityQueueItem.ProcessingStatus = IntegrationCore.SyncEntityQueueItemBase.ProcessingStatus.NotProcessed;
      syncEntityQueueItem.EntityType = entityType;
      syncEntityQueueItem.Save();
      
      return syncEntityQueueItem;
    }
    
    /// <summary>
    /// Получение элемента очереди синхронизации.
    /// </summary>
    /// <param name="id">Ид элемента очереди.</param>
    /// <returns>Элемент очереди синхронизации.</returns>
    [Public]
    public static ISyncEntityQueueItemBase GetSyncEntityQueueItem(long id)
    {
      return SyncEntityQueueItemBases.GetAll(i => i.Id == id).FirstOrDefault();
    }
    
    /// <summary>
    /// Заполнить свойства очереди.
    /// </summary>
    /// <param name="status">Статус.</param>
    /// <param name="note">Примечание.</param>
    /// <param name="retryIteration">Количество итераций.</param>
    [Public]
    public void SetQueueProperties(Sungero.Core.Enumeration status, string note, int? retryIteration)
    {
      if (_obj.ProcessingStatus != status)
        _obj.ProcessingStatus = status;
      
      if (!string.IsNullOrEmpty(note) && _obj.Note != note)
        _obj.Note = note;
      
      if (retryIteration != null && _obj.Retries != retryIteration)
        _obj.Retries = retryIteration;

      if (_obj.State.IsChanged)
        _obj.Save();
    }
  }
}