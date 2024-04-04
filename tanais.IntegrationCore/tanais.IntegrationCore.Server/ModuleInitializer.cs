using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Sungero.Domain.Initialization;

namespace tanais.IntegrationCore.Server
{
  public partial class ModuleInitializer
  {

    public override bool IsModuleVisible()
    {
      return true;
    }

    public override void Initializing(Sungero.Domain.ModuleInitializingEventArgs e)
    {
      CreateRoles();
      GrantRightsOnEntities();
    }
    
    #region Создание ролей

    /// <summary>
    /// Создать роли.
    /// </summary>
    public static void CreateRoles()
    {
      // Ответственные за интеграцию.
      Sungero.Docflow.PublicInitializationFunctions.Module.CreateRole(
        tanais.IntegrationCore.Resources.RoleNameIntegrationResponsible,
        tanais.IntegrationCore.Resources.RoleNameIntegrationResponsible,
        Constants.Module.RoleGuid.IntegrationResponsibleRole);
      
      // Пользователи с доступом к настройкам интеграции.
      Sungero.Docflow.PublicInitializationFunctions.Module.CreateRole(
        tanais.IntegrationCore.Resources.RoleNameUsersWithAccessToIntegrationSettings,
        tanais.IntegrationCore.Resources.RoleNameUsersWithAccessToIntegrationSettings,
        Constants.Module.RoleGuid.ReadIntegrationSettingRole);
    }
    
    #endregion
    
    #region Выдача прав
    
    // <summary>
    /// Выдать права на сущности.
    /// </summary>
    public static void GrantRightsOnEntities()
    {
      var readIntegrationSettingRole = GetRoleByGuid(Constants.Module.RoleGuid.ReadIntegrationSettingRole);
      if (readIntegrationSettingRole != null)
      {
        InitializationLogger.Debug("Init: Grant rights on IntegrationSetting to readIntegrationSettingRole.");
        GrantRightsOnEntityType(readIntegrationSettingRole, IntegrationSettings.AccessRights, DefaultAccessRightsTypes.Read);
        
        InitializationLogger.Debug("Init: Grant rights on IntegratedSystems to readIntegrationSettingRole.");
        GrantRightsOnEntityType(readIntegrationSettingRole, IntegratedSystems.AccessRights, DefaultAccessRightsTypes.Read);
      }
      
      var integrationResponsibleRole = GetRoleByGuid(Constants.Module.RoleGuid.IntegrationResponsibleRole);
      if (integrationResponsibleRole != null)
      {
        InitializationLogger.Debug("Init: Grant rights on IntegrationSetting to integrationResponsibleRole.");
        GrantRightsOnEntityType(integrationResponsibleRole, IntegrationSettings.AccessRights, DefaultAccessRightsTypes.FullAccess);
        
        InitializationLogger.Debug("Init: Grant rights on IntegratedSystem to integrationResponsibleRole.");
        GrantRightsOnEntityType(integrationResponsibleRole, IntegratedSystems.AccessRights, DefaultAccessRightsTypes.FullAccess);
      }
    }
    
    /// <summary>
    /// Выдача прав доступа.
    /// </summary>
    /// <param name="role">Субъект прав.</param>
    /// <param name="accessRights">Права доступа репозитория сущности.</param>
    /// <param name="accessRightsTypes">Список типов прав.</param>
    [Public]
    private static void GrantRightsOnEntityType(IRole role, Sungero.Domain.Shared.IEntityAccessRights accessRights, params Guid[] accessRightsTypes)
    {
      accessRights.Grant(role, accessRightsTypes);
      accessRights.Save();
    }

    #endregion
    
    /// <summary>
    /// Получить роль по Guid.
    /// </summary>
    /// <param name="roleGuid">Guid роли.</param>
    /// <returns>Роль.</returns>
    [Public]
    public static IRole GetRoleByGuid(Guid roleGuid)
    {
      return Roles.GetAll(r => r.Sid == roleGuid).FirstOrDefault();
    }
    
  }
}
