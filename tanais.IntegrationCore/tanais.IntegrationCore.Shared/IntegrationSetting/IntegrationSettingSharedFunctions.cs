using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using tanais.IntegrationCore.IntegrationSetting;

namespace tanais.IntegrationCore.Shared
{
  partial class IntegrationSettingFunctions
  {

    /// <summary>
    /// Установить видимость, обязательность свойств.
    /// </summary>
    public virtual void SetStateProperties()
    {
      // Если Способ авторизации = По ключу.
      bool IsKey = _obj.AuthMethod == tanais.IntegrationCore.IntegrationSetting.AuthMethod.Key;
      // Если Способ авторизации = По паролю.
      bool IsPassword = _obj.AuthMethod == tanais.IntegrationCore.IntegrationSetting.AuthMethod.Password;

      // «Ключ доступа».
      _obj.State.Properties.VisibleAccessKey.IsVisible = IsKey;
      _obj.State.Properties.VisibleAccessKey.IsRequired = IsKey;
      
      // «Имя пользователя», «Пароль».
      _obj.State.Properties.Login.IsVisible = IsPassword;
      _obj.State.Properties.Login.IsRequired = IsPassword;
      _obj.State.Properties.VisiblePassword.IsVisible = IsPassword;
      _obj.State.Properties.VisiblePassword.IsRequired = IsPassword;
    }
    
    /// <summary>
    /// Заполнить имя записи справочника.
    /// </summary>
    public virtual void SetIntegrationSettingsName()
    {
      string name = string.Empty;
      
      if (_obj.BusinessUnit != null && _obj.IntegratedSystem != null)
        name = tanais.IntegrationCore.IntegrationSettings.Resources.RecordNameTemplateWithBusinessUnitFormat(_obj.IntegratedSystem.Name, _obj.BusinessUnit.Name);
      else if (_obj.IntegratedSystem != null)
        name = tanais.IntegrationCore.IntegrationSettings.Resources.RecordNameTemplateFormat(_obj.IntegratedSystem.Name);
      
      _obj.Name = Sungero.Docflow.PublicFunctions.Module.TrimSpecialSymbols(name);
    }
  }
}