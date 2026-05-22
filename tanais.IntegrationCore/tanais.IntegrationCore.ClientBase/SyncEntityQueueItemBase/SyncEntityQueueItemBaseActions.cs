using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Tanais.IntegrationCore.SyncEntityQueueItemBase;

namespace Tanais.IntegrationCore.Client
{
  partial class SyncEntityQueueItemBaseActions
  {
    public virtual void OpenEntity(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      var entity = IntegrationCore.Functions.Module.Remote.GetEntity(_obj.EntityType?.EntityTypeGuid, _obj.EntityId.GetValueOrDefault());
      if (entity == null)
      {
        e.AddInformation(Tanais.IntegrationCore.SyncEntityQueueItemBases.Resources.NotOpenEntityError);
        return;
      }
      
      entity.ShowModal();      
    }

    public virtual bool CanOpenEntity(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return _obj.EntityType != null && !string.IsNullOrEmpty(_obj.EntityType?.EntityTypeGuid) && _obj.EntityId.HasValue;
    }

  }

}