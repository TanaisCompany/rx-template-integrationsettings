using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Sungero.Domain;
using Sungero.Domain.Shared;
using Sungero.Domain.SessionExtensions;

namespace Tanais.IntegrationCore.Server
{
  public class ModuleFunctions
  {
    #region Фильтрация справочника "Элементы очереди синхронизации"
    
    /// <summary>
    /// Отфильтровать синхронизируемые сущности по условиям фильтрации.
    /// </summary>
    /// <param name="query">Сущности для фильтрации.</param>
    /// <param name="filter">Фильтр.</param>
    /// <returns>Сущности.</returns>
    public virtual IQueryable<ISyncEntityQueueItemBase> SyncEntityQueueItemBaseApplyFilter(IQueryable<ISyncEntityQueueItemBase> query, ISyncEntityQueueItemBaseFilterState filter)
    {
      if (filter == null || query == null)
        return query;
      
      // Фильтр по типу сущности.
      if (filter.ExternalEntityType != null)
        query = query.Where(i => i.EntityType == filter.ExternalEntityType);
            
      // Фильтр по статусу обработки. Если все галочки включены, то нет смысла добавлять фильтр.
      if ((filter.Processed || filter.NotProcessed || filter.InProcessing || filter.ErrorOccurred) &&
          !(filter.Processed && filter.NotProcessed && filter.InProcessing && filter.ErrorOccurred))
        query = query.Where(l => filter.Processed && l.ProcessingStatus == Tanais.IntegrationCore.SyncEntityQueueItemBase.ProcessingStatus.Processed ||
                            filter.NotProcessed && l.ProcessingStatus == Tanais.IntegrationCore.SyncEntityQueueItemBase.ProcessingStatus.NotProcessed ||
                            filter.InProcessing && l.ProcessingStatus == Tanais.IntegrationCore.SyncEntityQueueItemBase.ProcessingStatus.InProcessing ||
                            filter.ErrorOccurred && l.ProcessingStatus == Tanais.IntegrationCore.SyncEntityQueueItemBase.ProcessingStatus.Error);
      
      // Фильтр по интервалу времени.
      if (filter.Last7Days || filter.Last30Days || filter.Last90Days || filter.ManualPeriod)
        query = this.SyncEntityQueueItemBaseApplyFilterByDate(query, filter);
      
      return query;
    }
    
    /// <summary>
    /// Отфильтровать синхронизируемые сущности по дате изменения.
    /// </summary>
    /// <param name="query">Сущности для фильтрации.</param>
    /// <param name="filter">Фильтр.</param>
    /// <returns>Отфильтрованные cущности.</returns>
    public virtual IQueryable<ISyncEntityQueueItemBase> SyncEntityQueueItemBaseApplyFilterByDate(IQueryable<ISyncEntityQueueItemBase> query, ISyncEntityQueueItemBaseFilterState filter)
    {
      if (filter == null || query == null)
        return query;
      
      var periodBegin = Calendar.UserToday.AddDays(-7);
      var periodEnd = Calendar.UserToday.EndOfDay();
      
      if (filter.Last7Days)
        periodBegin = Calendar.UserToday.AddDays(-7);
      
      if (filter.Last30Days)
        periodBegin = Calendar.UserToday.AddDays(-30);
      
      if (filter.Last90Days)
        periodBegin = Calendar.UserToday.AddDays(-90);
      
      if (filter.ManualPeriod)
      {
        periodBegin = filter.DateRangeFrom ?? Calendar.SqlMinValue;
        periodEnd = filter.DateRangeTo ?? Calendar.SqlMaxValue;
      }

      var serverPeriodBegin = Equals(Calendar.SqlMinValue, periodBegin) ? periodBegin : Sungero.Docflow.PublicFunctions.Module.Remote.GetTenantDateTimeFromUserDay(periodBegin);
      var serverPeriodEnd = Equals(Calendar.SqlMaxValue, periodEnd) ? periodEnd : periodEnd.EndOfDay().FromUserTime();
      
      // Если временные оффсеты клиента и сервера не отличаются, то отфильтровать сущности по периоду.
      if (TenantInfo.UtcOffset == Users.UtcOffsetOfCurrent)
      {
        query = query.Where(j => j.LastUpdate.Between(serverPeriodBegin, serverPeriodEnd));
      }
      else
      {
        // Если временные оффсеты клиента и сервера отличаются.
        var clientPeriodEnd = !Equals(Calendar.SqlMaxValue, periodEnd) ? periodEnd.AddDays(1) : Calendar.SqlMaxValue;
        query = query.Where(j => (j.LastUpdate.Between(serverPeriodBegin, serverPeriodEnd) ||
                                  j.LastUpdate == periodBegin) && j.LastUpdate != clientPeriodEnd);
      }
      
      return query;
    }
    
    #endregion
    
    /// <summary>
    /// Получить сущность.
    /// </summary>
    /// <param name="typeGuid">Guid типа сущности.</param>
    /// <param name="id">ИД сущности.</param>
    /// <returns>Cущность.</returns>
    [Public][Remote(IsPure = true, PackResultEntityEagerly = true)]
    public virtual Sungero.Domain.Shared.IEntity GetEntity(string typeGuid, long id)
    {
      var entityType = Sungero.Domain.Shared.TypeExtension.GetTypeByGuid(Guid.Parse(typeGuid));
      using (var session = new Sungero.Domain.Session())
      {
        return session.GetAll(entityType).FirstOrDefault(e => e.Id == id);
      }
    }
  }
}