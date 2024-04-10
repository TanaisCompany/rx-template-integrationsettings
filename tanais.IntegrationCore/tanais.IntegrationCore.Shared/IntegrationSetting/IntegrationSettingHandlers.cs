using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Tanais.IntegrationCore.IntegrationSetting;

namespace Tanais.IntegrationCore
{
  partial class IntegrationSettingSharedHandlers
  {

    public virtual void BusinessUnitChanged(Tanais.IntegrationCore.Shared.IntegrationSettingBusinessUnitChangedEventArgs e)
    {
      Tanais.IntegrationCore.Functions.IntegrationSetting.FillName(_obj);
    }

    public virtual void IntegratedSystemChanged(Tanais.IntegrationCore.Shared.IntegrationSettingIntegratedSystemChangedEventArgs e)
    {
      Tanais.IntegrationCore.Functions.IntegrationSetting.FillName(_obj);
    }

  }
}