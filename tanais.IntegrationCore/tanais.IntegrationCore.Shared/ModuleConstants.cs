using System;
using Sungero.Core;

namespace tanais.IntegrationCore.Constants
{
  public static class Module
  {
    #region Группы и роли
    
    [Public]
    public static class RoleGuid
    {
      // GUID роли "Ответственные за интеграцию".
      [Public]
      public static readonly Guid IntegrationResponsibleRole = Guid.Parse("8ddf12da-a8d1-48e2-8b3a-093f42e0793b");
      
      // GUID роли "Пользователи с доступом к настройкам интеграции".
      [Public]
      public static readonly Guid ReadIntegrationSettingRole = Guid.Parse("7ae3a651-7271-4261-854b-d56f522e532c");
    }
    
    #endregion
  }
}