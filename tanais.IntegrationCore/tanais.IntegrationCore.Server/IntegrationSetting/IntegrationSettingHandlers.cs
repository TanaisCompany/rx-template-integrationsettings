using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using tanais.IntegrationCore.IntegrationSetting;

namespace tanais.IntegrationCore
{
  partial class IntegrationSettingServerHandlers
  {

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      var isDouble = tanais.IntegrationCore.IntegrationSettings
        .GetAll(s => Equals(s.IntegratedSystem, _obj.IntegratedSystem) && Equals(s.Postfix, _obj.Postfix) && !Equals(s, _obj))
        .Any();
      
      if (isDouble)
        e.AddError(tanais.IntegrationCore.IntegrationSettings.Resources.ValidationUniqueValueWarning);
    }
  }

}