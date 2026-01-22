using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Tanais.IntegrationCore.SyncEntityQueueItemBase;

namespace Tanais.IntegrationCore
{
  partial class SyncEntityQueueItemBaseFilteringServerHandler<T>
  {

    public override IQueryable<T> Filtering(IQueryable<T> query, Sungero.Domain.FilteringEventArgs e)
    {
      if (_filter == null)
        return base.Filtering(query, e);
      
      query = Functions.Module.SyncEntityQueueItemBaseApplyFilter(query, _filter).Cast<T>();
      
      return query;
    }
  }


}