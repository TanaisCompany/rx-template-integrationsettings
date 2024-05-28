using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Tanais.IntegrationCore.IntegrationSetting;

namespace Tanais.IntegrationCore
{
  partial class IntegrationSettingServerHandlers
  {

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      var isDouble = Tanais.IntegrationCore.IntegrationSettings
        .GetAll(s => Equals(s.IntegratedSystem, _obj.IntegratedSystem) && Equals(s.Postfix, _obj.Postfix) && !Equals(s, _obj))
        .Any();
      
      if (isDouble)
        e.AddError(Tanais.IntegrationCore.IntegrationSettings.Resources.ValidationUniqueValueWarning);
    }
  }

}